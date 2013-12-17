using System;
using System.Drawing;
using System.Windows.Forms;
using AnalogOutput.Logic;
using Hulahoop.Controller;
using Hulahoop.Interface;

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
            label_IntialValue.DataBindings.Add("Text", _controller, "Unit", false,
                DataSourceUpdateMode.OnPropertyChanged);

            if (String.IsNullOrEmpty(_controller.Iterator))
                textBox_Value.DataBindings.Add("Text", _controller, "Value");
            else
                textBox_Value.DataBindings.Add("Text", _controller, "Iterator");


            textBox_Value.ContextMenu = new ContextMenu();
            textBox_Value.ContextMenu.Popup += OnContextMenu;
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

        private void OnContextMenu(object sender, EventArgs e)
        {
            var origSender = (ContextMenu) sender;

            var contextMenu = new ContextMenu();

            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                var item = new MenuItem(iterator.Name(), SwitchToHooping);
                contextMenu.MenuItems.Add(item);
            }

            contextMenu.MenuItems.Add(new MenuItem("Enable", SwitchToManual));
            contextMenu.Show(origSender.SourceControl, new Point(0));
        }

        private void SwitchToHooping(object sender, EventArgs e)
        {
            var item = (MenuItem) sender;
            var menu = (ContextMenu) item.Parent;
            var textBox = (TextBox) menu.SourceControl;

            textBox.ReadOnly = true;
            textBox.DataBindings.RemoveAt(0);

            _controller.Iterator = item.Text;
            textBox.DataBindings.Add("Text", _controller, "Iterator");
        }

        private void SwitchToManual(object sender, EventArgs e)
        {
            var item = (MenuItem) sender;
            var menu = (ContextMenu) item.Parent;
            var textBox = (TextBox) menu.SourceControl;

            textBox.ReadOnly = false;
            textBox.DataBindings.RemoveAt(0);

            _controller.Iterator = null;
            textBox.DataBindings.Add("Text", _controller, "Value");
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            _controller.ResetCalibration();
        }
    }
}