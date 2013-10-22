using System;
using System.Drawing;
using System.Windows.Forms;
using Hulahoop.Controller;
using Hulahoop.GUI;
using Hulahoop.Model;

namespace Hulahoop
{
    public partial class HulahoopDigital : Form
    {
        public HulahoopDigital()
        {
            InitializeComponent();
        }

        private void button_Iterator_Click(object sender, EventArgs e)
        {
            var newIterator = new ControllerIterator(new ModelIterator());
            int yPosition = panel_Iterator.Controls.Count;
            HoopManager.Iterators.Add(newIterator);
            panel_Iterator.Controls.Add(new IteratorGUI(newIterator) {Location = new Point(0, 45*yPosition)});
        }

        private void button_EveryXthrun_Click(object sender, EventArgs e)
        {
            var newEveryXRun = new ControllerEveryXRun(new ModelRunEveryX());
            int yPosition = panel_everyRun.Controls.Count;
            HoopManager.EveryXRun.Add(newEveryXRun);
            panel_everyRun.Controls.Add(new EveryXRunGUI(newEveryXRun) {Location = new Point(0, 45*yPosition)});
        }

        private void HulahoopDigital_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            var window = (Form) sender;
            window.Visible = false;
        }

        private void button_EveryXthRun_Click(object sender, EventArgs e)
        {

        }
    }
}