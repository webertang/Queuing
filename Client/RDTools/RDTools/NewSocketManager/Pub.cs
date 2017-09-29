using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace RDTools.NewSocketManager
{
    public class Pub
    {
        /// <summary>
        /// 获取本机Ip
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetHostIPAddress()
        {
            foreach (IPAddress item in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (item.AddressFamily.ToString() == "InterNetwork")
                {
                    return item;
                }
            }

            //返回本机ip
            return null;
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">公钥</param>
        /// <returns>密文</returns>
        public static string EncryptStringToBytes_Des(string plainText, string key, string iv)
        {

            if (plainText.Trim() == string.Empty)
                return plainText;
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();//des进行加密


            byte[] Key = Encoding.UTF8.GetBytes(key);
            byte[] IV = Encoding.UTF8.GetBytes(iv);
            if (Key.Length != 8)
            {
                byte[] tmp = new byte[8];

                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] = Key[i];

                    if (i == Key.Length - 1)
                    {
                        break;
                    }
                }

                Key = tmp;
            }


            if (IV.Length != 8)
            {
                byte[] tmp = new byte[8];

                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] = IV[i];

                    if (i == IV.Length - 1)
                    {
                        break;
                    }
                }

                IV = tmp;
            }
            DES.Key = Key;
            DES.IV = IV;
            ICryptoTransform desencrypt = DES.CreateEncryptor();

            MemoryStream ms = new MemoryStream();//存储加密后的数据
            CryptoStream cs = new CryptoStream(ms, desencrypt, CryptoStreamMode.Write);
            byte[] data = Encoding.UTF8.GetBytes(plainText);//取到密码的字节流
            cs.Write(data, 0, data.Length);//进行加密
            cs.FlushFinalBlock();
            byte[] res = ms.ToArray();//取加密后的数据
            cs.Close();
            ms.Close();
            return Convert.ToBase64String(res);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="cipherText">密文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">公钥</param>
        /// <returns>明文</returns>
        public static string DecryptStringFromBytes_Des(string cipherText, string key, string iv)
        {

            if (cipherText.Trim() == string.Empty)
                return cipherText;

            byte[] Key = Encoding.UTF8.GetBytes(key);
            byte[] IV = Encoding.UTF8.GetBytes(iv);
            if (Key.Length != 8)
            {
                byte[] tmp = new byte[8];

                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] = Key[i];

                    if (i == Key.Length - 1)
                    {
                        break;
                    }
                }

                Key = tmp;
            }


            if (IV.Length != 8)
            {
                byte[] tmp = new byte[8];

                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] = IV[i];

                    if (i == IV.Length - 1)
                    {
                        break;
                    }
                }

                IV = tmp;
            }
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();

            DES.Key = Key;
            DES.IV = IV;
            ICryptoTransform desencrypt = DES.CreateDecryptor();

            MemoryStream ms = new MemoryStream();//存储解密后的数据

            CryptoStream cs = new CryptoStream(ms, desencrypt, CryptoStreamMode.Write);

            byte[] data = Convert.FromBase64String(cipherText);
            cs.Write(data, 0, data.Length);//解密数据
            try
            {
                cs.FlushFinalBlock();
            }
            catch
            {
                throw new Exception("数据不正确！");
            }
            byte[] res = ms.ToArray();
            cs.Close();
            ms.Close();
            return Encoding.UTF8.GetString(res);//返回解密后的数据，这里返回的数据应该和参数pwd的值相同。
        }

        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T">要反序列化的类型</typeparam>
        /// <param name="jsonString">反序列化的类</param>
        /// <returns>反序列化对象</returns>
        public static T DeserializeJson<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <param name="value">要序列化的对象</param>
        /// <returns>序列化后的Json字符串</returns>
        public static string SerializeJson(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

    }
}
