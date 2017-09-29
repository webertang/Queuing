using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace RDTools.NewSocketManager
{
    public class SocketEventArgs : EventArgs
    {
        public Socket Message { get; private set; }

        public SocketEventArgs(Socket message)
        {
            Message = message;
        }
    }
}
