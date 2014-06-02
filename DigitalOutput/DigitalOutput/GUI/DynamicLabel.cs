using System;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Controller;
using Hulahoop.Controller;
using Hulahoop.Interface;

namespace DigitalOutput.GUI
{
    public class DynamicLabel : Label
    {
        private readonly ControllerChannel _controller;
        public DynamicLabel(ControllerChannel controller)
        {
            _controller = controller;

            if(String.IsNullOrEmpty(_controller.Iterator))
                MouseClick += _controller.ChangeValue;

            DataBindings.Add("BackColor", _controller, "Color", false,
                             DataSourceUpdateMode.OnPropertyChanged);
            DataBindings.Add("Text", _controller, "Iterator", false,
                             DataSourceUpdateMode.OnPropertyChanged);

            
            ContextMenu = new ContextMenu();
            ContextMenu.Popup += OnContextMenu;
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
            
            MouseClick -= _controller.ChangeValue;
            _controller.Iterator = item.Text;           
        }

        private void SwitchToManual(object sender, EventArgs e)
        {             
            if(_controller.Iterator != null)
                MouseClick += _controller.ChangeValue;
            _controller.Iterator = null;
            _controller.UnSetIteratorColor();        
        }

    }
}