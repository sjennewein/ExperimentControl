using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Arction.LightningChartBasic;

namespace APDTrigger_WinForms
{
    public partial class ApdSignalContextMenu : Form
    {
        private MainWindow _myCaller;
        private LightningChartBasic _myChart;
        private bool _myAutoUpdate;
        public double _myMin { get; set; }
        public double _myMax { get; set; }

        public ApdSignalContextMenu(MainWindow caller, LightningChartBasic chart)
        {
            InitializeComponent();
            _myCaller = caller;
            _myChart = chart;
            _myAutoUpdate = caller.AutoUpdate;
            yMaxBox.DataBindings.Add("Text", this, "_myMax", true, DataSourceUpdateMode.OnPropertyChanged);
            yMinBox.DataBindings.Add("Text", this, "_myMin", true, DataSourceUpdateMode.OnPropertyChanged);

            if(_myAutoUpdate)
            {
                autoscaleCheckbox.Checked = true;
                yMinBox.Enabled = false;
                yMaxBox.Enabled = false;
            }
            else
            {
                autoscaleCheckbox.Checked = false;
                activateTextboxes();
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            updateChart();
        }

        private void autoscaleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox) sender;
            if (checkBox.Checked)
            {
                _myAutoUpdate = true;
                yMaxBox.Enabled = false;
                yMinBox.Enabled = false;
            }
            else
            {
                _myAutoUpdate = false;
                activateTextboxes();
                
            }
           
        }

        private void activateTextboxes()
        {
            yMinBox.Enabled = true;
            yMaxBox.Enabled = true;
            _myMin = _myChart.YAxes[0].Minimum;
            _myMax = _myChart.YAxes[0].Maximum;

        }

        private void yMinBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                updateChart();
            }
        }

        private void updateChart()
        {
            if (_myAutoUpdate)
            {
                _myCaller.AutoUpdate = true;
            }
            else
            {
                _myCaller.AutoUpdate = false;
                _myChart.BeginUpdate();
                _myChart.YAxes[0].SetRange(_myMin, _myMax);
                _myChart.EndUpdate();
            }
        }

        private void yMaxBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                updateChart();
            }
        }
    }
}
