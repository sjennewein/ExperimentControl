using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using APDTrigger_WinForms.Controls;
using Arction.LightningChartBasic;
using Arction.LightningChartBasic.Series;
using Timer = System.Threading.Timer;

namespace APDTrigger_WinForms
{
    public partial class Form1 : Form
    {
        private readonly Controlling controller = new Controlling();
        private Timer _myApdSignalTimer;
        private Timer _myApdHistogramTimer;
        private Int16 _pointCount;
        private Random rand = new Random();

        public Form1()
        {
            controller.Binning = 10;
            controller.APDBinsize = 100;
            controller.DetectionBins = 3;
            controller.Threshold = 18000;
            controller.Finished += OnFinished;


            InitializeComponent();

            binningInput.DataBindings.Add("Text", controller, "Binning", true, DataSourceUpdateMode.OnPropertyChanged);
            thresholdInput.DataBindings.Add("Text", controller, "Threshold", true,
                DataSourceUpdateMode.OnPropertyChanged);
            detectionInput.DataBindings.Add("Text", controller, "DetectionBins", true,
                DataSourceUpdateMode.OnPropertyChanged);
            cyclesInput.DataBindings.Add("Text", controller, "Cycles", true, DataSourceUpdateMode.OnPropertyChanged);
            runsInput.DataBindings.Add("Text", controller, "Runs", true, DataSourceUpdateMode.OnPropertyChanged);
            apdInput.DataBindings.Add("Text", controller, "APDBinsize", true, DataSourceUpdateMode.OnPropertyChanged);
            acquireInput.DataBindings.Add("Text", controller, "Samples2Acquire", true,
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

            apdSignal.XAxis.SetRange(0,5);
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

            apdHistogram.BarViewOptions.Grouping = BarsGrouping.ByIndex;
            
            apdHistogram.BarViewOptions.BarSpacing = 5;

            apdHistogram.MouseInteraction = false;
            apdHistogram.XAxis.SetRange(0, 50);
            apdHistogram.YAxes[0].SetRange(0,100);

            apdHistogram.EndUpdate();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            start_button.Enabled = false;
            controller.Start();
            
            //_myApdSignalTimer = new Timer(UpdateApdSignal, null, 0, 10);
            _myApdHistogramTimer =  new Timer(UpdateApdHistogram, null, 0, 1000);
            timer1.Start();
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            controller.Stop();            
            //_myApdSignalTimer.Dispose();
            _myApdHistogramTimer.Dispose();
            timer1.Stop();
        }

        private void OnFinished(object sender, EventArgs e)
        {            
            start_button.Enabled = true;
        }

        private void UpdateApdHistogram(object state)
        {
            if (InvokeRequired)
            {
                controller.UpdateHistogramData();

                myGuiCallback callback = UpdateApdHistogram;
                Invoke(callback, new[] {state});
            }
            else
            {                               
                apdHistogram.BeginUpdate();
                apdHistogram.BarSeries.Clear();
                
                BarSeries bs = new BarSeries(apdHistogram,apdHistogram.YAxes[0]);
                apdHistogram.BarSeries.Add(bs);
                bs.BarWidth = 3;
                
                
                for (int iBucket = 0; iBucket < 50; iBucket++)
                {
                    //barData[iBucket].Y = controller.HistogramData[iBucket];

                    bs.AddValue(iBucket, (double) controller.HistogramData[iBucket], "", false);
                    
                }

                bool foobar = true;
                apdHistogram.YAxes[0].Fit(10, out foobar, false);
                apdHistogram.EndUpdate();
                                 
            }
        }

        private void UpdateApdSignal(object state)
        {
            //if (InvokeRequired)
            //{
            //    myGuiCallback callback = UpdateApdSignal;
            //    Invoke(callback, new[] {state});
            //}
            //else
            //{
                double dataInterval = controller.Binning/800.0;
                //System.Console.WriteLine(controller.Data[0]);
                apdSignal.BeginUpdate();
                _pointCount++;
                double x = _pointCount*dataInterval;
                double y = controller.Data;
                var pointsArray = new SeriesPoint[1];
                pointsArray[0].X = x;
                pointsArray[0].Y = y;
                apdSignal.PointLineSeries[0].AddPoints(pointsArray, false);
                apdSignal.XAxis.ScrollPosition = x;                
                bool foobar = true;
                apdSignal.YAxes[0].Fit(1, out foobar, false);
                apdSignal.EndUpdate();
            //}
        }

        private void triggerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
        }

        internal delegate void myGuiCallback(object state);

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateApdSignal(null);
        }
    }
}