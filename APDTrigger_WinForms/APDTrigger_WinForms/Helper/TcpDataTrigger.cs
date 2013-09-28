using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace APDTrigger_WinForms.Helper
{
    public class TcpDataTrigger
    {
        private readonly AutoResetEvent _myClientGate = new AutoResetEvent(false);
        private readonly AutoResetEvent _myTriggerGate = new AutoResetEvent(false);

        public NetworkData Data;

        public TcpDataTrigger()
        {
            var triggerThread = new Thread(StartTcp);
            triggerThread.Start();
        }

        private void StartUdp()
        {
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var test = new UdpClient(9898);
        }


        private void StartTcp()
        {
            var listener = new TcpListener(IPAddress.Any, 51111);

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
            var listener = (TcpListener) result.AsyncState;
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
                    var w = new BinaryWriter(ns);
                    var r = new BinaryReader(ns);
                    // handshaking
                    w.Write("HELLO?");
                    w.Flush();
                    Console.WriteLine("trying to shake hands");
                    string test = r.ReadString();
                    if (test != "HELLO!")
                        throw new Exception("Wrong handshake message!");
                    Console.WriteLine("handshake successful");
                    // decrease read timeout to 1 second now that data is coming in
                    ns.ReadTimeout = 1000;


                    w.Write("DATA/TRIGGER?");
                    w.Flush();

                    switch (r.ReadString())
                    {
                        case "DATA!":
                            string message = "NODATA!";

                            if (Data != null)
                            {
                                message = Data.Serialize();
                            }

                            w.Write(message);
                            w.Flush();
                            break;
                        case "TRIGGER!":
                            _myTriggerGate.WaitOne();
                            w.Write("GO!");
                            break;
                    }

                    ns.Close();
                }
            }            
            finally
            {
                if (client != null)
                    client.Close();
            }
        }
    }
}