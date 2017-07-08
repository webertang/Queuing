using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using RDTools.Common;
using System.Linq;
using System.Net;
using System.IO;
using PDTools.SocketManager;

namespace PDTools
{
    [Guid("2DB63310-85E6-4E95-9B38-E94B7AED83C3")]
    public class TTSInterface
    {
        /// <summary>
        /// 发送消息到Android端进行展示
        /// </summary>
        /// <param name="iep">IpEndPoint</param>
        /// <param name="OfficeId">诊室ID</param>
        /// <param name="OperatorId">操作员ID</param>
        /// <param name="CompositeScreentId">综合屏ID</param>
        /// <param name="MessageType">消息类型0刷新页面1刷新页面同时弹出提示2弹出提示不刷新主页</param>
        public string SendAndroidMessage(string iep, string OfficeId, string OperatorId, string CompositeScreentId, int MessageType)
        {
            try
            {
                Sender sd = new Sender();
                sd.SendAndroidMessage(iep, OfficeId, OperatorId, CompositeScreentId, MessageType);
                return "1";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        /// <summary>
        /// 设置安卓屏显示内容
        /// </summary>
        /// <param name="iep">安卓屏和回传服务器地址</param>
        /// <param name="url">显示URL至少3个用"|"分割</param>
        public String SendSetAndroidInfo(string iep, string url)
        {
            try
            {
                Sender sd = new Sender();
                sd.SendSetAndroidInfo(iep, url);
                return "1";
            }
            catch (Exception e)
            {
                return e.Message.ToString() ;
            }
        }

        //文字转语音
        public void speak(int speed, string txtcontent)
        {
            //string str = "";
            //if (!this.ReadEyeRegister(ref str)) //判断是否注册
            //{
            //    if (!getKey())//注册
            //    {
            //        saveReg();
            //        MessageBox.Show("提示", str);
            //        return;
            //    }
            //}
            //if (DateTime.Parse("2018.5.1") < DateTime.Now)
            //{
            //    MessageBox.Show("当前系统使用效期已过，为不影响正常使用请尽快联系管理员!\r联系QQ:13015156", "过期提醒");
            //}

            PDTools.MainFrm fm = new PDTools.MainFrm();
            fm.TxtToVoice(speed, txtcontent);
            fm.Close();

            //叫号内容保存
            //txtcontent = txtcontent + Environment.NewLine;
            //string path = @"c:\TTSContent.txt";//写入内容文件的径
            //File.AppendAllText(path, txtcontent, Encoding.UTF8);//写入内容 // 根据路径出内容
        }
        //发送文本消息
        public string sendMessage(string[] msg, int screentType)
        {
            string[] msgTmp = new string[1];
            switch (screentType)
            {           //字体12号
                case 1://医生门头屏2行12字
                    return new EQ2008.EQCtroller().sendMessage(msg, 10, 0, 2, 1, 12);
                case 2:
                    //通过卡地址2发送滚动消息 到胎监
                    if (msg.Count() >= 11)
                    {
                        if (!(null == msg[10]) && msg[10].Length > 0 && !(msg[10] == ""))
                        //    new EQ2008.EQCtroller().scrollMessage(msg[10], 320, 7);//显示位置：胎监显示屏底部
                        {
                            msgTmp[0] = msg[10];
                            new EQ2008.EQCtroller().sendMessage(msgTmp, 20, 7, 0, 2, 10);
                            msg[10] = "";
                        }
                    }
                    //B超大屏
                    return new EQ2008.EQCtroller().sendMessage(msg, 28, 1, 0, 1, 12);
                case 3://28字
                    //门诊医生大屏
                    return new EQ2008.EQCtroller().sendMessage(msg, 28, 1, 0, 1, 12);
                case 4://20字
                    //通过卡地址2发送滚动消息 B超大屏底部滚动内容
                    //return new EQ2008.EQCtroller().scrollMessage(msg[0], 448, 11);
                    return new EQ2008.EQCtroller().sendMessage(msg, 28, 11, 0, 2, 10);
                //胎监大屏
                //return new EQ2008.EQCtroller().sendMessage(msg, 20, 0, 0);
                case 5://第一列屏显示
                    return new EQ2008.EQCtroller().sendMessage(msg, 6, 0, 0, 1, 12);
                case 6://第二列屏显示
                    return new EQ2008.EQCtroller().sendMessage(msg, 6, 0, 7, 1, 12);
                case 7://第三列屏显示
                    return new EQ2008.EQCtroller().sendMessage(msg, 6, 0, 14, 1, 12);
                case 8://第一行屏显示9-1
                    return new EQ2008.EQCtroller().sendMessage(msg, 12, 0, 0, 1, 12);
                case 9://第二行屏显示9-2
                    return new EQ2008.EQCtroller().sendMessage(msg, 12, 1, 0, 1, 12);
                default:
                    return "屏类型错误！";
            }

        }
        /// <summary>
        /// 用户自定义当前内容显示行
        /// </summary>
        /// <param name="msg">显示内容</param>
        /// <param name="screentType">屏幕类型</param>
        /// <param name="rowID">所在行号</param>
        /// <returns></returns>
        public string sendMessage(string[] msg, int screentType, int rowNum, int iCardNum)
        {

            switch (screentType)
            {    //字体12号
                case 1://屏幕总高度显示8个字，分割为上下4排显示，上下内容显示相同（别问我为什么要这样）
                    //  0            1              2              3       
                    //rowID|QueueNumber+Name|Departments(科室)|Office(诊室)|
                    for (int i = 1; i < 4; i++)
                    {
                        string sTmp1 = "", sTmp2 = "";
                        int msgLength = Encoding.Default.GetBytes(msg[i]).Count();
                        //截取需要显示的内容（这里显示6个汉字12个字符）
                        if (msgLength > 12)
                        {
                            //判断是否为中文，中文取两个字符以免显示乱码
                            string[] msgTmp = new string[12];
                            int i1 = 0;
                            for (int j1 = 0; j1 < 12; j1++)
                            {
                                sTmp1 = Encoding.Default.GetString(Encoding.Default.GetBytes(msg[i]), j1, 2);
                                if ((int)sTmp1[0] > 127)
                                {
                                    if (j1 + 1 >= 12) continue;
                                    sTmp2 += sTmp1;
                                    j1++;
                                }
                                else
                                {
                                    sTmp2 += Encoding.Default.GetString(Encoding.Default.GetBytes(msg[i]), j1, 1);
                                }
                                i1++;
                            }
                            msg[i] = sTmp2;
                        }
                        //sEmpty = "";
                        //补充占位符以固定显示长度
                        //for (int j2 = 1; j2 < 12 - Encoding.Default.GetBytes(msg[i]).Count(); j2++)
                        //{
                        //    sEmpty += " ";
                        //}
                        //switch (i)
                        //{
                        //    case 3:
                        //        msg[i] = " " + sEmpty + msg[i];
                        //        break;
                        //    case 1:
                        //        msg[i] = " " + msg[i] + sEmpty + "  ";
                        //        break;
                        //    default:
                        //        msg[i] = msg[i] + sEmpty;
                        //        break;
                        //}

                    }

                    for (int i = 0; i < 2; i++)
                    {
                        string[] newMsg = { msg[2] };
                        if (new EQ2008.EQCtroller().sendMessageChange(newMsg, 88, rowNum + (i * 4), 0, iCardNum, 10)
                            != "成功")
                            return "连接失败";
                        newMsg[0] = msg[1];
                        new EQ2008.EQCtroller().sendMessageChange(newMsg, 88, rowNum + (i * 4), 88, iCardNum, 10);
                        newMsg[0] = msg[3];
                        new EQ2008.EQCtroller().sendMessageChange(newMsg, 80, rowNum + (i * 4), 176, iCardNum, 10);
                    }
                    return "成功";

                default:
                    return "屏类型错误！";
            }

        }
        //发送滚动消息
        public string scrollMessage(string msg, int screentType)
        {
            switch (screentType)
            {           //字体12号
                case 1://医生门头屏2行12字
                    return new EQ2008.EQCtroller().scrollMessage(msg, 192, 0);
                case 2:
                case 3://28字
                    return new EQ2008.EQCtroller().scrollMessage(msg, 448, 1);
                case 4://20字
                    return new EQ2008.EQCtroller().scrollMessage(msg, 320, 0);
                default:
                    return "屏类型错误！";
            }

        }

        // 汉字转码:安卓数据传输中文使用
        public String StrEncode(string str)
        {
            UnicodeEncoding a = new UnicodeEncoding();
            String ReturnStr = "";
            byte[] b = a.GetBytes(str);
            byte[] d = new byte[2];
            for (int i = 0; i < b.Length; i++)
            {
                i++;
                if (b[i] != 0)
                {
                    d[0] = b[i - 1];
                    d[1] = b[i];
                    ReturnStr = ReturnStr + System.Web.HttpUtility.UrlEncode(a.GetString(d)).ToUpper();
                }
                else
                {
                    d[0] = b[i - 1];
                    d[1] = b[i];
                    ReturnStr = ReturnStr + a.GetString(d);
                }
            }
            if (!this.ReadEyeRegister(ref str))
            {
                return "system not register, contact QQ:13015156";
            }
            return ReturnStr;
        }

        /// <summary>
        /// 检测当前系统是否注册
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        public bool ReadEyeRegister(ref string strMessage)
        {
            try
            {
                EncryptAndDecrypt ed = new EncryptAndDecrypt();

                string stringin = RegEdit.Query("key");
                string outer = ed.Decrypt(stringin);
                string[] mygets = outer.Split('|');

                if (mygets.Length < 12 || mygets[0] != "RDHIS")
                {
                    strMessage = "不合法的注册认证文件";
                    return false;
                }

                if (mygets[4] != RDManagementClass.GetMAC() &&
                mygets[5] != RDManagementClass.GetCPUID() &&
                mygets[6] != RDManagementClass.GetHardID())
                {
                    strMessage = "不合法的注册认证文件";
                    return false;
                }

                DateTime thedate = DateTime.Parse(mygets[9]);
                if (thedate < DateTime.Now)
                {
                    strMessage = "注册文件已过期,请重新注册";
                    return false;
                }
                else
                {
                    TimeSpan timeSpan = thedate - DateTime.Now;
                    if (timeSpan.Days <= 10)
                    {
                        MessageBox.Show("还有" + timeSpan.Days + "天注册过期");
                    }
                }

                return true;
            }
            catch (Exception)
            {
                strMessage = "不合法的注册认证文件";
                return false;
            }

        }

        //保存注册文件
        public void saveReg()
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
        public Boolean getKey()
        {
            if (!File.Exists(System.Environment.CurrentDirectory + "\\key.key"))
            {
                //RDMessage.MsgInfo("没有找到注册文件！");
                return false;
            }

            StreamReader streamReader = new StreamReader(System.Environment.CurrentDirectory + "\\key.key", System.Text.Encoding.UTF8);
            string content = streamReader.ReadToEnd();
            streamReader.Close();

            string[] contents = new EncryptAndDecrypt().Decrypt(content).Split('|');

            if (contents.Length < 12 || contents[0] != "RDHIS")
            {
                //RDMessage.MsgInfo("不合法的注册认证文件");
                return false;
            }

            if (contents[4] != RDManagementClass.GetMAC() &&
                contents[5] != RDManagementClass.GetCPUID() &&
                contents[6] != RDManagementClass.GetHardID())
            {
                //RDMessage.MsgInfo("不合法的注册认证文件!");
                return false;
            }

            if (Convert.ToDateTime(contents[9]) < DateTime.Now)
            {
                //RDMessage.MsgInfo("注册文件已过期,请重新注册！");
                return false;
            }

            RegEdit.RegisterOver("Key", content);
            //RDMessage.MsgInfo("恭喜,注册已生效！");
            return true;

        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="ip">所要发送到的ip地址</param>
        /// <param name="port">发送消息的端口</param>
        /// <param name="message">消息体</param>
        public void Send(string ip, int port, string message)
        {
            try
            {
                RDTools.SocketManager.Sender sender = new RDTools.SocketManager.Sender();
                sender.Send(ip, port, message);
            }
            catch(Exception e)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Error.log";
                string title = "Socket连接失败！";
                string text = e.Message+"请确认语音服务器是否打开！";
                File.AppendAllText(path, "\r\n" + title + DateTime.Now + "\r\n错误信息：" + text, Encoding.UTF8);//写入内容 // 根据路径出内容
            }

        }
    }
}
