using Newtonsoft.Json;
using RDTools.Pass.CreateJSONStrForKM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace RD.Pass
{
    public class PassFunctionForKM
    {
        public static void InitializeKM()
        {
            System.Diagnostics.Process.Start("KMWATCH.EXE");

            //System.Diagnostics.Process LandEatSnake
        }

        public static string Prescription_Analysis(string _BufferJSON, string Url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                //request.ContentLength = Encoding.UTF8.GetByteCount(_BufferJSON);
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.UTF8);
                myStreamWriter.Write(_BufferJSON);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(retString);
                JsonReaderCls p1 = (JsonReaderCls)serializer.Deserialize(new JsonTextReader(sr), typeof(JsonReaderCls));

                if (p1.state == "SUCCESS")
                {
                    return "";
                }
                else
                {
                    return p1.state ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ExitKM()
        {
            //获得进程对象，以用来操作   
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程    
            try
            {
                //获得需要杀死的进程名   
                foreach (Process thisproc in Process.GetProcessesByName("kmwatch"))
                {
                    //立即杀死进程   
                    thisproc.Kill();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
