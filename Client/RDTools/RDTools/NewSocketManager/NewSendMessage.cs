using RDTools.SocketManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RDTools.NewSocketManager
{
    /// <summary>
    /// 
    /// </summary>
    public class NewSendMessage
    {
        private static volatile List<Client> clients;
        static byte[] buffer = new byte[1024];
        private int port;
        private int clientCount;

        private static volatile SynchronizationContext synchronizationContext;

        public EventHandler<MessageEventArgs> ReMessage;
        public static EventHandler<ClientEventArgs> Login;
        public EventHandler<SocketEventArgs> Client;
        private static volatile Queue<NewMessage> messages;//队列消息 comment by:tzw@2016.8.26

        private bool run = false;
        public NewSendMessage(int port, int clientCount)
        {
            clients = new List<Client>();
            synchronizationContext = SynchronizationContext.Current;
            messages = new Queue<NewMessage>();
            this.port = port;
            this.clientCount = clientCount;
        }

        public void ConnClient()
        {
            IPAddress ip = IPAddress.Parse(Pub.GetHostIPAddress().ToString());
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //将该socket绑定到主机上面的某个端口

            socket.Bind(new IPEndPoint(ip, port));

            //启动监听，并且设置一个最大的队列长度
            socket.Listen(clientCount);

            socket.BeginAccept(new AsyncCallback(ClientAccepted), socket);

            //run = true;
            //_threadWork = new Thread(Work);
            //_threadWork.IsBackground = true;
            //_threadWork.Start();

        }
        private static void Enqueue(string message)
        {
            lock (messages)
            {
                //messages.Enqueue(message.DecryptStringFromBytes_Des("888", "888").ToT<NewMessage>());
                messages.Enqueue(message.ToT<NewMessage>());
            }
        }

        private NewMessage Dequeue()
        {
            lock (messages)
            {
                if (messages.Count <= 0)
                {
                    return null;
                }

                return messages.Dequeue();
            }
        }


        public void ClientAccepted(IAsyncResult ar)
        {

            var socket = ar.AsyncState as Socket;

            //这就是客户端的Socket实例，我们后续可以将其保存起来
            var client = socket.EndAccept(ar);

            //Socket clientSocket = serverSocket.Accept();

            IPEndPoint clientipe = (IPEndPoint)client.RemoteEndPoint;

            AddClient(new Client { IP = clientipe.Address.ToString(), Port = clientipe.Port, ClientSocket = client });
            //实现每隔两秒钟给服务器发一个消息
            //这里我们使用了一个定时器


            //接收客户端的消息(这个和在客户端实现的方式是一样的）
            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), client);

            //准备接受下一个客户端请求
            socket.BeginAccept(new AsyncCallback(ClientAccepted), socket);
        }

        public static void Send(Socket clientSocket, NewMessage message)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                //clientSocket.Send(Encoding.UTF8.GetBytes(message.ToJson().EncryptStringToBytes_Des("888", "888")));
                message.SendTime = DateTime.Now.Date;

                clientSocket.Send(Encoding.UTF8.GetBytes(message.ToJson()));
            }
        }

        //通过接收到的客户端IP和端口内容进行转发
        public static void Send(NewMessage message)
        {
            Sender sendMsg = new Sender();
            sendMsg.Send(message);
        }

        public static void ReceiveMessage(IAsyncResult ar)
        {

            string cip = "";
            int cport = 0;
            try
            {
                var socket = ar.AsyncState as Socket;

                IPEndPoint clientipe = (IPEndPoint)socket.RemoteEndPoint;
                cip = clientipe.Address.ToString();
                cport = clientipe.Port;

                var length = socket.EndReceive(ar);
                //读取出来消息内容
                var message = Encoding.UTF8.GetString(buffer, 0, length);

                //Enqueue(mes);
                //显示消息
                //NewMessage mes = message.DecryptStringFromBytes_Des("888", "888").ToT<NewMessage>();
                NewMessage mes = null;
                try
                {
                    mes = message.ToT<NewMessage>();
                }
                catch (Exception)
                {
                }

                if (mes != null)
                {
                    Client lient;
                    if (mes.MessageType == MessageTypeEnum.Login)
                    {
                        mes.SenderIp = mes.SenderIp.Replace("/", "");
                        Client client = UpdateClient(mes.SenderIp, mes.SenderPort, mes.Computer, mes.OfficeId, mes.OperatorId);

                        synchronizationContext.Post(SynLogin, client);
                    }
                    else if (mes.MessageType != MessageTypeEnum.Heartbeat)
                    {
                        if (!string.IsNullOrEmpty(mes.ReceiverIp))
                        {
                            lient = FindClient(mes.ReceiverIp, mes.ReceiverPort);
                            if (lient != null)
                            {
                                Send(lient.ClientSocket, mes);
                            }
                            else
                            {
                                //接收APP回传数据
                                if (mes.DataType != null)
                                {
                                    //if (mes.DataType[0] == "Notice") ;//App回传消息处理，数据处理完成后再转发到App
                                }
                                //根据客户端提供的IP和端口就行消息转发 add by:tzw@2016.7.8
                                if (mes.MessageType == MessageTypeEnum.Next)
                                {
                                    //新接口消息

                                }
                                else
                                {
                                    Send(mes);
                                }
                            }
                        }
                        else if (!string.IsNullOrEmpty(mes.OfficeId))
                        {
                            lient = FindClient(mes.OfficeId, mes.Computer);
                            if (lient != null)
                            {
                                Send(lient.ClientSocket, mes);
                            }
                        }
                        else
                        {
                            IEnumerable<RDTools.NewSocketManager.Client> fClient = FindClient(mes.Computer);

                            foreach (RDTools.NewSocketManager.Client sc in fClient)
                            {
                                if (!string.IsNullOrEmpty(mes.OfficeId))
                                {
                                    if (mes.OfficeId == sc.OfficeId)
                                    {
                                        Send(sc.ClientSocket, mes);
                                    }
                                }
                                else
                                {
                                    Send(sc.ClientSocket, mes);
                                }
                            }
                        }
                        //synchronizationContext.Post(SynLogin, mes);

                    }

                }

                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                //这个如果客户端断开，就会进入异常处理T掉客户端
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception ex)
            {
                DeleteClient(cip + ex.Message, cport);
            }


        }

        private void Work()
        {
            while (run)
            {
                //Thread.Sleep(2000);
                NewMessage mes = null;

                lock (messages)
                {
                    if (messages.Count > 0)
                    {
                        mes = this.Dequeue();
                    }
                }
                if (mes != null)
                {
                    Client lient;
                    if (mes.MessageType == MessageTypeEnum.Login)
                    {
                        mes.SenderIp = mes.SenderIp.Replace("/", "");
                        Client client = UpdateClient(mes.SenderIp, mes.SenderPort, mes.Computer, mes.OfficeId, mes.OperatorId);

                        synchronizationContext.Post(SynLogin, client);
                    }
                    else if (mes.MessageType != MessageTypeEnum.Heartbeat)
                    {
                        if (!string.IsNullOrEmpty(mes.ReceiverIp))
                        {
                            lient = FindClient(mes.ReceiverIp, mes.ReceiverPort);
                            if (lient != null)
                            {
                                Send(lient.ClientSocket, mes);
                            }
                        }
                        else if (!string.IsNullOrEmpty(mes.OfficeId))
                        {
                            lient = FindClient(mes.OfficeId, mes.Computer);
                            if (lient != null)
                            {
                                Send(lient.ClientSocket, mes);
                            }
                        }
                        else
                        {
                            IEnumerable<RDTools.NewSocketManager.Client> fClient = FindClient(mes.Computer);

                            foreach (RDTools.NewSocketManager.Client sc in fClient)
                            {
                                if (!string.IsNullOrEmpty(mes.OfficeId))
                                {
                                    if (mes.OfficeId == sc.OfficeId)
                                    {
                                        Send(sc.ClientSocket, mes);
                                    }
                                }
                                else
                                {
                                    Send(sc.ClientSocket, mes);
                                }
                            }
                        }
                        //synchronizationContext.Post(SynReceiveMessage, mes);
                    }
                }
            }
        }

        private static Client DeleteClient(string ip, int port)
        {
            lock (clients)
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
        private static void AddClient(Client client)
        {
            lock (clients)
            {
                if (FindClient(client.IP, client.Port) == null)
                {
                    clients.Add(client);
                }
            }
        }
        public static Client FindClient(string clientip, int port)
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

        public static Client FindClient(string officeid, ComputerEnum computer)
        {
            if (clients.Count <= 0)
            {
                return null;
            }

            IEnumerable<Client> ie = clients.Where(a => a.OfficeId == officeid && a.ComputerType == computer);
            if (ie.Count() > 0)
            {
                return ie.ElementAt(0);

            }
            return null;
        }
        public static IEnumerable<Client> FindClient(ComputerEnum computer)
        {
            if (clients.Count <= 0)
            {
                return null;
            }

            return clients.Where(a => a.ComputerType == computer);
        }

        private static Client UpdateClient(string ip, int port, ComputerEnum computerType, string officeId, string operatorId)
        {
            lock (clients)
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

        private static void SynLogin(object client)
        {
            //OnLogin(NewSendMessage, new ClientEventArgs(client == null ? null : client as Client));
        }

        private void SynReceiveMessage(object message)
        {
            OnReceiveMessage(this, new MessageEventArgs(message as NewMessage));
        }

        protected virtual void OnReceiveMessage(object sender, MessageEventArgs e)
        {
            if (ReMessage != null)
            {
                ReMessage(sender, e);
            }
        }

        protected virtual void OnLogin(object sender, ClientEventArgs e)
        {
            if (Login != null)
            {
                Login(sender, e);
            }
        }


    }
}
