using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DigitalOutput.Controller;
using DigitalOutput.GUI;
using DigitalOutput.Model;
using Hulahoop;
using Hulahoop.Controller;
using Ionic.Zip;
using fastJSON;
using Buffer = DigitalOutput.Hardware.Buffer;

namespace DigitalOutput
{
    public partial class DigitalMainwindow : Form
    {
        private readonly Buffer _buffer = new Buffer();
        private readonly HulahoopDigital _loops = new HulahoopDigital();
        private ControllerCard _card;

        public DigitalMainwindow()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            Console.WriteLine(Width);
            _buffer.Started += delegate { HardwareStarted(); };
            _buffer.Stopped += delegate { HardwareStopped(); };
            Initialize();
        }

        private void Initialize(ModelCard model = null)
        {
            _card = ControllerFabric.GenerateCard(_buffer, this, model);
            _card.RunDataChanged += RunDataChanged;

            if (model == null)
            {
                _card.Ip = "127.0.0.1";
                _card.Port = 9898;
            }
            else
            {
                textBox_Flow.DataBindings.RemoveAt(0);
                textBox_Ip.DataBindings.RemoveAt(0);
                textBox_Port.DataBindings.RemoveAt(0);
                label_CycleDone.DataBindings.RemoveAt(0);
                label_RunsDone.DataBindings.RemoveAt(0);
                label_CyclesPerRun.DataBindings.RemoveAt(0);
            }

            textBox_Ip.DataBindings.Add("Text", _card, "Ip", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_Port.DataBindings.Add("Text", _card, "Port", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_Flow.DataBindings.Add("Text", _card, "Flow", false, DataSourceUpdateMode.OnPropertyChanged);
            label_CycleDone.DataBindings.Add("Text", _card, "CyclesDone", false, DataSourceUpdateMode.OnPropertyChanged);
            label_RunsDone.DataBindings.Add("Text", _card, "RunsDone", false, DataSourceUpdateMode.OnPropertyChanged);
            label_CyclesPerRun.DataBindings.Add("Text", _card, "CyclesPerRun", false, DataSourceUpdateMode.OnPropertyChanged);

            if (model == null)
            {
                SuspendLayout();
                Helper.GenerateTabView(TabPanel, _card);
                ResumeLayout();
            }
            else
            {
                SuspendLayout();
                _loops.ReLoad();
                Helper.DisposeTabs(TabPanel);
                Helper.GenerateTabView(TabPanel, _card);
                ResumeLayout();
            }
        }

        private void RunDataChanged(object sender, EventArgs e)
        {
            label_Buffer.BackColor = Color.FromArgb(196, 20, 94);
            label_Buffer.Text = "Nope";
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Digital runs (*.drun)|*.drun|All files|*.*";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _card.Save(saveFileDialog.FileName);
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            var loadFileDialog = new OpenFileDialog();
            string input;
            loadFileDialog.Filter = "Digital runs (*.drun)|*.drun|All files|*.*";
            loadFileDialog.RestoreDirectory = true;

            if (loadFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (ZipFile zip = ZipFile.Read(loadFileDialog.FileName))
            {
                using (var ms = new MemoryStream())
                {
                    ZipEntry entry = zip["DigitalData.txt"];
                    entry.Extract(ms);
                    ms.Flush();
                    ms.Position = 0;
                    input = new StreamReader(ms).ReadToEnd();
                    ms.Close();
                }
                HoopManager.Load(zip); // has to be restored before the card fabric is called
            }

            var loadedCard = (ModelCard) JSON.Instance.ToObject(input);

            Initialize(loadedCard);
        }

        private void button_Synchronize_Click(object sender, EventArgs e)
        {
            label_Buffer.BackColor = Color.FromArgb(127, 210, 21);
            label_Buffer.Text = "Synced";
            _card.CopyToBuffer();
            _card.StoreSyncedValues();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (String.Equals(_card.Flow, String.Empty) || _card.Flow == null)
            {
                MessageBox.Show("You have to provide a \"Program flow\"!");
                return;
            }

            _card.Start();
            label_Buffer.BackColor = Color.FromArgb(127, 210, 21);
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            _card.Stop();
        }

        private void button_Undo_Click(object sender, EventArgs e)
        {
            _card.RestoreSyncedValues();
            label_Buffer.BackColor = Color.FromArgb(127, 210, 21);
            label_Buffer.Text = "Synced";
        }

        private void button_HulaHoop_Click(object sender, EventArgs e)
        {
            _loops.Visible = true;
            _loops.Focus();
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            _card.Connect();
        }

        private void checkBox_Network_CheckedChanged(object sender, EventArgs e)
        {
            var source = (CheckBox) sender;
            if (source.Checked)
            {
                textBox_Ip.Enabled = true;
                textBox_Port.Enabled = true;
                button_Connect.Enabled = true;
                button_Disconnect.Enabled = true;
                _card.Networking = true;
            }
            else
            {               
                textBox_Ip.Enabled = false;
                textBox_Port.Enabled = false;
                button_Connect.Enabled = false;
                button_Disconnect.Enabled = false;
                _card.Networking = false;
            }
        }

        private void button_Disconnect_Click(object sender, EventArgs e)
        {
            _card.Disconnect();
        }

        private void HardwareStopped()
        {
            if (InvokeRequired)
            {
                GuiUpdate callback = HardwareStopped;
                Invoke(callback);
            }
            else
            {
                label_Status.Text = "Stopped";
                label_Status.BackColor = Color.FromArgb(196, 20, 94);
            }
        }

        private void HardwareStarted()
        {
            if (InvokeRequired)
            {
                GuiUpdate callback = HardwareStarted;
                Invoke(callback);
            }
            else
            {
                label_Status.Text = "Started";
                label_Status.BackColor = Color.FromArgb(127, 210, 21);
            }
        }

        #region Nested type: GuiUpdate

        private delegate void GuiUpdate();

        #endregion

        private void DigitalMainwindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }
}