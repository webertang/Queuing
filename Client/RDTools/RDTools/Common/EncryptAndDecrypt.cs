using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RDTools.Common
{
    public class EncryptAndDecrypt
    {
        //12个字符  add by:tzw@2016.11.23
        private static string customIV = "ou95G2/WziI=";//ou95G2/WziI=
        //32个字符  add by:tzw@2016.11.23
        private static string keyFrom = "VToaqZjp8C27V90oSmT/CF+afvRGClc9";//VToaqZjp8C27V90oSmT/CF+afvRGClc9
        private static string keyTo = "VToaqZjp8C27V09oSmTv4444fvRGClc9";//VToaqZjp8C27V90oSmT/CF+afvRGClc9

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

            _encryptionAlgorithm.Key = Convert.FromBase64String(keyFrom);
            key = Convert.FromBase64String(keyTo);

            _encryptionAlgorithm.IV = Convert.FromBase64String(customIV);
            iv = Convert.FromBase64String(customIV);
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
            try
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
            catch
            {
                return cipherText;
            }
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
            try
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
            catch
            {
                return input;
            }
        }
    }
}
