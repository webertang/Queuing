using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace SystemFramework.SocketManager
{
    /// <summary>
    /// 历史消息
    /// </summary>
    public partial class FormHistoryMessage : Form
    {
        public FormHistoryMessage()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            listMessage.Items.Clear();

            string folderPath = Application.StartupPath + @"\xmlMessage";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = folderPath + @"\xmlMessage" + dateQuery.Value.ToString("yyyyMMdd") + ".xml";
            if (File.Exists(filePath))
            {
                XmlTextReader xmlTextReader = new XmlTextReader(filePath);
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName == "content")
                    {
                        listMessage.Items.Add(xmlTextReader.ReadString());
                    }
                }

                xmlTextReader.Close();
            }
        }
    }
}
