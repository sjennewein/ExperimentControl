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
        }

        private void OnContextMenu(object sender, EventArgs e)
        {
            var origSender = (ContextMenu)sender;

            var contextMenu = new ContextMenu();

            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                var item = new MenuItem(iterator.Name(), SwitchToHooping);
                contextMenu.MenuItems.Add(item);
            }

            contextMenu.MenuItems.Add(new MenuItem("Enable", SwitchToManual));
            contextMenu.Show(origSender.SourceControl, new Point(0));
        }

        private void SwitchToHooping(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            _controller.Iterator = item.Text;
            ReadOnly = true;
            DataBindings.RemoveAt(0);
            DataBindings.Add("Text", _controller, "Iterator");
        }

        private void SwitchToManual(object sender, EventArgs e)
        {           
            _controller.Iterator = null;
            ReadOnly = false;
            DataBindings.RemoveAt(0);
            DataBindings.Add("Text", _controller, "Duration", false, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
