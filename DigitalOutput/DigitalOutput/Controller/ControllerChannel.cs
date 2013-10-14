using System;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerChannel
    {
        private readonly ModelData _model;

        public ControllerChannel(ModelData model)
        {
            _model = model;
        }

        public int Value
        {
            get { return _model.Value; }
            set { _model.Value = value; }
        }

        public void ChangeValue(object sender, EventArgs e)
        {
            var checkBox = (CheckBox) sender;
            if(checkBox.Checked)
            {
                Value = 1;
                checkBox.BackColor = Color.GreenYellow;
            }
            else
            {
                Value = 0;
                checkBox.BackColor = Color.Red;
            }
        }
    }
}