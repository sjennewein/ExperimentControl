using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace APDTrigger_WinForms.Helper
{
    public class TcpDataTrigger
    {
        private readonly AutoResetEvent _myClientGate = new AutoResetEvent(false);
        private readonly AutoResetEvent _myTriggerGate = new AutoResetEvent(false);
        private readonly TcpListener _listener;
        private bool _run = true;
        public NetworkData Data;

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
            Data = new NetworkData(0, 2, 23, 921, 23, 2.1);
            try
            {
                // get the stream to talk to the client over
                using (NetworkStream ns = client.GetStream())
                {
                    // set initial read timeout to 1 minute to allow for connection
                    ns.ReadTimeout = 1000;
                    ns.WriteTimeout = 1000;
                    var w = new BinaryWriter(ns);
                    //var r = new BinaryReader(ns);
                    // handshaking
                    w.Write("HELLO!");
                    w.Flush();
                    Console.WriteLine("trying to shake hands");

                    
                    if (ReceiveMessage(ns) != "HELLO!")
                        throw new Exception("Wrong handshake message!");
                    Console.WriteLine("handshake successful");
                    //// decrease read timeout to 1 second now that data is coming in
                    //ns.ReadTimeout = 1000;

                    w.Write("DATA/TRIGGER!");
                    w.Flush();

                    switch (ReceiveMessage(ns))
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

                    

                    if (ReceiveMessage(ns) == "BYE!")
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

        private string ReceiveMessage(NetworkStream ns)
        {
            byte[] bytes = new byte[1024];
            var encoder = new ASCIIEncoding();
            var clientData = new StringBuilder();
            int bytesRead = 0;
            string message = "";            
            bool run = true;
            int loopCounter = 0;

            // Loop to receive all the data sent by the client.
            do
            {
                //check if data is available
                if (ns.DataAvailable)
                {
                    //set how much data has been read
                    bytesRead = ns.Read(bytes, 0, bytes.Length);
                }
                else
                {
                    //if no data was available it's 0
                    bytesRead = 0;
                }

                if (bytesRead > 0)
                {
                    // Translate data bytes to an ASCII string and append
                    clientData.Append(encoder.GetString(bytes, 0, bytesRead));
                }
                

                message = clientData.ToString();
                
                if(message.Length > 0)
                {
                    //check if termination byte appeared
                    if (message[message.Length - 1].ToString() == "!")
                        //if yes stop receiving data
                        run = false;
                }

                if (loopCounter > 10)
                {
                    throw new Exception("Broken communication :(");
                }
                    

                loopCounter++;
            } while (run);
            
            return message;
        }
    }
}