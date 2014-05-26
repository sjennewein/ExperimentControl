using System;
using System.ComponentModel;
using System.Net;
using AnalogOutput.Data;
using ColdNetworkStack.Client;
using fastJSON;

namespace AnalogOutput.Logic
{
    public class LogicNetwork : INotifyPropertyChanged 
    {
        private Client _client = new Client("AnalogOutput");

        public bool Activated = false;
        private DataNetwork _data = new DataNetwork();

        public string Ip
        {
            get { return _data.Ip; }
            set { _data.Ip = value; }
        }

        public int Port
        {
            get { return _data.Port; }
            set { _data.Port = value; }
        }

        public int Data
        {
            get { return _client.Cycles; }
        }

        public string ToJSON()
        {
            return JSON.Instance.ToJSON(_data);
        }

        public void FromJSON(string json)
        {
            _data = (DataNetwork) JSON.Instance.ToObject(json);
            PropertyChangedEvent("Ip");
            PropertyChangedEvent("Port");
        }

        public void Connect()
        {
            if (Activated)
            {
                _client = new Client("AnalogOutput");
                _client.DataReceived += delegate { OnDataReceived(); };
                _client.LaunchNextRun += delegate { OnStartNextRun(); };
                _client.Connect(IPAddress.Parse(Ip), Port);
                TriggerEvent(Connected);
            }
        }

        public void ListenToTrigger()
        {
            _client.ListenForTrigger();
        }

        public void Disconnect()
        {
            if (Activated)
            {
                _client.Disconnect();
                TriggerEvent(Disconnected);
            }
        }

        public void StartNextRun()
        {
            _client.ListenForTrigger();
        }

        public void HardwareStarted()
        {
            _client.ThisClientIsReady();
            Console.WriteLine("Network resumed");
        }

        private void OnDataReceived()
        {
            TriggerEvent(DataUpdated);
        }

        private void OnStartNextRun()
        {
            TriggerEvent(StartRun);
        }
        
        private void TriggerEvent(EventHandler newEvent)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        private void PropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (null != propertyChanged)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler DataUpdated;
        public event EventHandler StartRun;
        public event EventHandler Connected;
        public event EventHandler Disconnected;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}