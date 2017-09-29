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
    /// 客户端消息发送助手
    /// </summary>
    public class NewSender
    {
        static byte[] buffer = new byte[1024];

        private int serverPort;
        private string serverIp;
        private readonly int heartbeatInterval;
        private Socket clientSocket;
        private ComputerEnum computer;
        private string officeId;
        private string operatorId;
        private volatile SynchronizationContext synchronizationContext;

        public EventHandler<MessageEventArgs> ReMessage;

        protected virtual void OnReceiveMessage(object sender, MessageEventArgs e)
        {
            if (ReMessage != null)
            {
                ReMessage(sender, e);
            }
        }
        public NewSender()
        {
        }
        public NewSender(int size, string serverIp, int serverPort, int heartbeatInterval, ComputerEnum computer, string officeId, string operatorId)
        {

            this.serverIp = serverIp;
            this.serverPort = serverPort;
            this.heartbeatInterval = heartbeatInterval;
            this.computer = computer;
            this.officeId = officeId;
            this.operatorId = operatorId;
            synchronizationContext = SynchronizationContext.Current;
        }

        public void Connect()
        {
            //创建一个Socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            clientSocket = socket;
            //连接到指定服务器的指定端口

            socket.Connect(serverIp, serverPort);


            Send(new NewMessage { MessageType = MessageTypeEnum.Login, Computer = computer, OfficeId = this.officeId, OperatorId = this.operatorId });

            //实现接受消息的方法
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);

            //接受用户输入，将消息发送给服务器端
            //while (true)
            //{
            //    var message = "连接成功" + Console.ReadLine();
            //    var outputBuffer = Encoding.Unicode.GetBytes(message);
            //    socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
            //}
        }

        public void Send(NewMessage message)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                IPEndPoint clientipe = (IPEndPoint)clientSocket.LocalEndPoint;
                message.SenderIp = clientipe.Address.ToString();
                message.SenderPort = clientipe.Port;
                //clientSocket.Send(Encoding.UTF8.GetBytes(message.ToJson().EncryptStringToBytes_Des("888", "888")));

                clientSocket.Send(Encoding.UTF8.GetBytes(message.ToJson()));
            }
        }
        public void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;

                var length = socket.EndReceive(ar);
                //读取出来消息内容
                var message = Encoding.UTF8.GetString(buffer, 0, length);
                //显示消息
                //Console.WriteLine(message);
                //NewMessage mes = message.DecryptStringFromBytes_Des("888", "888").ToT<NewMessage>();
                NewMessage mes = message.ToT<NewMessage>();
                if (mes != null)
                {
                    synchronizationContext.Post(SynReceiveMessage, mes);
                }


                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);
            }
        }

        private void SynReceiveMessage(object message)
        {
            NewSender me = new NewSender();
            OnReceiveMessage(me, new MessageEventArgs(message as NewMessage));
        }

    }
}
