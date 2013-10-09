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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //ModelCard test = ModelFabric.GenerateCard();
            ControllerCard test = ControllerFabric.GenerateCard();
        }
    }
}
