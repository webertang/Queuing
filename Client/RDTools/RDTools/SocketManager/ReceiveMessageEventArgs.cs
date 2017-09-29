using System;

namespace SystemFramework.SocketManager
{
    /// <summary>
    /// 接受消息事件参数类
    /// </summary>
    public class ReceiveMessageEventArgs : EventArgs
    {
        public string[] ParaItems { get; set; }
        public string ClassFullName { get; set; }
        public string MessageContent { get; set; }

        public ReceiveMessageEventArgs(string classFullName, string[] paraItems, string messageContent)
        {
            ClassFullName = classFullName;
            ParaItems = paraItems;
            MessageContent = messageContent;
        }
    }
}
