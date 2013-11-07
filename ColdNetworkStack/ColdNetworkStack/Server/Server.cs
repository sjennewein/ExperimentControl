using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ColdNetworkStack.Server
{
    public class Server
    {
        private readonly List<ClientProtocol> _clientTalks = new List<ClientProtocol>();
        private readonly TcpListener _listener;
        private readonly AutoResetEvent _myClientGate = new AutoResetEvent(false);
        private readonly List<String> _registeredClients = new List<string>();
        public string AnalogData = "";
        public int CyclesPerRun = 0;
        public string DigitalData = "";
        public string TriggerData = "";
        private long _enteredClients = 0;        
        private bool _serverRun = true;

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

        public void RegisterClient(string name)
        {
            _registeredClients.Add(name);
        }

        public void UnregisterClient(string name)
        {
            _registeredClients.Remove(name);
        }

        private void HandleAsyncConnection(IAsyncResult result)
        {
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

            var clientTalk = new ClientProtocol(this);
            _clientTalks.Add(clientTalk);

            clientTalk.StartCommunication(client);

            _clientTalks.Remove(clientTalk);
            client.Close();
        }        

        public void ClientEntered()
        {
            Interlocked.Add(ref _enteredClients, 1);

            if (Interlocked.Read(ref _enteredClients) == _registeredClients.Count)
            {
                Interlocked.Exchange(ref _enteredClients, 0);
                
                foreach (ClientProtocol client in _clientTalks)
                    client.SendTrigger();
                
                ReadyForNextRun();
            }
        }

        private void ReadyForNextRun()
        {
            EventHandler nextRun = AllClientsAreReady;
            if(nextRun != null)
                AllClientsAreReady(this,new EventArgs());
        }

        public event EventHandler AllClientsAreReady;
    }
}