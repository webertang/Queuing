using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DotNetSpeech;
using System.Windows.Forms;

namespace WaveLib
{
    public class WaveMain
    {
        private WaveOutPlayer m_Player;
        private Stream m_AudioStream;
        private WaveFormat m_Format;
        private int _DevId;
        private int m_AudioSize;
        
        public int DevId
        {
            get { return _DevId; }
            set { _DevId = value; }
        }
        public WaveMain(string msg, int number)
        {
            CloseFile();
            try
            {
                DevId = number;

                //写入语音文件
                SpFileStream stream = new SpFileStream();
                stream.Open(number + ".wav", SpeechStreamFileMode.SSFMCreateForWrite, false);
                SpVoice voice = new SpVoice();
                voice.AudioOutputStream = stream;
                voice.Speak(msg);
                voice.WaitUntilDone(System.Threading.Timeout.Infinite);
                stream.Close();

                //加载语音文件
                WaveStream S = new WaveStream(number + ".wav");
                m_AudioSize = Convert.ToInt32(S.Length);
                if (S.Length <= 0)
                    throw new Exception("Invalid WAV file");
                m_Format = S.Format;
                if (m_Format.wFormatTag != (short)WaveFormats.Pcm && m_Format.wFormatTag != (short)WaveFormats.Float)
                    throw new Exception("Olny PCM files are supported");
                m_AudioStream = S;

                //文件加载到内存不会锁定文件
                //byte[] file = new byte[2000000];
                //file = File.ReadAllBytes("begin.wav");
                //MemoryStream ms = new MemoryStream();
                //WaveStream S = new WaveStream(ms);
                //m_AudioSize = Convert.ToInt32(ms.Length);
                //if (ms.Length <= 0)
                //    throw new Exception("Invalid WAV file");
                //if (S.Length <= 0)
                //{
                //    throw new Exception("无效的WAV文件.");//Invalid WAV file
                //}
                //m_Format = S.Format;
                //if (m_Format.wFormatTag != (short)WaveFormats.Pcm && m_Format.wFormatTag != (short)WaveFormats.Float)
                //    throw new Exception("Olny PCM files are supported");
                //m_AudioStream = S;

            }
            catch (Exception e)
            {
                CloseFile();
            }
        }


        private void Filler(IntPtr data, int size)
        {
            byte[] b = new byte[size];
            if (m_AudioStream != null)
            {
                int pos = 0;
                //循环加载流内容
                //while (pos < size)
                //{
                int toget = size - pos;
                int got = m_AudioStream.Read(b, pos, toget);
                if (got < toget)
                    m_AudioStream.Position = 0; // loop if the file ends
                pos += got;
                //}
            }
            else
            {
                for (int i = 0; i < b.Length; i++)
                    b[i] = 0;
            }
            System.Runtime.InteropServices.Marshal.Copy(b, 0, data, size);
        }
        public void Play()
        {
            Stop();
            if (m_AudioStream != null)
            {
                m_AudioStream.Position = 0;
                //m_Player = new WaveOutPlayer(DevId, m_Format, 16384, 3, new BufferFillEventHandler(Filler));
                m_Player = new WaveOutPlayer(DevId, m_Format, m_AudioSize, 3, new BufferFillEventHandler(Filler), m_AudioStream, DevId);
            }
        }

        public bool getFinished()
        {
            return m_Player.getFinished();
        }

        public void Stop()
        {

            if (m_Player != null)
                try
                {
                    m_Player.Dispose();
                }
                finally
                {
                    m_Player = null;
                }
        }
        public void CloseFile()
        {
            Stop();
            if (m_AudioStream != null)
                try
                {
                    m_AudioStream.Close();
                }
                finally
                {
                    m_AudioStream = null;
                }
        }
    }
}
