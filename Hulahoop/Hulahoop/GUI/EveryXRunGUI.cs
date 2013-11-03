using System.Windows.Forms;
using Hulahoop.Controller;

namespace Hulahoop.GUI
{
    public partial class EveryXRunGUI : UserControl
    {
        private readonly ControllerEveryXRun _controller;

        public EveryXRunGUI(ControllerEveryXRun controller)
        {
            _controller = controller;
            InitializeComponent();
            textBox_XRun.DataBindings.Add("Text", _controller, "EveryXthRun", false,
                                          DataSourceUpdateMode.OnPropertyChanged);
            textBox_Name.DataBindings.Add("Text", _controller, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            label_Close.MouseClick += Delete;
        }

        public void Delete(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            HoopManager.EveryXRun.Remove(_controller);
            var label = (Label) sender;
            var parent = (HulahoopDigital) label.Parent.Parent.Parent;
            parent.Remove(this);
        }
    }
}