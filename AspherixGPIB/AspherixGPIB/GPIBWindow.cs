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
        
        private CtrlGPIBArb _GpibWaveform = new CtrlGPIBArb();
        private CtrlGPIBGeneric _GpibGeneric = new CtrlGPIBGeneric();

        public GPIBWindow()
        {            
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            textBox_A.DataBindings.Add("Text", _GpibWaveform, "Amplitude", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_arbAddress.DataBindings.Add("Text", _GpibWaveform, "Address", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_amplitude.DataBindings.Add("Text", _GpibWaveform, "AmplitudeVolt", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_sampling.DataBindings.Add("Text", _GpibWaveform, "SamplingFrequency", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_x0.DataBindings.Add("Text", _GpibWaveform, "X0", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_sigma.DataBindings.Add("Text", _GpibWaveform, "Sigma", false, DataSourceUpdateMode.OnPropertyChanged);
            textBox_x.DataBindings.Add("Text", _GpibWaveform, "Samples", false, DataSourceUpdateMode.OnPropertyChanged);

            textBox_GPAddress.DataBindings.Add("Text", _GpibGeneric, "Address");
            textBox_GPCommands.DataBindings.Add("Text", _GpibGeneric, "Commands");
        }

        public void Restore()
        {
            
        }

        public void Save()
        {
            
        }

        private void button_setGeneric_Click(object sender, EventArgs e)
        {

        }

        private void button_setArb_Click(object sender, EventArgs e)
        {

        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {

        }


    }
}
