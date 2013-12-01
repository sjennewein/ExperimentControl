using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnalogOutput.Data;
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
            if (_controller.Type == StepType.File)
            {
                button_File.Visible = true;
                radioButton_File.Checked = true;
            }
        }

        private void button_File_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            DialogResult dr = fileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                _controller.FileName = fileDialog.FileName;
                try
                {
                    _controller.LoadFile();    
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
                
        }

        private void radioButton_InputMethod_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton) sender;
            
            switch(button.Text)
            {
                case "File":
                    _controller.Type = StepType.File;
                    button_File.Visible = true;
                    textBox_Duration.Enabled = false;
                    textBox_Voltage.Enabled = false;
                    break;
                case "Manual":
                    _controller.Type = StepType.GUI;
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

        private void textBox_Voltage_Validating(object sender, CancelEventArgs e)
        {
            var textBox = (TextBox) sender;
            double value = 11;
            try
            {
                value = Convert.ToDouble(textBox.Text);
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Not a valid integer", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (Math.Abs(value) > 10)
            {
                MessageBox.Show("Only values from -10 to 10 are valid.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
}
}
