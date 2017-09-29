using System;
using RDTools.Common;

namespace RDTools.SocketManager
{
    /// <summary>
    /// 消息发送方法类
    /// </summary>
    public class SendMessage
    {
        #region 上线
        /// <summary>
        /// 上线
        /// </summary>
        /// <param name="operatorID">操作员ID</param>
        /// <param name="operatorCode">操作员编号</param>
        /// <param name="operatorName">操作员名称</param>
        /// <param name="operatorOfficeID">操作员科室ID</param>
        /// <param name="operatorOffice">操作员科室名称</param>
        /// <param name="key">密钥</param>
        /// <returns>监听IP，null为不成功</returns>
        public string LoginServer(string operatorID, string operatorCode, string operatorName, string operatorOfficeID, string operatorOffice, string operatorWorkkind, int port, string key)
        {
            Sender sender = new Sender();

            try
            {
                string serverIp = string.Empty;
                int serverPort = 0;
                int index = 0;

                if (GetConfigMessage(ref serverIp, ref serverPort, ref index))
                {
                    string message = "上线@" + operatorID + "|" +
                        RDManagementClass.GetHostName() + "|" + operatorCode + "|" + operatorName + "|"
                        + operatorOfficeID + "|" + operatorOffice + "|"
                        + serverIp + ":" + port + "|" + operatorWorkkind;

                    if (key != null)
                    {
                        sender.Send(serverIp, serverPort, message, key);
                    }
                    else
                    {
                        sender.Send(serverIp, serverPort, message);
                    }

                    return serverIp + ":" + port;
                }
            }
            catch
            {
                ;
            }

            return null;
        }

        public void LoginServer(string operatorID, string operatorCode, string operatorName,
            string operatorOfficeID, string operatorOffice, string operatorWorkkind,
            string serverIp, int serverPort, string key, string IP)
        {
            Sender sender = new Sender();

            try
            {
                string message = "上线@" + operatorID + "|" +
                    RDManagementClass.GetHostName() + "|" + operatorCode + "|" + operatorName + "|"
                    + operatorOfficeID + "|" + operatorOffice + "|"
                    + IP + "|" + operatorWorkkind;

                if (key != null)
                {
                    sender.Send(serverIp, serverPort,message, key);
                }
                else
                {
                    sender.Send(serverIp, serverPort, message);
                }
            }
            catch
            {
            }
        }

        public string LoginServer(string operatorID, string operatorCode, string operatorName, string operatorOfficeID, string operatorOffice, string operatorWorkkind, int port)
        {
            return LoginServer(operatorID, operatorCode, operatorName, operatorOfficeID, operatorOffice, operatorWorkkind, port, null);
        }

        #endregion

        #region 下线
        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="operatorID">操作员ID</param>
        /// <param name="key">密钥</param>
        /// <returns>是否成功</returns>
        public bool ExitServer(string ip, string key)
        {
            Sender sender = new Sender();

            try
            {
                string serverIp = string.Empty;
                int serverPort = 0;
                int index = 0;

                if (GetConfigMessage(ref serverIp, ref serverPort, ref index))
                {
                    if (key != null)
                    {
                        sender.Send(serverIp, serverPort, "下线@" + ip, key);
                    }
                    else
                    {
                        sender.Send(serverIp, serverPort, "下线@" + ip);
                    }

                    return true;
                }
            }
            catch
            {
                ;
            }

            return false;
        }

        public bool ExitServer(string ip)
        {
            return ExitServer(ip, null);
        }

        public bool ExitServer(string serverIp, int serverPort, string ip, string key)
        {
            Sender sender = new Sender();

            try
            {
                if (key != null)
                {
                    sender.Send(serverIp, serverPort, "下线@" + ip, key);
                }
                else
                {
                    sender.Send(serverIp, serverPort, "下线@" + ip);
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        #endregion

        #region 消息
        /// <summary>
        /// 发送任意消息
        /// </summary>
        /// <param name="message">消息体</param>
        /// <param name="key">密钥</param>
        /// <returns>是否成功</returns>
        public bool SendAnyMessage(string operatorID, string operatorOfficeID, string messageType, string message, string messageID, string key)
        {
            Sender sender = new Sender();

            try
            {
                string serverIp = string.Empty;
                int serverPort = 0;
                int index = 0;

                if (GetConfigMessage(ref serverIp, ref serverPort, ref index))
                {
                    if (key != null)
                    {
                        //sender.Send(serverIp, serverPort, "消息@" + operatorID + "|" + operatorOfficeID + "|" +
                        //    messageType + "|" + message + "|" + messageID, key);
                        sender.Send(serverIp, serverPort, operatorID + "|" + operatorOfficeID + "|" +
                            messageType + "|" + message + "|" + messageID, key);
                    }
                    else
                    {
                        //sender.Send(serverIp, serverPort, "消息@" + operatorID + "|" + operatorOfficeID + "|" +
                        //    messageType + "|" + message + "|" + messageID);
                        sender.Send(serverIp, serverPort, operatorID + "|" + operatorOfficeID + "|" +
                            messageType + "|" + message + "|" + messageID);
                    }

                    return true;
                }
            }
            catch
            {
                ;
            }

            return false;
        }

        public bool SendAnyMessage(string operatorID, string operatorOfficeID, string messageType, string message, string messageID)
        {
            return SendAnyMessage(operatorID, operatorOfficeID, messageType, message, messageID, null);
        }

        public bool SendSoundMessage(string messageType, string message, string key)
        {
            Sender sender = new Sender();

            try
            {
                string serverIp = string.Empty;
                int serverPort = 0;
                string soundIp = string.Empty;
                string screenIp = string.Empty;
                int index = 0;

                if (GetConfigMessage(ref serverIp, ref serverPort, ref index))
                {
                    if (key != null)
                    {
                        sender.Send(serverIp, serverPort, "消息@" + message, key);
                    }
                    else
                    {
                        sender.Send(serverIp, serverPort, "消息@" + message);
                    }

                    return true;
                }
            }
            catch
            {
                ;
            }

            return false;
        }
    
        public bool SendSoundMessage(string messageType, string message)
        {
            return SendSoundMessage(messageType, message, null);
        }

        public bool SendSoundMessage(string messageType, string message, string serverIp, int serverPort, string key)
        {
            Sender sender = new Sender();

            try
            {
                string soundIp = string.Empty;
                string screenIp = string.Empty;

                if (key != null)
                {
                    //sender.Send(serverIp, serverPort, "消息@" + message, key);
                    sender.Send(serverIp, serverPort, message, key);
                }
                else
                {
                    //sender.Send(serverIp, serverPort, "消息@" + message);
                    sender.Send(serverIp, serverPort, message);
                }

                return true;
            }
            catch
            {
                ;
            }

            return false;
        }

        public bool SendSoundMessage(string messageType, string message, string serverIp, int serverPort)
        {
            return SendSoundMessage(messageType, message, serverIp, serverPort,null);
        }
        #endregion

        public bool GetConfigMessage(ref string serverIP, ref int serverPort, ref int index)
        {
            try
            {
                index = 0;
                serverIP = RDManagementClass.GetHostIP(index).ToString();
                serverPort = Convert.ToInt32(AppConfig.GetAppSetting("Messageport"));

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}