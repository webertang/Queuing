using RDTools.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace RD.Service.Class
{
    public static class Reg
    {
        //保存注册文件
        public static void saveReg()
        {
            string mac = RDManagementClass.GetMAC();
            if (mac == string.Empty)
                return;
            string cpu = RDManagementClass.GetCPUID();
            if (cpu == string.Empty)
                return;
            string hardid = RDManagementClass.GetHardID();
            if (hardid == string.Empty)
                return;
            
            StringBuilder sb = new StringBuilder();
            sb.Append("REG").Append('|');
            sb.Append(DateTime.Now.ToString("yyyyMMdd")).Append('|');
            sb.Append(Guid.NewGuid()).Append('|');
            sb.Append(Dns.GetHostName()).Append('|');
            sb.Append(RDManagementClass.GetHostIP(0).ToString()).Append('|');
            sb.Append(mac).Append('|');
            sb.Append(cpu).Append('|');
            sb.Append(hardid).Append('|');
            sb.Append("pdjh");

            string fileContent = new EncryptAndDecrypt().Encrypt(sb.ToString());

            using (StreamWriter sw = new StreamWriter("reg.ser", false, Encoding.UTF8))
            {
                sw.Write(fileContent);
            }

        }
        //获取key文件
        public static void getKey()
        {
            if (!File.Exists(System.Environment.CurrentDirectory + "\\key.key"))
            {
                //RDMessage.MsgInfo("没有找到注册文件！");
                return;
            }

            StreamReader streamReader = new StreamReader(System.Environment.CurrentDirectory + "\\key.key", System.Text.Encoding.UTF8);
            string content = streamReader.ReadToEnd();
            streamReader.Close();

            string[] contents = new EncryptAndDecrypt().Decrypt(content).Split('|');

            if (contents.Length < 12 || contents[0] != "RDHIS")
            {
                //RDMessage.MsgInfo("不合法的注册认证文件");
                return;
            }

            if (contents[4] != RDManagementClass.GetMAC() &&
                contents[5] != RDManagementClass.GetCPUID() &&
                contents[6] != RDManagementClass.GetHardID())
            {
                //RDMessage.MsgInfo("不合法的注册认证文件!");
                return;
            }

            if (Convert.ToDateTime(contents[9]) < DateTime.Now)
            {
                //RDMessage.MsgInfo("注册文件已过期,请重新注册！");
                return;
            }

            RegEdit.RegisterOver("Key", content);
            //RDMessage.MsgInfo("恭喜,注册已生效！");

            //Close();
        }

    }
}
