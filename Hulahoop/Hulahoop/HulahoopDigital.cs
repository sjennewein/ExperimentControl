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
        private int _iterator = 0;
        private int _xthRun = 0;
        public HulahoopDigital()
        {
            InitializeComponent();
        }

        private void button_Iterator_Click(object sender, EventArgs e)
        {
            var newIterator = new ControllerLinearIterator(new ModelLinearIterator());
            newIterator.Name = "Iterator" + _iterator;
            int yPosition = panel_Iterator.Controls.Count;
            HoopManager.LinearIterators.Add(newIterator);
            panel_Iterator.Controls.Add(new LinearIteratorGui(newIterator) {Location = new Point(0, 45*yPosition)});
            _iterator++;
        }

        private void button_EveryXthRun_Click(object sender, EventArgs e)
        {
            var newEveryXRun = new ControllerFileIterator(new ModelFileIterator());
            newEveryXRun.Name = "XthRun" + _xthRun;
            int yPosition = panel_everyRun.Controls.Count;
            HoopManager.FileIterators.Add(newEveryXRun);
            panel_everyRun.Controls.Add(new FileIteratorGui(newEveryXRun) {Location = new Point(0, 45*yPosition)});
            _xthRun++;
        }

        private void HulahoopDigital_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            var window = (Form) sender;
            window.Visible = false;
        }

        public void ReLoad()
        {
            panel_Iterator.Controls.Clear();
            panel_everyRun.Controls.Clear();
            foreach (ControllerLinearIterator iterator in HoopManager.LinearIterators)
            {
                int yPosition = panel_Iterator.Controls.Count;
                panel_Iterator.Controls.Add(new LinearIteratorGui(iterator) {Location = new Point(0, 45*yPosition)});
            }
            foreach (ControllerFileIterator everyXRun in HoopManager.FileIterators)
            {
                int yPosition = panel_everyRun.Controls.Count;
                panel_everyRun.Controls.Add(new FileIteratorGui(everyXRun) {Location = new Point(0, 45*yPosition)});
            }
        }

        public void Remove(LinearIteratorGui linearIterator)
        {
            SuspendLayout();
            panel_Iterator.Controls.Remove(linearIterator);
            for (int iElement = 0; iElement < panel_Iterator.Controls.Count; iElement++)
            {
                panel_Iterator.Controls[iElement].Location = new Point(0, 45*iElement);
            }
            ResumeLayout();
        }

        public void Remove(FileIteratorGui fileIterator)
        {
            SuspendLayout();
            panel_everyRun.Controls.Remove(fileIterator);
            for (int iElement = 0; iElement < panel_everyRun.Controls.Count; iElement++)
            {
                panel_everyRun.Controls[iElement].Location = new Point(0, 45*iElement);
            }
            ResumeLayout();
        }

        
    }
}