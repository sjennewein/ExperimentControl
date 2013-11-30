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
    public partial class ChannelHeader : UserControl
    {
        private readonly LogicChannel _controller;
        public ChannelHeader(LogicChannel controller)
        {
            _controller = controller;
            InitializeComponent(); 

            textBox_Name.DataBindings.Add("Text", _controller, "Name");
            textBox_Value.DataBindings.Add("Text", _controller, "Value");                       
        }
    }
}
