using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ColdNetworkStack.Client
{
    public class Client
    {
        private readonly TcpClient _client = new TcpClient();
        private readonly string _name;
        private readonly AutoResetEvent _signal = new AutoResetEvent(false);
        private NetworkStream _NetworkStream;                
        public bool Connection = false;
        public int Cycles = 0;
        private bool _registered = false;
        private Thread _workerThread;

        public Client(string name)
        {
            _name = name;
        }

        public void Connect(IPAddress ip, int port)
        {
            _client.Connect(ip, port);

            if(!_registered)
                Register();

            Connection = true;
        }

        private void Register()
        {
            WriteNetworkStream(_client, Commands.Register.ToString());  //write command                                                        
            WriteNetworkStream(_client, _name); //write name
            _registered = true;
        }

        public void Unregister()
        {
            WriteNetworkStream(_client, Commands.UnRegister.ToString());
            WriteNetworkStream(_client, _name);
        }

        public void Disconnect()
        {            
            Unregister();
            WriteNetworkStream(_client, Commands.Disconnect.ToString());
            _client.Close();
            Connection = false;
            LaunchNextRun = null;
            DataReceived = null;
            NetworkFinished = null;
        }    

        public void ListenForTrigger()
        {
            _workerThread = new Thread(WaitForTrigger) { Name = "LOOP" };
            _workerThread.Start();
        }

        private void WaitForTrigger()
        {
            WriteNetworkStream(_client, Commands.Trigger.ToString());
            var answer = ReadNetworkStream(_client);

            Cycles = Convert.ToInt32(answer);

            
            TriggerEvent(DataReceived);
            
            var trigger = ReadNetworkStream(_client);
            Console.WriteLine("Network " + trigger + ": " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
            if (trigger == Commands.Trigger.ToString())
                TriggerEvent(LaunchNextRun);
            if (trigger == Commands.Finished.ToString())
                TriggerEvent(NetworkFinished);
            Console.WriteLine("Waiting for hardware: " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
            _signal.WaitOne();
            WriteNetworkStream(_client, Answers.Ack.ToString());    //signalize that this client is ready     
            Console.WriteLine("Network resumed: " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
        }


        public void ThisClientIsReady()
        {            
            _signal.Set();
        }

        #region NetworkCommunication
        private string ReadNetworkStream(TcpClient client)
        {
            var readHeader = new byte[4];
            var completeMessage = new StringBuilder();
            int totalBytesRead = 0;

            if (_NetworkStream == null)
                _NetworkStream = client.GetStream();

            try
            {
                _NetworkStream.ReadTimeout = 1800000; // two minutes timeout     
                
                _NetworkStream.Read(readHeader, 0, 4);
                Int32 bytesToRead = BitConverter.ToInt32(readHeader, 0);
                
                byte[] readBuffer = new byte[bytesToRead];
                
                do
                {
                    int numberOfBytesRead = _NetworkStream.Read(readBuffer, 0, readBuffer.Length);
                    completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, numberOfBytesRead));
                    totalBytesRead += numberOfBytesRead;
                } while (totalBytesRead < bytesToRead);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            return completeMessage.ToString();
        }

        private void WriteNetworkStream(TcpClient client, string message)
        {
            if (_NetworkStream == null)
                _NetworkStream = client.GetStream();


            try
            {
                byte[] payload = Encoding.ASCII.GetBytes(message);
                Int16 length = (Int16) payload.Length;
                byte[] header = BitConverter.GetBytes(length);
                byte[] packet = new byte[sizeof(Int16) + length];
                Array.Copy(header, packet, header.Length);
                Array.Copy(payload, 0, packet, header.Length, payload.Length); //copy the message behind the header
                _NetworkStream.Write(packet, 0, packet.Length);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
        #endregion

        private void TriggerEvent(EventHandler newEvent)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        public event EventHandler LaunchNextRun;
        public event EventHandler DataReceived;
        public event EventHandler NetworkFinished;
    }
}