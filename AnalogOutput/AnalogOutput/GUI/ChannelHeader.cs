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

        private void button_Calibrate_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            DialogResult dr = fileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {                
                try
                {
                    _controller.LoadFile(fileDialog.FileName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
