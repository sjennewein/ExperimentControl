using System;
using System.Windows.Forms;
using AnalogOutput.GUI;
using Hulahoop;

namespace AnalogOutput
{
    public partial class AnalogOutput : Form
    {
        private readonly Controller _controller = new Controller();
        private readonly HulaHoopAnalog _loops = new HulaHoopAnalog();

        public AnalogOutput()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            if (_controller.Hardware == null)
                _controller.Initialize();

            SuspendLayout();
            panel_Network.Controls.Add(new Networking(_controller.Network));
            tabControl_pattern.Controls.Clear();
            TabFabric.GenerateTabView(tabControl_pattern, _controller.Hardware);
            ResumeLayout();
        }


        private void button_Save_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Analog runs (*.arun)|*.arun|All files|*.*";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _controller.Save(saveFileDialog.FileName);
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            var loadFileDialog = new OpenFileDialog();
            string input;
            loadFileDialog.Filter = "Analog runs (*.arun)|*.arun|All files|*.*";
            loadFileDialog.RestoreDirectory = true;

            if (loadFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _controller.Load(loadFileDialog.FileName);

            Initialize();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            _controller.Start();
        }

        private void button_Hoops_Click(object sender, EventArgs e)
        {
            _loops.Visible = true;
            _loops.Focus();
        }
    }
}