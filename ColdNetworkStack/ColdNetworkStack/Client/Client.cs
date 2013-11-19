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
        private readonly AutoResetEvent _nextRun = new AutoResetEvent(false);
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
            WriteNetworkStream(_client, Commands.Register.ToString());
            Console.WriteLine(ReadNetworkStream(_client));
            
            WriteNetworkStream(_client, _name);
            Console.WriteLine(ReadNetworkStream(_client));
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
            WriteNetworkStream(_client, Commands.CyclesPerRun.ToString());  //enter trigger mode
            Console.WriteLine(ReadNetworkStream(_client));                      //read ack from server
            var answer = ReadNetworkStream(_client);                            //read how many cycles per run
            CyclesPerRun = Convert.ToInt32(answer);
            Console.WriteLine(answer);
            WriteNetworkStream(_client, Answers.Ack.ToString());                //send ack
            TriggerEvent(DataReceived);

            _loop = true;
            _workerThread = new Thread(RunLoop) {Name = "LOOP"};
            _workerThread.Start();
        }

        public void StopLoop()
        {
            _loop = false;
            _nextRun.Set();
        }

        private void RunLoop()
        {
            while (_loop)
            {
                _nextRun.WaitOne();
                if (!_loop)
                    break;
                WriteNetworkStream(_client, Commands.WaitingForTrigger.ToString());
                Console.WriteLine("Waiting for trigger: " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
                ReadNetworkStream(_client);
                var trigger = ReadNetworkStream(_client);
                Console.WriteLine(trigger + ": " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
                if (trigger == Commands.Trigger.ToString())
                    TriggerEvent(LaunchNextRun);
            }
        }

        public void ReadyForNextRun()
        {
            _nextRun.Set();
        }

        public void RunIsLaunched()
        {
            WriteNetworkStream(_client, Answers.Ack.ToString());
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
                Int16 bytesToRead = Convert.ToInt16(readHeader);
                
                byte[] readBuffer = new byte[bytesToRead];
                
                do
                {
                    int numberOfBytesRead = _NetworkStream.Read(readBuffer, 0, readBuffer.Length);
                    completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, numberOfBytesRead));
                    totalBytesRead += numberOfBytesRead;
                } while (bytesToRead == totalBytesRead);
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
                _NetworkStream.WriteTimeout = 300000;
                byte[] writeBuffer = Encoding.ASCII.GetBytes(message);
                Int32 length = writeBuffer.Length;
                byte[] payload = BitConverter.GetBytes(length);
                Array.Copy(writeBuffer, 0, payload, 4, writeBuffer.Length); //copy the message behind the header
                _NetworkStream.Write(payload, 0, payload.Length);
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
        public event EventHandler Connected;
        public event EventHandler Disconnected;
    }
}