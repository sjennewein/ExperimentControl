using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Model;
using System.ComponentModel;

namespace DigitalOutput.Controller
{
    public class ControllerChannel 
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
                _model.Value = _syncedValue;   
                UpdateGUI();
            }
        }

        public ControllerChannel(ModelData model, int channel)
        {
            _model = model;
            PickColor(channel);
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

        public Color OnColor { get; set; }
        public Color OffColor { get; set; }

        public void ChangeValue(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            _myControl = (Label) sender;
            
            if(Value == 0)
            {
                Value = 1;
                _myControl.BackColor = OnColor;
            }
            else
            {
                Value = 0;
                _myControl.BackColor = OffColor;
            }
        }

        private void UpdateGUI()
        {
            _myControl.BackColor = Value == 1 ? OnColor : OffColor;
        }

        private void PickColor(int channel)
        {
            switch(channel % 4)
            {
                case 0:
                    OnColor = Color.FromArgb(0xF0,0x69,0xA2);                    
                    OffColor = Color.FromArgb(0x7A, 0x36, 0x52);
                    break;
                case 1:
                    OnColor = Color.FromArgb(0xF9,0xBE,0x6D);
                    OffColor = Color.FromArgb(0x8F, 0x6D, 0x3F);
                    break;
                case 2:
                    OnColor = Color.FromArgb(0x6D, 0xAE, 0xE4);
                    OffColor = Color.FromArgb(0x2C, 0x47, 0x5D);
                    break;
                case 3:
                    OnColor = Color.FromArgb(0xB8, 0xF4, 0x6B);
                    OffColor = Color.FromArgb(0x63, 0x83, 0x39);
                    break;
            }
                
        }

       
    }
}