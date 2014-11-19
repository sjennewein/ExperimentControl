using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AspherixGPIB.Data;
using Hulahoop.Controller;
using Hulahoop.Interface;
using Ionic.Zip;
using Ivi.Visa.Interop;
using fastJSON;

namespace AspherixGPIB.Controller
{
    public class CtrlGPIBGeneric : IteratorObserver, INotifyPropertyChanged
    {
        private DataGPIBGeneric _data = new DataGPIBGeneric();
        private ResourceManager rm;
        private FormattedIO488 _gpib = new FormattedIO488();
        private IMessage msg;
        public bool Activated = false;

        private enum gpibState
        {
            Connected,
            Disconnected
        };

        private gpibState _deviceState = gpibState.Disconnected;

        public string Address
        {
            get { return _data.Address; }
            set { _data.Address = value; }
        }

        public string Commands
        {
            get { return _data.Commands; }
            set { _data.Commands = value; }
        }      

        public void ManualSet()
        {       
            GpibSendMessage();
        }

        public void Update()
        {
            if (Activated)
                GpibSendMessage();
        }

        public void ManualDisconnect()
        {
            if(_deviceState == gpibState.Connected)
                GpibDisconnect();
        }

        private void GpibSendMessage()
        {
            if(_deviceState == gpibState.Disconnected)
                GpibConnect();

            string message = Commands;
            foreach (KeyValuePair<string, double> pair in _data.Iterator)
            {
                string key = pair.Key;
                string value = Convert.ToString(pair.Value);
                message = message.Replace(key, value);
            }
            _gpib.WriteString(message);
        }

        public void CheckText(object sender, EventArgs e)
        {
            var textBox = (RichTextBox) sender;
            textBox.SelectAll();
            textBox.SelectionBackColor = Color.White;
            textBox.SelectionColor = Color.Black;
            textBox.SelectionLength = 0;
            UnregisterFromSubject();
            _data.Commands = textBox.Text;
            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                var name = iterator.Name();
                var remainder = textBox.Text;
                int position = 0;
                int index = 0;
                bool registered = false;
                while((index = remainder.IndexOf(name)) != -1)
                {                    
                    position += index;
                    remainder = remainder.Substring(index + name.Length);
                    textBox.Select(position, name.Length);
                    position += name.Length;
                    textBox.SelectionBackColor = Color.Blue;
                    textBox.SelectionColor = Color.White;
                    if(!registered)
                    {
                        RegisterToSubject(name);
                        registered = true;
                    }
                }                
            }
            textBox.Select(textBox.Text.Length,0);
        }

        private void RegisterToSubject(string name)
        {
            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                if (iterator.Name() == name)
                    iterator.Register(this);
            }            
        }

        private void UnregisterFromSubject()
        {
            foreach(KeyValuePair<string,double> pair in _data.Iterator)
            {
                foreach (IteratorSubject iterator in HoopManager.Iterators)
                {
                    if (iterator.Name() == pair.Key)
                        iterator.UnRegister(this);
                }
            }

            _data.Iterator.Clear();
        }

        private void GpibDisconnect()
        {
            if (_gpib.IO == null)
                return;

            _gpib.IO.Close();

            _deviceState = gpibState.Disconnected;

        }

        private void GpibConnect()
        {
            rm = new ResourceManager();
            try
            {
                msg = (rm.Open(Address)) as IMessage;
                _gpib.IO = msg;
                _deviceState = gpibState.Connected;    
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Open failed on " + Address + " " + ex.Source + "  " + ex.Message, "ApplyBurst");
                _gpib.IO = null;
                _deviceState = gpibState.Disconnected;                    
            }
                    
   
        }

        public void NewValue(double value, string sender)
        {            
            _data.Iterator.Remove(sender);
            _data.Iterator.Add(sender,value);
        }

        public void NewName(string newName, string oldName)
        {
            //needed to fulfill the interface
        }

        private void GetInstrumentID()
        {
            string m_strReturn;
            _gpib.WriteString("*RST");
            _gpib.WriteString("*IDN?");
            
            Console.Write(_gpib.ReadString());
            
        }

        public void FromJSON(string gpibGeneric)
        {
            DataGPIBGeneric data = (DataGPIBGeneric)fastJSON.JSON.Instance.ToObject(gpibGeneric);
            _data.Commands = data.Commands;
            _data.Address = data.Address;

            foreach (KeyValuePair<string, double> pair in data.Iterator)
            {
                RegisterToSubject(pair.Key);
            }

            PropertyHasChanged("Commands");
        }

        public void Save(ZipFile zip)
        {
            string json = JSON.Instance.ToJSON(_data);
            zip.AddEntry("GPIBGeneric.txt", json);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropertyHasChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (null != propertyChanged)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}