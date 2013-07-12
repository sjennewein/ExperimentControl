﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace _2DPhotodiodes
{
    internal delegate void myGUICallback(int e);

    public partial class Form1 : Form
    {
        #region Input enum

        public enum Input
        {
            X1,
            X2,
            Y1,
            Y2,
            Z1,
            Z2
        };

        #endregion

        private const int samples = 1000;
        private int counter = 0;
        private Thread _collectorThread;
        private bool _stop = false;
        private int _delay = 1000;
        
        #region graph declaration
        private float[] x1Power = new float[samples];
        private readonly Series x1PowerSeries = new Series("x1 power");
        private readonly float[] x1X = new float[samples];
        private readonly Series x1XSeries = new Series("x1 x-axis");
        private readonly float[] x1Y = new float[samples];
        private readonly Series x1YSeries = new Series("x1 y-axis");

        private float[] x2Power = new float[samples];
        private readonly Series x2PowerSeries = new Series("x2 power");
        private readonly float[] x2X = new float[samples];
        private readonly Series x2XSeries = new Series("x2 x-axis");
        private readonly float[] x2Y = new float[samples];
        private readonly Series x2YSeries = new Series("x2 y-axis");

        private float[] y1Power = new float[samples];
        private readonly Series y1PowerSeries = new Series("y1 power");
        private readonly float[] y1X = new float[samples];
        private readonly Series y1XSeries = new Series("y1 x-axis");
        private readonly float[] y1Y = new float[samples];
        private readonly Series y1YSeries = new Series("y1 y-axis");

        private float[] y2Power = new float[samples];
        private Series y2PowerSeries = new Series("y2 power");
        private readonly float[] y2X = new float[samples];
        private readonly Series y2XSeries = new Series("y2 x-axis");
        private readonly float[] y2Y = new float[samples];
        private readonly Series y2YSeries = new Series("y2 y-axis");

        private float[] z1Power = new float[samples];
        private readonly Series z1PowerSeries = new Series("z1 power");
        private readonly float[] z1X = new float[samples];
        private readonly Series z1XSeries = new Series("z1 x-axis");
        private readonly float[] z1Y = new float[samples];
        private readonly Series z1YSeries = new Series("z1 y-axis");

        private float[] z2Power = new float[samples];
        private readonly Series z2PowerSeries = new Series("z2 power");
        private readonly float[] z2X = new float[samples];
        private readonly Series z2XSeries = new Series("z2 x-axis");
        private readonly float[] z2Y = new float[samples];
        private readonly Series z2YSeries = new Series("z2 y-axis");
        #endregion

        public Form1()
        {
            InitializeComponent();
            x1.Series.Clear();
            x2.Series.Clear();
            y1.Series.Clear();
            y2.Series.Clear();
            z1.Series.Clear();
            z2.Series.Clear();
            power.Series.Clear();

            #region initialize ActiveX

            x1Diode.Visible = false;
            x2Diode.Visible = false;
            y1Diode.Visible = false;
            y2Diode.Visible = false;
            z1Diode.Visible = false;
            z2Diode.Visible = false;
            x1Diode.HWSerialNum = 89835583;
            x2Diode.HWSerialNum = 89836997;
            y1Diode.HWSerialNum = 89836609;
            y2Diode.HWSerialNum = 89836614;
           
            #endregion

            #region initialize x1
            x1.ChartAreas[0].AxisX.Minimum = 0;
            x1.ChartAreas[0].AxisX.Maximum = samples;
            x1.ChartAreas[0].AxisY.Minimum = 0;            
            x1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            x1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            x1.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            x1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            x1.ChartAreas[0].AxisY2.IsStartedFromZero = x1.ChartAreas[0].AxisY.IsStartedFromZero;
            x1.Legends[0].Docking = Docking.Top;
            

            x1XSeries.ChartType = SeriesChartType.Point;
            x1YSeries.ChartType = SeriesChartType.Point;

            x1.Series.Add(x1XSeries);
            x1.Series.Add(x1YSeries);

            x1.Series[0].YAxisType = AxisType.Primary;
            x1.Series[1].YAxisType = AxisType.Secondary;
            #endregion

            #region initialize x2
            x2.ChartAreas[0].AxisX.Minimum = 0;
            x2.ChartAreas[0].AxisX.Maximum = samples;
            x2.ChartAreas[0].AxisY.Minimum = 0;
            x2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            x2.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            x2.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            x2.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            x2.ChartAreas[0].AxisY2.IsStartedFromZero = x2.ChartAreas[0].AxisY.IsStartedFromZero;
            x2.Legends[0].Docking = Docking.Top;

            x2XSeries.ChartType = SeriesChartType.Point;
            x2YSeries.ChartType = SeriesChartType.Point;

            x2.Series.Add(x2XSeries);
            x2.Series.Add(x2YSeries);

            x2.Series[0].YAxisType = AxisType.Primary;
            x2.Series[1].YAxisType = AxisType.Secondary;
            #endregion

            #region initialize y1
            y1.ChartAreas[0].AxisX.Minimum = 0;
            y1.ChartAreas[0].AxisX.Maximum = samples;
            y1.ChartAreas[0].AxisY.Minimum = 0;
            y1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            y1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            y1.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            y1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            y1.ChartAreas[0].AxisY2.IsStartedFromZero = y1.ChartAreas[0].AxisY.IsStartedFromZero;
            y1.Legends[0].Docking = Docking.Top;

            y1XSeries.ChartType = SeriesChartType.Point;
            y1YSeries.ChartType = SeriesChartType.Point;

            y1.Series.Add(y1XSeries);
            y1.Series.Add(y1YSeries);

            y1.Series[0].YAxisType = AxisType.Primary;
            y1.Series[1].YAxisType = AxisType.Secondary;
            #endregion

            #region initialize y2
            y2.ChartAreas[0].AxisX.Minimum = 0;
            y2.ChartAreas[0].AxisX.Maximum = samples;
            y2.ChartAreas[0].AxisY.Minimum = 0;
            y2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            y2.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            y2.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            y2.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            y2.ChartAreas[0].AxisY2.IsStartedFromZero = y2.ChartAreas[0].AxisY.IsStartedFromZero;
            y2.Legends[0].Docking = Docking.Top;

            y2XSeries.ChartType = SeriesChartType.Point;
            y2YSeries.ChartType = SeriesChartType.Point;

            y2.Series.Add(y2XSeries);
            y2.Series.Add(y2YSeries);

            y2.Series[0].YAxisType = AxisType.Primary;
            y2.Series[1].YAxisType = AxisType.Secondary;
            #endregion

            #region initialize z1
            z1.ChartAreas[0].AxisX.Minimum = 0;
            z1.ChartAreas[0].AxisX.Maximum = samples;
            z1.ChartAreas[0].AxisY.Minimum = 0;
            z1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            z1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            z1.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            z1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            z1.ChartAreas[0].AxisY2.IsStartedFromZero = z1.ChartAreas[0].AxisY.IsStartedFromZero;
            z1.Legends[0].Docking = Docking.Top;

            z1XSeries.ChartType = SeriesChartType.Point;
            z1YSeries.ChartType = SeriesChartType.Point;

            z1.Series.Add(z1XSeries);
            z1.Series.Add(z1YSeries);

            z1.Series[0].YAxisType = AxisType.Primary;
            z1.Series[1].YAxisType = AxisType.Secondary;
            #endregion

            #region initialize z2
            z2.ChartAreas[0].AxisX.Minimum = 0;
            z2.ChartAreas[0].AxisX.Maximum = samples;
            z2.ChartAreas[0].AxisY.Minimum = 0;
            z2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            z2.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            z2.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            z2.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            z2.ChartAreas[0].AxisY2.IsStartedFromZero = z2.ChartAreas[0].AxisY.IsStartedFromZero;
            z2.Legends[0].Docking = Docking.Top;

            z2XSeries.ChartType = SeriesChartType.Point;
            z2YSeries.ChartType = SeriesChartType.Point;

            z2.Series.Add(z2XSeries);
            z2.Series.Add(z2YSeries);

            z2.Series[0].YAxisType = AxisType.Primary;
            z2.Series[1].YAxisType = AxisType.Secondary;

            #endregion

            #region initialize power

            power.ChartAreas[0].AxisX.Minimum = 0;
            power.ChartAreas[0].AxisX.Maximum = samples;
            power.ChartAreas[0].AxisY.Minimum = 0;
            power.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            power.Legends[0].Docking = Docking.Top;

            x1PowerSeries.ChartType = SeriesChartType.Point;
            x2PowerSeries.ChartType = SeriesChartType.Point;
            y1PowerSeries.ChartType = SeriesChartType.Point;
            y2PowerSeries.ChartType = SeriesChartType.Point;
            z1PowerSeries.ChartType = SeriesChartType.Point;
            z2PowerSeries.ChartType = SeriesChartType.Point;

            power.Series.Add(x1PowerSeries);
            power.Series.Add(x2PowerSeries);
            power.Series.Add(y1PowerSeries);
            power.Series.Add(y2PowerSeries);
            power.Series.Add(z1PowerSeries);
            power.Series.Add(z2PowerSeries);


            #endregion

        }       

        private void Add(float[] newValues)
        {
            counter++;
            Array.Copy(x1X, 1, x1X, 0, x1X.Length - 1);
            Array.Copy(x1Y, 1, x1Y, 0, x1Y.Length - 1);
            Array.Copy(x2X, 1, x2X, 0, x2X.Length - 1);
            Array.Copy(x2Y, 1, x2Y, 0, x2Y.Length - 1);
            Array.Copy(y1X, 1, y1X, 0, y1X.Length - 1);
            Array.Copy(y1Y, 1, y1Y, 0, y1Y.Length - 1);
            Array.Copy(y2X, 1, y2X, 0, y2X.Length - 1);
            Array.Copy(y2Y, 1, y2Y, 0, y2Y.Length - 1);
            Array.Copy(z1X, 1, z1X, 0, z1X.Length - 1);
            Array.Copy(z1Y, 1, z1Y, 0, z1Y.Length - 1);
            Array.Copy(z2X, 1, z2X, 0, z2X.Length - 1);
            Array.Copy(z2Y, 1, z2Y, 0, z2Y.Length - 1);

            x1X[samples - 1] = newValues[0];
            x1Y[samples - 1] = newValues[1];
            x2X[samples - 1] = newValues[2];
            x2Y[samples - 1] = newValues[3];

            y1X[samples - 1] = newValues[4];
            y1Y[samples - 1] = newValues[5];
            y2X[samples - 1] = newValues[6];
            y2Y[samples - 1] = newValues[7];

            z1X[samples - 1] = newValues[8];
            z1Y[samples - 1] = newValues[9];
            z2X[samples - 1] = newValues[10];
            z2Y[samples - 1] = newValues[11];
        }

        private void CollectData()
        {
            int i = 0;
            float test = 0, test1 = 0, test2 = 0;
            var random = new Random();
            float[] newValue = new float[18];
            do
            {
                x1Diode.ReadSumDiffSignals(ref test, ref test1, ref test2);
                //for (i = 0; i < 18; i++)
                //{
                //    newValue[i] = 5;
                //    //(float) random.NextDouble();
                //}

                Add(newValue);
                UpdateGraphs(1);
                UpdateTextBoxes(1);
                Thread.Sleep(_delay);
                i++;
            } while (!_stop);
            _collectorThread = null;           
        }

        private void UpdateTextBoxes(int e)
        {
            if(InvokeRequired)
            {
                myGUICallback callback = UpdateTextBoxes;
                Invoke(callback, new object[] {e});
            }
            else
            {
                #region x1
                meanx1X.Text = Convert.ToString(Mean(x1X));
                meanx1Y.Text = Convert.ToString(Mean(x1Y));
                meanx1P.Text = Convert.ToString(Mean(x1Power));
                actualx1X.Text = Convert.ToString(x1X[samples - 1]);
                actualx1Y.Text = Convert.ToString(x1Y[samples - 1]);
                actualx1P.Text = Convert.ToString(x1Power[samples - 1]);
                #endregion

                #region x2
                meanx2X.Text = Convert.ToString(Mean(x2X));
                meanx2Y.Text = Convert.ToString(Mean(x2Y));
                meanx2P.Text = Convert.ToString(Mean(x2Power));
                actualx2X.Text = Convert.ToString(x2X[samples - 1]);
                actualx2Y.Text = Convert.ToString(x2Y[samples - 1]);
                actualx2P.Text = Convert.ToString(x2Power[samples - 1]);
                #endregion

                #region y1
                meany1X.Text = Convert.ToString(Mean(y1X));
                meany1Y.Text = Convert.ToString(Mean(y1Y));
                meany1P.Text = Convert.ToString(Mean(y1Power));
                actualy1X.Text = Convert.ToString(y1X[samples - 1]);
                actualy1Y.Text = Convert.ToString(y1Y[samples - 1]);
                actualy1P.Text = Convert.ToString(y1Power[samples - 1]);
                #endregion

                #region y2
                meany2X.Text = Convert.ToString(Mean(y2X));
                meany2Y.Text = Convert.ToString(Mean(y2Y));
                meany2P.Text = Convert.ToString(Mean(y2Power));
                actualy2X.Text = Convert.ToString(y2X[samples - 1]);
                actualy2Y.Text = Convert.ToString(y2Y[samples - 1]);
                actualy2P.Text = Convert.ToString(y2Power[samples - 1]);
                #endregion

                #region z1
                meanz1X.Text = Convert.ToString(Mean(z1X));
                meanz1Y.Text = Convert.ToString(Mean(z1Y));
                meanz1P.Text = Convert.ToString(Mean(z1Power));
                actualz1X.Text = Convert.ToString(z1X[samples - 1]);
                actualz1Y.Text = Convert.ToString(z1Y[samples - 1]);
                actualz1P.Text = Convert.ToString(z1Power[samples - 1]);
                #endregion

                #region z2
                meanz2X.Text = Convert.ToString(Mean(z2X));
                meanz2Y.Text = Convert.ToString(Mean(z2Y));
                meanz2P.Text = Convert.ToString(Mean(z2Power));
                actualz2X.Text = Convert.ToString(z2X[samples - 1]);
                actualz2Y.Text = Convert.ToString(z2Y[samples - 1]);
                actualz2P.Text = Convert.ToString(z2Power[samples - 1]);
                #endregion
            }
        }

        private void UpdateGraphs(int e)
        {
            if (InvokeRequired)
            {
                myGUICallback callback = UpdateGraphs;
                Invoke(callback, new object[] {e});
            }
            else
            {
                #region x1
                x1XSeries.Points.Clear();
                x1XSeries.Points.DataBindY(x1X);
                x1YSeries.Points.Clear();
                x1YSeries.Points.DataBindY(x1Y);
                x1.Update();
                #endregion

                #region x2
                x2XSeries.Points.Clear();
                x2XSeries.Points.DataBindY(x2X);
                x2YSeries.Points.Clear();
                x2YSeries.Points.DataBindY(x2Y);
                x2.Update();
                #endregion

                #region y1
                y1XSeries.Points.Clear();
                y1XSeries.Points.DataBindY(y1X);
                y1YSeries.Points.Clear();
                y1YSeries.Points.DataBindY(y1Y);
                y1.Update();
                #endregion

                #region y2
                y2XSeries.Points.Clear();
                y2XSeries.Points.DataBindY(y2X);
                y2YSeries.Points.Clear();
                y2YSeries.Points.DataBindY(y2Y);
                y2.Update();
                #endregion

                #region z1
                z1XSeries.Points.Clear();
                z1XSeries.Points.DataBindY(z1X);
                z1YSeries.Points.Clear();
                z1YSeries.Points.DataBindY(z1Y);
                z1.Update();
                #endregion

                #region z2
                z2XSeries.Points.Clear();
                z2XSeries.Points.DataBindY(z2X);
                z2YSeries.Points.Clear();
                z2YSeries.Points.DataBindY(z2Y);
                z2.Update();
                #endregion
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            int input;
            try
            {
                input = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception)
            {
                input = 0;
            }
            
            if (input > 0)
            {
                _delay = input;
                textBox1.BackColor = Color.White;
            }
            else
            {
                textBox1.BackColor = Color.Red;
                return;
            }

            if (_collectorThread == null)
            {
                textBox1.Enabled = false;
                _collectorThread = new Thread(CollectData) { Name = "DataCollector", IsBackground = true };
                _stop = false;
                _collectorThread.Start();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _stop = true;
            textBox1.Enabled = true;
        } 
        
        private float Mean(float[] data)
        {
            float mean = 0;
            if(counter < samples)
            {
                for (int i = samples - 1; i > samples - (counter + 1); i--)
                    mean += data[i];
                mean /= counter;
            }
            else
            {
                for (int i = 0; i < samples; i++)
                    mean += data[i];
                mean /= samples;
            }
            return mean;
        }
    }
}