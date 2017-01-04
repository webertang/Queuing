using System;
using System.Windows.Forms;
using System.Linq;
using RD.Data;
using RDTools.Common;
using System.Collections.Generic;

namespace RD.Service
{
    public partial class frmOtherConManage : DevExpress.XtraEditors.XtraForm
    {
        EncryptAndDecrypt ed = new EncryptAndDecrypt();
        string DataBaseType;
        List<TypeList> Lst;
        class TypeList
        {
            public int SelIndex { get; set; }
            public string TypeName { get; set; }
            public DatabaseType DatabaseType 
            {
                get 
                {
                    switch (DatabaseStr.ToLower())
                    {
                        default:
                        case "sqlserver":
                            return DatabaseType.MSSQLServer;
                        case "oracle":
                            return DatabaseType.Oracle;
                    }
                }
            }
            public string DatabaseStr { get; set; }
            public string SqlConnection { get; set; }
            public string DatabaseStrName { get; set; }
            public string SqlConnectioName { get; set; }
        }

        public frmOtherConManage()
        {
            InitializeComponent();
            FillType();
            comboBoxEdit1.SelectedIndex = 0;
        }

        public void FillType()
        {
            Lst = new List<TypeList>();
            Lst.Add(new TypeList
            {
                SelIndex = 0,
                TypeName = "手术麻醉系统视图",
                DatabaseStr = AppConfig.GetAppSetting("DataBaseTypeOris"),
                SqlConnection = AppConfig.GetAppSetting("SqlConnectionOris"),
                DatabaseStrName = "DataBaseTypeOris",
                SqlConnectioName = "SqlConnectionOris"
            });

     
            PersistenceProperty.OtherConnectionStringList.Clear();
            PersistenceProperty.OtherDatabaseType.Clear();
            foreach (TypeList tl in Lst.OrderBy(a => a.SelIndex))
            {
                PersistenceProperty.OtherConnectionStringList.Add(ed.Decrypt(tl.SqlConnection).TrimEnd('\0'));
                PersistenceProperty.OtherDatabaseType.Add(tl.DatabaseType);
            }
        }

        private void frmConfigManage_Load(object sender, System.EventArgs e)
        {
            ReadConfigInfo();
        }

        public void ReadConfigInfo()
        {
            try
            {
                if (comboBoxEdit1.SelectedItem.ToString() == string.Empty)
                    return;
                string SqlConnection = ((TypeList)comboBoxEdit1.Tag).SqlConnection;
                DataBaseType = ((TypeList)comboBoxEdit1.Tag).DatabaseStr;
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

                        break;
                }
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

            if (cmbType.SelectedIndex == 0)
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

                    break;
            }
            try
            {
                string encryMsg = ed.Encrypt(connText);
                AppConfig.WriteAppSetting(Application.ExecutablePath, ((TypeList)comboBoxEdit1.Tag).SqlConnectioName, encryMsg);
                AppConfig.WriteAppSetting(Application.ExecutablePath, ((TypeList)comboBoxEdit1.Tag).DatabaseStrName, DataBaseType);
                FillType();
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
                //PersistenceProperty.DatabaseType = DatabaseType.Oracle;
                //PersistenceProperty.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.118)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=test;Password=test;";
                RD.Data.DataAccessFactory.instance.CreateDataAccess(((TypeList)comboBoxEdit1.Tag).SelIndex).Open();//
                RD.Data.DataAccessFactory.instance.CreateDataAccess(((TypeList)comboBoxEdit1.Tag).SelIndex).Close();
                RDMessage.MsgInfo("数据库服务器连接成功!");
            }
            catch (Exception err)
            {
                RDMessage.MsgError("数据库服务器连接失败!" + err.Message);
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit1.Tag = Lst.FirstOrDefault(a => a.TypeName == comboBoxEdit1.SelectedItem.ToString());
            ReadConfigInfo();
        }
    }
}
