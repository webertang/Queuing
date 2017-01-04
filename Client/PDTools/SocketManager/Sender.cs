using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using RDTools.Common;
using System.Threading;
using RDTools.NewSocketManager;
using System.Collections.Generic;

namespace PDTools.SocketManager
{
    /// <summary>
    /// 消息发送类
    /// </summary>
    public class Sender
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="ip">所要发送到的ip地址</param>
        /// <param name="port">发送消息的端口</param>
        /// <param name="message">消息体</param>
        public void Send(string ip, int port, string message)
        {
            message = EncryptAndDecrypt.Encrypt(message, "");
            Send(ip, port, message, null);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="ip">所要发送到的ip地址</param>
        /// <param name="port">发送消息的端口</param>
        /// <param name="message">消息体</param>
        /// <param name="key">8位密钥</param>
        public void Send(string ip, int port, string message, string key)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);//设置接收端IP及端口

                if (ipAddress != null)
                {
                    byte[] bytes;
                    IPEndPoint iep = new IPEndPoint(ipAddress, port);

                    //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //socket.Connect(iep);
                    //使用带连接超时设置的连接
                    Socket socket = Connect(iep, 10000);

                    if (string.IsNullOrEmpty(key))//用密钥加密消息并转换成字符数组
                    {
                        bytes = System.Text.Encoding.GetEncoding("GBK").GetBytes(message);
                        //Encoding.UTF8.GetBytes(message);
                    }
                    else
                    {
                        //bytes = System.Text.Encoding.GetEncoding("GBK").GetBytes(message);
                        bytes =
                            Encoding.UTF8.GetBytes(EncryptAndDecrypt.Encrypt(message, key));
                    }

                    socket.Send(bytes);//发送
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();//释放资源
                }
            }
            catch
            {
                throw new Exception("对方拒绝连接！");//连接错误
            }
        }
        /// <summary>
        /// 发送消息到Android端进行展示
        /// </summary>
        /// <param name="iep">IpEndPoint</param>
        /// <param name="OfficeId">诊室ID</param>
        /// <param name="OperatorId">操作员ID</param>
        /// <param name="CompositeScreentId">综合屏ID</param>
        /// <param name="MessageType">消息类型0刷新主页面无提示、1刷新页面同时弹出提示2弹出提示不刷新主页</param>
        public void SendAndroidMessage(string iep, string OfficeId, string OperatorId, String CompositeScreentId, int MessageType)
        {
            NewMessage newMessage = new NewMessage();
            newMessage.MessageType = MessageTypeEnum.Notice; //消息类型固定变
            String[] ipcontent;
            try { ipcontent = iep.Split(':'); }
            catch { throw new Exception("解析IP地址失败！"); }
            if (!(ipcontent.Length > 3))
                throw new Exception("地址设置不完善!");
            newMessage.ReceiverIp = ipcontent[0]; //服务端端IP
            newMessage.ReceiverPort = Convert.ToInt32(ipcontent[1]); //服务端端口
            newMessage.SenderIp = ipcontent[2]; //Android端IP
            newMessage.SenderPort = Convert.ToInt32(ipcontent[3]); //Android端口

            newMessage.OfficeId = OfficeId;     //诊室ID
            newMessage.OperatorId = OperatorId; //操作员ID或病人ID
            switch (MessageType)
            {
                case 1:
                    newMessage.DataType = new List<string> { "NewMessage" };
                    break;
                case 2:
                    newMessage.DataType = new List<string> { "NewMessage" };
                    newMessage.Content = new List<string> { CompositeScreentId };
                    break;
                case 3:
                    newMessage.DataType = new List<string> { "Notice" };
                    newMessage.Content = new List<string> { CompositeScreentId };
                    break;
            }
            Send(newMessage);
        }

        /// <summary>
        /// 设置安卓屏显示内容
        /// </summary>
        /// <param name="iep">安卓屏和回传服务器地址</param>
        /// <param name="url">显示URL至少3个用"|"分割</param>
        public void SendSetAndroidInfo(string iep, string url)
        {
            NewMessage newMessage = new NewMessage();
            newMessage.MessageType = MessageTypeEnum.Modify; //消息类型固定变
            String[] ipcontent;
            try { ipcontent = iep.Split(':'); }
            catch { throw new Exception("解析IP地址失败！"); }
            if (!(ipcontent.Length > 3))
                throw new Exception("地址设置不完善!");
            newMessage.ReceiverIp = ipcontent[0]; //服务端端IP
            newMessage.ReceiverPort = Convert.ToInt32(ipcontent[1]); //服务端端口
            newMessage.SenderIp = ipcontent[2]; //Android端IP
            newMessage.SenderPort = Convert.ToInt32(ipcontent[3]); //Android端口

            //读取url
            string[] urls = url.Split('|');
            List<string> list = new List<string>();
            for (int i = 0; i < urls.Length; i++)
            {
                list.Add(urls[i]);
            }
            newMessage.Content = list;
            Send(newMessage);
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="ip">服务器ip地址</param>
        /// <param name="port">服务器端口</param>
        /// <param name="message">消息体</param>
        public void Send(NewMessage message)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(message.ReceiverIp);//设置接收端IP及端口

                if (ipAddress != null)
                {
                    IPEndPoint iep = new IPEndPoint(ipAddress, message.ReceiverPort);
                    //由于消息由服务端转发，结构体内容发出前交换接收端IP和服务端IP
                    string tIp = message.SenderIp;
                    int tPort = message.SenderPort;
                    message.SenderIp = message.ReceiverIp;
                    message.SenderPort = message.ReceiverPort;
                    message.ReceiverIp = tIp;
                    message.ReceiverPort = tPort;
                    //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //socket.Connect(iep);
                    //使用带连接超时设置的连接
                    Socket socket = Connect(iep, 5000);
                    message.SendTime = DateTime.Now.Date;
                    //socket.Send(Encoding.UTF8.GetBytes(message.ToJson().EncryptStringToBytes_Des("888", "888")));
                    socket.Send(Encoding.UTF8.GetBytes(message.ToJson()));
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();//释放资源
                }
            }
            catch
            {
                throw new Exception("对方拒绝连接！");//连接错误
            }
        }

        #region Socket超时链接
        private readonly ManualResetEvent TimeoutObject = new ManualResetEvent(false);
        /// <summary>   
        /// Socket连接请求         
        /// </summary>       
        ///<param name="remoteEndPoint">网络端点</param>        
        ///<param name="timeoutMSec">超时时间</param>   
        public Socket Connect(IPEndPoint remoteEndPoint, int timeoutMSec)
        {
            TimeoutObject.Reset();
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.BeginConnect(remoteEndPoint, CallBackMethod, socket);
            //阻塞当前线程             
            if (TimeoutObject.WaitOne(timeoutMSec, false))
            {
                //MessageBox.Show("网络正常");  
                return socket;
            }
            else
            {
                //MessageBox.Show("连接超时");  
                throw new Exception("网络异常！");
            }
        }
        //--异步回调方法         
        private void CallBackMethod(IAsyncResult asyncresult)
        {
            //使阻塞的线程继续          
            TimeoutObject.Set();
        }
        #endregion

    }
}
