using System.Net;
using AnalogOutput.Data;
using ColdNetworkStack.Client;
using fastJSON;

namespace AnalogOutput.Logic
{
    public class LogicNetwork
    {
        private DataNetwork _data = new DataNetwork();
        private readonly Client _client = new Client("AnalogOutput");

        public bool Activated = false;

        public LogicNetwork()
        {

        }

        

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

        public string ToJSON()
        {
            return JSON.Instance.ToJSON(_data);
        }

        public void FromJSON(string json)
        {
            _data = (DataNetwork) JSON.Instance.ToObject(json);
        }

        public void Connect()
        {
            if(Activated)
                _client.Connect(IPAddress.Parse(Ip), Port);
        }

        public void Disconnect()
        {
            if(Activated)
                _client.Disconnect();
        }
    }
}