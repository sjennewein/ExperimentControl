using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AnalogOutput.Data;
using AnalogOutput.Logic;
using Hulahoop.Controller;

namespace AnalogOutput.GUI
{
    public partial class Step : UserControl
    {
        private readonly LogicStep _controller;

        public Step(LogicStep controller)
        {
            _controller = controller;
            InitializeComponent();
            textBox_Name.DataBindings.Add("Text", _controller, "Description");
            textBox_Duration.DataBindings.Add("Text", _controller, "Duration");
            textBox_Voltage.DataBindings.Add("Text", _controller, "Value");
            textBox_Duration.ContextMenu = new ContextMenu();
            textBox_Voltage.ContextMenu = new ContextMenu();
            textBox_Voltage.ContextMenu.Popup += OnContextMenu;
            textBox_Duration.ContextMenu.Popup += OnContextMenu;

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
            var button = (RadioButton) sender;

            switch (button.Text)
            {
                case "File":
                    _controller.Type = StepType.File;
                    button_File.Visible = true;
                    textBox_Duration.Enabled = false;
                    textBox_Voltage.Enabled = false;
                    break;
                case "Manual":
                    _controller.Type = StepType.Manual;
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

            if (duration%2 != 0 || duration < 0)
            {
                MessageBox.Show("Only positive numbers and multiples of 2 are allowed.", "Invalid Input",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Only values from -10 to 10 are valid.", "Invalid Input", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void OnContextMenu(object sender, EventArgs e)
        {
            var origSender = (ContextMenu) sender;

            var contextMenu = new ContextMenu();

            foreach (ControllerIterator iterator in HoopManager.LinearIterators)
            {
                var item = new MenuItem(iterator.Name, SwitchToHooping);
                contextMenu.MenuItems.Add(item);
            }

            contextMenu.MenuItems.Add(new MenuItem("Enable", SwitchToManual));
            contextMenu.Show(origSender.SourceControl, new Point(0));
        }

        public void SwitchToHooping(object sender, EventArgs e)
        {
            var item = (MenuItem) sender;
            var menu = (ContextMenu) item.Parent;
            var textBox = (TextBox) menu.SourceControl;

            textBox.ReadOnly = true;
            textBox.DataBindings.RemoveAt(0);

            switch (textBox.Name)
            {
                case "textBox_Voltage":
                    _controller.ValueIterator = item.Text;                    
                    textBox.DataBindings.Add("Text", _controller, "ValueIterator");
                    break;
                case "textBox_Duration":
                    _controller.DurationIterator = item.Text;                    
                    textBox.DataBindings.Add("Text", _controller, "DurationIterator");
                    break;
            }
        }

        public void SwitchToManual(object sender, EventArgs e)
        {
            var item = (MenuItem) sender;
            var menu = (ContextMenu) item.Parent;
            var textBox = (TextBox) menu.SourceControl;

            
            
            textBox.ReadOnly = false;
            textBox.DataBindings.RemoveAt(0);

            switch(textBox.Name)
            {
                case "textBox_Voltage":
                    _controller.ValueIterator = null;                    
                    textBox.DataBindings.Add("Text", _controller, "ValueIterator");
                    break;                    
                case "textBox_Duration":
                    _controller.DurationIterator = null;
                    textBox.DataBindings.Add("Text", _controller, "DurationIterator");
                    break;
            }
            
        }
    }
}