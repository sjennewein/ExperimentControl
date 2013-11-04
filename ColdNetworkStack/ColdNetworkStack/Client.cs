using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColdNetworkStack
{
    public enum State
    {
        Connected,
        Disconnected,
        Receiving,
        Received,
        Sending,
        Sent,
        Error
    }

    public class Client
    {
        private State oldState = new State();
        private State newState = new State();

        public void Connect()
        {
            
        }

        public void Disconnect()
        {
            
        }

        public void ReceiveData()
        {
            
        }

        public void SendData()
        {
            
        }

        public void WaitForTrigger()
        {
            
        }

    }
}
