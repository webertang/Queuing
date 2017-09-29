using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Net;

namespace RDTools.Common
{
    public class RDManagementClass
    {
        /// <summary>
        /// 获取MAC 地址
        /// </summary>
        public static string GetMAC()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                string str = "";
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        str = mo["MacAddress"].ToString();
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                throw new Exception("网络硬件设备错误，" + ex.Message);
            }
        }

        /// <summary>
        /// 获取本机CPU序列号  
        /// </summary>
        /// <returns></returns>
        public static string GetCPUID()
        {
            string CPUID = "";
            using (ManagementClass mc = new ManagementClass("Win32_Processor"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject item in moc)
                {
                    CPUID = item["ProcessorId"].ToString().Trim();
                }
            }
            return CPUID;
        }

        /// <summary>
        /// 获取本机硬盘序列号
        /// </summary>
        /// <returns></returns>
        public static string GetHardID()
        {
            string HardID = "";
            using (ManagementClass mc = new ManagementClass("Win32_DiskDrive"))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject item in moc)
                {
                    HardID = item["Model"].ToString().Trim();
                }
            }
            return HardID;
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
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

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }
    }
}
