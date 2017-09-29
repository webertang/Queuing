using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RDTools.NewSocketManager
{
    /// <summary>
    /// 服务端监听Socket消息，此处只接收消息不做消息处理，消息处理由委托回传到别处处理
    /// </summary>
    public class NewListener
    {
        private volatile byte[] result;
        private int size = 1024;
        private int port;
        private Socket serverSocket;

        private Thread _thread;
        private Thread _threadWork;
        
        private volatile List<Client> clients;
        private volatile Queue<NewMessage> messages;
        private bool run = false;
        private volatile SynchronizationContext synchronizationContext;
        private readonly int clientCount;
        //private readonly int heartbeatInterval;

        public EventHandler<MessageEventArgs> ReceiveMessage;
        public EventHandler<ClientEventArgs> Login;
        public EventHandler<ClientEventArgs> OutLine;

        protected virtual void OnReceiveMessage(object sender, MessageEventArgs e)
        {
            if (ReceiveMessage != null)
            {
                ReceiveMessage(sender, e);
            }
        }

        protected virtual void OnLogin(object sender, ClientEventArgs e)
        {
            if (Login != null)
            {
                Login(sender, e);
            }
        }

        protected virtual void OnOutLine(object sender, ClientEventArgs e)
        {
            if (OutLine != null)
            {
                OutLine(sender, e);
            }
        }

        public NewListener(int size, int port, int clientCount)
        {
            this.size = size;
            this.port = port;
            this.clientCount = clientCount;
        }

        public void Init()
        {
            clients = new List<Client>();
            messages = new Queue<NewMessage>();
            ThreadPool.SetMaxThreads(10, 10);
            synchronizationContext = SynchronizationContext.Current;
        }

        private void Enqueue(string message)
        {
            lock (this.messages)
            {
                this.messages.Enqueue(message.DecryptStringFromBytes_Des("888", "888").ToT<NewMessage>());
                //this.messages.Enqueue(message.ToT<NewMessage>()); //add by:tzw@2016.7.9
            }
        }

        private NewMessage Dequeue()
        {
            lock (this.messages)
            {
                if(messages.Count <= 0)
                {
                    return null;
                }

                return this.messages.Dequeue();
            }
        }

        public IEnumerable<Client> FindClient(ComputerEnum computer)
        {
            if (clients.Count <= 0)
            {
                return null;
            }

            return clients.Where(a => a.ComputerType == computer);
        }

        public Client FindClient(string clientip,int port)
        {
            if (clients.Count <= 0)
            {
                return null;
            }

            IEnumerable<Client> ie = clients.Where(a => a.IP == clientip && a.Port == port);
            if (ie.Count() > 0)
            {
                return ie.ElementAt(0);
            
            }
            return null;
        }
        private void AddClient(Client client)
        {
            lock (this.clients)
            {
                if (FindClient(client.IP, client.Port) == null)
                {
                    clients.Add(client);
                }
            }
        }

        private Client UpdateClient(string ip, int port, ComputerEnum computerType, string officeId, string operatorId)
        {
            lock (this.clients)
            {
                if (clients.Count > 0)
                {
                    int index = clients.FindIndex(a => a.IP == ip && a.Port == port);

                    if (index >= 0)
                    {
                        clients[index].ComputerType = computerType;
                        clients[index].OfficeId = officeId;
                        clients[index].OperatorId = operatorId;

                        return clients[index];
                    }
                }

                return null;
            }
        }

        private Client DeleteClient(string ip, int port)
        {
            lock (this.clients)
            {
                if (clients.Count > 0)
                {
                    int index = clients.FindIndex(a => a.IP == ip && a.Port == port);

                    if (index >= 0)
                    {
                        Client cli = clients[index];
                        clients.RemoveAt(index);

                        return cli;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="port">监听端口</param>
        public void Start()
        {
            Init();  //add by:tzw@2016.7.9
            run = true;
            IPAddress ip = IPAddress.Parse(Pub.GetHostIPAddress().ToString());
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, port));  //绑定IP地址：端口  
            serverSocket.Listen(clientCount);    //设定最多10个排队连接请求
            //通过Clientsoket发送数据  
            _thread = new Thread(ListenClientConnect);
            _thread.IsBackground = true;
            _thread.Start();
            //通过委托回传客户端发的消息 comment by:tzw@2016.7.9
            _threadWork = new Thread(Work);
            _threadWork.IsBackground = true;
            _threadWork.Start();
        }

        private void Work()
        {
            while(run)
            {
                Thread.Sleep(300);
                NewMessage message = null;

                lock (messages)
                {
                    if (messages.Count > 0)
                    {
                        message = this.Dequeue();
                    }
                }

                if(message != null)
                {
                    if(message.MessageType == MessageTypeEnum.Login)
                    {
                        Client client = UpdateClient(message.SenderIp, message.SenderPort, message.Computer, message.OfficeId, message.OperatorId);

                        synchronizationContext.Post(SynLogin, client);
                    }
                    else if (message.MessageType != MessageTypeEnum.Heartbeat)
                    {
                        synchronizationContext.Post(SynReceiveMessage, message);
                    }
                }
            }
        }

        private void SynLogin(object client)
        {
            OnLogin(this, new ClientEventArgs(client == null ? null : client as Client));
        }

        private void SynReceiveMessage(object message)
        {
            OnReceiveMessage(this, new MessageEventArgs(message as NewMessage));
        }

        /// <summary>
        /// 监听客户端连接
        /// </summary>
        private void ListenClientConnect()
        {
            while (run)
            {
                Socket clientSocket = serverSocket.Accept();
                IPEndPoint clientipe = (IPEndPoint)clientSocket.RemoteEndPoint;

                AddClient(new Client { IP = clientipe.Address.ToString(), Port = clientipe.Port, ClientSocket = clientSocket });

                ThreadPool.QueueUserWorkItem(new WaitCallback(Receive), clientSocket);
                //Thread receiveThread = new Thread(ReceiveMessage);
                //receiveThread.IsBackground = true;
                //receiveThread.Start(clientSocket);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private void Receive(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            IPEndPoint clientipe = (IPEndPoint)myClientSocket.RemoteEndPoint;
            string cip = clientipe.Address.ToString();
            int cport = clientipe.Port;

            while (run)
            {
                try
                {
                    result = new byte[size];
                    //通过clientSocket接收数据  
                    int receiveNumber = myClientSocket.Receive(result);
                    string ret = Encoding.UTF8.GetString(result, 0, receiveNumber);

                    if (string.IsNullOrWhiteSpace(ret))
                    {
                        break;
                    }

                    Enqueue(ret);

                    Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("远程主机强迫关闭了一个现有的连接。"))
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            myClientSocket.Shutdown(SocketShutdown.Both);
            myClientSocket.Close();
            Client client = DeleteClient(cip, cport);

            synchronizationContext.Post(SynOutLine, client);
        }

        private void SynOutLine(object client)
        {
            OnOutLine(this, new ClientEventArgs(client == null ? null : client as Client));
        }

        /// <summary>
        /// 启动监听
        /// </summary>
        public void Stop()
        {
            run = false;

            if(_thread != null)
            {
                if(_thread.ThreadState == ThreadState.Running)
                {
                    _thread.Abort();
                }

                _thread = null;
            }

            if(_threadWork != null)
            {
                if (_threadWork.ThreadState == ThreadState.Running)
                {
                    _threadWork.Abort();
                }

                _threadWork = null;
            }

            if(serverSocket != null)
            {
                serverSocket.Shutdown(SocketShutdown.Both);
                serverSocket.Close();
            }
        }

        public void Send(Socket clientSocket, NewMessage message)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(message.ToJson().EncryptStringToBytes_Des("888", "888")));
            }
        }
    }
}
