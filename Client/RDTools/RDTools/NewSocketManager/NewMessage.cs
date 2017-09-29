using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.NewSocketManager
{
    public class NewMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        private MessageTypeEnum messageType;

        public MessageTypeEnum MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }
        /// <summary>
        /// 终端机器类型
        /// </summary>
        private ComputerEnum computer;

        public ComputerEnum Computer
        {
            get { return computer; }
            set { computer = value; }
        }
        /// <summary>
        /// 发送端IP
        /// </summary>
        private string senderIp;

        public string SenderIp
        {
            get { return senderIp; }
            set { senderIp = value; }
        }
        /// <summary>
        /// 发送端端口
        /// </summary>
        private int senderPort;

        public int SenderPort
        {
            get { return senderPort; }
            set { senderPort = value; }
        }
        /// <summary>
        /// 接收端IP
        /// </summary>
        private string receiverIp;

        public string ReceiverIp
        {
            get { return receiverIp; }
            set { receiverIp = value; }
        }
        /// <summary>
        /// 接收端端口
        /// </summary>
        private int receiverPort;

        public int ReceiverPort
        {
            get { return receiverPort; }
            set { receiverPort = value; }
        }
        /// <summary>
        /// 数据类型
        /// </summary>
        private IList<string> dataType;

        public IList<string> DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }
        /// <summary>
        /// 数据正文
        /// </summary>
        private IList<string> content;

        public IList<string> Content
        {
            get { return content; }
            set { content = value; }
        }


        /// <summary>
        /// 秒数
        /// </summary>
        private int second;

        public int Second
        {
            get { return second; }
            set { second = value; }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        private DateTime sendTime;

        public DateTime SendTime
        {
            get { return sendTime; }
            set { sendTime = value; }
        }

        private string officeId;

        public string OfficeId
        {
            get { return officeId; }
            set { officeId = value; }
        }

        private string operatorId;

        public string OperatorId
        {
            get { return operatorId; }
            set { operatorId = value; }
        }
    }
}
