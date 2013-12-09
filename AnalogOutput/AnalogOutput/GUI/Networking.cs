using System;
using System.Drawing;
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
            _network.Connected += delegate { OnConnected(); };
            _network.Disconnected += delegate { OnDisconnected(); };
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

        private void OnConnected()
        {
            label_Connection.Text = "Connected";
            label_Connection.BackColor = Color.FromArgb(127, 210, 21);
        }

        private  void OnDisconnected()
        {
            label_Connection.Text = "Disconnected";
            label_Connection.BackColor = Color.FromArgb(196, 20, 94);
        }
    }
}