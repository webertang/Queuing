using PDTools.SocketManager;
using RD.Proxy.Common;
using RD.Service.Class;
using RD.Service.Properties;
using RDTools.Common;
using RDTools.SocketManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RD.Service
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        TcpChannel tcpChannel = null;
        private bool exit;
        private string port;
        private string messageport;
        private Listener _listener;
        private List<UserCls> lst_User;

        private string Listenter;

        private int Serversize;
        private int Serverport;
        private int clientCount;

        //语音叫号部分定义 add by:tzw@2016.1130
        private Thread _thread;
        MessageSender messageSender = new MessageSender();//LED屏、语音消息发送
        private object _screenConnect = new object();
        private int repeatCallTimes;//重叫次数
        private int VoiceSystem = Convert.ToInt32(AppConfig.GetAppSetting("VoiceSystem"));
        private int VoiceSpeed = Convert.ToInt32(AppConfig.GetAppSetting("VoiceSpeed"));

        private RDTools.NewSocketManager.NewSendMessage newlstener;
        public FrmMain()
        {
            InitializeComponent();

            new frmConManage().ReadConfigInfo();
            new frmOtherConManage().FillType();
            port = AppConfig.GetAppSetting("port");
            messageport = AppConfig.GetAppSetting("Messageport");

            Listenter = AppConfig.GetAppSetting("Listenter");

            repeatCallTimes = Convert.ToInt32(AppConfig.GetAppSetting("RepeatCallTimes"));

            if (Listenter == "1")
            {
                Serversize = Convert.ToInt32(AppConfig.GetAppSetting("Serversize"));
                Serverport = Convert.ToInt32(AppConfig.GetAppSetting("Serverport"));
                clientCount = Convert.ToInt32(AppConfig.GetAppSetting("clientCount"));

                //socket绑定,递归调用,无法使用委托返回消息内容 comment by:tzw
                newlstener = new RDTools.NewSocketManager.NewSendMessage(Serverport, clientCount);
                newlstener.ConnClient();
                //使用线程监听端口，效率可能没有上面的高，但是可以通过委托调用返回数据 add by:tzw@2016.7.9
                //newlstener = new RDTools.NewSocketManager.NewListener(1024, port, clientCount);
                //newlstener.ReceiveMessage += new EventHandler<RDTools.NewSocketManager.MessageEventArgs>(newlstener_ReceiveMessage);
                //newlstener.Start();
            }

        }

        private void ShowIconInfo()
        {
            EncryptAndDecrypt ed = new EncryptAndDecrypt();
            string showText = string.Empty;
            try
            {
                string SqlConnection = AppConfig.GetAppSetting("SqlConnection");
                string dbType = AppConfig.GetAppSetting("DataBaseType");
                string endStr = "";

                switch (dbType.ToLower())
                {
                    case "sqlserver":
                        endStr = ed.Decrypt(SqlConnection).TrimEnd('\0');
                        string[] conArray1 = endStr.Split(';', '=');
                        showText += "数据库名:" + conArray1[11];
                        showText += "\n数据库IP:" + conArray1[7];
                        break;
                    case "oracle":
                        endStr = ed.Decrypt(SqlConnection);
                        int index = endStr.IndexOf("SERVICE_NAME=") + "SERVICE_NAME=".Length;
                        showText += "数据库名:" + endStr.Substring(index, endStr.IndexOf(")", index) - index);
                        index = endStr.IndexOf("HOST=") + "HOST=".Length; ;
                        showText += "\n数据库IP:" + endStr.Substring(index, endStr.IndexOf(")", index) - index);
                        break;
                }

                showText += string.Format("\n服务地址:{0}:{1}", RDManagementClass.GetHostIP(0).ToString(), port);
                this.notifyIcon1.Text = showText;
                this.notifyIcon1.ShowBalloonTip(1000, "系统配置信息", this.notifyIcon1.Text, ToolTipIcon.Info);

                //第二页
                new Thread(delegate()
                {
                    Thread.Sleep(4000);
                    showText = string.Format("语音服务地址:{0}:{1}", RDManagementClass.GetHostIP(0).ToString(), messageport);
                    this.notifyIcon1.Text = showText;
                    this.notifyIcon1.ShowBalloonTip(1000, "系统配置信息", this.notifyIcon1.Text, ToolTipIcon.Info);

                }).Start();
            }
            catch (Exception f)
            {
                RDMessage.MsgError(f.Message);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Reg.saveReg();//保存注册文件
            Reg.getKey();//将有效文件进行注册
            ShowIconInfo();

            LifetimeServices.LeaseTime = TimeSpan.Zero;
            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
            RemotingConfiguration.CustomErrorsEnabled(false);
            btnStartServer_ItemClick(null, null);

            string strMessage = string.Empty;
            if (!ReadEyeRegister(ref strMessage))
            {
                //RDMessage.MsgInfo(strMessage);
            }
            //叫号功能 add by:tzw 2016.8.29
            if (VoiceSystem > 0)
            {
                //线程监听法
                _listener = new Listener(this);
                _listener.ReceivedMessage += new Listener.ListenerMessages(_listener_ReceivedMessage);
                _listener.Listen(Convert.ToInt32(AppConfig.GetAppSetting("Messageport")), "66666666", 0);
                lst_User = new List<UserCls>();
                switch (VoiceSystem)
                {
                    case 1://捷通发声
                        messageSender.StartSpeak();
                        break;
                    default://默认启动微软发声
                        messageSender.Start();//启动LED叫号功能，实时监测语音数据有语音数据就叫号
                        break;
                }

            }

            //tablealter();

        }


        private void newlstener_ReceiveMessage(object sender, RDTools.NewSocketManager.MessageEventArgs e)
        {
            MessageBox.Show("", "ok");
        }

        private void newlstener_Login(object sender, RDTools.NewSocketManager.ClientEventArgs e)
        {

        }
        private void newlstener_OutLine(object sender, RDTools.NewSocketManager.ClientEventArgs e)
        {

        }

        //添加语音并发送内容到LED，由于LED超时可能过长采用多线程处理 add by:tzw@2016.7.3
        private void Work(object obj)
        {
            //0      @1(0                       1        2              3            4                5                    6                     7                       8          9
            //LED屏ID@rowID(LED行号/安卓屏幕ID)|Name|Departments(科室)|Office(诊室)|Doctor(医生名称)|SoundCardID(声卡ID)|visitNumber(就诊人号码)|visitName(就诊人姓名)|waitNumber(等候人号码)|waitName(等候人姓名)
            MessageEventArgs messageEventArgs = (MessageEventArgs)obj;
            WriteLog("内容：", messageEventArgs.Message);
            try
            {
                string[] iCardNum = messageEventArgs.Message.Split('@');
                if (iCardNum[0] == "呼叫")
                {
                    //市妇保院叫号，直接呼叫，语音服务器
                    if (repeatCallTimes <= 0)
                    {
                        repeatCallTimes = 1;
                    }
                    lock (messageSender)
                    {
                        for (int i = 0; i < repeatCallTimes; i++)
                        {
                            if (voiceMF != null && VoiceSystem == 2)
                                voiceMF.TxtToVoice(VoiceSpeed, iCardNum[1]);//微软TTS转语音
                            if (VoiceSystem == 1)
                                messageSender.Send(0, iCardNum[1]);//捷通TTS
                        }
                    }
                }
                else
                {
                    //内江叫号接口调用
                    //AndroidScreent
                    string[] message = iCardNum[1].Split('|');
                    if (message.Length < 10)
                    {
                        WriteLog("错误:", "↑数据不完整：不予叫号");
                        return;
                    }

                    messageSender.UpdateQueue(iCardNum);//更新安卓屏内容显示

                    //请!QueueNumber+Name到Office就诊!
                    string sendText = "请!" + message[1] + "到" + message[2] + "," + message[3];
                    //发送声音
                    messageSender.Send(Convert.ToInt32(message[5]), sendText);

                    //没有LED屏幕ID,LED屏不发送内容
                    if (string.IsNullOrEmpty(iCardNum[0].Trim()))
                    {
                        File.AppendAllText("错误:", "↑无LED屏ID：不发送LED内容");
                        return;
                    }
                    //VoiceSystem
                    //在连接屏幕发送内容时候禁止其他客户端连接发送内容,否则导致接口内存溢出程序崩溃
                    lock (_screenConnect)
                    {
                        PDTools.TTSInterface screenMessage = new PDTools.TTSInterface();
                        screenMessage.sendMessage(message, 1, Convert.ToInt32(message[0]) - 1, Convert.ToInt32(iCardNum[0]));
                    }
                }
            }
            catch
            {
                WriteLog("程序异常", messageEventArgs.Message);
            }
        }

        private void WriteLog(string title, string txt)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\MSG.log";
            File.AppendAllText(path, "\r\n" + title + DateTime.Now + "\r\n数据：" + txt, Encoding.UTF8);//写入内容 // 根据路径出内容
        }

        PDTools.MainFrm voiceMF = null;
        private void _listener_ReceivedMessage(object sender, MessageEventArgs e)
        {
            //叫号功能 add by:tzw@2016.7.3
            if (VoiceSystem > 0)
            {
                if (voiceMF == null && VoiceSystem == 3)
                    voiceMF = new PDTools.MainFrm();

                _thread = new Thread(new ParameterizedThreadStart(Work));
                _thread.IsBackground = true;
                _thread.Start(e);

                //_thread.Join(500);
                //_thread.Abort();
            }

            //string[] message = e.Message.Split('@');
            //string[] users = message[1].Split('|');

            //switch (message[0])
            //{
            //    case "上线":

            //        if (lst_User.Count(a => a.Ip == users[6]) > 0)
            //        {
            //            lst_User.Remove(lst_User.FirstOrDefault());
            //        }

            //        lst_User.Add(new UserCls
            //        {
            //            UserID = users[0],
            //            ComputerName = users[1],
            //            UserCode = users[2],
            //            UserName = users[3],
            //            DepartmentID = users[4],
            //            DepartmentName = users[5],
            //            Ip = users[6],
            //            OperatorWorkkind = users[7],
            //            LoginTime = DateTime.Now
            //        });
            //        gridControl1.DataSource = null;
            //        gridControl1.DataSource = lst_User;
            //        break;
            //    case "下线":
            //        for (int i = lst_User.Count - 1; i > -1; i--)
            //        {
            //            if (lst_User[i].Ip == message[1])
            //            {
            //                lst_User.RemoveAt(i);
            //            }
            //        }

            //        gridControl1.DataSource = null;
            //        gridControl1.DataSource = lst_User;
            //        break;
            //}
        }

        private void btnStartServer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                IChannel[] channels = ChannelServices.RegisteredChannels;

                if (channels.Length == 0)
                {
                    TcpChannel channel = new TcpChannel(Convert.ToInt32(port));

                    ChannelServices.RegisterChannel(channel, false);
                    Assembly asm = Assembly.LoadFrom(Application.StartupPath + "//RD.Proxy.dll");
                    foreach (System.Type t in asm.GetExportedTypes())
                    {
                        if (t.MemberType == MemberTypes.TypeInfo)
                        {
                            //try
                            //{
                            //    MarshalByRefObject serviceObj = System.Activator.CreateInstance(t) as MarshalByRefObject;
                            //    RemotingServices.Marshal(serviceObj, t.Name);
                            //}
                            //catch(Exception ex)
                            //{
                            //    throw new Exception(t.Name);
                            //}                        
                            RemotingConfiguration.RegisterWellKnownServiceType(t, t.Name, WellKnownObjectMode.SingleCall);
                        }
                    }
                }
                else
                {
                    foreach (IChannel eachChannel in channels)
                    {
                        if (eachChannel.ChannelName == "tcp")
                        {
                            tcpChannel = (TcpChannel)eachChannel;
                            tcpChannel.StartListening(null);
                        }
                    }
                }

                btnStartServer.Enabled = false;
                btnStopServer.Enabled = true;
                notifyIcon1.Icon = (System.Drawing.Icon)Resources.play;
            }
            catch (Exception ex)
            {
                RDMessage.MsgError(ex.Message);
            }
        }

        private void btnStopServer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IChannel[] channels = ChannelServices.RegisteredChannels;
            foreach (IChannel eachChannel in channels)
            {
                if (eachChannel.ChannelName == "tcp")
                {
                    tcpChannel = (TcpChannel)eachChannel;
                    tcpChannel.StopListening(null);
                    ChannelServices.UnregisterChannel(tcpChannel);
                }
            }

            btnStartServer.Enabled = true;
            btnStopServer.Enabled = false;
            notifyIcon1.Icon = (System.Drawing.Icon)Resources.rec;
        }

        #region Click
        private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            if (this.Visible == false)
                this.Visible = true;
        }

        private void 打开控制中心ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1_DoubleClick(null, null);

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ChannelServices.RegisteredChannels.Length == 0)
                    this.notifyIcon1.ShowBalloonTip(1000, "系统配置信息(已关闭)", this.notifyIcon1.Text, ToolTipIcon.Info);
                else
                    this.notifyIcon1.ShowBalloonTip(1000, "系统配置信息", this.notifyIcon1.Text, ToolTipIcon.Info);
            }
        }
        #endregion

        #region 设置数据库
        private void btnSetConn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmConManage frm = new frmConManage();
            frm.ShowDialog();
            ShowIconInfo();
        }

        private void 数据库设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSetConn_ItemClick(null, null);
        }
        #endregion

        #region 注册
        private void btnRegist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormRegisterFile frm = new FormRegisterFile();
            frm.ShowDialog();
        }

        private void 注册RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRegist_ItemClick(null, null);
        }
        #endregion

        #region 退出
        private void exitApp()
        {
            if (RDMessage.MsgInfo("确实要退出系统吗？", false))
            {
                exit = true;
                Application.Exit();
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exit)
            {
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                if (_listener != null)
                {
                    _listener.ListenerRun = false;
                    _listener.Stop();
                }
                notifyIcon1.Dispose();
            }
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            exitApp();
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exitApp();
        }
        #endregion

        string myKey;
        private bool ReadEyeRegister(ref string strMessage)
        {
            try
            {
                EncryptAndDecrypt ed = new EncryptAndDecrypt();

                string stringin = RegEdit.Query("key");
                myKey = ed.Decrypt(stringin);
                string[] mygets = myKey.Split('|');

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

        private void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmOtherConManage frm = new frmOtherConManage();
            frm.ShowDialog();
        }

        //自动更改数据库表结构  added by cyc 2016.3.9
        private void tablealter()
        {
            //QuerySolutionProxy querySolutionFacade = new QuerySolutionProxy();
            //string sql;
            //DataSet ds = new DataSet();
            ////新增加药品字典BaseMed_Codex的藏文列
            //sql = "select * from syscolumns where id=(select id from sysobjects where name ='BaseMed_CodexDetail') and name='ZWM'";
            //ds = querySolutionFacade.ExecCustomQuery(sql);
            //if (ds.Tables[0].Rows.Count < 1)
            //{
            //    sql = "alter table BaseMed_CodexDetail add ZWM NVARCHAR (100) NULL";
            //    try
            //    {
            //        querySolutionFacade.ExecCustomQuery(sql);

            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message);
            //    }

            //}
            ////新增加药品字典BaseMed_Codex的藏文列输入码列
            //sql = "select * from syscolumns where id=(select id from sysobjects where name ='BaseMed_CodexDetail') and name='ZWSRM'";
            //ds = querySolutionFacade.ExecCustomQuery(sql);
            //if (ds.Tables[0].Rows.Count < 1)
            //{
            //    sql = "alter table BaseMed_CodexDetail add ZWSRM VARCHAR (20) NULL";
            //    try
            //    {
            //        querySolutionFacade.ExecCustomQuery(sql);

            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message);
            //    }

            //}
            //测试
            //string zwm = "दवा का ";
            //sql = "update BaseMed_CodexDetail set zwm = :zwm where LeechdomNo = '00000003'";
            //IDictionary<string, object> _IDictionary = new Dictionary<string, object>();
            //_IDictionary.Add(":zwm",zwm);
            //querySolutionFacade.ExecCustomQuery(sql,_IDictionary);

        }
    }
}
