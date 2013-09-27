using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace APDTrigger_WinForms.Helper
{
    public class TcpDataTrigger
    {
        private readonly AutoResetEvent _myTriggerGate = new AutoResetEvent(false);
        private readonly AutoResetEvent _myClientGate = new AutoResetEvent(false);

        public NetworkData Data;

        public TcpDataTrigger()
        {
            var triggerThread = new Thread(StartTcp);
            triggerThread.Start();
        }

        private void StartUdp()
        {
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpClient test = new UdpClient(9898);
            
        }


        private void StartTcp()
        {
            var listener = new TcpListener(IPAddress.Any, 9898);

            listener.Start();
            while (true)
            {
                IAsyncResult result = listener.BeginAcceptTcpClient(HandleAsyncConnection, listener);
                _myClientGate.WaitOne();                
            }
        }

        public void Trigger()
        {
            _myTriggerGate.Set();
        }

        private void HandleAsyncConnection(IAsyncResult result)
        {
            
            TcpListener listener = (TcpListener) result.AsyncState;
            TcpClient client = listener.EndAcceptTcpClient(result);
            _myClientGate.Set();
            
            
            client.NoDelay = true;
            client.Client.NoDelay = true;            
            
            try
            {         

                // get the stream to talk to the client over
                using (NetworkStream ns = client.GetStream())    
                {
                    // set initial read timeout to 1 minute to allow for connection
                    ns.ReadTimeout = 60000;
                    ns.WriteTimeout = 1000;

                    // handshaking
                    SendMessage(ns, "HELLO?");
                    Console.WriteLine("trying to shake hands");
                    var test = ReceiveMessage(ns).Trim('\0');
                    if (test != "HELLO!")
                        throw new Exception("Wrong handshake message!");
                    Console.WriteLine("handshake successful");
                    // decrease read timeout to 1 second now that data is coming in
                    ns.ReadTimeout = 1000;
                    
                    SendMessage(ns, "DATA/TRIGGER?");
                    
                    switch(ReceiveMessage(ns))
                    {
                        case "DATA!":
                            var message = "NODATA!";
                            
                            if(Data != null)
                            {
                                message = Data.Serialize();    
                            }
                            
                            SendMessage(ns, message);
                            break;
                        case "TRIGGER!":                            
                            _myTriggerGate.WaitOne();
                            SendMessage(ns, "GO!");
                            break;
                    }
                                       
                    ns.Close();

                }

            }
            //catch (Exception)
            //{

            //}
            finally
            {                
                if (client != null)
                    client.Close();
                
            }
            
        }

        private string ReceiveMessage(NetworkStream ns)
        {
            byte[] bytes = new byte[4096];
            var encoder = new ASCIIEncoding();
            var clientData = new StringBuilder();
            int bytesRead = 0;
            string message = "";
            
            // Loop to receive all the data sent by the client.
            do
            {
            //    try
            //    {
                    bytesRead = ns.Read(bytes, 0, bytes.Length);
                    if (bytesRead > 0)
                    {
                        // Translate data bytes to an ASCII string
                        clientData.Append(encoder.GetString(bytes, 0, bytesRead));

                    }
                //}
                //catch (IOException ioe)
                //{
                   // bytesRead = 0;
                //}
            } while (bytesRead > 0);
            
            message = clientData.ToString();
            return message;
        }

        private void SendMessage(NetworkStream ns, string message)
        {
            var encoder = new ASCIIEncoding();
            byte[] bytes = encoder.GetBytes(message);                    
            ns.Write(bytes, 0, bytes.Length);
            ns.Flush();
        }
        
    }
}