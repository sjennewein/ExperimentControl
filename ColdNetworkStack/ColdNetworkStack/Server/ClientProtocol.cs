using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ColdNetworkStack.Server
{
    public class ClientProtocol
    {
        private readonly Server _parent;
        private readonly AutoResetEvent _triggerSynchronization = new AutoResetEvent(false);
        private NetworkStream _NetworkStream;
        private bool _run;
        private bool _trigger;

        public ClientProtocol(Server parent)
        {
            _parent = parent;
        }

        public void Stop()
        {
            _run = false;
        }

        public void SendTrigger()
        {
            _triggerSynchronization.Set();
        }

        public void StopTriggerMode()
        {
            _trigger = false;
            _triggerSynchronization.Set();
        }

        public void StartCommunication(TcpClient client)
        {
            _run = true;
            while (_run)
            {
                string input = ReadNetworkStream(client);
                var command = (Commands) Enum.Parse(typeof (Commands), input);

                switch (command)
                {
                    case Commands.Register:
                        RegisterClient(client);
                        break;
                    case Commands.CyclesPerRun:
                        CyclesPerRun(client);
                        break;
                    case Commands.Data:
                        HandleData(client);
                        break;
                    case Commands.UnRegister:
                        UnRegisterClient(client);
                        break;
                    case Commands.EnterTriggerMode:
                        TriggerMode(client);
                        break;
                    case Commands.Disconnect:
                        Stop();
                        break;
                }
            }
        }

        private void HandleData(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());
            var command = (Commands) Enum.Parse(typeof (Commands), ReadNetworkStream(client));
        }

        private void CyclesPerRun(TcpClient client)
        {
            WriteNetworkStream(client, _parent.CyclesPerRun.ToString());
            ReadNetworkStream(client);
        }


        private void TriggerMode(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());
            //Thread.Sleep(5);
            
            _parent.ClientReady();

            _triggerSynchronization.WaitOne(); //all clients wait until all returned                

            if (!_trigger)
                return;

            WriteNetworkStream(client, Commands.Trigger.ToString());
            ReadNetworkStream(client);
        }        

        private void RegisterClient(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());

            string name = ReadNetworkStream(client);
            _parent.RegisterClient(name);

            WriteNetworkStream(client, Answers.Ack.ToString());
        }

        private void UnRegisterClient(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());

            string name = ReadNetworkStream(client);
            _parent.UnregisterClient(name);

            WriteNetworkStream(client, Answers.Ack.ToString());
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
                _NetworkStream.ReadTimeout = 300000; // two minutes timeout                    
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
                _NetworkStream.WriteTimeout = 300000;
                byte[] writeBuffer = Encoding.ASCII.GetBytes(message);

                _NetworkStream.Write(writeBuffer, 0, writeBuffer.Length);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}