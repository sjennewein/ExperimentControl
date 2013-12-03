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
        private Thread _workerThread;
        private bool _loop = true;
        public bool Connection = false;
        public int CyclesPerRun = 0;

        public Client(string name)
        {
            _name = name;
        }

        public void Connect(IPAddress ip, int port)
        {
            _client.Connect(ip, port);
            WriteNetworkStream(_client, Commands.Register.ToString());  //write command
            ReadNetworkStream(_client);                                 //read ack
            
            WriteNetworkStream(_client, _name);                         //write name
            ReadNetworkStream(_client);                                 //read ack
            Connection = true;
        }

        public void Disconnect()
        {
            WriteNetworkStream(_client, Commands.UnRegister.ToString());
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, _name);
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, Commands.Disconnect.ToString());
            _client.Close();
            Connection = false;
        }    

        public void StartLoop()
        {
            WriteNetworkStream(_client, Commands.CyclesPerRun.ToString());      //enter trigger mode
            Console.WriteLine(ReadNetworkStream(_client));                      //read ack from server
            var answer = ReadNetworkStream(_client);                            //read how many cycles per run
            CyclesPerRun = Convert.ToInt32(answer);
            
            WriteNetworkStream(_client, Answers.Ack.ToString());                //send ack
            
            
            TriggerEvent(DataReceived);

            _loop = true;
            _workerThread = new Thread(RunLoop) {Name = "LOOP"};
            _workerThread.Start();
        }

        public void StopLoop()
        {
            _loop = false;
            _signal.Set();
        }

        private void RunLoop()
        {
            Console.WriteLine("starting loop");
            while (_loop)
            {
                _signal.WaitOne();
                if (!_loop)
                    break;
                WriteNetworkStream(_client, Commands.WaitingForTrigger.ToString());
                Console.WriteLine("Waiting for trigger: " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
                ReadNetworkStream(_client);
                var trigger = ReadNetworkStream(_client);
                Console.WriteLine(trigger + ": " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
                if (trigger == Commands.Trigger.ToString())
                    TriggerEvent(LaunchNextRun);
                _signal.WaitOne();
                //Thread.Sleep(5);
                WriteNetworkStream(_client, Answers.Ack.ToString());
            }
        }

        public void Resume()
        {
            _signal.Set();
        }


        private string ReadNetworkStream(TcpClient client)
        {
            var readHeader = new byte[4];
            var completeMessage = new StringBuilder();
            int totalBytesRead = 0;

            if (_NetworkStream == null)
                _NetworkStream = client.GetStream();

            try
            {
                _NetworkStream.ReadTimeout = 300000; // two minutes timeout     
                
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
                Int32 length = payload.Length;
                byte[] header = BitConverter.GetBytes(length);
                byte[] packet = new byte[sizeof(Int32) + length];
                Array.Copy(header, packet, header.Length);
                Array.Copy(payload, 0, packet, header.Length, payload.Length); //copy the message behind the header
                _NetworkStream.Write(packet, 0, packet.Length);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        private void TriggerEvent(EventHandler newEvent)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        public event EventHandler LaunchNextRun;
        public event EventHandler DataReceived;
        //public event EventHandler Connected;
        //public event EventHandler Disconnected;
    }
}