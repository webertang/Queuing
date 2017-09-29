using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.NewSocketManager
{
    public static class Extend
    {
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T AS<T>(this object obj) where T : class
        {
            return obj as T;
        }

        /// <summary>
        /// 序列化json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return Pub.SerializeJson(obj);
        }

        /// <summary>
        /// json转反序列化T类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToT<T>(this string obj)
        {
            return Pub.DeserializeJson<T>(obj);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptStringToBytes_Des(this string obj, string key, string iv)
        {
            return Pub.EncryptStringToBytes_Des(obj, key, iv);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptStringFromBytes_Des(this string obj, string key, string iv)
        {
            return Pub.DecryptStringFromBytes_Des(obj, key, iv);
        }

        public static short ToShort(this object obj)
        {
            return Convert.ToInt16(obj);
        }

        public static int ToInt32(this object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static long ToLong(this object obj)
        {
            return Convert.ToInt64(obj);
        }

        public static decimal ToDecimal(this object obj)
        {
            return Convert.ToDecimal(obj);
        }

        public static double ToDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }

        public static float ToFloat(this object obj)
        {
            return Convert.ToSingle(obj);
        }

        public static bool ToBool(this object obj)
        {
            return Convert.ToBoolean(obj);
        }

        public static DateTime ToDateTime(this object obj)
        {
            return Convert.ToDateTime(obj);
        }

    }
}
