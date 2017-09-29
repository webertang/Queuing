using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.NewSocketManager
{
    public class ClientEventArgs : EventArgs
    {
        public Client Client { get; private set; }

        public ClientEventArgs(Client client)
        {
            Client = client;
        }
    }
}
