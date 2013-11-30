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
    public partial class Step : UserControl
    {
        private readonly  LogicStep _controller;
        public Step(LogicStep controller)
        {
            _controller = controller;
            InitializeComponent();
            textBox_Name.DataBindings.Add("Text", _controller, "Description");
            textBox_Duration.DataBindings.Add("Text", _controller, "Duration");
            textBox_Voltage.DataBindings.Add("Text", _controller, "Value");
        }

        private void button_File_Click(object sender, EventArgs e)
        {

        }

        private void radioButton_InputMethod_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton) sender;
            
            switch(button.Text)
            {
                case "File":
                    button_File.Visible = true;
                    textBox_Duration.Enabled = false;
                    textBox_Voltage.Enabled = false;
                    break;
                case "Manual":
                    button_File.Visible = false;
                    textBox_Duration.Enabled = true;
                    textBox_Voltage.Enabled = true;
                    break;
            }
        }    

        private void textBox_Duration_Validating(object sender, CancelEventArgs e)
        {
            var textBox = (TextBox) sender;
            int duration = -1;
            try
            {
                duration = Convert.ToInt32(textBox.Text);
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Not a valid integer", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            
            if (duration % 2 != 0 || duration < 0)
            {
                MessageBox.Show("Only positive numbers and multiples of 2 are allowed.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;                
            }
        }
}
}
