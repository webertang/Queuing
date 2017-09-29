using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using RDTools.Voice;

namespace SystemFramework.SocketManager
{
    /// <summary>
    /// 解析处理接收到的消息类
    /// </summary>
    public class ReceiveMessage
    {
        public ReceiveMessage()
        {
            System.Action action = null;
            this._messageQueue = new Queue<string>();
            if (action == null)
            {
                action = delegate
                {
                    while (true)
                    {
                        if (this._messageQueue.Count > 0)
                        {
                            Voice voice = new Voice();
                            string text = this._messageQueue.Dequeue().Replace(" ", string.Empty);
                            voice.PlayText(text);
                            voice.EndJtts();
                            Thread.Sleep((int)(text.Length * 280));
                        }
                        Thread.Sleep(200);
                    }
                };
            }
            action.BeginInvoke(null, null);
        }

        private volatile Queue<string> _messageQueue;
        private FormMessage _formMessage;

        public delegate void Action(object sender, ReceiveMessageEventArgs e);
        public event Action ItemsDClick;

        protected virtual void OnItemsDClick(object sender, ReceiveMessageEventArgs e)
        {
            if (ItemsDClick != null)
            {
                ItemsDClick(sender, e);
            }
        }

        public void ShowMessageInScreen(string message, Action<string> action)
        {
            //if (message.IndexOf("大屏显示@") >= 0)
            //{
                message = message.Split('@')[1];
                action(message);
                _messageQueue.Enqueue(message);
            //}
        }

        /// <summary>
        /// 显示消息窗体
        /// </summary>
        /// <param name="message">新消息</param>
        public void ShowMessageWindow(string message)
        {
            if (message == "确认是否在线" || message.IndexOf("大屏显示@") >= 0)
            {
                return;
            }

            int? time = null;
            if (message.IndexOf("关机通知") == 0)
            {
                time = Convert.ToInt32(message.Split('@')[1]);
                message = "服务器将在" + time + "秒后关机或重启，请尽快保存！";
            }

            int type = 0;
            if (message.IndexOf("服务器消息：@C") == 0 || message.IndexOf("服务器消息：@Z") == 0
                || message.IndexOf("服务器消息：@CZ") == 0)
            {
                string[] mgs = message.Split('-');

                try
                {
                    time = mgs.Length == 1 ? 6 : Convert.ToInt32(mgs[1]);
                }
                catch
                {
                    time = 6;
                }

                switch (mgs[0])
                {
                    case "服务器消息：@C":
                        type = 1;
                        message = "注销！";
                        break;
                    case "服务器消息：@Z":
                        type = 2;
                        message = "重启！";
                        break;
                    case "服务器消息：@CZ":
                        type = 3;
                        message = "关机！";
                        break;
                }

                message = "计算机将在" + time + "秒后" + message;
            }

            if (message.IndexOf("语音@") >= 0)
            {
                _messageQueue.Enqueue(message.Split('@')[1]);
            }

            FormCollection formList = Application.OpenForms;
            IEnumerable<FormMessage> formMessages = formList.OfType<FormMessage>();

            if (formMessages.Count() != 0)
            {
                _formMessage = formMessages.First();
            }
            else
            {
                _formMessage = new FormMessage();
                _formMessage.listMessage.DoubleClick += new EventHandler(listMessage_DoubleClick);
            }

            if (message.IndexOf("语音@") >= 0)
            {
                _formMessage.AddMessage("呼叫：" + message.Split('@')[1], "0");
            }
            else
            {
                string[] messages = message.Split('|');
                _formMessage.AddMessage(messages[0], messages.Length > 1 ? messages[1] : "0");
            }

            if (!_formMessage.Visible)
            {
                _formMessage.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - _formMessage.Width,
                   Screen.PrimaryScreen.WorkingArea.Height - _formMessage.Height);
            }

            _formMessage.Show();

            if (time != null)
            {
                if (type == 0)
                {
                    _formMessage.OpenCloesTimer(Convert.ToInt32(time));
                }
                else
                {
                    _formMessage.OpenCloesTimer(Convert.ToInt32(time), type);
                }
            }
        }

        private void listMessage_DoubleClick(object sender, EventArgs e)
        {
            if (_formMessage.listMessage.SelectedItems.Count != 0)
            {
                if (!(_formMessage.listMessage.Text.IndexOf("服务器将在") > -1 ||
                    _formMessage.listMessage.Text.IndexOf("服务器消息") > -1 ||
                    _formMessage.listMessage.Text.IndexOf("计算机将在") > -1 || _formMessage.listMessage.Text.IndexOf("呼叫：") > -1))
                {
                    string[] messageInfo = _formMessage.listMessage.SelectedValue.ToString().Split(':');

                    _formMessage.Hide();

                    OnItemsDClick(this, new ReceiveMessageEventArgs(messageInfo[0], messageInfo[1].Split('#'), _formMessage.listMessage.Text));
                }
            }
        }
    }
}
