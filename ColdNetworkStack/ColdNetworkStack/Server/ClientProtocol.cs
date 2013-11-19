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
                    case Commands.WaitingForTrigger:
                        TriggerMode(client);
                        break;
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
            WriteNetworkStream(client, Answers.Ack.ToString());
            WriteNetworkStream(client, _parent.CyclesPerRun.ToString());
            ReadNetworkStream(client);
        }


        private void TriggerMode(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());
            //Thread.Sleep(5); might be needed
            
            _parent.ClientReady();

            _triggerSynchronization.WaitOne(); //all clients wait until all returned                

            if (!_trigger)
                return;

            WriteNetworkStream(client, Commands.Trigger.ToString());
            ReadNetworkStream(client);

            _parent.ClientStarted();
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
                _NetworkStream.WriteTimeout = 300000;
                byte[] payload = Encoding.ASCII.GetBytes(message);
                Int32 length = payload.Length;
                byte[] header = BitConverter.GetBytes(length);
                byte[] packet = new byte[sizeof(Int32) + length];
                Array.Copy(header,packet,header.Length);
                Array.Copy(payload,0,packet,header.Length,payload.Length); //copy the message behind the header
                _NetworkStream.Write(packet, 0, packet.Length);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}