using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ColdNetworkStack.Server
{
    public class ClientProtocol
    {
        private readonly Server _parent;
        private readonly AutoResetEvent _triggerSynchronization = new AutoResetEvent(false);
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
                var command = (Commands) Enum.Parse(typeof (Commands), ReadNetworkStream(client));

                switch (command)
                {
                    case Commands.Register:
                        RegisterClient(client);
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

            switch (command)
            {
                case Commands.Load:
                    LoadData(client);
                    break;
                case Commands.Save:
                    SaveData(client);
                    break;
            }
        }

        private void TriggerMode(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());

            WriteNetworkStream(client, _parent.CyclesPerRun.ToString());
            ReadNetworkStream(client);

            _trigger = true;
            while (_trigger)
            {
                var command = (Commands) Enum.Parse(typeof (Commands), ReadNetworkStream(client));
                switch (command)
                {
                    case Commands.WaitingForTrigger:
                        _parent.ClientReady();
                        break;
                    case Commands.Quit:
                        _trigger = false;
                        break;
                    default:
                        return;
                }            

                _triggerSynchronization.WaitOne(); //all clients wait until all returned                
                
                if (!_trigger)
                    break;

                WriteNetworkStream(client, Commands.Trigger.ToString());                
            }

            WriteNetworkStream(client,Commands.Quit.ToString());            
        }

        private void SaveData(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());
            var command = (Commands) Enum.Parse(typeof (Commands), ReadNetworkStream(client));
            WriteNetworkStream(client, Answers.Ack.ToString());

            switch (command)
            {
                case Commands.Digital:
                    _parent.DigitalData = ReadNetworkStream(client);
                    break;
                case Commands.Analog:
                    _parent.AnalogData = ReadNetworkStream(client);
                    break;
            }

            WriteNetworkStream(client, Answers.Ack.ToString());
        }

        private void LoadData(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());
            var command = (Commands) Enum.Parse(typeof (Commands), ReadNetworkStream(client));
            WriteNetworkStream(client, Answers.Ack.ToString());

            switch (command)
            {
                case Commands.Digital:
                    WriteNetworkStream(client, _parent.DigitalData);
                    break;
                case Commands.Analog:
                    WriteNetworkStream(client, _parent.AnalogData);
                    break;
                case Commands.Trigger:
                    WriteNetworkStream(client, _parent.TriggerData);
                    break;
            }
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
            try
            {
                using (NetworkStream ns = client.GetStream())
                {
                    ns.ReadTimeout = 120000;

                    var reader = new BinaryReader(ns);
                    string input = reader.ReadString();

                    reader.Close();
                    return input;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            return "";
        }

        private void WriteNetworkStream(TcpClient client, string message)
        {
            try
            {
                using (NetworkStream ns = client.GetStream())
                {
                    ns.WriteTimeout = 120000;
                    var writer = new BinaryWriter(ns);
                    writer.Write(message);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}