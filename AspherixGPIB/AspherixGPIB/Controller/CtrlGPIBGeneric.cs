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
        private FormattedIO488 ioArbFG = new FormattedIO488();
        private IMessage msg;

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
            _data.Iterator.Add(name);
        }

        private void UnregisterFromSubject()
        {
            foreach(String name in _data.Iterator)
            {
                foreach (IteratorSubject iterator in HoopManager.Iterators)
                {
                    if (iterator.Name() == name)
                        iterator.UnRegister(this);
                }
            }

            _data.Iterator.Clear();

        }

        public void NewValue(double value, string sender)
        {
            //needed to fulfill the interface
        }

        public void NewName(string newName, string oldName)
        {
            //needed to fulfill the interface
        }
    }
}