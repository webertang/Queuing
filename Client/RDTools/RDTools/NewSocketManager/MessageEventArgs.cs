using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.NewSocketManager
{
    public class MessageEventArgs : EventArgs
    {
        public NewMessage Message { get; private set; }

        public MessageEventArgs(NewMessage message)
        {
            Message = message;
        }
    }
}
