using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Entity
{
    /// <summary>
    /// 缓存类
    /// </summary>
    public static  class HttpRuntimeCache
    {
        #region 存入Cache
        /// <summary>   
        ///  存入Cache   
        /// </summary>    
        /// <param name="key">缓存key</param>  
        /// <param name="value">缓存的值</param> 
        /// <param name="time_MM">存xx分钟</param>  
        /// <returns>是否执行成功[bool]</returns>  
        public static bool SetCache(string key, object value, int minutes)
        {
            if (string.IsNullOrEmpty(key))
            { return false; }
            bool result = false;
            try
            {
                TimeSpan ts = new TimeSpan(0, minutes, 0);
                System.Web.HttpRuntime.Cache.Insert(key,
                    value,
                    null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration,
                     ts);
                result = true;
            }
            catch { }
            return result;
        }
        #endregion
        #region 取得Cache
        /// <summary>    
        /// 取得Cache   
        /// /// </summary>   
        /// /// <param name="key">key</param>   
        /// /// <returns>object类型</returns>   
        public static T GetCache<T>(string key) where T:class
        {
            if (string.IsNullOrEmpty(key))
            { return null; }
            return (T)System.Web.HttpRuntime.Cache.Get(key);
        }
        #endregion
        #region 查询Cache是否存在
        /// <summary>    ///
        /// 查询Cache是否存在    ///
        /// </summary>    /// 
        /// <param name="key">key 值</param>    
        /// <returns>bool</returns>   
        public static bool IsCacheExist(string key)
        {
            if (string.IsNullOrEmpty(key))
            { return false; }
            bool result = false;
            object temp = System.Web.HttpRuntime.Cache.Get(key);
            if (null != temp)
            {
                result = true;
            }
            return result;
        }
        #endregion

        public static void RemoveCache(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                System.Web.HttpRuntime.Cache.Remove(key);
            }
        }
    }
}
