using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AspherixGPIB.Data;
using Hulahoop.Controller;
using Hulahoop.Interface;
using Ivi.Visa.Interop;

namespace AspherixGPIB.Controller
{
    public class CtrlGPIBGeneric : IteratorObserver
    {
        private DataGPIBGeneric _data;
        private ResourceManager rm;
        private FormattedIO488 _gpib = new FormattedIO488();
        private IMessage msg;

        private enum gpibState
        {
            Connected,
            Disconnected
        };

        private gpibState _deviceState = gpibState.Disconnected;

        public CtrlGPIBGeneric(DataGPIBGeneric data = null)
        {
            if (data == null)
                _data = new DataGPIBGeneric();
            else
                _data = data;
        }

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
            if (_deviceState == gpibState.Disconnected)
                GpibConnect();
            //GetInstrumentID();
            GpibSendMessage();
        }

        public void ManualDisconnect()
        {
            if(_deviceState == gpibState.Connected)
                GpibDisconnect();
        }

        private void GpibSendMessage()
        {
            string message = Commands;
            foreach (KeyValuePair<string, double> pair in _data.Iterator)
            {
                string key = pair.Key;
                string value = Convert.ToString(pair.Value);
                message = message.Replace(key, value);
            }
            _gpib.WriteString(message);
            //System.Console.Write(_gpib.ReadString());
        }

        public void CheckText(object sender, EventArgs e)
        {
            var textBox = (RichTextBox) sender;
            textBox.SelectAll();
            textBox.SelectionBackColor = Color.White;
            textBox.SelectionColor = Color.Black;
            textBox.SelectionLength = 0;
            UnregisterFromSubject();
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
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Open failed on " + Address + " " + ex.Source + "  " + ex.Message, "ApplyBurst");
                _gpib.IO = null;
                return;

            }
            _deviceState = gpibState.Connected;            
   
        }

        public void NewValue(double value, string sender)
        {
            _data.Iterator.RemoveAll(kvp => kvp.Key == sender);
            _data.Iterator.Add(new KeyValuePair<string, double>(sender,value));
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
    }
}