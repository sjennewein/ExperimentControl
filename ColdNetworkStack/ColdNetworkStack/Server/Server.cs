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
        private string _analog = "";
        private string _digital = "";
        private bool _run = true;

        public Server(IPAddress ip, int port)
        {
            _listener = new TcpListener(ip, port);
            new Thread(RunServer).Start();
        }

        private void RunServer()
        {
            _listener.Start();
            while (_run)
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

        private void HandleAsyncConnection(IAsyncResult result)
        {
            var state = new ConnectionStates();
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

            state.Status = StateType.Connect;

            while (true)
            {
                var command = (Commands) Enum.Parse(typeof (Commands), ReadNetworkStream(client));

                switch (command)
                {
                    case Commands.Register:
                        RegisterClient(client);
                        break;
                    case Commands.Data:
                        state.Status = StateType.Data;
                        HandleData(client);
                        break;
                    case Commands.UnRegister:
                        UnRegisterClient(client);
                        break;
                    case Commands.Disconnect:
                        state.Status = StateType.Disconnect;
                        break;
                }

                if (state.Status == StateType.Disconnect)
                    break;
            }
            client.Close();
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
                    _digital = ReadNetworkStream(client);
                    break;
                case Commands.Analog:
                    _analog = ReadNetworkStream(client);
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
                    WriteNetworkStream(client, _digital);                    
                    break;
                case Commands.Analog:
                    WriteNetworkStream(client, _analog);
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
                    ns.ReadTimeout = 1000;

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
        }
    }
}