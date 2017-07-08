using RD.Proxy.Common;
using RDTools.Voice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using WaveLib;

namespace PDTools.SocketManager
{
    public delegate bool EqualsComparer<T>(T x, T y);
    public class Compare<T> : IEqualityComparer<T>
    {
        private EqualsComparer<T> _equalsComparer;

        public Compare(EqualsComparer<T> equalsComparer)
        {
            this._equalsComparer = equalsComparer;
        }

        public bool Equals(T x, T y)
        {
            if (null != this._equalsComparer)
                return this._equalsComparer(x, y);
            else
                return false;
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

    /// <summary>
    /// 消息类
    /// </summary>
    public class MessageSender
    {
        private Thread _thread;

        //启动微软TTS转语音叫号
        public void Start()
        {
            _thread = new Thread(VoiceWork);
            _thread.IsBackground = true;
            _thread.Start();
        }
        //追加队列消息
        public void Send(int ID, string Msg)
        {
            lock (VoiceQueue.syncRoot)
            {
                VoiceQueue.queueMessage.Add(new NewRecMessage(ID, Msg));
            }
        }


        #region 语音叫号不指定声卡呼叫
        //捷通化声TTS转语音
        public void StartSpeak()
        {
            _thread = new Thread(SpeakWork);
            _thread.IsBackground = true;
            _thread.Start();
        }

        //语音队列查询
        private void SpeakWork()
        {
            //Voice voice = new Voice();
            while (true)
            {
                if (VoiceQueue.queueMessage.Count() > 0)
                {
                    Voice voice = new Voice();
                    string text = VoiceQueue.queueMessage[0].MsgText.Replace(" ", string.Empty);
                    voice.PlayText(text);
                    voice.EndJtts();
                    System.Threading.Thread.Sleep((int)(text.Length * 280));
                    
                    //if (voice == null) voice = new Voice();

                    //if (voice.jTTS_GetStatus() == Jtts.STATUS_READING)
                    //    continue;
                    //string text = VoiceQueue.queueMessage[0].MsgText.Replace(" ", string.Empty);
                    //voice.PlayText(text);
                    //删除已呼叫内容
                    lock (VoiceQueue.syncRoot)
                    {
                        VoiceQueue.queueMessage.RemoveAt(0);
                    }
                }
                Thread.Sleep(200);
            }
        }

        #endregion

        /// <summary>
        /// 更新数据库队列显示信息
        /// </summary>
        public void UpdateQueue(String[] message)
        {
            //0    1   2      @  0    1               2         3            4      5              6         7            8          9
            //IP1|IP2|IP3|……@rowID|Name|Departments(科室)|OfficeId(诊室)|Doctor|SoundCardID|visitNumber|visitPatient|waitNumber|waitpatient
            string iCardNum = message[0];
            string[] data = message[1].Split('|');

            String visitNumber = data[6], visitPatient = data[7], waitnumber = data[8], waitpatient = data[9],
                    officeId = data[3].Replace("诊室", ""), department = data[2], screenId = message[0], Doctor = data[4];

            QuerySolutionProxy querySolutionFacade = new QuerySolutionProxy();
            string sql = "update pd_display set brxm='" + visitPatient + "', pdhm='" + visitNumber +
                                            "', waitpatient='" + waitpatient + "', waitnumber='" + waitnumber +
                                            "', ksmc='" + department + "', ysmc='" + Doctor +
                                    "'  where  zsdm = '" + officeId + "' and pmid = '" + screenId + "'";

            try
            {
                querySolutionFacade.ExecCustomQuery(sql);
            }
            catch (Exception)
            {

            }
        }

        //发送声音
        private void sendVoice(int num, string msg)
        {
            NewRecMessage NRMsg = new NewRecMessage(num, msg);
            _thread = new Thread(new ParameterizedThreadStart(Work));
            _thread.IsBackground = true;
            _thread.Start(NRMsg);
        }
        //播放声音调用
        private void Work(object obj)
        {
            NewRecMessage NRMsg = new NewRecMessage();
            NRMsg = (NewRecMessage)obj;
            WaveMain wm = new WaveMain(NRMsg.MsgText, NRMsg.MsgID);
            wm.Play();
        }

        //语音队列查询
        private void VoiceWork()
        {
            while (true)
            {
                //使用声卡ID进行分组实现多个声卡同时呼叫
                List<NewRecMessage> delegateList = VoiceQueue.queueMessage.Distinct(new Compare<NewRecMessage>(
                    delegate(NewRecMessage x, NewRecMessage y)
                    {
                        if (null == x || null == y) return false;
                        return x.MsgID == y.MsgID;
                    })).ToList();

                for (int i = 0; i < delegateList.Count(); i++)
                {
                    if (VoiceQueue.queueMessage.Count() > 0)
                    {
                        string vFileName = @"" + VoiceQueue.queueMessage[VoiceQueue.queueMessage.IndexOf(delegateList[i])].MsgID + ".wav";
                        if (!File.Exists(vFileName))//没有文件
                        {
                            //呼叫
                            sendVoice(VoiceQueue.queueMessage[VoiceQueue.queueMessage.IndexOf(delegateList[i])].MsgID,
                                      VoiceQueue.queueMessage[VoiceQueue.queueMessage.IndexOf(delegateList[i])].MsgText);
                            //删除已呼叫内容
                            lock (VoiceQueue.syncRoot)
                            {
                                VoiceQueue.queueMessage.RemoveAt(VoiceQueue.queueMessage.IndexOf(delegateList[i]));
                            }
                        }
                        else
                        {
                            if (File.Exists(@"" + VoiceQueue.queueMessage[VoiceQueue.queueMessage.IndexOf(delegateList[i])].MsgID + ".wav"))
                            {
                                try
                                {
                                    //如果存在则删除
                                    File.Delete(@"" + VoiceQueue.queueMessage[VoiceQueue.queueMessage.IndexOf(delegateList[i])].MsgID + ".wav");
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                    //Thread.Sleep(500);
                }
                Thread.Sleep(500);
            }
        }


    }
}
