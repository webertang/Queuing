using System;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace RDTools.Common
{
    public class AppConfig
    {
        public static string GetAppSetting(string appPath, string key)
        {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(appPath);
            //return config.AppSettings.Settings[key].Value;

            if (File.Exists(configfilepath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(applicationDocumentPath);
                XmlNode xmlNode = xmlDoc.SelectSingleNode("configuration/" + section);
                if (xmlNode != null)
                {
                    foreach (XmlNode x in xmlNode.ChildNodes)
                    {
                        if (x.Name != "add")
                            continue;
                        if (config == x.Attributes["key"].Value)
                        {
                            return x.Attributes["value"].Value;
                        }
                    }
                }
                else
                {
                    return "1";
                }
            }
            return "1";

        }

        public static string GetAppSetting(string key)
        {
            return GetAppSetting(Application.ExecutablePath, key);
        }

        public static void WriteAppSetting(string appPath, string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(appPath);
                config.AppSettings.Settings[key].Value = value;
                config.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetRdClientManageConfig(string section, string config)
        {
            string applicationDocumentPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            if (File.Exists(applicationDocumentPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(applicationDocumentPath);
                XmlNode xmlNode = xmlDoc.SelectSingleNode("configuration/" + section);
                if (xmlNode != null)
                {
                    foreach (XmlNode x in xmlNode.ChildNodes)
                    {
                        if (x.Name != "add")
                            continue;
                        if (config == x.Attributes["key"].Value)
                        {
                            return x.Attributes["value"].Value;
                        }
                    }
                }
                else
                {
                    return "1";
                }
            }
            return "1";
        }
    }
}
