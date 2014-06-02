using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Model;
using System.ComponentModel;
using Hulahoop.Controller;
using Hulahoop.Interface;

namespace DigitalOutput.Controller
{
    public class ControllerChannel : INotifyPropertyChanged, IteratorObserver
    {
        private readonly ModelData _model;
        
        private ControllerStep _parent;
        //private Label _myControl;

        private void SomethingHasChanged()
        {
            _parent.SomethingHasChanged();
        }
        

        public ControllerChannel(ModelData model, int channel, ControllerStep parent)
        {
            _parent = parent;
            _model = model;
            PickColor(channel);

            if(Iterator != null)
            {
                if(_model.Value == 1)
                    Color = Color.FromArgb(0, 255, 0);
                else
                    Color = Color.FromArgb(255, 0, 0);

                return;
            }

            if(_model.Value == 1)
            {
                Color = _onColor;
            }
            else
            {
                Color = _offColor;
            }
        }

        public int Value
        {
            get { return _model.Value; }
            set
            {
                //Console.WriteLine(value);
                SomethingHasChanged();
                _model.Value = value;
            }
        }

        public Color Color { get; set; }
        private Color _onColor; 
        private Color _offColor; 

        public void ChangeValue(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;           
            
            if(Value == 0)
            {
                Value = 1;
                Color = _onColor;
            }
            else
            {
                Value = 0;
                Color = _offColor;
            }
            PropertyHasChanged("Color");
        }

        private void PickColor(int channel)
        {
            switch(channel % 4)
            {
                case 0:
                    _onColor = Color.FromArgb(0xF0,0x69,0xA2);                    
                    _offColor = Color.FromArgb(0x7A, 0x36, 0x52);                    
                    break;
                case 1:
                    _onColor = Color.FromArgb(0xF9,0xBE,0x6D);
                    _offColor = Color.FromArgb(0x8F, 0x6D, 0x3F);
                    break;
                case 2:
                    _onColor = Color.FromArgb(0x6D, 0xAE, 0xE4);
                    _offColor = Color.FromArgb(0x2C, 0x47, 0x5D);
                    break;
                case 3:
                    _onColor = Color.FromArgb(0xB8, 0xF4, 0x6B);
                    _offColor = Color.FromArgb(0x63, 0x83, 0x39);
                    break;
            }
                
        }

        private void PropertyHasChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (null != propertyChanged)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Iterator
        {
            get { return _model.Iterator; }
            set
            {                
                UnregisterFromSubject(_model.Iterator);
                _model.Iterator = value;
                RegisterToSubject(_model.Iterator);
            }
        }

        public void SetIteratorColor()
        {
            //Color = Color.FromArgb(255, 0, 0);
            //PropertyHasChanged("Color");
        }

        public void UnSetIteratorColor()
        {
            if (Value == 1)
                Color = _onColor;
            else
                Color = _offColor;
        
            PropertyHasChanged("Color");               
        }


        private void RegisterToSubject(string iteratorName)
        {
            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                if (iterator.Name() == iteratorName)
                    iterator.Register(this);
            }
        }

        private void UnregisterFromSubject(string iteratorName)
        {
            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                if (iterator.Name() == iteratorName)
                    iterator.UnRegister(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NewValue(double value, string sender)
        {
            if(value > 0)
            {
                Value = 1;
                Color = Color.FromArgb(0, 255, 0);
                PropertyHasChanged("Color");
            }
            else
            {
                Value = 0;
                Color = Color.FromArgb(255, 0, 0);
                PropertyHasChanged("Color");
            }
        }

        public void NewName(string newName, string oldName)
        {
            _model.Iterator = newName;
            PropertyHasChanged("Iterator");
        }
    }
}