using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using APDTrigger_WinForms.Controls;
using Arction.LightningChartBasic;

namespace APDTrigger_WinForms
{
    public partial class Form1 : Form
    {
        internal delegate void myGuiCallback(object sender, EventArgs e);

        private Controlling controller = new Controlling();
        private int _pointCount = 0;

        public Form1()
        {

            controller.Binning = 10;
            controller.APDBinsize = 100;
            controller.DetectionBins = 3;
            controller.Threshold = 18000;
            controller.Finished += OnFinished;
            controller.NewData += OnNewData;

            InitializeComponent();
            
            binningInput.DataBindings.Add("Text", controller, "Binning", true, DataSourceUpdateMode.OnPropertyChanged);
            thresholdInput.DataBindings.Add("Text", controller, "Threshold", true, DataSourceUpdateMode.OnPropertyChanged);
            detectionInput.DataBindings.Add("Text", controller, "DetectionBins", true, DataSourceUpdateMode.OnPropertyChanged);
            cyclesInput.DataBindings.Add("Text", controller, "Cycles", true, DataSourceUpdateMode.OnPropertyChanged);
            runsInput.DataBindings.Add("Text", controller, "Runs", true, DataSourceUpdateMode.OnPropertyChanged);
            apdInput.DataBindings.Add("Text", controller, "APDBinsize", true, DataSourceUpdateMode.OnPropertyChanged);
            acquireInput.DataBindings.Add("Text", controller, "Samples2Acquire", true, DataSourceUpdateMode.OnPropertyChanged);                       

            InitializeChart();           
        }

        private void InitializeChart()
        {
            lightningChartBasic1.BeginUpdate();

            lightningChartBasic1.Title.Text = "APD Signal";
            lightningChartBasic1.Title.Shadow.Visible = false;
            lightningChartBasic1.Title.Color = Color.Black;

            lightningChartBasic1.GraphMargins = new Padding(75, 40, 40, 50);

            lightningChartBasic1.GraphBackground.Color = Color.White;
            lightningChartBasic1.GraphBackground.GradientColor = Color.White;
            lightningChartBasic1.LegendBox.Visible = false;
            lightningChartBasic1.Background.Color = Color.LightGray;
            lightningChartBasic1.Background.GradientColor = Color.LightGray;

            lightningChartBasic1.YAxes[0].Title.Text = "Counts per bin ";
            lightningChartBasic1.YAxes[0].Title.Color = Color.Black;
            lightningChartBasic1.YAxes[0].Title.Shadow.Visible = false;
            lightningChartBasic1.YAxes[0].Units.Visible = false;

            lightningChartBasic1.XAxis.SetRange(0, 5);
            lightningChartBasic1.XAxis.Title.Text = "Time [mm:ss]";
            lightningChartBasic1.XAxis.Units.Visible = false;
            lightningChartBasic1.XAxis.Title.Color = Color.Black;
            lightningChartBasic1.XAxis.Title.Shadow.Visible = false;

            lightningChartBasic1.XAxis.ScrollMode = XAxisScrollMode.Scrolling;
            lightningChartBasic1.XAxis.ScrollPosition = 0;

            lightningChartBasic1.DropOldSeriesData = true;

            lightningChartBasic1.MouseInteraction = false;

            lightningChartBasic1.PointLineSeries[0].MouseInteraction = false;
            lightningChartBasic1.PointLineSeries[0].LineStyle.Color = Color.Blue;

            lightningChartBasic1.EndUpdate();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            controller.Start();
            start_button.Enabled = false;
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            controller.Stop();            
        }

        private void OnFinished(object sender, EventArgs e)
        {
            System.Console.WriteLine("STOPPED");
            start_button.Enabled = true;
        }

        private void OnNewData(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                myGuiCallback callback = OnNewData;
                Invoke(callback, new[]{sender, e});
            }
            else
            {
                double dataInterval = controller.Binning / 300.0;
                System.Console.WriteLine(controller.Data[0]);
                lightningChartBasic1.BeginUpdate();
                _pointCount++;
                double x = (double)_pointCount * dataInterval;
                double y = controller.Data[0];
                SeriesPoint[] pointsArray = new SeriesPoint[1];
                pointsArray[0].X = x;
                pointsArray[0].Y = y;
                lightningChartBasic1.PointLineSeries[0].AddPoints(pointsArray, false);
                lightningChartBasic1.XAxis.ScrollPosition = x;
                bool foobar = true;
                lightningChartBasic1.YAxes[0].Fit(1, out foobar, false);
                lightningChartBasic1.EndUpdate();
            }
            
        }

        private void triggerRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        

    }
}
