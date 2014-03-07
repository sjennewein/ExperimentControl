using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DigitalOutput.GUI;
using DigitalOutput.Model;
using Hulahoop;

namespace DigitalOutput
{
    public partial class DigitalMainwindow : Form
    {
        private readonly HulaHoopWindow _loops = new HulaHoopWindow();
        private readonly Manager _manager;

        public DigitalMainwindow()
        {
            var customCulture = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = customCulture;

            _manager = new Manager(this);
            _manager.BufferUnsynced += delegate { OnBufferUnsynced(); };
            _manager.BufferSynced += delegate { OnBufferSynced(); };
            _manager.DaqmxStarted += delegate { OnDaqmxStarted(); };
            _manager.DaqmxStopped += delegate { OnDaqmxStopped(); };

            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            Initialize();
        }

        private void Initialize(ModelCard model = null)
        {
            if (_manager.Hardware == null)
                _manager.Initialize();

            SuspendLayout();
            groupBox_Network.Controls.Add(new Network(_manager.Network));
            TabPanel_pattern.Controls.Clear();
            TabFabric.GenerateTabView(TabPanel_pattern, _manager.Hardware);
            label_CycleCounter.DataBindings.Add("Text", _manager, "CycleCounter");
            label_RunCounter.DataBindings.Add("Text", _manager, "RunCounter");
            label_CyclesPerRun.DataBindings.Add("Text", _manager, "CyclesPerRun");
            ResumeLayout(true);
        }


        private void button_Save_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Digital runs (*.drun)|*.drun|All files|*.*";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _manager.Save(saveFileDialog.FileName);
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            var loadFileDialog = new OpenFileDialog();
            string input;
            loadFileDialog.Filter = "Digital runs (*.drun)|*.drun|All files|*.*";
            loadFileDialog.RestoreDirectory = true;

            if (loadFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _manager.Load(loadFileDialog.FileName);

            label_CycleCounter.DataBindings.Clear();
            label_RunCounter.DataBindings.Clear();
            label_CyclesPerRun.DataBindings.Clear();
            _loops.ReDraw();
            Initialize();
        }

        private void button_Synchronize_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_manager.Hardware.Flow))
            {
                MessageBox.Show("No flow defined!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _manager.CopyToBuffer();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_manager.Hardware.Flow))
            {
                MessageBox.Show("No flow defined!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _manager.CopyToBuffer();
            _manager.Start();
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            _manager.Stop();
        }


        private void button_HulaHoop_Click(object sender, EventArgs e)
        {
            _loops.Visible = true;
            _loops.Focus();
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
                label_Buffer.BackColor = Color.FromArgb(127, 210, 21);
                label_Buffer.Text = "SYNCED";
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
                label_Buffer.BackColor = Color.FromArgb(196, 20, 94);
                label_Buffer.Text = "Nope";
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
                label_Status.Text = "Stopped";
                label_Status.BackColor = Color.FromArgb(196, 20, 94);
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
                label_Status.Text = "Started";
                label_Status.BackColor = Color.FromArgb(127, 210, 21);
            }
        }


        private delegate void GuiUpdate();
    }
}