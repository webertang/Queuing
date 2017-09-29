using System;

namespace RDTools.SocketManager
{
    /// <summary>
    /// 消息事件参数类
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        private string _message;

        public MessageEventArgs(string message)
        {
            _message = message;
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message
        {
            get { return _message; }
        }
    }
}
