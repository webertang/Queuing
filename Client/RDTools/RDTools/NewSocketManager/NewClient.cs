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
    /// 客户端Socket
    /// </summary>
    public class NewClient
    {
        private volatile byte[] result;
        private int size = 1024;
        private int serverPort;
        private string serverIp;
        private Socket clientSocket;

        private Thread _thread;
        private Thread _threadWork;
        private Thread _threadHeartbeat;

        private volatile Queue<NewMessage> messages;
        private bool run = false;
        private volatile SynchronizationContext synchronizationContext;
        private readonly int heartbeatInterval;

        private ComputerEnum computer;
        private string officeId;
        private string operatorId;


        public EventHandler<MessageEventArgs> ReceiveMessage;

        protected virtual void OnReceiveMessage(object sender, MessageEventArgs e)
        {
            if (ReceiveMessage != null)
            {
                ReceiveMessage(sender, e);
            }
        }

        public NewClient(int size, string serverIp, int serverPort, int heartbeatInterval, ComputerEnum computer, string officeId, string operatorId)
        {
            this.size = size;
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            this.heartbeatInterval = heartbeatInterval;
            this.computer = computer;
            this.officeId = officeId;
            this.operatorId = operatorId;
        }

        public void Init()
        {
            messages = new Queue<NewMessage>();
            ThreadPool.SetMaxThreads(10, 10);
            synchronizationContext = SynchronizationContext.Current;
        }

        private void Enqueue(string message)
        {
            lock (this.messages)
            {
                this.messages.Enqueue(message.DecryptStringFromBytes_Des("888", "888").ToT<NewMessage>());
            }
        }

        private NewMessage Dequeue()
        {
            lock (this.messages)
            {
                if (messages.Count <= 0)
                {
                    return null;
                }

                return this.messages.Dequeue();
            }
        }

        public void Connect()
        {
            //通过Clientsoket发送数据  
            _thread = new Thread(ServerConnect);
            _thread.IsBackground = true;
            _thread.Start();

            _threadWork = new Thread(Work);
            _threadWork.IsBackground = true;
            _threadWork.Start();

            _threadHeartbeat = new Thread(Heartbeat);
            _threadHeartbeat.IsBackground = true;
            _threadHeartbeat.Start();
        }

        private void ServerConnect()
        {
            run = true;

            IPAddress ip = IPAddress.Parse(serverIp);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(new IPEndPoint(ip, serverPort)); //配置服务器IP与端口
            }
            catch
            {
                run = false;
                return;
            }

            Send(new NewMessage { MessageType = MessageTypeEnum.Login, Computer = computer, OfficeId = this.officeId, OperatorId = this.operatorId });

            while(run)
            {
                result = new byte[size];
                int receiveLength = clientSocket.Receive(result);
                Enqueue(Encoding.UTF8.GetString(result, 0, receiveLength));

                Thread.Sleep(200);
            }
        }

        private void Work()
        {
            while (run)
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

                if (message != null)
                {
                    synchronizationContext.Post(SynReceiveMessage, message);
                }
            }
        }

        private void SynReceiveMessage(object message)
        {
            OnReceiveMessage(this, new MessageEventArgs(message as NewMessage));
        }

        private void Heartbeat()
        {
            while(run)
            {
                Thread.Sleep(heartbeatInterval);

                Send(new NewMessage { MessageType = MessageTypeEnum.Heartbeat });
            }
        }

        public void Send(NewMessage message)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(message.ToJson().EncryptStringToBytes_Des("888", "888")));
            }
        }

        public void Stop()
        {
            run = false;

            if (_thread != null)
            {
                if (_thread.ThreadState == ThreadState.Running)
                {
                    _thread.Abort();
                }

                _thread = null;
            }

            if (_threadWork != null)
            {
                if (_threadWork.ThreadState == ThreadState.Running)
                {
                    _threadWork.Abort();
                }

                _threadWork = null;
            }

            if (_threadHeartbeat != null)
            {
                if (_threadHeartbeat.ThreadState == ThreadState.Running)
                {
                    _threadHeartbeat.Abort();
                }

                _threadHeartbeat = null;
            }

            if(clientSocket != null)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        public void ReStart()
        {
            Stop();

            Connect();
        }
    }
}
