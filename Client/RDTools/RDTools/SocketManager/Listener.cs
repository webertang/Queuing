using System;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading;
using RDTools.Common;

namespace RDTools.SocketManager
{
    /// <summary>
    /// 监听类
    /// </summary>
    public class Listener
    {
        private int _port;
        private Thread _thread;
        private Socket _socket, _client;
        private volatile bool _listenerRun = true;
        private string _key;
        private System.Windows.Forms.Control _control;

        public delegate void ListenerMessages(object sender, MessageEventArgs e);
        public event ListenerMessages ReceivedMessage;

        protected virtual void OnReceivedMessage(object sender, MessageEventArgs e)
        {
            if (ReceivedMessage != null)
            {
                ReceivedMessage(sender, e);
            }
        }

        public bool ListenerRun
        {
            get { return _listenerRun; }
            set { _listenerRun = value; }
        }

        public System.Windows.Forms.Control Control
        {
            get { return _control; }
            set { _control = value; }
        }

        public Listener(System.Windows.Forms.Control control)
        {
            _control = control;
        }

        public Listener()
        {
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="port">监听端口</param>
        public void Listen(int port, int index)
        {
            _port = port;
            _thread = new Thread(new ParameterizedThreadStart(StartListen));
            _thread.Start(new object[] { false, index });
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="port">监听端口</param>
        /// <param name="key">8位密钥</param>
        public void Listen(int port, string key, int index)
        {
            _port = port;
            _key = key;
            _thread = new Thread(new ParameterizedThreadStart(StartListen));
            _thread.Start(new object[] { true, index });
        }

        /// <summary>
        /// 停止监听，释放资源
        /// </summary>
        public void Stop()
        {
            _listenerRun = false;

            if (_socket != null)
            {
                _socket.Close();
            }

            if (_thread != null)
            {
                _thread.Abort(); //终止线程
            }
        }
        //单独线程处理客户端数据以免客户端住塞线程
        private void Work(object obj)
        {
            Socket so = (Socket)obj;
            int runTimes = 0;
            while (runTimes < 100)
            {
                byte[] byteMessage = new byte[10240];
                int rlen = so.Available;
                if (rlen == 0 && runTimes < 10)
                {
                    runTimes++;
                    Thread.Sleep(1000);
                    continue;
                }
                //10秒钟后还没消息则关闭客户端
                if (rlen == 0)
                {
                    so.Close();
                    break;
                }
                so.Receive(byteMessage);
                so.Close();
                string temp = Encoding.UTF8.GetString(byteMessage);//用密钥解密接收到的信息
                OnReceivedMessage(this, new MessageEventArgs(EncryptAndDecrypt.Decrypt(temp.Replace("\0", ""), _key)));
                //OnReceivedMessage(this, new MessageEventArgs(temp.Replace("\0", "")));
                break;
            }

        }
        private void StartListen(object obj)
        {
            object[] objs = obj as object[];
            object canUseDes = objs[0];
            int index = Convert.ToInt32(objs[1]);

            IPAddress serverIp = GetHostIP(index);//获取本机IP地址，并开启端口的监听
            if (serverIp == null)
            {
                return;
            }

            IPEndPoint ipE = new IPEndPoint(serverIp, _port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   //建立socket对象

            try
            {
                _socket.Bind(ipE);                //绑定IP并开始监听
                _socket.Listen(5);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            while (_listenerRun)
            {
                try
                {
                    //byte[] byteMessage = new byte[300];
                    //Socket so = _socket.Accept();//获取消息，填充字符数组
                    //so.Receive(byteMessage);
                    //so.Close();

                    //string temp = Encoding.UTF8.GetString(byteMessage);//用密钥解密接收到的信息

                    //_control.Invoke(new Action(delegate()
                    //{
                    //    if (Convert.ToBoolean(canUseDes))
                    //    {
                    //        OnReceivedMessage(this, new MessageEventArgs(EncryptAndDecrypt.Decrypt(temp.Replace("\0", ""), _key)));
                    //    }
                    //    else
                    //    {
                    //        OnReceivedMessage(this, new MessageEventArgs(temp.Replace("\0", "")));
                    //    }
                    //}));

                    //byteMessage = null;

                    //改由单独线程处理一个客户端，以免住塞线程 by:tzw@2016.6.23
                    _client = _socket.Accept();
                    new Thread(new ParameterizedThreadStart(Work)).Start(_client);
                }
                catch (SecurityException)
                {
                    throw new Exception("请重新设置防火墙后再试！");
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("端口") > 0)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public static IPAddress GetHostIP(int index)
        {
            IPAddress serverIp = null;
            IPAddress[] ipAddresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            if (ipAddresses.Length > index && ipAddresses[index].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ipAddresses[index];
            }

            foreach (IPAddress ipAddress in ipAddresses)
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    serverIp = ipAddress;
                }
            }

            return serverIp;
        }


        public int BeginListen(Listener listener)
        {
            int index = 0;

            try
            {
                int port = Convert.ToInt32(AppConfig.GetAppSetting("Messageport"));
                listener.Listen(port, "66666666", index);
                return port;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
