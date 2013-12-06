using System;
using System.Windows.Forms;
using AnalogOutput.Logic;

namespace AnalogOutput.GUI
{
    public partial class Networking : UserControl
    {
        private readonly LogicNetwork _network;

        public Networking(LogicNetwork network)
        {
            _network = network;
            InitializeComponent();
            textBox_IP.DataBindings.Add("Text", _network, "Ip");
            textBox_Port.DataBindings.Add("Text", _network, "Port");
        }

        private void checkBox_Network_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox) sender;
            if (checkBox.Checked)
            {
                textBox_IP.Enabled = true;
                textBox_Port.Enabled = true;
                _network.Activated = true;
            }
            else
            {
                textBox_IP.Enabled = false;
                textBox_Port.Enabled = false;
                _network.Activated = false;
            }
        }
    }
}