using System.Collections.Generic;

namespace PDTools.SocketManager
{
    //语音队列
    public static class VoiceQueue
    {
        public static object syncRoot = new object();
        public static List<NewRecMessage> queueMessage = new List<NewRecMessage>();
    }
    //消息内容
    public class NewRecMessage
    {
        //消息文本
        public NewRecMessage()
        {
        }
        //消息文本
        public NewRecMessage(int ID, string Text)
        {
            this.msgID = ID;
            this.msgText = Text;
        }
        //屏幕类型
        private int screenType;
        public int ScreenType
        {
            get { return screenType; }
            set { screenType = value; }
        }
        //行号
        private int rowNum;
        public int RowNum
        {
            get { return rowNum; }
            set { rowNum = value; }
        }
        //LED卡地址
        private int iCardNum;
        public int ICardNum
        {
            get { return iCardNum; }
            set { iCardNum = value; }
        }
        //消息列
        private string[] msgList;
        public string[] MsgList
        {
            get { return msgList; }
            set { msgList = value; }
        }

        private int msgID;
        /// <summary>
        /// 消息ID
        /// </summary>
        public int MsgID
        {
            get { return msgID; }
            set { msgID = value; }
        }

        private string msgText;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MsgText
        {
            get { return msgText; }
            set { msgText = value; }
        }
    }
}
