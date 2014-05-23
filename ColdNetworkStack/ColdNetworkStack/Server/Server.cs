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
        
        public int Cycles = 0;
        
        public string TriggerData = "";
        private long _readyClients;
        private long _startedClients;
        private bool _serverRun = true;

        public List<String> RegisteredClients { get { return _registeredClients; } } 

        public Server(IPAddress ip, int port)
        {
            _listener = new TcpListener(ip, port);
            new Thread(Listen).Start();
        }

        private void Listen()
        {
            _listener.Start();
            while (_serverRun)
            {
                try
                {
                    _listener.BeginAcceptTcpClient(ListenCallback, _listener);
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
            TriggerEvent(ClientsChanged);
        }

        public void UnregisterClient(string name)
        {
            _registeredClients.Remove(name);
            TriggerEvent(ClientsChanged);
        }

        private void ListenCallback(IAsyncResult result)
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
       
        public void ClientReady()
        {
            Interlocked.Add(ref _readyClients, 1);

            if (Interlocked.Read(ref _readyClients) == _registeredClients.Count)
            {
                Interlocked.Exchange(ref _readyClients, 0);
                StartNextRun();
                Console.WriteLine("All clients returned: " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
            }
        }

        public void ClientStarted()
        {
            Interlocked.Add(ref _startedClients, 1);
            if(Interlocked.Read(ref _startedClients) == _registeredClients.Count)
            {
                Interlocked.Exchange(ref _startedClients, 0);
                TriggerEvent(AllClientsAreLaunched);
            }
            
        }

        public void StopTrigger()
        {
            foreach (ClientProtocol client in _clientTalks)
                client.StopTriggerMode();
        }

        private void StartNextRun()
        {
            foreach (ClientProtocol client in _clientTalks)
                client.SendTrigger();
            //call ClientStarted once for the apd itself
            ClientStarted();
        }

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        public event EventHandler AllClientsAreReady;
        public event EventHandler AllClientsAreLaunched;
        public event EventHandler ClientsChanged;
    }
}