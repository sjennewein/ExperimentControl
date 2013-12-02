using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnalogOutput.Logic;

namespace AnalogOutput.GUI
{
    public partial class Networking : UserControl
    {
        private readonly LogicNetwork _controller;

        public Networking(LogicNetwork controller)
        {
            _controller = controller;
            InitializeComponent();
            textBox_IP.DataBindings.Add("Text", _controller, "Ip");
            textBox_Port.DataBindings.Add("Text", _controller, "Port");
        }
    }
}
