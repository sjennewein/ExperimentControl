using System;
using System.Windows.Forms;
using Hulahoop.Controller;

namespace Hulahoop.GUI
{
    public partial class FileIteratorGui : UserControl
    {
        private readonly ControllerFileIterator _controller;

        public FileIteratorGui(ControllerFileIterator controller)
        {
            _controller = controller;
            InitializeComponent();

            textBox_Filename.DataBindings.Add("Text", _controller, "FileName");
            textBox_Name.DataBindings.Add("Text", _controller, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            label_Length.DataBindings.Add("Text", _controller, "Lines");
                
            label_Close.MouseClick += Delete;
        }

        public void Delete(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            HoopManager.FileIterators.Remove(_controller);
            var label = (Label) sender;
            var parent = (HulaHoopWindow) label.Parent.Parent.Parent;
            parent.Remove(this);
        }

        private void button_Load_Click(object sender, System.EventArgs e)
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
    }
}