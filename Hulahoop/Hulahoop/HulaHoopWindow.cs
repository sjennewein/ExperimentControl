using System;
using System.Drawing;
using System.Windows.Forms;
using Hulahoop.Controller;
using Hulahoop.GUI;
using Hulahoop.Model;

namespace Hulahoop
{
    public partial class HulaHoopWindow : Form
    {
        private int _fileIterator;
        private int _linearIterator;

        public HulaHoopWindow()
        {
            InitializeComponent();
        }

        private void button_Iterator_Click(object sender, EventArgs e)
        {
            var iterator = new ControllerLinearIterator(new ModelLinearIterator());
            iterator.Name = "Iterator" + _linearIterator;
            int yPosition = panel_LinearIterator.Controls.Count;
            HoopManager.LinearIterators.Add(iterator);
            panel_LinearIterator.Controls.Add(new LinearIteratorGui(iterator) {Location = new Point(0, 45*yPosition)});
            _linearIterator++;
        }

        private void button_FileIterator_Click(object sender, EventArgs e)
        {
            var iterator = new ControllerFileIterator(new ModelFileIterator());
            iterator.Name = "File-Iterator " + _fileIterator;
            int yPosition = panel_FileIterator.Controls.Count;
            HoopManager.FileIterators.Add(iterator);
            panel_FileIterator.Controls.Add(new FileIteratorGui(iterator) {Location = new Point(0, 45*yPosition)});
            _fileIterator++;
        }

        public void Remove(LinearIteratorGui linearIterator)
        {
            SuspendLayout();
            panel_LinearIterator.Controls.Remove(linearIterator);
            for (int iElement = 0; iElement < panel_LinearIterator.Controls.Count; iElement++)
            {
                panel_LinearIterator.Controls[iElement].Location = new Point(0, 45*iElement);
            }
            ResumeLayout();
        }

        public void ReDraw()
        {
            panel_LinearIterator.Controls.Clear();
            panel_FileIterator.Controls.Clear();
            foreach (ControllerLinearIterator iterator in HoopManager.LinearIterators)
            {
                int yPosition = panel_LinearIterator.Controls.Count;
                panel_LinearIterator.Controls.Add(new LinearIteratorGui(iterator)
                    {Location = new Point(0, 45*yPosition)});
            }
            foreach (ControllerFileIterator everyXRun in HoopManager.FileIterators)
            {
                int yPosition = panel_FileIterator.Controls.Count;
                panel_FileIterator.Controls.Add(new FileIteratorGui(everyXRun) {Location = new Point(0, 45*yPosition)});
            }
        }

        public void Remove(FileIteratorGui fileIterator)
        {
            SuspendLayout();
            panel_FileIterator.Controls.Remove(fileIterator);
            for (int iElement = 0; iElement < panel_FileIterator.Controls.Count; iElement++)
            {
                panel_FileIterator.Controls[iElement].Location = new Point(0, 45*iElement);
            }
            ResumeLayout();
        }

        private void HulaHoopWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            var window = (Form) sender;
            window.Visible = false;
        }
    }
}