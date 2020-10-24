using System;

namespace JobProject
    {
    public partial class Client
        {
        public class EndClientTaskEventArgs : EventArgs
            {
            public Client Client;
            public EndClientTaskEventArgs(Client client)
                {
                this.Client = client;
                }
            }
        }
    }