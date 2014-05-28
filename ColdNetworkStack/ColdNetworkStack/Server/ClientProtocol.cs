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
        private bool _trigger = true;

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
                Console.WriteLine(input);
                Commands command;
                try
                {
                     command = (Commands)Enum.Parse(typeof(Commands), input);    
                }
                catch(Exception e)
                {
                    command = Commands.Disconnect;
                }
                
                switch (command)
                {
                    case Commands.Trigger: //matlab workaround because it can't send long strings
                        TriggerMode(client);
                        break;                    
                    case Commands.Register:
                        RegisterClient(client);
                        break;
                    case Commands.UnRegister:
                        UnRegisterClient(client);
                        break;
                    case Commands.Disconnect:
                        Stop();
                        break;
                }
            }
        }


        private void TriggerMode(TcpClient client)
        {                    
            _parent.ClientReady();
            WriteNetworkStream(client,_parent.Cycles.ToString());

            _triggerSynchronization.WaitOne(); //all clients wait until all returned                

            if (!_trigger)
            {
                WriteNetworkStream(client, Commands.Finished.ToString());
                _parent.ClientFinished();
                return;
            }
                

            WriteNetworkStream(client, Commands.Trigger.ToString());
            Console.WriteLine("SEND TRIGGER!");
            Console.Write(ReadNetworkStream(client));            

            _parent.ClientStarted();
        }

        private void RegisterClient(TcpClient client)
        {

            string name = ReadNetworkStream(client);
            Console.WriteLine(name);
            _parent.RegisterClient(name);
        }

        private void UnRegisterClient(TcpClient client)
        {          
            string name = ReadNetworkStream(client);
            _parent.UnregisterClient(name);
        }

        private string ReadNetworkStream(TcpClient client)
        {
            var header = new byte[2];
            var completeMessage = new StringBuilder();
            int totalBytesRead = 0;

            if (_NetworkStream == null)
                _NetworkStream = client.GetStream();

            try
            {
                _NetworkStream.ReadTimeout = 1800000; // two minutes timeout     

                _NetworkStream.Read(header, 0, header.Length);
                Int32 bytesToRead = BitConverter.ToInt16(header, 0);
                var readBuffer = new byte[bytesToRead];
             
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
                _NetworkStream.WriteTimeout = 1800000;
                byte[] payload = Encoding.ASCII.GetBytes(message);
                Int32 length = payload.Length;
                
                byte[] header = BitConverter.GetBytes(length);
                var packet = new byte[header.Length + payload.Length];
                Array.Copy(header, packet, header.Length);
                Array.Copy(payload, 0, packet, header.Length, payload.Length); //copy the message behind the header
                _NetworkStream.Write(packet, 0, packet.Length);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}