using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RDTools.Common
{
    public static class LogHelper
    {
        public static void WriteLog(string title, string txt)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\\MSG.log";
            File.AppendAllText(path, @"\r\n" + title + DateTime.Now + @"\r\n数据：" + txt, Encoding.UTF8);//写入内容 // 根据路径出内容
        }
    }
}
