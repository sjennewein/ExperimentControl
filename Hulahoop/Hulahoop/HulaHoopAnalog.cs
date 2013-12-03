using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hulahoop.Controller;
using Hulahoop.GUI;
using Hulahoop.Model;

namespace Hulahoop
{
    public partial class HulaHoopAnalog : Form
    {
        private int _iterator = 0;
        public HulaHoopAnalog()
        {
            InitializeComponent();
        }

        private void button_Iterator_Click(object sender, EventArgs e)
        {
            var newIterator = new ControllerIterator(new ModelIterator());
            newIterator.Name = "Iterator" + _iterator;
            int yPosition = panel_Iterator.Controls.Count;
            HoopManager.LinearIterators.Add(newIterator);
            panel_Iterator.Controls.Add(new LinearIteratorGui(newIterator) { Location = new Point(0, 45 * yPosition) });
            _iterator++;
        }
    }
}
