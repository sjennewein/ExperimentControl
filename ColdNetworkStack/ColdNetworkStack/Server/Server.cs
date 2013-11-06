using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ColdNetworkStack.Server
{
    public class Server
    {
        private readonly TcpListener _listener;
        private readonly AutoResetEvent _myClientGate = new AutoResetEvent(false);
        private readonly List<String> _registeredClients = new List<string>();
        private readonly ManualResetEvent _startClientRun = new ManualResetEvent(false);        
        private string _analogData = "";
        private string _digitalData = "";
        private int _enteredClients;
        private bool _serverRun = true;        
        private int _startedClients;
        private string _triggerData = "";

        public Server(IPAddress ip, int port)
        {
            _listener = new TcpListener(ip, port);
            new Thread(RunServer).Start();
        }

        private void RunServer()
        {
            _listener.Start();
            while (_serverRun)
            {
                try
                {
                    _listener.BeginAcceptTcpClient(HandleAsyncConnection, _listener);
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
            _serverRun = false;
            _myClientGate.Set();
        }

        private void HandleAsyncConnection(IAsyncResult result)
        {
            bool _run = true;
            var listener = (TcpListener) result.AsyncState;
            TcpClient client;
            //catch exception when the server is closed
            try
            {
                client = listener.EndAcceptTcpClient(result);
            }
            catch (Exception)
            {
                //when something happens just return and do nothing
                return;
            }
            finally
            {
                _myClientGate.Set();
            }

            client.NoDelay = true;
            client.Client.NoDelay = true;

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
                        _run = false;
                        break;
                }               
            }
            client.Close();
        }

        private void TriggerMode(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());
            while (true)
            {
                if (ReadNetworkStream(client) == Commands.WaitingForTrigger.ToString())                
                    _enteredClients++;                
                else
                    return;
                
                if (_enteredClients == _registeredClients.Count) //the last client starts the next round
                {
                    AllAreWaiting();                                                                          
                }

                _startClientRun.WaitOne(); //all clients wait until all returned                
                                
                WriteNetworkStream(client, Commands.Trigger.ToString());

                _startClientRun.Reset(); //block the next run                               
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

        private void SaveData(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());
            var command = (Commands) Enum.Parse(typeof (Commands), ReadNetworkStream(client));
            WriteNetworkStream(client, Answers.Ack.ToString());

            switch (command)
            {
                case Commands.Digital:
                    _digitalData = ReadNetworkStream(client);
                    break;
                case Commands.Analog:
                    _analogData = ReadNetworkStream(client);
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
                    WriteNetworkStream(client, _digitalData);
                    break;
                case Commands.Analog:
                    WriteNetworkStream(client, _analogData);
                    break;
                case Commands.Trigger:
                    WriteNetworkStream(client, _triggerData);
                    break;
            }
        }

        private void RegisterClient(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());

            string name = ReadNetworkStream(client);
            _registeredClients.Add(name);

            WriteNetworkStream(client, Answers.Ack.ToString());
        }

        private void UnRegisterClient(TcpClient client)
        {
            WriteNetworkStream(client, Answers.Ack.ToString());

            string name = ReadNetworkStream(client);
            _registeredClients.Remove(name);

            WriteNetworkStream(client, Answers.Ack.ToString());
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

        public void StartNextRun()
        {
            _startClientRun.Set();
        }

        public void SetTriggerData(string data)
        {
            _triggerData = data;
        }

        private void AllAreWaiting()
        {
            EventHandler startTriggerOutput = AllClientsAreWaiting;
            if (startTriggerOutput != null)
                startTriggerOutput(this, new EventArgs());
        }

        public event EventHandler AllClientsAreWaiting;
    }
}