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

            apdSignal.YAxes[0].Title.Text = "Counts per bin ";
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

        private void InitializeApdHistogram()
        {
            apdHistogram.BeginUpdate();

            apdHistogram.Title.Text = "APD Counter Histogram";
            apdHistogram.Title.Shadow.Visible = false;
            apdHistogram.Title.Color = Color.Black;

            apdHistogram.GraphMargins = new Padding(75, 40, 40, 50);
            
            apdHistogram.LegendBox.Visible = false;
            apdHistogram.DropOldSeriesData = true;
            apdHistogram.XAxis.ValueType = XAxisValueType.Number;

            apdHistogram.BarViewOptions.Grouping = BarsGrouping.ByIndexFitWidth;
            apdHistogram.BarViewOptions.BarSpacing = 5;

            apdHistogram.XAxis.SetRange(0, 50);
            apdHistogram.YAxes[0].SetRange(0,100);

            apdHistogram.EndUpdate();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            start_button.Enabled = false;
            controller.Start();
            //UpdateApdHistogram(null);
           // _myApdSignalTimer = new Timer(UpdateApdSignal, null, 0, 20);
            _myApdHistogramTimer =  new Timer(UpdateApdHistogram, null, 0, 1000);
            
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            controller.Stop();            
            //_myApdSignalTimer.Dispose();

            _myApdHistogramTimer.Dispose();
        }

        private void OnFinished(object sender, EventArgs e)
        {
            Console.WriteLine("STOPPED");
            start_button.Enabled = true;
        }

        private void UpdateApdHistogram(object state)
        {
            if (InvokeRequired)
            {
                myGuiCallback callback = UpdateApdHistogram;
                Invoke(callback, new[] {state});
            }
            else
            {
                controller.UpdateHistogramData();
                
                apdHistogram.BeginUpdate();
                apdHistogram.BarSeries.Clear();
                BarSeries bs = new BarSeries(apdHistogram,apdHistogram.YAxes[0]);
                apdHistogram.BarSeries.Add(bs);
                
                
                for (int iBucket = 0; iBucket < 50; iBucket++)
                {
                    //barData[iBucket].Y = controller.HistogramData[iBucket];

                    bs.AddValue(iBucket, (double) controller.HistogramData[iBucket], "", false);
                    
                }

                
                apdHistogram.EndUpdate();
                                 
            }
        }

        private void UpdateApdSignal(object state)
        {
            if (InvokeRequired)
            {
                myGuiCallback callback = UpdateApdSignal;
                Invoke(callback, new[] {state});
            }
            else
            {
                double dataInterval = controller.Binning/300.0;
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
            }
        }

        private void triggerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
        }

        internal delegate void myGuiCallback(object state);
    }
}