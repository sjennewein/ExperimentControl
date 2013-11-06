using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ColdNetworkStack.Client
{
    public class Client
    {
        private readonly TcpClient _client = new TcpClient();
        private readonly AutoResetEvent _runFinished;
        private readonly string _name;
        private bool _loop;

        public Client(string name)
        {
            _name = name;
        }

        public void Connect(IPAddress ip, int port)
        {
            _client.Connect(ip, port);
            WriteNetworkStream(_client,Commands.Register.ToString());
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, _name);
            ReadNetworkStream(_client);
        }

        public void Disconnect()
        {
            WriteNetworkStream(_client,Commands.UnRegister.ToString());
            ReadNetworkStream(_client);
            WriteNetworkStream(_client, _name);
            ReadNetworkStream(_client);
            WriteNetworkStream(_client,Commands.Disconnect.ToString());
            _client.Close();
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
            WriteNetworkStream(_client, Commands.EnterTriggerMode.ToString());
            ReadNetworkStream(_client);
            _loop = true;
            new Thread(RunLoop);
        }

        public void StopLoop()
        {
            _loop = false;
            _runFinished.Set();
        }

        private void RunLoop()
        {
            while (_loop)
            {
                _runFinished.WaitOne();
                if (_loop)
                    break;
                WriteNetworkStream(_client,Commands.WaitingForTrigger.ToString());
                if (ReadNetworkStream(_client) == Commands.Trigger.ToString())
                    RunInitiated();
            }
        }

        private string ReadNetworkStream(TcpClient client)
        {
            try
            {
                using (NetworkStream ns = client.GetStream())
                {
                    ns.ReadTimeout = 60000;

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
                    ns.WriteTimeout = 1000;
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

        private void RunInitiated()
        {
            EventHandler runHasBeenInitiated = RunHasBeenInitiated;
            if (runHasBeenInitiated != null)
                runHasBeenInitiated(this, new EventArgs());
        }

        public event EventHandler RunHasBeenInitiated;

    }
}