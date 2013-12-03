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
    public partial class LinearIteratorGui : UserControl
    {
        private ControllerLinearIterator _controllerLinear;

        public LinearIteratorGui(ControllerLinearIterator controller)
        {
            _controllerLinear = controller;
            InitializeComponent();
            textBox_Name.DataBindings.Add("Text", _controllerLinear, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_Start.DataBindings.Add("Text", _controllerLinear, "Start", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_Stop.DataBindings.Add("Text", _controllerLinear, "Stop", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_Stepsize.DataBindings.Add("Text", _controllerLinear, "StepSize", false,
                                              DataSourceUpdateMode.OnPropertyChanged);
            label_Close.MouseClick += Delete;
        }

        public void Delete(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            HoopManager.LinearIterators.Remove(_controllerLinear);            
            var label = (Label) sender;
            var parent = (HulaHoopWindow) label.Parent.Parent.Parent;
            parent.Remove(this);
        }
    }
}
