using System;
using System.Windows.Forms;
using RD.Data;
using RDTools.Common;

namespace RD.Service
{
    public partial class frmConManage : DevExpress.XtraEditors.XtraForm
    {
        EncryptAndDecrypt ed = new EncryptAndDecrypt();
        string DataBaseType;

        public frmConManage()
        {
            InitializeComponent();
        }

        private void frmConfigManage_Load(object sender, System.EventArgs e)
        {
            ReadConfigInfo();
        }

        public void ReadConfigInfo()
        {
            try
            {
                string SqlConnection = AppConfig.GetAppSetting("SqlConnection");
                DataBaseType = AppConfig.GetAppSetting("DataBaseType");
                string endStr = "";

                switch (DataBaseType.ToLower())
                {
                    case "sqlserver":
                        cmbType.SelectedIndex = 0;
                        endStr = ed.Decrypt(SqlConnection).TrimEnd('\0');
                        string[] conArray1 = endStr.Split(';', '=');

                        txtDBName.Text = conArray1[11];
                        txtUserName.Text = conArray1[3];
                        txtPassWord.Text = conArray1[5];
                        txtServerName.Text = conArray1[7];

                        PersistenceProperty.DatabaseType = DatabaseType.MSSQLServer;
                        break;
                    case "oracle":
                        cmbType.SelectedIndex = 1;
                        endStr = ed.Decrypt(SqlConnection);

                        int index = endStr.IndexOf("SERVICE_NAME=") + "SERVICE_NAME=".Length;
                        txtDBName.Text = endStr.Substring(index, endStr.IndexOf(")", index) - index);

                        index = endStr.IndexOf("User Id=") + "User Id=".Length; ;
                        txtUserName.Text = endStr.Substring(index, endStr.IndexOf(";", index) - index);

                        index = endStr.IndexOf("Password=") + "Password=".Length; ;
                        txtPassWord.Text = endStr.Substring(index, endStr.IndexOf(";", index) - index);

                        index = endStr.IndexOf("HOST=") + "HOST=".Length; ;
                        txtServerName.Text = endStr.Substring(index, endStr.IndexOf(")", index) - index);

                        string passWord = txtPassWord.Text.Trim();
                        string serverName = txtServerName.Text.Trim();
                        string userName = txtUserName.Text.Trim();
                        string dbName = txtDBName.Text.Trim();

                        endStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=" +
                        "(ADDRESS=(PROTOCOL=TCP)(HOST=" + serverName + ")(PORT=1521)))" +
                        "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + dbName + ")));" +
                        "User Id=" + userName + ";Password=" + passWord + ";";

                        PersistenceProperty.DatabaseType = DatabaseType.Oracle;
                        break;
                }
                PersistenceProperty.ConnectionString = endStr;
            }
            catch (Exception f)
            {
                RDMessage.MsgError(f.Message);
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            string connText = string.Empty;
            string userName = string.Empty;
            string passWord = string.Empty;
            string serverName = string.Empty;
            string dbName = string.Empty;

            if (cmbType.SelectedIndex ==0)
                DataBaseType = "SqlServer";
            else 
                DataBaseType = "Oracle";
            switch (DataBaseType)
            {
                case "SqlServer":
                    userName = txtUserName.Text.Trim();
                    passWord = txtPassWord.Text.Trim();
                    serverName = txtServerName.Text.Trim();
                    dbName = txtDBName.Text.Trim();

                    connText = "packet size=4096;user id="
                        + userName + ";password="
                        + passWord + ";data source="
                        + serverName + ";persist security info=False;initial catalog="
                        + dbName + ";Connect Timeout=30;Pooling=true";

                    PersistenceProperty.DatabaseType = DatabaseType.MSSQLServer;
                    break;
                case "Oracle":
                    passWord = txtPassWord.Text.Trim();
                    serverName = txtServerName.Text.Trim();
                    userName = txtUserName.Text.Trim();
                    dbName = txtDBName.Text.Trim();

                    connText = "Data Source=(DESCRIPTION=(ADDRESS_LIST=" +
                                "(ADDRESS=(PROTOCOL=TCP)(HOST=" + serverName + ")(PORT=1521)))" +
                                "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + dbName + ")));" +
                                "User Id=" + userName + ";Password=" + passWord + ";";        
                                
                    PersistenceProperty.DatabaseType = DatabaseType.Oracle;
                    break;
            }
            try
            {
                string encryMsg = ed.Encrypt(connText);
                PersistenceProperty.ConnectionString = connText;
                AppConfig.WriteAppSetting(Application.ExecutablePath, "SqlConnection", encryMsg);
                AppConfig.WriteAppSetting(Application.ExecutablePath, "DataBaseType", DataBaseType);
                RDMessage.MsgInfo("写配置文件成功！");
            }
            catch (Exception err)
            {
                RDMessage.MsgError("写配置文件错误：" + err.Message);
            }
        }

        private void btnTestConnect_Click(object sender, System.EventArgs e)
        {
            try
            {
                RD.Data.DataAccessFactory.instance.CreateDataAccess().Open();
                RD.Data.DataAccessFactory.instance.CreateDataAccess().Close();
                RDMessage.MsgInfo("数据库服务器连接成功!");
            }
            catch (Exception err)
            {
                RDMessage.MsgError("数据库服务器连接失败!" + err.Message);
            }
        }            
            
        
    }
}
