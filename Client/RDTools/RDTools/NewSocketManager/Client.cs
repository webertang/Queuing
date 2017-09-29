using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace RDTools.NewSocketManager
{
    public class Client
    {
        public string IP { get; set; }

        public int Port { get; set; }

        public ComputerEnum ComputerType { get; set; }

        public string OfficeId { get; set; }

        public Socket ClientSocket { get; set; }


        public string OperatorId { get; set; }
    }
}
