﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;

namespace PDTools
{
    [Guid("2DB63310-85E6-4E95-9B38-E94B7AED83C3")]
    public class TTSInterface
    {
        //向Led发送消息
        public string sendMessage(string[] msg, int screentType)
        {

            switch (screentType)
            {           //字体12号
                case 1://医生门头屏2行12字
                    return new EQ2008.EQCtroller().sendMessage(msg, 10, 0, 2);
                case 2:
                    //通过卡地址2发送滚动消息 到胎监
                    if (msg.Length >= 11)
                    {
                        if (!(null == msg[10]) && msg[10].Length > 0 && !(msg[10] == ""))
                            new EQ2008.EQCtroller().scrollMessage(msg[10], 320, 7);//显示位置：胎监显示屏底部
                        msg[10] = "";
                    }
                    //B超大屏
                    return new EQ2008.EQCtroller().sendMessage(msg, 28, 1, 0);
                case 3://28字
                    //门诊医生大屏
                    return new EQ2008.EQCtroller().sendMessage(msg, 28, 1, 0);
                case 4://20字
                    //通过卡地址2发送滚动消息 B超大屏底部滚动内容
                    return new EQ2008.EQCtroller().scrollMessage(msg[0], 448, 11);
                //胎监大屏
                //return new EQ2008.EQCtroller().sendMessage(msg, 20, 0, 0);
                case 5://第一列屏显示
                    return new EQ2008.EQCtroller().sendMessage(msg, 6, 0, 0);
                case 6://第二列屏显示
                    return new EQ2008.EQCtroller().sendMessage(msg, 6, 0, 7);
                case 7://第三列屏显示
                    return new EQ2008.EQCtroller().sendMessage(msg, 6, 0, 14);
                case 8://第二列屏显示
                    return new EQ2008.EQCtroller().sendMessage(msg, 12, 0, 0);
                case 9://第二列屏显示
                    return new EQ2008.EQCtroller().sendMessage(msg, 12, 1, 0);
                default:
                    return "屏类型错误！";
            }

        }
        //向Led屏发送滚动消息
        public string scrollMessage(string msg, int screentType)
        {
            switch (screentType)
            {           //字体12号
                case 1://医生门头屏2行12字
                    return new EQ2008.EQCtroller().scrollMessage(msg, 192, 0);
                case 2:
                case 3://28字
                    return new EQ2008.EQCtroller().scrollMessage(msg, 448, 1);
                case 4://20字
                    return new EQ2008.EQCtroller().scrollMessage(msg, 320, 0);
                default:
                    return "屏类型错误！";
            }

        }

        // 汉字转码:安卓数据传输中文使用
        public String StrEncode(string str)
        {
            UnicodeEncoding a = new UnicodeEncoding();
            String ReturnStr = "";
            byte[] b = a.GetBytes(str);
            byte[] d = new byte[2];
            for (int i = 0; i < b.Length; i++)
            {
                i++;
                if (b[i] != 0)
                {
                    d[0] = b[i - 1];
                    d[1] = b[i];
                    ReturnStr = ReturnStr + System.Web.HttpUtility.UrlEncode(a.GetString(d)).ToUpper();
                }
                else
                {
                    d[0] = b[i - 1];
                    d[1] = b[i];
                    ReturnStr = ReturnStr + a.GetString(d);
                }
            }
            return ReturnStr;
        }

        //发送Socket消息
        public void Send(string ip, int port, string msg)
        {
            try
            {
                Sender sendMsg = new Sender();
                sendMsg.Send(ip, port, msg);
            }
            catch(Exception e)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Error.log";
                string title = "Socket连接错误！";
                string txt = e.Message;
                File.AppendAllText(path, "\r\n" + title + DateTime.Now + "\r\n错误信息：" + txt, Encoding.UTF8);//写入内容 // 根据路径出内容
            }
        }

    }

    #region ISocketMessager
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
                        bytes = System.Text.Encoding.GetEncoding("GBK").GetBytes(EncryptAndDecrypt.Encrypt(message, ""));
                        //Encoding.UTF8.GetBytes(message);
                    }
                    else
                    {
                        bytes = System.Text.Encoding.GetEncoding("GBK").GetBytes(EncryptAndDecrypt.Encrypt(message, ""));
                        //bytes =
                        //    Encoding.UTF8.GetBytes(EncryptAndDecrypt.Encrypt(message, key));
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
    }

    public class EncryptAndDecrypt
    {
        private SymmetricAlgorithm _encryptionAlgorithm;
        byte[] key = null;
        byte[] iv = null;

        public EncryptAndDecrypt()
        {
            Init();
        }

        public void Init()
        {
            _encryptionAlgorithm = TripleDESCryptoServiceProvider.Create();

            _encryptionAlgorithm.Key = Convert.FromBase64String("VToaqZjp8C27V90oSmT/CF+afvRGClc9");
            key = Convert.FromBase64String("VToaqZjp8C27V09oSmTv4444fvRGClc9");

            _encryptionAlgorithm.IV = Convert.FromBase64String("ou95G2/WziI=");
            iv = Convert.FromBase64String("ou95G2/WziI=");
        }

        public string Encrypt(string plainText)
        {
            string encryMsg = null;

            byte[] cipherValue = null;
            byte[] plainValue = Encoding.UTF8.GetBytes(plainText);

            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memStream, _encryptionAlgorithm.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(plainValue, 0, plainValue.Length);
                cryptoStream.Flush();
                cryptoStream.FlushFinalBlock();
                cipherValue = memStream.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception("无法加密！原因：\r\n" + e.Message, e);
            }
            finally
            {
                if (plainValue != null)
                    Array.Clear(plainValue, 0, plainValue.Length);
                memStream.Close();
                cryptoStream.Close();
            }
            encryMsg = Convert.ToBase64String(cipherValue);
            return encryMsg;
        }

        public string Decrypt(string cipherText)
        {
            byte[] cipherValue = Convert.FromBase64String(cipherText);
            byte[] plainValue = new byte[cipherValue.Length];
            string plainMsg = null;

            MemoryStream memStream = new MemoryStream(cipherValue);
            CryptoStream cryptoStream = new CryptoStream(memStream, _encryptionAlgorithm.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            try
            {
                cryptoStream.Read(plainValue, 0, plainValue.Length);
            }
            catch (Exception e)
            {
                throw new Exception("无法解密！原因：\r\n" + e.Message, e);
            }
            finally
            {
                if (cipherValue != null)
                    Array.Clear(cipherValue, 0, cipherValue.Length);
                memStream.Close();
                cryptoStream.Close();
            }
            plainMsg = Encoding.UTF8.GetString(plainValue);
            return plainMsg;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input">明文</param>
        /// <param name="rgbKey">密钥</param>
        /// <returns>密文</returns>
        public static string Encrypt(string input, string rgbKey)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            UTF8Encoding utf8Encoding = new UTF8Encoding();

            //处理密钥，密钥必须是8位
            //密钥为空或为空字符串时直接赋值为八个6
            if (string.IsNullOrEmpty(rgbKey))
            {
                rgbKey = "66666666";
            }
            //密钥长度小于8位时就加上10的他的长度与7的差次幂，补齐8位
            else if (rgbKey.Length < 8)
            {
                rgbKey = rgbKey + Math.Pow(10, 8 - (rgbKey.Length + 1)).ToString();
            }
            //其次在密钥长度不等于8时就只有大于8一种情况，所以截取前八位作为密钥
            else if (rgbKey.Length != 8)
            {
                rgbKey = rgbKey.Substring(0, 8);
            }

            //设置计算量为密钥的首位替换成8处理
            Byte[] rgbIV = utf8Encoding.GetBytes(rgbKey.Replace(rgbKey[0], '8'));

            byte[] inputByteArray = utf8Encoding.GetBytes(input);

            System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServericeProvider =
                new System.Security.Cryptography.DESCryptoServiceProvider();

            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                System.Security.Cryptography.CryptoStream cryptoStream =
                    new System.Security.Cryptography.CryptoStream(memoryStream,
                        dESCryptoServericeProvider.CreateEncryptor(utf8Encoding.GetBytes(rgbKey), rgbIV),
                        System.Security.Cryptography.CryptoStreamMode.Write);

                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="input">密文</param>
        /// <param name="rgbKey">密钥8位数字</param>
        /// <returns>明文</returns>
        public static string Decrypt(string input, string rgbKey)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            UTF8Encoding utf8Encoding = new UTF8Encoding();

            //处理密钥，密钥必须是8位
            //密钥为空或为空字符串时直接赋值为八个6
            if (string.IsNullOrEmpty(rgbKey))
            {
                rgbKey = "66666666";
            }
            //密钥长度小于8位时就加上10的他的长度与7的差次幂，补齐8位
            else if (rgbKey.Length < 8)
            {
                rgbKey = rgbKey + Math.Pow(10, 8 - (rgbKey.Length + 1)).ToString();
            }
            //其次在密钥长度不等于8时就只有大于8一种情况，所以截取前八位作为密钥
            else if (rgbKey.Length != 8)
            {
                rgbKey = rgbKey.Substring(0, 8);
            }

            //设置计算量为密钥的首位替换成8处理
            Byte[] rgbIV = utf8Encoding.GetBytes(rgbKey.Replace(rgbKey[0], '8'));

            byte[] inputByteArray = Convert.FromBase64String(input);

            System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServericeProvider =
                new System.Security.Cryptography.DESCryptoServiceProvider();

            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                System.Security.Cryptography.CryptoStream cryptoStream =
                    new System.Security.Cryptography.CryptoStream(memoryStream,
                        dESCryptoServericeProvider.CreateDecryptor(utf8Encoding.GetBytes(rgbKey), rgbIV),
                        System.Security.Cryptography.CryptoStreamMode.Write);

                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();

                return utf8Encoding.GetString(memoryStream.ToArray());
            }
        }
    }
    #endregion

}
