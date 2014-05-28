using System;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Controller;

namespace DigitalOutput.GUI
{
    public partial class Network : UserControl
    {
        private readonly ControllerNetwork _network;

        public Network(ControllerNetwork network)
        {
            _network = network;
            InitializeComponent();
            textBox_Ip.DataBindings.Add("Text", _network, "Ip");
            textBox_Port.DataBindings.Add("Text", _network, "Port");
            _network.Connected += delegate { OnConnected(); };
            _network.Disconnected += delegate { OnDisconnected(); };
        }

        private void checkBox_Network_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox) sender;
            if (checkBox.Checked)
            {
                textBox_Ip.Enabled = true;
                textBox_Port.Enabled = true;
                _network.Activated = true;
            }
            else
            {
                textBox_Ip.Enabled = false;
                textBox_Port.Enabled = false;
                _network.Activated = false;
            }
        }

        private void OnConnected()
        {
            label_Connection.Text = "Connected";
            label_Connection.BackColor = Color.FromArgb(127, 210, 21);
        }

        private void OnDisconnected()
        {
            if (InvokeRequired)
            {
                GuiUpdate callback = OnDisconnected;
                Invoke(callback);
            }
            else
            {
            label_Connection.Text = "Disconnected";
            label_Connection.BackColor = Color.FromArgb(196, 20, 94);
            }
        }
        private delegate void GuiUpdate();
    }
}