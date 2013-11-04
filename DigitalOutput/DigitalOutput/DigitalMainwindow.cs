using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using DigitalOutput.Controller;
using DigitalOutput.GUI;
using DigitalOutput.Hardware;
using DigitalOutput.Model;
using Hulahoop.Controller;
using fastJSON;
using Hulahoop;
using Buffer = DigitalOutput.Hardware.Buffer;
using Ionic.Zip;


namespace DigitalOutput
{
    public partial class DigitalMainwindow : Form
    {
        private ControllerCard _card;
        private Buffer _buffer = new Buffer();        
        private HulahoopDigital _loops = new HulahoopDigital();

        public DigitalMainwindow()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;            
            Console.WriteLine(Width);
            _card = ControllerFabric.GenerateCard(_buffer);
            textBox_Flow.DataBindings.Add("Text", _card, "Flow", false, DataSourceUpdateMode.OnPropertyChanged);
            SuspendLayout();
            Helper.GenerateTabView(TabPanel, _card);
            ResumeLayout();
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
            
            ModelCard loadedCard = (ModelCard) fastJSON.JSON.Instance.ToObject(input);
            _card = ControllerFabric.GenerateCard(_buffer, loadedCard);
            
            SuspendLayout();
            _loops.ReLoad();
            Helper.DisposeTabs(TabPanel);                        
            Helper.GenerateTabView(TabPanel,_card);            
            ResumeLayout();
            textBox_Flow.DataBindings.RemoveAt(0);
            textBox_Flow.DataBindings.Add("Text", _card, "Flow", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button_Synchronize_Click(object sender, EventArgs e)
        {
            label_Buffer.BackColor = Color.FromArgb(127, 210, 21);
            _card.CopyToBuffer();
            _card.StoreSyncedValues();            
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (String.Equals(_card.Flow, String.Empty) || _card.Flow == null)
                return;
            _card.Start();
            label_Buffer.BackColor = Color.FromArgb(127,210,21);
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            _card.Stop();
        }

        private void button_Undo_Click(object sender, EventArgs e)
        {
            _card.RestoreSyncedValues();
        }

        private void button_HulaHoop_Click(object sender, EventArgs e)
        {
            _loops.Visible = true;
        }       
    }
}