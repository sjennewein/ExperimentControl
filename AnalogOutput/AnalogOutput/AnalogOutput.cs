using System;
using System.Drawing;
using System.Windows.Forms;
using AnalogOutput.GUI;
using Hulahoop;

namespace AnalogOutput
{
    public partial class AnalogOutput : Form
    {
        private readonly Controller _controller;
        private readonly HulaHoopWindow _loops = new HulaHoopWindow();

        public AnalogOutput()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            _controller = new Controller(this);
            _controller.BufferUnsynced += delegate { OnBufferUnsynced(); };
            _controller.BufferSynced += delegate { OnBufferSynced(); };
            _controller.DaqmxStarted += delegate { OnDaqmxStarted(); };
            _controller.DaqmxStopped += delegate { OnDaqmxStopped(); };

            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            if (_controller.Hardware == null)
                _controller.Initialize();

            SuspendLayout();
            panel_Network.Controls.Add(new Networking(_controller.Network));
            tabControl_pattern.Controls.Clear();
            TabFabric.GenerateTabView(tabControl_pattern, _controller.Hardware);
            label_CycleCounter.DataBindings.Add("Text", _controller, "CycleCounter");
            label_RunCounter.DataBindings.Add("Text", _controller, "RunCounter");
            label_CyclePerRun.DataBindings.Add("Text", _controller, "CyclesPerRun");
            ResumeLayout();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Analog runs (*.arun)|*.arun|All files|*.*";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _controller.Save(saveFileDialog.FileName);
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            var loadFileDialog = new OpenFileDialog();            
            loadFileDialog.Filter = "Analog runs (*.arun)|*.arun|All files|*.*";
            loadFileDialog.RestoreDirectory = true;

            if (loadFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _controller.Load(loadFileDialog.FileName);


            label_CycleCounter.DataBindings.Clear();
            label_RunCounter.DataBindings.Clear();
            label_CyclePerRun.DataBindings.Clear();
            _loops.ReDraw();
            Initialize();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_controller.Hardware.Flow))
            {
                MessageBox.Show("No flow defined!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 

            _controller.Start();
        }

        private void button_Hoops_Click(object sender, EventArgs e)
        {
            _loops.Visible = true;
            _loops.Focus();
        }

        private void button_Sync_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_controller.Hardware.Flow))
            {
                MessageBox.Show("No flow defined!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }               
            _controller.CopyToBuffer();
            
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            _controller.Stop();
        }

        private void OnBufferSynced()
        {
            if (InvokeRequired)
            {
                GuiUpdate callback = OnBufferSynced;
                Invoke(callback);
            }
            else
            {
                label_Sync.BackColor = Color.FromArgb(127, 210, 21);
                label_Sync.Text = "SYNCED";
            }
            
        }

        private void OnBufferUnsynced()
        {
            if (InvokeRequired)
            {
                GuiUpdate callback = OnBufferUnsynced;
                Invoke(callback);
            }
            else
            {
                label_Sync.BackColor = Color.FromArgb(196, 20, 94);
                label_Sync.Text = "UNSYNCED";
            }
        }

        private void OnDaqmxStarted()
        {
            if (InvokeRequired)
            {
                GuiUpdate callback = OnDaqmxStarted;
                Invoke(callback);
            }
            else
            {
                label_Hardware.BackColor = Color.FromArgb(127, 210, 21);
                label_Hardware.Text = "RUNNING";
            }
        }

        private void OnDaqmxStopped()
        {
            if (InvokeRequired)
            {
                GuiUpdate callback = OnDaqmxStopped;
                Invoke(callback);
            }
            else
            {
                label_Hardware.BackColor = Color.FromArgb(196, 20, 94);
                label_Hardware.Text = "STOPPED";
            }
        }

        private delegate void GuiUpdate();

    }
}