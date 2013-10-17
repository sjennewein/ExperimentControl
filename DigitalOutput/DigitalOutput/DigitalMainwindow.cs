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
        private ControllerCard card;
        public DigitalMainwindow()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            //ModelCard card = ModelFabric.GenerateCard();
            Console.WriteLine(Width);
            card = ControllerFabric.GenerateCard();
            SuspendLayout();
            GUI.Helper.GenerateTabView(General, card);
            ResumeLayout();
        }
       
    }
}
