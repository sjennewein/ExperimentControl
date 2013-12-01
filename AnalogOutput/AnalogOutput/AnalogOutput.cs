using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnalogOutput.Data;
using AnalogOutput.GUI;
using fastJSON;
using Ionic.Zip;

namespace AnalogOutput
{
    public partial class AnalogOutput : Form
    {
        private readonly Controller _controller = new Controller();

        public AnalogOutput()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize(string storedData = null)
        {
            _controller.Initialize(storedData);

            SuspendLayout();
            TabFabric.GenerateTabView(tabControl_pattern,_controller.Hardware);
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

            SuspendLayout();
            tabControl_pattern.Controls.Clear();
            TabFabric.GenerateTabView(tabControl_pattern, _controller.Hardware);
            ResumeLayout();
        }
    }
}
