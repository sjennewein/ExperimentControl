using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AspherixGPIB.Controller;
using AspherixGPIB.Data;
using Ivi.Visa.Interop;

namespace AspherixGPIB
{
    public partial class GPIBWindow : Form
    {
        
        private CtrlGPIBArb _GpibWaveform;
        private CtrlGPIBGeneric _GpibGeneric;

        public GPIBWindow(CtrlGPIBArb gpibArbWave = null, CtrlGPIBGeneric gpibGeneric = null)
        {
            _GpibWaveform = gpibArbWave ?? new CtrlGPIBArb();

            if (gpibGeneric == null)
                _GpibGeneric = new CtrlGPIBGeneric();
            else
                _GpibGeneric = gpibGeneric;

            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            textBox_A.SetController(_GpibWaveform.Amplitude);
            textBox_arbAddress.DataBindings.Add("Text", _GpibWaveform, "Address",false,DataSourceUpdateMode.OnPropertyChanged);
            textBox_amplitude.SetController(_GpibWaveform.AmplitudeVolt);
            textBox_sampling.SetController(_GpibWaveform.SamplingFrequency);
            textBox_x0.SetController(_GpibWaveform.X0);
            textBox_sigma.SetController(_GpibWaveform.Sigma);
            textBox_x.SetController(_GpibWaveform.Samples);

            textBox_GPAddress.DataBindings.Add("Text", _GpibGeneric, "Address",false,DataSourceUpdateMode.OnPropertyChanged);
            richTextBox_GPCommands.DataBindings.Add("Text", _GpibGeneric, "Commands");
            richTextBox_GPCommands.TextChanged += _GpibGeneric.CheckText;
        }

        //public void Restore()
        //{
            
        //}

        //public void Save()
        //{
            
        //}

        private void button_setGeneric_Click(object sender, EventArgs e)
        {
            _GpibGeneric.ManualSet();
        }

        private void button_setArb_Click(object sender, EventArgs e)
        {

        }


        private void GPIBWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            var window = (Form)sender;
            window.Visible = false;
        }

        private void checkBox_GeneralPurpose_CheckedChanged(object sender, EventArgs e)
        {
            var box = (CheckBox) sender;
            if (box.Checked)
            {
                textBox_GPAddress.Enabled = true;
                richTextBox_GPCommands.Enabled = true;
                button_GPDisconnect.Enabled = true;
                button_setGeneric.Enabled = true;
            }
            else
            {
                textBox_GPAddress.Enabled = false;
                richTextBox_GPCommands.Enabled = false;
                button_GPDisconnect.Enabled = false;
                button_setGeneric.Enabled = false;
            }
        }

        private void checkBox_arb_CheckedChanged(object sender, EventArgs e)
        {
            var box = (CheckBox)sender;
            if (box.Checked)
            {
                textBox_A.Enabled = true;
                textBox_amplitude.Enabled = true;
                textBox_arbAddress.Enabled = true;
                textBox_sampling.Enabled = true;
                textBox_sigma.Enabled = true;
                textBox_x.Enabled = true;
                textBox_x0.Enabled = true;
                button_ArbDisconnect.Enabled = true;
                button_setArb.Enabled = true;
            }
            else
            {
                textBox_A.Enabled = false;
                textBox_amplitude.Enabled = false;
                textBox_arbAddress.Enabled = false;
                textBox_sampling.Enabled = false;
                textBox_sigma.Enabled = false;
                textBox_x.Enabled = false;
                textBox_x0.Enabled = false;
                button_ArbDisconnect.Enabled = false;
                button_setArb.Enabled = false;
            }
        }

        private void button_GPDisconnect_Click(object sender, EventArgs e)
        {

        }

       


    }
}
