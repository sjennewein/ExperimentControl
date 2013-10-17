using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using DigitalOutput.Controller;
using DigitalOutput.GUI;
using DigitalOutput.Model;
using fastJSON;

namespace DigitalOutput
{
    public partial class DigitalMainwindow : Form
    {
        private ControllerCard _card;

        public DigitalMainwindow()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;            
            Console.WriteLine(Width);
            _card = ControllerFabric.GenerateCard();
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

            using (Stream fileStream = saveFileDialog.OpenFile())
            {
                _card.Save(fileStream);
                fileStream.Close();
            }   
            
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            var loadFileDialog = new OpenFileDialog();
            string input;
            loadFileDialog.Filter = "Digital runs (*.drun)|*.drun|All files|*.*";
            loadFileDialog.RestoreDirectory = true;

            if (loadFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (FileStream fileStream = new FileStream(loadFileDialog.FileName, FileMode.Open))
            {                             
                   using(var sr = new StreamReader(fileStream))
                   {
                       input = sr.ReadLine();
                       sr.Close();
                   }
                fileStream.Close();
            }

            
            ModelCard loadedCard = (ModelCard) fastJSON.JSON.Instance.ToObject(input);
            _card = ControllerFabric.GenerateCard(loadedCard);
            SuspendLayout();
            Helper.DisposeTabs(TabPanel);                        
            Helper.GenerateTabView(TabPanel,_card);
            ResumeLayout();

        }
    }
}