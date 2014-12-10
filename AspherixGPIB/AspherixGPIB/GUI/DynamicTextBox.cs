using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AspherixGPIB.Controller;
using Hulahoop.Controller;
using Hulahoop.Interface;

namespace AspherixGPIB.GUI
{
    public class DynamicTextBox : TextBox
    {
        private CtrlGPIBArbParam _controller;
        public DynamicTextBox()
        {
            ContextMenu = new ContextMenu();
            ContextMenu.Popup += OnContextMenu;
        }

        public void SetController(CtrlGPIBArbParam controller)
        {
            _controller = controller;
            _controller.DataLoaded += (sender, args) => UpdateGUI();
            if(string.IsNullOrEmpty(_controller.Iterator))
                SwitchToManual(new object(), new EventArgs());
            else
                ActivateLoops();                     
        }

        private void OnContextMenu(object sender, EventArgs e)
        {
            var origSender = (ContextMenu)sender;

            var contextMenu = new ContextMenu();

            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                var item = new MenuItem(iterator.Name(), SwitchToLoops);
                contextMenu.MenuItems.Add(item);
            }

            contextMenu.MenuItems.Add(new MenuItem("Enable", SwitchToManual));
            contextMenu.Show(origSender.SourceControl, new Point(0));
        }

        private void SwitchToLoops(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            _controller.Iterator = item.Text;
            ActivateLoops();
        }

        private void ActivateLoops()
        {
            ReadOnly = true;
            DataBindings.Clear();
            DataBindings.Add("Text", _controller, "Iterator");
        }

        private void SwitchToManual(object sender, EventArgs e)
        {                       
            ReadOnly = false;
            DataBindings.Clear();
            DataBindings.Add("Text", _controller, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void UpdateGUI()
        {
            if (string.IsNullOrEmpty(_controller.Iterator))
                SwitchToManual(new object(), new EventArgs());
            else
                ActivateLoops();    
        }
    }
}
