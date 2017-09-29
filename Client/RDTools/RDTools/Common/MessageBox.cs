using System;
using System.Windows.Forms;

namespace RDTools.Common
{
	public class RDMessage
	{
        
		public static void MsgInfo(string Msg)
		{

            MessageBox.Show(ChangeInhosID(Msg), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);   
		}

		public static void MsgStop(string Msg)
		{
            MessageBox.Show(ChangeInhosID(Msg), "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);   
		}

		public static void MsgError(string Msg)
		{
            MessageBox.Show(ChangeInhosID(Msg), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);   
		}

		public static bool MsgInfo(string Msg,bool Default)
		{
			if(Default)
			{
                if (MessageBox.Show(ChangeInhosID(Msg), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
                if (MessageBox.Show(ChangeInhosID(Msg), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

        private static string ChangeInhosID(string Msg)
        {
            try
            {
                string InhosID = SystemInfo.SystemConfigs["住院号显示为"].DefaultValue;
                if (Msg.Contains("住院号") && InhosID != "")
                    Msg = Msg.Replace("住院号", InhosID);
            }
            catch { }
            return Msg;
        }
	}
}
