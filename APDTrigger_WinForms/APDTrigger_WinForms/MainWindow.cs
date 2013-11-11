using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using APDTrigger_WinForms.Helper;
using Arction.LightningChartBasic;
using Arction.LightningChartBasic.Series;

namespace APDTrigger_WinForms
{
    public partial class MainWindow : Form
    {
        private readonly Controller _myController;
        public bool AutoUpdate;
        private bool _apdIsRunning;
        private DisplayType _myChart2Display = DisplayType.Histogram;
        private int _pointCount;

        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            _myController = new Controller(this);
            InitializeComponent();
            _myController.Binning = 10;
            _myController.APDBinsize = 100;
            _myController.DetectionBins = 3;
            _myController.Threshold = 400;
            _myController.Samples2Acquire = 50000;
            _myController.Cycles = 100;
            _myController.TotalRuns = 1;
            _myController.Frequency = 0.5;
            stop_button.Enabled = false;
            button_StopFrequency.Enabled = false;

            _myController.APDStopped += OnApdStopped;

            textBox_binningInput.DataBindings.Add("Text", _myController, "Binning", true,
                                                  DataSourceUpdateMode.OnPropertyChanged);
            thresholdInput.DataBindings.Add("Text", _myController, "Threshold", true,
                                            DataSourceUpdateMode.OnPropertyChanged);
            textBox_detectionInput.DataBindings.Add("Text", _myController, "DetectionBins", true,
                                                    DataSourceUpdateMode.OnPropertyChanged);
            textBox_cyclesInput.DataBindings.Add("Text", _myController, "Cycles", true,
                                                 DataSourceUpdateMode.OnPropertyChanged);
            textBox_runsInput.DataBindings.Add("Text", _myController, "TotalRuns", true,
                                               DataSourceUpdateMode.OnPropertyChanged);
            textBox_apdInput.DataBindings.Add("Text", _myController, "APDBinsize", true,
                                              DataSourceUpdateMode.OnPropertyChanged);
            textBox_acquireInput.DataBindings.Add("Text", _myController, "Samples2Acquire", true,
                                                  DataSourceUpdateMode.OnPropertyChanged);
            textBox_totalRuns.DataBindings.Add("Text", _myController, "TotalRuns", true,
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
            textBox_Frequency.DataBindings.Add("Text", _myController, "Frequency", true,
                                               DataSourceUpdateMode.OnPropertyChanged);

            InitializeApdSignalChart();
            InitializeApdHistogram();
        }

        /// <summary>
        /// Initializes the signal chart 
        /// </summary>
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

        /// <summary>
        /// Initializes the histogram
        /// </summary>
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

        /// <summary>
        /// Starts the experiment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Start_Click(object sender, EventArgs e)
        {
            _apdIsRunning = true;
            DisableAllInputs();
            stop_button.Enabled = true;
            _myController.Start();
            ApdHistogramUpdate.Start();
            ApdSignalUpdate.Start();
        }

        /// <summary>
        /// Stops the experiment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Stop_Click(object sender, EventArgs e)
        {
            _myController.Stop();
        }

        /// <summary>
        /// Callback function for when the experiment finishes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApdStopped(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                GuiUpdate callback = OnApdStopped;
                Invoke(callback, new[] {sender, e});
            }
            else
            {
                ApdSignalUpdate.Stop();
                ApdHistogramUpdate.Stop();
                EnableAllInputs();
                stop_button.Enabled = false;
                button_StopFrequency.Enabled = false;
                _apdIsRunning = false;
            }
        }

        /// <summary>
        /// Callback function to update the histogram
        /// </summary>
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
            switch (_myChart2Display)
            {
                case DisplayType.Histogram:
                    for (int iBucket = 0; iBucket < 600; iBucket++)
                    {
                        bs.AddValue(iBucket, _myController.HistogramData[iBucket], "", true);
                    }
                    break;
                case DisplayType.Spectrum:
                    if (_myController.BinnedSpectrum != null)
                        for (int iBucket = 0;
                             iBucket < Math.Ceiling((double) _myController.Samples2Acquire/_myController.APDBinsize);
                             iBucket++)
                        {
                            bs.AddValue(iBucket, _myController.BinnedSpectrum[iBucket], "", true);
                        }
                    break;
            }


            bool foobar = true;
            apdHistogram.YAxes[0].Fit(10, out foobar, false);
            apdHistogram.EndUpdate();
        }

        /// <summary>
        /// Callback function to update the apd signal chart
        /// </summary>
        private void UpdateApdSignal()
        {
            double dataInterval = _myController.Binning/500.0;
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

        /// <summary>
        /// checks which radio button for the run type is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_Mode_CheckedChanged(object sender, EventArgs e)
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
                        _myController.Mode = Controller.RunType.Measurement;
                        break;
                    case "Monitor":
                        _myController.Mode = Controller.RunType.Monitor;
                        break;
                    case "Network":
                        _myController.Mode = Controller.RunType.Network;
                        break;
                }
            }
        }

        /// <summary>
        /// callback for double clicking the apd signal chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart_ApdSignal_DoubleClick(object sender, EventArgs e)
        {
            Form contextMenu = new ApdSignalContextMenu(this, apdSignal);
            contextMenu.ShowDialog();
        }

        /// <summary>
        /// saves if apd signal data should be saved or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_Save_CheckedChanged(object sender, EventArgs e)
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

        /// <summary>
        /// Callback function for the timer that updates the APD signal chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_ApdSignal_Tick(object sender, EventArgs e)
        {
            UpdateApdSignal();
        }

        /// <summary>
        /// Disables all user inputs that are important for a run
        /// </summary>
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
            radioButton_Monitor.Enabled = false;
            radioButton_Measurement.Enabled = false;
            CheckBox_SaveSignal.Enabled = false;
            textBox_apdInput.Enabled = false;
            textBox_acquireInput.Enabled = false;
            checkBox_SaveHistogram.Enabled = false;
            button_StartFrequency.Enabled = false;
            button_StopFrequency.Enabled = false;
            textBox_Frequency.Enabled = false;
            radioButton_Network.Enabled = false;
        }

        /// <summary>
        /// Enables all user inputs that are important for a run
        /// </summary>
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
            radioButton_Monitor.Enabled = true;
            radioButton_Measurement.Enabled = true;
            CheckBox_SaveSignal.Enabled = true;
            textBox_apdInput.Enabled = true;
            textBox_acquireInput.Enabled = true;
            checkBox_SaveHistogram.Enabled = true;
            button_StartFrequency.Enabled = true;
            button_StopFrequency.Enabled = true;
            textBox_Frequency.Enabled = true;
            radioButton_Network.Enabled = true;
        }

        /// <summary>
        /// Callback function for the timer that updates the histogram
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_ApdHistogram_Tick(object sender, EventArgs e)
        {
            UpdateApdHistogram();
        }

        private void checkBox_SaveHistogram_CheckedChanged(object sender, EventArgs e)
        {
            var originalSender = (CheckBox) sender;
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
                        apdHistogram.XAxis.SetRange(0,
                                                    Math.Ceiling((double) _myController.Samples2Acquire/
                                                                 _myController.APDBinsize));
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
            if (_myChart2Display == DisplayType.Histogram)
            {
                apdHistogram.XAxis.SetRange(0, 600);
            }

            if (_myChart2Display == DisplayType.Spectrum)
            {
                apdHistogram.XAxis.SetRange(0,
                                            Math.Ceiling((double) _myController.Samples2Acquire/_myController.APDBinsize));
            }
        }

        private void button_StartFrequency_Click(object sender, EventArgs e)
        {
            _apdIsRunning = true;
            radioButton_Monitor.Checked = true;
            _myController.Start(true);
            DisableAllInputs();
            button_StopFrequency.Enabled = true;
            ApdHistogramUpdate.Start();
            ApdSignalUpdate.Start();
        }

        private void button_StopFrequency_Click(object sender, EventArgs e)
        {
            _myController.Stop();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_apdIsRunning)
            {
                e.Cancel = true;
                MessageBox.Show("You have to stop the APD first!");
                return;
            }


            _myController.Quit();
        }

        #region Nested type: DisplayType

        private enum DisplayType
        {
            Histogram,
            Spectrum
        };

        #endregion

        #region Nested type: GuiUpdate

        private delegate void GuiUpdate(object sender, EventArgs e);

        #endregion

        #region Nested type: myGuiCallback

        internal delegate void myGuiCallback(object state);

        #endregion
    }
}