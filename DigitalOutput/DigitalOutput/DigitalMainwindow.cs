using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalOutput.Controller;
using DigitalOutput.Model;

namespace DigitalOutput
{
    public partial class DigitalMainwindow : Form
    {
        private ControllerCard test;
        public DigitalMainwindow()
        {
            InitializeComponent();
            //ModelCard test = ModelFabric.GenerateCard();
            test = ControllerFabric.GenerateCard();
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            
                GUI.Helper.GenerateTabView(tabControl1, test);    
            
            
        }
    }
}
