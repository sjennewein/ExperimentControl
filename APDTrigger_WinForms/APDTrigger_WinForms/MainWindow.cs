using System;
using System.Drawing;
using System.Windows.Forms;
using APDTrigger_WinForms.Helper;
using Arction.LightningChartBasic;
using Arction.LightningChartBasic.Series;

namespace APDTrigger_WinForms
{
    public partial class MainWindow : Form
    {
        private readonly Controller _myController;
        public bool AutoUpdate = false;
        private int _pointCount;

        private enum DisplayType
        {
            Histogram,
            Spectrum
        };

        private DisplayType _myChart2Display = DisplayType.Histogram;

        public MainWindow()
        {
            _myController = new Controller(this);
            InitializeComponent();
            _myController.Binning = 10;
            _myController.APDBinsize = 100;
            _myController.DetectionBins = 3;
            _myController.Threshold = 18000;
            _myController.Samples2Acquire = 29000;
            _myController.Cycles = 100;
            _myController.Runs = 1;
            stop_button.Enabled = false;

            _myController.Finished += OnFinished;
            this.FormClosing += OnQuit; //hopefully enough to close all hardware handles when closing the application            

            textBox_binningInput.DataBindings.Add("Text", _myController, "Binning", true, DataSourceUpdateMode.OnPropertyChanged);
            thresholdInput.DataBindings.Add("Text", _myController, "Threshold", true,
                DataSourceUpdateMode.OnPropertyChanged);
            textBox_detectionInput.DataBindings.Add("Text", _myController, "DetectionBins", true,
                DataSourceUpdateMode.OnPropertyChanged);
            textBox_cyclesInput.DataBindings.Add("Text", _myController, "Cycles", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox_runsInput.DataBindings.Add("Text", _myController, "Runs", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox_apdInput.DataBindings.Add("Text", _myController, "APDBinsize", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox_acquireInput.DataBindings.Add("Text", _myController, "Samples2Acquire", true,
                DataSourceUpdateMode.OnPropertyChanged);
            textBox_totalRuns.DataBindings.Add("Text", _myController, "Runs", true,
                DataSourceUpdateMode.OnPropertyChanged);
            textBox_runsDone.DataBindings.Add("Text", _myController, "RunsDone", true,
                DataSourceUpdateMode.OnPropertyChanged);
            textBox_CyclesDone.DataBindings.Add("Text", _myController, "CyclesDone", true,
                DataSourceUpdateMode.OnPropertyChanged);
            textBox_RecaptureRate.DataBindings.Add("Text", _myController, "RecaptureRate", true,
                DataSourceUpdateMode.OnPropertyChanged);
            textBox_Atoms.DataBindings.Add("Text", _myController, "Atoms", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox_NoAtoms.DataBindings.Add("Text", _myController, "NoAtoms", true,
                DataSourceUpdateMode.OnPropertyChanged);


            InitializeApdSignalChart();
            InitializeApdHistogram();
        }

        private void InitializeApdSignalChart()
        {
            apdSignal.BeginUpdate();

            apdSignal.Title.Text = "APD Signal";
            apdSignal.Title.Shadow.Visible = false;
            apdSignal.Title.Color = Color.Black;

            apdSignal.GraphMargins = new Padding(75, 40, 40, 50);

            apdSignal.GraphBackground.Color = Color.White;
            apdSignal.GraphBackground.GradientColor = Color.White;
            apdSignal.LegendBox.Visible = false;
            apdSignal.Background.Color = Color.LightGray;
            apdSignal.Background.GradientColor = Color.LightGray;

            apdSignal.YAxes[0].Title.Text = "Counts per bin";
            apdSignal.YAxes[0].Title.Color = Color.Black;
            apdSignal.YAxes[0].Title.Shadow.Visible = false;
            apdSignal.YAxes[0].Units.Visible = false;
            //apdSignal.YAxes[0].SetRange(0,30);

            apdSignal.XAxis.SetRange(0, 5);
            apdSignal.XAxis.Title.Text = "Time [mm:ss]";
            apdSignal.XAxis.Units.Visible = false;
            apdSignal.XAxis.Title.Color = Color.Black;
            apdSignal.XAxis.Title.Shadow.Visible = false;

            apdSignal.XAxis.ScrollMode = XAxisScrollMode.Scrolling;
            apdSignal.XAxis.ScrollPosition = 0;

            apdSignal.DropOldSeriesData = true;

            apdSignal.MouseInteraction = false;

            apdSignal.PointLineSeries[0].MouseInteraction = false;
            apdSignal.PointLineSeries[0].LineStyle.Color = Color.Blue;

            apdSignal.EndUpdate();
        }

        private void InitializeApdHistogram()
        {
            apdHistogram.BeginUpdate();

            apdHistogram.Title.Text = "APD Counter Histogram";
            apdHistogram.Title.Shadow.Visible = false;
            apdHistogram.Title.Color = Color.Black;

            apdHistogram.GraphMargins = new Padding(75, 40, 40, 50);

            apdHistogram.GraphBackground.Color = Color.White;
            apdHistogram.GraphBackground.GradientColor = Color.White;
            apdHistogram.LegendBox.Visible = false;
            apdHistogram.Background.Color = Color.LightGray;
            apdHistogram.Background.GradientColor = Color.LightGray;

            apdHistogram.YAxes[0].Title.Text = "Magnitude";
            apdHistogram.YAxes[0].Title.Color = Color.Black;
            apdHistogram.YAxes[0].Title.Shadow.Visible = false;
            apdHistogram.YAxes[0].Units.Visible = false;

            apdHistogram.XAxis.Title.Text = "Counts";
            apdHistogram.XAxis.Units.Visible = false;
            apdHistogram.XAxis.Title.Color = Color.Black;
            apdHistogram.XAxis.Title.Shadow.Visible = false;

            apdHistogram.LegendBox.Visible = false;
            apdHistogram.DropOldSeriesData = true;
            apdHistogram.XAxis.ValueType = XAxisValueType.Number;

            apdHistogram.BarViewOptions.BarSpacing = 1;
            apdHistogram.BarViewOptions.Grouping = BarsGrouping.ByXValue;
            apdHistogram.BarViewOptions.Stacking = BarsStacking.None;
            apdHistogram.BarViewOptions.IndexGroupingFitGroupDistance = 0;
            apdHistogram.BarViewOptions.IndexGroupingFitSideMargins = 0;

            //apdHistogram.MouseInteraction = false;
            apdHistogram.XAxis.SetRange(0, 600);
            apdHistogram.YAxes[0].SetRange(0, 100);

            apdHistogram.EndUpdate();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            DisableAllInputs();
            stop_button.Enabled = true;
            _myController.Start();
            ApdHistogramUpdate.Start();
            ApdSignalUpdate.Start();
        }

        private void stop_button_Click(object sender, EventArgs e)
        {            
            _myController.Stop();                                   
        }

        private void OnFinished(object sender, EventArgs e)
        {
            if(InvokeRequired)
            {
                GuiUpdate callback = OnFinished;
                Invoke(callback, new object[] {sender,e});
            }
            else
            {
                ApdSignalUpdate.Stop();
                ApdHistogramUpdate.Stop();
                EnableAllInputs();
                start_button.Enabled = true;
                stop_button.Enabled = false;    
            }
            
        }

        private void UpdateApdHistogram()
        {
            _myController.UpdateHistogramData();

            apdHistogram.BeginUpdate();
            apdHistogram.BarSeries.Clear();

            var bs = new BarSeries(apdHistogram, apdHistogram.YAxes[0]);
            apdHistogram.BarSeries.Add(bs);
            bs.Shadow.Visible = false;
            bs.Fill.GradientColor = ChartTools.CalcGradient(Color.Black, Color.Black, 10);
            bs.Fill.Color = Color.LightGray;
            bs.BarWidth = 0;
            switch(_myChart2Display)
            {
                case DisplayType.Histogram:
                    for (int iBucket = 0; iBucket < 600; iBucket++)
                    {
                        bs.AddValue(iBucket, _myController.HistogramData[iBucket], "", true);
                    }
                    break;
                case DisplayType.Spectrum:
                    if(_myController.BinnedSpectrum != null)
                        for (int iBucket = 0; iBucket < Math.Ceiling((double) _myController.Samples2Acquire / _myController.APDBinsize); iBucket++ )
                        {
                            bs.AddValue(iBucket, _myController.BinnedSpectrum[iBucket], "", true);
                        }
                    break;
            }
            

            bool foobar = true;
            apdHistogram.YAxes[0].Fit(10, out foobar, false);
            apdHistogram.EndUpdate();
        }

        private void UpdateApdSignal()
        {
            double dataInterval = _myController.Binning/800.0;
            //System.Console.WriteLine(_myController.Data[0]);
            apdSignal.BeginUpdate();
            _pointCount++;
            double x = _pointCount*dataInterval;
            double y = _myController.Data;
            var pointsArray = new SeriesPoint[1];
            pointsArray[0].X = x;
            pointsArray[0].Y = y;
            apdSignal.PointLineSeries[0].AddPoints(pointsArray, false);
            apdSignal.XAxis.ScrollPosition = x;
            bool foobar = true;
            if (AutoUpdate)
            {
                apdSignal.YAxes[0].Fit(1, out foobar, false);
            }
            apdSignal.EndUpdate();
        }

        private void triggerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control control in groupBox_Trigger.Controls)
            {
                if ((control is RadioButton) == false)
                    continue;

                var radio = control as RadioButton;

                if (radio.Checked == false)
                    continue;

                switch (radio.Text)
                {
                    case "Measurement":
                        _myController.Run = Controller.RunType.Measurement;
                        break;
                    case "Monitor":
                        _myController.Run = Controller.RunType.Monitor;
                        break;
                }
            }
        }

        private void apdSignalChart_DoubleClick(object sender, EventArgs e)
        {
            Form contextMenu = new ApdSignalContextMenu(this, apdSignal);
            contextMenu.ShowDialog();
        }

        private void saveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var originalSender = (CheckBox) sender;
            if (originalSender.Checked)
            {
                _myController.SaveApdSignal = true;
            }
            else
            {
                _myController.SaveApdSignal = false;
            }
        }

        private void ApdSignalUpdate_Tick(object sender, EventArgs e)
        {
            UpdateApdSignal();
        }

        private void DisableAllInputs()
        {
            start_button.Enabled = false;
            stop_button.Enabled = false;
            textBox_binningInput.Enabled = false;
            thresholdInput.Enabled = false;
            textBox_detectionInput.Enabled = false;
            textBox_cyclesInput.Enabled = false;
            textBox_runsInput.Enabled = false;
            textBox_apdInput.Enabled = false;
            textBox_acquireInput.Enabled = false;
            radioButton_Endless.Enabled = false;
            radioButton_triggered.Enabled = false;
            CheckBox_SaveSignal.Enabled = false;
            radioButton_No.Enabled = false;
            radioButton_Yes.Enabled = false;
            textBox_apdInput.Enabled = false;
            textBox_acquireInput.Enabled = false;
            checkBox_SaveHistogram.Enabled = false;
        }

        private void EnableAllInputs()
        {
            start_button.Enabled = true;
            stop_button.Enabled = true;
            textBox_binningInput.Enabled = true;
            thresholdInput.Enabled = true;
            textBox_detectionInput.Enabled = true;
            textBox_cyclesInput.Enabled = true;
            textBox_runsInput.Enabled = true;
            textBox_apdInput.Enabled = true;
            textBox_acquireInput.Enabled = true;
            radioButton_Endless.Enabled = true;
            radioButton_triggered.Enabled = true;
            CheckBox_SaveSignal.Enabled = true;
            radioButton_No.Enabled = true;
            radioButton_Yes.Enabled = true;
            textBox_apdInput.Enabled = true;
            textBox_acquireInput.Enabled = true;
            checkBox_SaveHistogram.Enabled = true;
        }

        private void OnQuit(object sender, EventArgs e)
        {
            
                _myController.Quit();
            
        }

        private void ApdHistogramUpdate_Tick(object sender, EventArgs e)
        {
            UpdateApdHistogram();
        }

        internal delegate void myGuiCallback(object state);

        private void radioButton_Recapture_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control control in groupBox_Recapture.Controls)
            {
                if ((control is RadioButton) == false)
                    continue;

                var radio = control as RadioButton;

                if (radio.Checked == false)
                    continue;

                switch (radio.Text)
                {
                    case "Yes":
                        _myController.Recapture = true;
                        break;
                    case "No":
                        _myController.Recapture = false;
                        break;
                }
            }
        }

        private void checkBox_SaveHistogram_CheckedChanged(object sender, EventArgs e)
        {
            var originalSender = (CheckBox)sender;
            if (originalSender.Checked)
            {
                _myController.SaveSpectrum = true;
            }
            else
            {
                _myController.SaveSpectrum = false;
            }
        }

        private void radioButton_HistogramDisplay_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control control in groupBox_Histogram.Controls)
            {
                if ((control is RadioButton) == false)
                    continue;

                var radio = control as RadioButton;

                if (radio.Checked == false)
                    continue;

                switch (radio.Text)
                {
                    case "Spectrum":
                        apdHistogram.BeginUpdate();
                        apdHistogram.Title.Text = "Spectrum";
                        apdHistogram.XAxis.SetRange(0, Math.Ceiling( (double) _myController.Samples2Acquire / _myController.APDBinsize));
                        apdHistogram.EndUpdate();
                        _myChart2Display = DisplayType.Spectrum;                        
                        break;
                    case "Signal Histogram":
                        apdHistogram.BeginUpdate();
                        apdHistogram.Title.Text = "APD Counter Histogram";
                        apdHistogram.XAxis.SetRange(0, 600);
                        apdHistogram.EndUpdate();

                        _myChart2Display = DisplayType.Histogram;
                        break;
                }
            }
        }

        private void button_Rescale_Click(object sender, EventArgs e)
        {
            if(_myChart2Display == DisplayType.Histogram)
            {
                apdHistogram.XAxis.SetRange(0,600);
            }

            if(_myChart2Display == DisplayType.Spectrum)
            {
                apdHistogram.XAxis.SetRange(0, Math.Ceiling((double)_myController.Samples2Acquire / _myController.APDBinsize));
            }
        }

        private delegate void GuiUpdate(object sender, EventArgs e);
    }
}