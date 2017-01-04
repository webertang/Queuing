using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using RDTools.Common;

namespace RD.Service
{
    public partial class FormRegisterFile : DevExpress.XtraEditors.XtraForm
    {           
        EncryptAndDecrypt ed = new EncryptAndDecrypt();

        public FormRegisterFile()
        {
            InitializeComponent();
        }

        private string GetDataBaseName
        {
            get
            {
                string endStr = string.Empty;
                try
                {
                    string SqlConnection = AppConfig.GetAppSetting("SqlConnection");
                    string DataBaseType = AppConfig.GetAppSetting("DataBaseType");
                    switch (DataBaseType.ToLower())
                    {
                        case "sqlserver":
                        default:
                            endStr = ed.Decrypt(SqlConnection);
                            string dealStr = "initial catalog=";
                            int len = dealStr.Length;
                            int dsIndex = endStr.IndexOf(dealStr);
                            endStr = endStr.Substring(dsIndex + len, endStr.IndexOf(";", dsIndex) - len - dsIndex);
                            break;
                        case "oracle":
                            endStr = ed.Decrypt(SqlConnection);
                            int index = endStr.IndexOf("SERVICE_NAME=") + "SERVICE_NAME=".Length;
                            endStr = endStr.Substring(index, endStr.IndexOf(")", index) - index);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    RDMessage.MsgError(ex.Message);
                }
                return endStr;
            }
        }

        private void FormRegisterFile_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            txtLocalName.Text = Dns.GetHostName();
            txtLocalIP.Text = RDManagementClass.GetHostIP(0).ToString();
            txtDataBaseName.Text = GetDataBaseName;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {    
            if (string.IsNullOrEmpty(txtName.Text))
            {
                RDMessage.MsgInfo("用户名不可为空！");
                txtName.Focus();
                return;
            }

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
            sb.Append(txtName.Text.Trim()).Append('|');
            sb.Append(txtLocalName.Text.Trim()).Append('|');
            sb.Append(txtLocalIP.Text.Trim()).Append('|');
            sb.Append(mac).Append('|');
            sb.Append(cpu).Append('|');
            sb.Append(hardid).Append('|');
            sb.Append(txtDataBaseName.Text.Trim());

            string fileContent = ed.Encrypt(sb.ToString());

            saveFile.FileName = txtName.Text.Trim();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFile.FileName, false, Encoding.UTF8))
                {
                    sw.Write(fileContent);
                }
                RDMessage.MsgInfo("申请秘钥已保存,请提交文件。");
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(openFile.FileName))
                    {
                        RDMessage.MsgInfo("没有找到注册文件！");
                        return;
                    }

                    StreamReader streamReader = new StreamReader(openFile.FileName, System.Text.Encoding.UTF8);
                    string content = streamReader.ReadToEnd();
                    streamReader.Close();

                    string[] contents = ed.Decrypt(content).Split('|');

                    if (contents.Length < 12 || contents[0] != "RDHIS")
                    {
                        RDMessage.MsgInfo("不合法的注册认证文件");
                        return;
                    }

                    if (contents[4] != RDManagementClass.GetMAC()&&
                        contents[5] != RDManagementClass.GetCPUID()&&
                        contents[6] != RDManagementClass.GetHardID())
                    {
                        RDMessage.MsgInfo("不合法的注册认证文件!");
                        return;
                    }

                    if (Convert.ToDateTime(contents[9]) < DateTime.Now)
                    {
                        RDMessage.MsgInfo("注册文件已过期,请重新注册！");
                        return;
                    }

                    RegEdit.RegisterOver("Key", content);
                    RDMessage.MsgInfo("恭喜,注册已生效！");

                    Close();
                }
            }
            catch (Exception ex)
            {
                RDMessage.MsgInfo(ex.Message);
            }
        }
    }
}
