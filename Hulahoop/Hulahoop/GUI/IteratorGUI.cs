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
    public partial class IteratorGUI : UserControl
    {
        private ControllerIterator _controller;

        public IteratorGUI(ControllerIterator controller)
        {
            _controller = controller;
            InitializeComponent();
            textBox_Name.DataBindings.Add("Text", _controller, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_Start.DataBindings.Add("Text", _controller, "Start", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_Stop.DataBindings.Add("Text", _controller, "Stop", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_Stepsize.DataBindings.Add("Text", _controller, "StepSize", false,
                                              DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
