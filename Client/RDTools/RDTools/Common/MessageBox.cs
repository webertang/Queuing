using System;
using System.Windows.Forms;

namespace RDTools.Common
{
	public class RDMessage
	{
        
		public static void MsgInfo(string Msg)
		{

            MessageBox.Show(ChangeInhosID(Msg), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);   
		}

		public static void MsgStop(string Msg)
		{
            MessageBox.Show(ChangeInhosID(Msg), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);   
		}

		public static void MsgError(string Msg)
		{
            MessageBox.Show(ChangeInhosID(Msg), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);   
		}

		public static bool MsgInfo(string Msg,bool Default)
		{
			if(Default)
			{
                if (MessageBox.Show(ChangeInhosID(Msg), "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
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
                if (MessageBox.Show(ChangeInhosID(Msg), "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
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
                string InhosID = SystemInfo.SystemConfigs["סԺ����ʾΪ"].DefaultValue;
                if (Msg.Contains("סԺ��") && InhosID != "")
                    Msg = Msg.Replace("סԺ��", InhosID);
            }
            catch { }
            return Msg;
        }
	}
}
