using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hulahoop.Controller;

namespace Hulahoop.GUI
{
    public partial class EveryXRunGUI : UserControl
    {
        private ControllerEveryXRun _controller;

        public EveryXRunGUI(ControllerEveryXRun controller)
        {
            _controller = controller;
            InitializeComponent();
            textBox_XRun.DataBindings.Add("Text", _controller, "EveryXthRun", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_Name.DataBindings.Add("Text", _controller, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
