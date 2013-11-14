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
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, _name);
            ReadNetworkStream(_client);
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

        public void SendDigitalData(string data)
        {
            WriteNetworkStream(_client, Commands.Data.ToString());
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, Commands.Save.ToString());
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, Commands.Digital.ToString());
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, data);
        }

        public void SendAnalogData(string data)
        {
            WriteNetworkStream(_client, Commands.Data.ToString());
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, Commands.Save.ToString());
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, Commands.Analog.ToString());
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, data);
        }

        public void StartLoop()
        {
            WriteNetworkStream(_client, Commands.EnterTriggerMode.ToString());  //enter trigger mode
            ReadNetworkStream(_client);                                         //read ack from server
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
                if (ReadNetworkStream(_client) == Commands.Trigger.ToString())
                    TriggerEvent(RunTriggered);
            }
        }

        public void ReadyForNextRun()
        {
            _nextRun.Set();
        }

        private string ReadNetworkStream(TcpClient client)
        {
            var readBuffer = new byte[1024];
            var completeMessage = new StringBuilder();
            int numberOfBytesRead = 0;

            if (_NetworkStream == null)
                _NetworkStream = client.GetStream();

            try
            {
                _NetworkStream.ReadTimeout = 120000; // two minutes timeout                    
                do
                {
                    numberOfBytesRead = _NetworkStream.Read(readBuffer, 0, readBuffer.Length);
                    completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, numberOfBytesRead));
                } while (_NetworkStream.DataAvailable);
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
                _NetworkStream.WriteTimeout = 120000;
                byte[] writeBuffer = Encoding.ASCII.GetBytes(message);

                _NetworkStream.Write(writeBuffer, 0, writeBuffer.Length);
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

        public event EventHandler RunTriggered;
        public event EventHandler DataReceived;
        public event EventHandler Connected;
        public event EventHandler Disconnected;
    }
}