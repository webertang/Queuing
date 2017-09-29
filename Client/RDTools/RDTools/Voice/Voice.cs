using System.Text;

namespace RDTools.Voice
{
    /// <summary>
    /// 语音叫号类
    /// </summary>
    public class Voice
    {
        public Voice()
        {
            int iErr = Jtts.jTTS_Init(null, null);
            Jtts.JTTS_CONFIG config = new Jtts.JTTS_CONFIG();
            Jtts.jTTS_Get(out config);
            config.nCodePage = (ushort)Encoding.Default.CodePage;
            config.nVolume = 9;
            Jtts.jTTS_Set(ref config);
        }           
           

        public int PlayText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return Jtts.jTTS_Play(text, 0);
            }

            return Jtts.ERR_NONE;
        }

        public int StopPlay()
        {
            return Jtts.jTTS_Stop();
        }

        public int PausePlay()
        {
            return Jtts.jTTS_Pause();
        }

        public int Resume()
        {
            return Jtts.jTTS_Resume();
        }

        public int EndJtts()
        {
            return Jtts.jTTS_End();
        }

        public int jTTS_GetStatus()
        {
            return Jtts.jTTS_GetStatus();
        }
    }
}
