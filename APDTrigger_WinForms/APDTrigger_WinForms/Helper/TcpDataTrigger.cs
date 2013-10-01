using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace APDTrigger_WinForms.Helper
{
    public class TcpDataTrigger
    {
        private readonly TcpListener _listener;
        private readonly AutoResetEvent _myClientGate = new AutoResetEvent(false);
        private readonly AutoResetEvent _myTriggerGate = new AutoResetEvent(false);
        public NetworkData Data;
        private bool _run = true;

        public TcpDataTrigger()
        {
            var triggerThread = new Thread(StartTcp);
            _listener = new TcpListener(IPAddress.Any, 51111);
            triggerThread.Start();
        }

        private void StartTcp()
        {
            _listener.Start();
            while (_run)
            {
                try
                {
                    IAsyncResult result = _listener.BeginAcceptTcpClient(HandleAsyncConnection, _listener);
                }
                catch
                {
                    break;
                }
                _myClientGate.WaitOne();
            }

            _listener.Stop();
        }


        public void Stop()
        {
            _run = false;
            _myClientGate.Set();
        }

        public void Trigger()
        {
            _myTriggerGate.Set();
        }

        private void HandleAsyncConnection(IAsyncResult result)
        {
            var listener = (TcpListener) result.AsyncState;
            TcpClient client;
            try
            {
                client = listener.EndAcceptTcpClient(result);
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                _myClientGate.Set();
            }


            client.NoDelay = true;
            client.Client.NoDelay = true;
            //Data = new NetworkData(0, 2, 23, 921, 23, 2.1);
            try
            {
                // get the stream to talk to the client over
                using (NetworkStream ns = client.GetStream())
                {
                    // set initial read timeout to 1 minute to allow for connection
                    ns.ReadTimeout = 1000;
                    ns.WriteTimeout = 1000;
                    var w = new BinaryWriter(ns);
                    var r = new BinaryReader(ns);
                    // handshaking
                    w.Write("HELLO!");
                    w.Flush();
                    Console.WriteLine("trying to shake hands");

                    // var test = ReceiveMessage(ns);
                    string test = r.ReadString();
                    if (test != "HELLO!")
                        throw new Exception("Wrong handshake message!");
                    Console.WriteLine("handshake successful");

                    w.Write("DATA/TRIGGER!");
                    w.Flush();

                    switch (r.ReadString())
                    {
                        case "DATA!":
                            string message = "NODATA";

                            if (Data != null)
                            {
                                message = Data.Serialize();
                            }
                            w.Write(message + "!");
                            w.Flush();
                            break;
                        case "TRIGGER!":
                            _myTriggerGate.WaitOne();
                            w.Write("GO!");
                            break;
                    }


                    if (r.ReadString() == "BYE!")
                    {
                        w.Write("BYE!");
                        w.Flush();
                    }


                    ns.Close();
                }
            }
            catch
            {
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }
    }
}