using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using ColdNetworkStack.Client;
using DigitalOutput.Model;
using Hulahoop.Controller;
using Ionic.Zip;
using fastJSON;
using Buffer = DigitalOutput.Hardware.Buffer;

namespace DigitalOutput.Controller
{
    public class ControllerCard : INotifyPropertyChanged
    {
        private readonly DigitalMainwindow _myGui;
        private readonly Buffer _hardwareBuffer;
        private readonly ModelCard _model;
        public bool Network = false;
        private Client _tcpClient;
        public ControllerPattern[] Patterns;

        public ControllerCard(ModelCard model, Buffer buffer, DigitalMainwindow gui)
        {
            _model = model;
            Patterns = new ControllerPattern[_model.Patterns.Length];
            _hardwareBuffer = buffer;
            _myGui = gui;
            _hardwareBuffer.CycleDone += delegate { UpdateCycles(); };

            for (int iPattern = 0; iPattern < _model.Patterns.Length; iPattern++)
            {
                ModelPattern modelPattern = _model.Patterns[iPattern];
                Patterns[iPattern] = new ControllerPattern(modelPattern, this);
            }            
        }

        public string Ip
        {
            get { return _model.Ip; }
            set { _model.Ip = value; }
        }

        public int Port
        {
            get { return _model.Port; }
            set { _model.Port = value; }
        }

        public string Flow
        {
            get { return _model.Flow; }
            set { _model.Flow = value; }
        }

        public int CyclesDone { get; set; }

        public int RunsDone { get; set; }

        private void UpdateCycles()
        {
            CyclesDone++;
            PropertyChangedEvent("CyclesDone");
            PropertyChangedEvent("RunsDone");
        }

        public void Start()
        {
            CyclesDone = 0;
            RunsDone = 0;
            _hardwareBuffer.Start(JSON.Instance.ToJSON(_model));
        }

        public void Stop()
        {
            _hardwareBuffer.Stop();
        }

        public void Save(string fileName)
        {
            string json = JSON.Instance.ToJSON(_model);
            using (var zip = new ZipFile())
            {
                zip.AddEntry("DigitalData.txt", json);
                HoopManager.Save(zip);
                zip.Save(fileName);
            }
        }

        public void CopyToBuffer()
        {
            string data = JSON.Instance.ToJSON(_model);
            _hardwareBuffer.UpdateData(data);
        }

        public string Status { get; set; }
        public Color StatusColor { get; set; }

        /// <summary>
        /// Saves Values for UNDO
        /// </summary>
        public void StoreSyncedValues()
        {
            foreach (ControllerPattern pattern in Patterns)
            {
                pattern.StoreSyncedValues();
            }
        }

        /// <summary>
        /// Restores Values for UNDO
        /// </summary>
        public void RestoreSyncedValues()
        {
            foreach (ControllerPattern pattern in Patterns)
            {
                pattern.RestoreSyncedValues();
            }
        }

        public void SomethingHasChanged()
        {
            EventHandler somethingChanged = SomethingChanged;
            if (somethingChanged != null)
                somethingChanged(this, new EventArgs());
        }

        public void Connect()
        {
            _tcpClient = new Client("DigitalCard");
            _tcpClient.Connect(IPAddress.Parse(Ip), Port);
        }

        public void Disconnect()
        {
            _tcpClient.Disconnect();
        }

        private void PropertyChangedEvent(string propertyName)
        {
            if (_myGui.InvokeRequired)
            {
                GuiUpdate callback = PropertyChangedEvent;
                _myGui.Invoke(callback, propertyName);                
            }
            else
            {
                PropertyChangedEventHandler propertyChanged = PropertyChanged;
                if (null != propertyChanged)
                    propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private delegate void GuiUpdate(string propertyName);

        public event EventHandler SomethingChanged;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}