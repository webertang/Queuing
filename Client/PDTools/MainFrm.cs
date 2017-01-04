using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using DotNetSpeech;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using PDTools.SocketManager;

namespace PDTools
{

    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }
        List<string> listStrRead = new List<string>();
        List<string> listStrTTS = new List<string>();
        int strLastLength;
        SpeechVoiceSpeakFlags SVPF = new SpeechVoiceSpeakFlags();
        SpVoice Voice = new SpVoice();
        int fileName = 1;    //mp3文件的名字(以数字命名）
        int strTTSMax = 20000;   //TTS的每个文件的最大字数
        string FloderName;  //随机文件夹名称
        private static string pathBase = Application.StartupPath;
        private Thread _thread;

        public void TxtToVoice(int speed, string spkcontent)
        {
            status('1');
            Voice.Rate = speed;
            listStrRead = WorkString(spkcontent, 10000, listStrRead);
            ControlRead(listStrRead);
        }

        public List<string> WorkString(string str, int strMaxSize, List<string> list)
        {

            int i = 0;
            int count = 0;   //循环的次数
            strLastLength = str.Length;   //字符串剩余的长度
            while (strLastLength > strMaxSize)
            {

                string temp = str.Substring(i, strMaxSize);
                list.Add(temp);
                i = i + strMaxSize;
                strLastLength = strLastLength - strMaxSize;
                count++;
            }
            if (i == 0)
            {
                list.Add(str);
                return list;
            }
            else
            {
                string temp = str.Substring(count * strMaxSize, strLastLength);
                list.Add(temp);
                return list;
            }

        }

        public void ControlRead(List<string> list)
        {
            try
            {
                if (list.Count > 0)
                {
                    if (!bgwRead.IsBusy)
                    {
                        bgwRead.RunWorkerAsync(list.First());
                    }
                    else
                    {
                        //bgwMessage.RunWorkerAsync("程序正忙呢，稍等一会");
                    }
                }
                else
                {
                    bgwMessage.RunWorkerAsync("没有内容怎么读啊？");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwRead_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Voice.Speak(e.Argument.ToString(), SVPF);
                Voice.WaitUntilDone(System.Threading.Timeout.Infinite);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox1.Text.Trim().Length > 0)
                {
                    //listStrRead = WorkString(richTextBox1.Text, 10000, listStrRead);
                    //ControlRead(listStrRead);

                    TxtToVoice(mediaSlider1.Value, richTextBox1.Text);
                }
                else
                {
                    //bgwMessage.RunWorkerAsync("文本框中没有内容，不能读");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            cmbMp3Size.SelectedIndex = 2;
        }

        private void btnTTS_Click(object sender, EventArgs e)
        {
            try
            {
                if (listStrTTS.Count > 0)
                {
                    listStrTTS.Clear();
                }
                if (richTextBox1.Text.Trim().Length > 0)
                {
                    if (!bgwTTS1.IsBusy)
                    {
                        listStrTTS = WorkString(richTextBox1.Text, strTTSMax, listStrTTS);
                        bgwTTS1.RunWorkerAsync(listStrTTS.First());
                    }
                    else
                    {
                        //bgwMessage.RunWorkerAsync("正在转中，不要重复按键");
                    }
                }
                else
                {
                    //bgwMessage.RunWorkerAsync("文本框中没有字，快点添加字");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void TxtToSpeech(string strTxt, string fileName)
        {
            try
            {
                SpeechStreamFileMode SSFM = SpeechStreamFileMode.SSFMCreateForWrite;
                SpFileStream SFS = new SpFileStream();
                //if (fileName == "1")
                //{
                //    FloderName = CreateFloderName();
                //    Directory.CreateDirectory(pathBase + @"\" + FloderName);
                //}

                SFS.Open(pathBase + @"\voice.wav", SSFM, false);
                Voice.AudioOutputStream = SFS;
                Voice.Speak(strTxt, SVPF);
                Voice.WaitUntilDone(System.Threading.Timeout.Infinite);
                SFS.Close();

            }
            catch (InvalidExpressionException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bgwTTS1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                TxtToSpeech(e.Argument.ToString(), fileName.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bgwRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (listStrRead.Count > 0)
                {
                    listStrRead.RemoveAt(0);
                    if (listStrRead.Count != 0)
                    {
                        status('1');
                        bgwRead.RunWorkerAsync(listStrRead.First());
                    }
                    else
                    {
                        status('0');
                        //bgwMessage.RunWorkerAsync("朗读完毕！请执行其他任务");
                        bgwRead.CancelAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwTTS1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                listStrTTS.RemoveAt(0);
                if (listStrTTS.Count > 0)
                {
                    fileName++;
                    bgwTTS1.RunWorkerAsync(listStrTTS.First());

                }
                else
                {
                    MessageBox.Show("转换完毕！请执行其他任务");
                    bgwTTS1.CancelAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string CreateFloderName()
        {
            for (int i = 1; i < 5; i++)
            {
                Random ram = new Random();
                int temp = ram.Next(0, 9);
                FloderName = FloderName + temp.ToString();
            }
            return FloderName;
        }

        private void mediaSlider1_Scrolled(object sender, EventArgs e)
        {
            Voice.Rate = mediaSlider1.Value;
        }
        bool arg = true;
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (arg)
            {
                Voice.Pause();
                btnPlay.Text = "继续";
                arg = false;
            }
            else
            {
                Voice.Resume();
                btnPlay.Text = "暂停";
                arg = true;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            lbMessage.Text = "字数:" + richTextBox1.Text.Trim().Length + "个";

        }

        private void bgwMessage_DoWork(object sender, DoWorkEventArgs e)
        {
            Voice.Speak(e.Argument.ToString(), SVPF);
        }

        private void cmbMp3Size_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbMp3Size.SelectedItem.ToString())
            {
                case "5000字":
                    strTTSMax = 5000;
                    break;
                case "10000字":
                    strTTSMax = 10000;
                    break;
                case "20000字":
                    strTTSMax = 20000;
                    break;
                case "50000字":
                    strTTSMax = 50000;
                    break;
                case "80000字":
                    strTTSMax = 80000;
                    break;

            }

        }



        private void myNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {

                ShowFormToolStripMenuItem.Visible = true;
                HideFormToolStripMenuItem1.Visible = false;
                this.Visible = false;
            }
            else
            {

                ShowFormToolStripMenuItem.Visible = false;
                HideFormToolStripMenuItem1.Visible = true;
                this.Visible = true;
            }
        }

        private void ShowFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            ShowFormToolStripMenuItem.Visible = false;
            HideFormToolStripMenuItem1.Visible = true;
        }

        private void HideFormToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            HideFormToolStripMenuItem1.Visible = false;
            ShowFormToolStripMenuItem.Visible = true;
        }

        private void ExitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                Clipboard.SetDataObject(richTextBox1.SelectedText);
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject idateoject = Clipboard.GetDataObject();
            if (idateoject.GetDataPresent(DataFormats.Text))
            {
                richTextBox1.Text += idateoject.GetData(DataFormats.Text) as string;
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText == "")
            {
                CopyToolStripMenuItem.Enabled = false;
            }
            else
            {
                CopyToolStripMenuItem.Enabled = true;
            }
        }
        //解决关闭窗体后notifyIcon延缓消失的问题
        protected override void OnClosing(CancelEventArgs e)
        {

            myNotifyIcon.Visible = false;
            base.OnClosing(e);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        internal void TxtToVoice(int p1, char p2)
        {
            throw new NotImplementedException();
        }

        public void status(char str)
        {
            FileInfo myFile = new FileInfo(@"play.ini");
            StreamWriter sw = myFile.CreateText();

            string[] strs = { "[option]", "status=" + str };
            foreach (var s in strs)
            {
                sw.WriteLine(s);
            }
            sw.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RDTools.Voice.Voice v = new RDTools.Voice.Voice();
            if (v.jTTS_GetStatus() == RDTools.Voice.Jtts.STATUS_READING)
            {
                MessageBox.Show("正在读");
                return;
            }

            v.PlayText("请001号张哎三到一诊室就诊");
        }

    }
}
