using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Model;
using System.ComponentModel;

namespace DigitalOutput.Controller
{
    public class ControllerChannel : INotifyPropertyChanged
    {
        private readonly ModelData _model;
        private int _syncedValue;
        private Label _myControl;

        public void StoreSyncedValue()
        {            
            _syncedValue = _model.Value;
        }

        public void RestoreSyncedValue()
        {
            if(_syncedValue != _model.Value)
            {
                ChangeValue(new object(), new MouseEventArgs(MouseButtons.Left, 0,0,0,0));   
                
            }
        }

        public ControllerChannel(ModelData model, int channel)
        {
            _model = model;
            PickColor(channel);
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

        private void UpdateGUI()
        {
            //_myControl.BackColor = Value == 1 ? _onColor : _offColor;
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

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}