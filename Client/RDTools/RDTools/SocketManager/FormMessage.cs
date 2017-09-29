using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SystemFramework.SocketManager
{
    /// <summary>
    /// 消息提示
    /// </summary>
    public partial class FormMessage : Form
    {
        public FormMessage()
        {
            InitializeComponent();
        }

        private int _closeTime = 0;
        private int _closeNeedTime = 0;
        private int _Special = 0;
        private List<MessageItem> items = new List<MessageItem>();

        private void FormMessage_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - Width,
               Screen.PrimaryScreen.WorkingArea.Height - Height);

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (this != null)
            {
                Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        public void AddMessage(string message, string id)
        {
            MessageItem item = new MessageItem();
            item.ID = id;
            item.Message = DateTime.Now.ToString("HH:mm") + " " + message;
            items.Insert(0, item);

            messageItemBindingSource.DataSource = null;
            messageItemBindingSource.DataSource = items;

            string folderPath = Application.StartupPath + @"\Sound";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = folderPath + @"\msg.wav";
            if (File.Exists(filePath))
            {
                //Common.PlaySound(filePath, IntPtr.Zero, 0x0001 | 0x00020000);
            }

            folderPath = Application.StartupPath + @"\xmlMessage";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            filePath = folderPath + @"\xmlMessage" + DateTime.Now.ToString("yyyyMMdd") + ".xml";
            if (!File.Exists(filePath))
            {
                XmlTextWriter xmlTextWriter = new XmlTextWriter(filePath, Encoding.UTF8);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.Indentation = 4;

                xmlTextWriter.WriteStartDocument();
                xmlTextWriter.WriteStartElement("xmlMessage");
                xmlTextWriter.WriteElementString("content",  DateTime.Now.ToString("HH:mm") + " " + message);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndDocument();
                xmlTextWriter.Close();
            }
            else
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                XmlNode xmlNode = xmlDocument.SelectSingleNode("xmlMessage");

                XmlElement xmlElement = xmlDocument.CreateElement("content");
                xmlElement.InnerText = DateTime.Now.ToString("HH:mm") + " " + message;

                xmlNode.PrependChild(xmlElement);
                xmlDocument.Save(filePath);
            }
        }

        public void OpenCloesTimer(int closeNeedTime)
        {
            _closeTime = 0;
            _closeNeedTime = closeNeedTime;
            timerClose.Start();
        }

        public void OpenCloesTimer(int closeNeedTime, int special)
        {
            _closeTime = 0;
            _closeNeedTime = closeNeedTime;
            _Special = special;
            timerClose.Start();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!Visible)
                {
                    Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - Width,
                   Screen.PrimaryScreen.WorkingArea.Height - Height);
                }
                Visible = true;
            }
        }

        private void labHistoryMessage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormHistoryMessage formHistoryMessage = new FormHistoryMessage();
            formHistoryMessage.ShowDialog(this);
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            _closeTime ++;

            if (_closeTime == _closeNeedTime)
            {
                timerClose.Stop();

                switch (_Special)
                {
                    case 0:
                        Application.Exit();
                        break;
                    //case 1:
                    //    Windows.Shutdown.WinShutdown.ExitWindows(Windows.Shutdown.RestartOptions.LogOff, false);
                    //    Application.Exit();
                    //    break;
                    //case 2:
                    //    Windows.Shutdown.WinShutdown.ExitWindows(Windows.Shutdown.RestartOptions.Reboot, false);
                    //    Application.Exit();
                    //    break;
                    //case 3:
                    //    Windows.Shutdown.WinShutdown.ExitWindows(Windows.Shutdown.RestartOptions.PowerOff, false);
                    //    Application.Exit();
                    //    break;
                }
            }
        }

        protected override void WndProc(ref   Message m)
        {
            if (m.Msg == 0x84)
            {
                //   分解当前鼠标的坐标   
                int nPosX = (int)m.LParam & 0xFFFF;
                int nPosY = (int)m.LParam >> 16;
                if (ClientRectangle.Contains(PointToClient(new Point(nPosX, nPosY))))
                {
                    m.Result = (IntPtr)2;
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
