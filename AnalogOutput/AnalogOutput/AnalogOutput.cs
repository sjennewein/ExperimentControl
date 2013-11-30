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
    }
}
