namespace APDTrigger_WinForms
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.groupBox_Trigger = new System.Windows.Forms.GroupBox();
            this.radioButton_Network = new System.Windows.Forms.RadioButton();
            this.checkBox_SaveHistogram = new System.Windows.Forms.CheckBox();
            this.CheckBox_SaveSignal = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_runsInput = new System.Windows.Forms.TextBox();
            this.radioButton_Monitor = new System.Windows.Forms.RadioButton();
            this.radioButton_Measurement = new System.Windows.Forms.RadioButton();
            this.stop_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_detectionInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.thresholdInput = new System.Windows.Forms.TextBox();
            this.textBox_cyclesInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_binningInput = new System.Windows.Forms.TextBox();
            this.groupBox_Recapture = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_acquireInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_apdInput = new System.Windows.Forms.TextBox();
            this.apdSignal = new Arction.LightningChartBasic.LightningChartBasic();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_NoAtoms = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_Atoms = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_RecaptureRate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_totalRuns = new System.Windows.Forms.TextBox();
            this.textBox_runsDone = new System.Windows.Forms.TextBox();
            this.textBox_CyclesDone = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.apdHistogram = new Arction.LightningChartBasic.LightningChartBasic();
            this.ApdSignalUpdate = new System.Windows.Forms.Timer(this.components);
            this.ApdHistogramUpdate = new System.Windows.Forms.Timer(this.components);
            this.groupBox_Histogram = new System.Windows.Forms.GroupBox();
            this.button_Rescale = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_StopFrequency = new System.Windows.Forms.Button();
            this.button_StartFrequency = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox_Frequency = new System.Windows.Forms.TextBox();
            this.groupBox_Trigger.SuspendLayout();
            this.groupBox_Recapture.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox_Histogram.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Trigger
            // 
            this.groupBox_Trigger.Controls.Add(this.radioButton_Network);
            this.groupBox_Trigger.Controls.Add(this.checkBox_SaveHistogram);
            this.groupBox_Trigger.Controls.Add(this.CheckBox_SaveSignal);
            this.groupBox_Trigger.Controls.Add(this.label8);
            this.groupBox_Trigger.Controls.Add(this.textBox_runsInput);
            this.groupBox_Trigger.Controls.Add(this.radioButton_Monitor);
            this.groupBox_Trigger.Controls.Add(this.radioButton_Measurement);
            this.groupBox_Trigger.Controls.Add(this.stop_button);
            this.groupBox_Trigger.Controls.Add(this.start_button);
            this.groupBox_Trigger.Controls.Add(this.label3);
            this.groupBox_Trigger.Controls.Add(this.textBox_detectionInput);
            this.groupBox_Trigger.Controls.Add(this.label2);
            this.groupBox_Trigger.Controls.Add(this.label4);
            this.groupBox_Trigger.Controls.Add(this.thresholdInput);
            this.groupBox_Trigger.Controls.Add(this.textBox_cyclesInput);
            this.groupBox_Trigger.Controls.Add(this.label1);
            this.groupBox_Trigger.Controls.Add(this.textBox_binningInput);
            this.groupBox_Trigger.Location = new System.Drawing.Point(12, 12);
            this.groupBox_Trigger.Name = "groupBox_Trigger";
            this.groupBox_Trigger.Size = new System.Drawing.Size(168, 270);
            this.groupBox_Trigger.TabIndex = 0;
            this.groupBox_Trigger.TabStop = false;
            this.groupBox_Trigger.Text = "Trigger Settings";
            // 
            // radioButton_Network
            // 
            this.radioButton_Network.AutoSize = true;
            this.radioButton_Network.Location = new System.Drawing.Point(75, 213);
            this.radioButton_Network.Name = "radioButton_Network";
            this.radioButton_Network.Size = new System.Drawing.Size(65, 17);
            this.radioButton_Network.TabIndex = 16;
            this.radioButton_Network.Text = "Network";
            this.radioButton_Network.UseVisualStyleBackColor = true;
            this.radioButton_Network.CheckedChanged += new System.EventHandler(this.triggerRadioButton_CheckedChanged);
            // 
            // checkBox_SaveHistogram
            // 
            this.checkBox_SaveHistogram.AutoSize = true;
            this.checkBox_SaveHistogram.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkBox_SaveHistogram.Location = new System.Drawing.Point(10, 161);
            this.checkBox_SaveHistogram.Name = "checkBox_SaveHistogram";
            this.checkBox_SaveHistogram.Size = new System.Drawing.Size(130, 17);
            this.checkBox_SaveHistogram.TabIndex = 15;
            this.checkBox_SaveHistogram.Text = "Save APD-Spektrum: ";
            this.checkBox_SaveHistogram.UseVisualStyleBackColor = true;
            this.checkBox_SaveHistogram.CheckedChanged += new System.EventHandler(this.checkBox_SaveHistogram_CheckedChanged);
            // 
            // CheckBox_SaveSignal
            // 
            this.CheckBox_SaveSignal.AutoSize = true;
            this.CheckBox_SaveSignal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CheckBox_SaveSignal.Location = new System.Drawing.Point(26, 138);
            this.CheckBox_SaveSignal.Name = "CheckBox_SaveSignal";
            this.CheckBox_SaveSignal.Size = new System.Drawing.Size(114, 17);
            this.CheckBox_SaveSignal.TabIndex = 14;
            this.CheckBox_SaveSignal.Text = "Save APD-Signal: ";
            this.CheckBox_SaveSignal.UseVisualStyleBackColor = true;
            this.CheckBox_SaveSignal.CheckedChanged += new System.EventHandler(this.saveCheckBox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(65, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Runs:";
            // 
            // textBox_runsInput
            // 
            this.textBox_runsInput.Location = new System.Drawing.Point(106, 112);
            this.textBox_runsInput.Name = "textBox_runsInput";
            this.textBox_runsInput.Size = new System.Drawing.Size(53, 20);
            this.textBox_runsInput.TabIndex = 11;
            // 
            // radioButton_Monitor
            // 
            this.radioButton_Monitor.Checked = true;
            this.radioButton_Monitor.Location = new System.Drawing.Point(6, 190);
            this.radioButton_Monitor.Name = "radioButton_Monitor";
            this.radioButton_Monitor.Size = new System.Drawing.Size(65, 17);
            this.radioButton_Monitor.TabIndex = 0;
            this.radioButton_Monitor.TabStop = true;
            this.radioButton_Monitor.Text = "Monitor";
            this.radioButton_Monitor.UseVisualStyleBackColor = true;
            this.radioButton_Monitor.CheckedChanged += new System.EventHandler(this.triggerRadioButton_CheckedChanged);
            // 
            // radioButton_Measurement
            // 
            this.radioButton_Measurement.AutoSize = true;
            this.radioButton_Measurement.Location = new System.Drawing.Point(75, 190);
            this.radioButton_Measurement.Name = "radioButton_Measurement";
            this.radioButton_Measurement.Size = new System.Drawing.Size(89, 17);
            this.radioButton_Measurement.TabIndex = 8;
            this.radioButton_Measurement.Text = "Measurement";
            this.radioButton_Measurement.UseVisualStyleBackColor = true;
            this.radioButton_Measurement.CheckedChanged += new System.EventHandler(this.triggerRadioButton_CheckedChanged);
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(103, 241);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(59, 23);
            this.stop_button.TabIndex = 7;
            this.stop_button.Text = "Stop";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(6, 241);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(59, 23);
            this.start_button.TabIndex = 6;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Detection bins:";
            // 
            // textBox_detectionInput
            // 
            this.textBox_detectionInput.Location = new System.Drawing.Point(106, 64);
            this.textBox_detectionInput.Name = "textBox_detectionInput";
            this.textBox_detectionInput.Size = new System.Drawing.Size(53, 20);
            this.textBox_detectionInput.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Threshold:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Cycles:";
            // 
            // thresholdInput
            // 
            this.thresholdInput.Location = new System.Drawing.Point(106, 42);
            this.thresholdInput.Name = "thresholdInput";
            this.thresholdInput.Size = new System.Drawing.Size(53, 20);
            this.thresholdInput.TabIndex = 2;
            // 
            // textBox_cyclesInput
            // 
            this.textBox_cyclesInput.Location = new System.Drawing.Point(106, 90);
            this.textBox_cyclesInput.Name = "textBox_cyclesInput";
            this.textBox_cyclesInput.Size = new System.Drawing.Size(53, 20);
            this.textBox_cyclesInput.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Binning (ms) :";
            // 
            // textBox_binningInput
            // 
            this.textBox_binningInput.Location = new System.Drawing.Point(106, 19);
            this.textBox_binningInput.Name = "textBox_binningInput";
            this.textBox_binningInput.Size = new System.Drawing.Size(53, 20);
            this.textBox_binningInput.TabIndex = 0;
            // 
            // groupBox_Recapture
            // 
            this.groupBox_Recapture.Controls.Add(this.label6);
            this.groupBox_Recapture.Controls.Add(this.textBox_acquireInput);
            this.groupBox_Recapture.Controls.Add(this.label5);
            this.groupBox_Recapture.Controls.Add(this.textBox_apdInput);
            this.groupBox_Recapture.Location = new System.Drawing.Point(12, 288);
            this.groupBox_Recapture.Name = "groupBox_Recapture";
            this.groupBox_Recapture.Size = new System.Drawing.Size(168, 69);
            this.groupBox_Recapture.TabIndex = 1;
            this.groupBox_Recapture.TabStop = false;
            this.groupBox_Recapture.Text = "Recapture Settings";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Samples to acquire:";
            // 
            // textBox_acquireInput
            // 
            this.textBox_acquireInput.Location = new System.Drawing.Point(109, 42);
            this.textBox_acquireInput.Name = "textBox_acquireInput";
            this.textBox_acquireInput.Size = new System.Drawing.Size(53, 20);
            this.textBox_acquireInput.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "APD Binsize:";
            // 
            // textBox_apdInput
            // 
            this.textBox_apdInput.Location = new System.Drawing.Point(109, 20);
            this.textBox_apdInput.Name = "textBox_apdInput";
            this.textBox_apdInput.Size = new System.Drawing.Size(53, 20);
            this.textBox_apdInput.TabIndex = 16;
            // 
            // apdSignal
            // 
            this.apdSignal.AllowPan = true;
            this.apdSignal.AllowZoom = true;
            this.apdSignal.AntiAliasLevel = ((uint)(0u));
            this.apdSignal.AutoShrinkYAxesGap = true;
            this.apdSignal.AutoYFit = ((Arction.LightningChartBasic.AutoYFit)(resources.GetObject("apdSignal.AutoYFit")));
            this.apdSignal.BackColor = System.Drawing.Color.LightGray;
            this.apdSignal.Background = ((Arction.LightningChartBasic.Fill)(resources.GetObject("apdSignal.Background")));
            this.apdSignal.Bands = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.Band>)(resources.GetObject("apdSignal.Bands")));
            this.apdSignal.BarSeries = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.BarSeries>)(resources.GetObject("apdSignal.BarSeries")));
            this.apdSignal.ChartEventMarkers = ((System.Collections.Generic.List<Arction.LightningChartBasic.EventMarkers.ChartEventMarker>)(resources.GetObject("apdSignal.ChartEventMarkers")));
            this.apdSignal.ConstantLines = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.ConstantLine>)(resources.GetObject("apdSignal.ConstantLines")));
            this.apdSignal.DropOldEventMarkers = true;
            this.apdSignal.DropOldSeriesData = true;
            this.apdSignal.FreeformPointLineSeries = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.FreeformPointLineSeries>)(resources.GetObject("apdSignal.FreeformPointLineSeries")));
            this.apdSignal.GraphBackground = ((Arction.LightningChartBasic.Fill)(resources.GetObject("apdSignal.GraphBackground")));
            this.apdSignal.GraphMargins = new System.Windows.Forms.Padding(80, 20, 30, 60);
            this.apdSignal.LegendBox = ((Arction.LightningChartBasic.LegendBox)(resources.GetObject("apdSignal.LegendBox")));
            this.apdSignal.LineSeriesCursors = ((System.Collections.Generic.List<Arction.LightningChartBasic.LineSeriesCursor>)(resources.GetObject("apdSignal.LineSeriesCursors")));
            this.apdSignal.Location = new System.Drawing.Point(186, 12);
            this.apdSignal.MinimumSize = new System.Drawing.Size(120, 90);
            this.apdSignal.MouseInteraction = true;
            this.apdSignal.Name = "apdSignal";
            this.apdSignal.PointLineSeries = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.PointLineSeries>)(resources.GetObject("apdSignal.PointLineSeries")));
            this.apdSignal.RaiseEventsAfterDraw = false;
            this.apdSignal.ScrollBars = ((System.Collections.Generic.List<Arction.LightningChartBasic.ScrollBar>)(resources.GetObject("apdSignal.ScrollBars")));
            this.apdSignal.Size = new System.Drawing.Size(530, 467);
            this.apdSignal.StackedYAxesGap = 20;
            this.apdSignal.TabIndex = 2;
            this.apdSignal.ThrowChartExceptions = false;
            this.apdSignal.Title = ((Arction.LightningChartBasic.Titles.ChartTitle)(resources.GetObject("apdSignal.Title")));
            this.apdSignal.XAxis = ((Arction.LightningChartBasic.Axes.AxisX)(resources.GetObject("apdSignal.XAxis")));
            this.apdSignal.YAxes = ((System.Collections.Generic.List<Arction.LightningChartBasic.Axes.AxisY>)(resources.GetObject("apdSignal.YAxes")));
            this.apdSignal.YAxesLayout = Arction.LightningChartBasic.YAxesLayout.LayeredCommonXAxis;
            this.apdSignal.ZoomFactor = 2D;
            this.apdSignal.ZoomRectLine = ((Arction.LightningChartBasic.LineStyle)(resources.GetObject("apdSignal.ZoomRectLine")));
            this.apdSignal.DoubleClick += new System.EventHandler(this.apdSignalChart_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_NoAtoms);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.textBox_Atoms);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.textBox_RecaptureRate);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBox_totalRuns);
            this.groupBox3.Controls.Add(this.textBox_runsDone);
            this.groupBox3.Controls.Add(this.textBox_CyclesDone);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(12, 664);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 153);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Statistics";
            // 
            // textBox_NoAtoms
            // 
            this.textBox_NoAtoms.Location = new System.Drawing.Point(112, 108);
            this.textBox_NoAtoms.Name = "textBox_NoAtoms";
            this.textBox_NoAtoms.ReadOnly = true;
            this.textBox_NoAtoms.Size = new System.Drawing.Size(53, 20);
            this.textBox_NoAtoms.TabIndex = 28;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(60, 111);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "No Atom:";
            // 
            // textBox_Atoms
            // 
            this.textBox_Atoms.Location = new System.Drawing.Point(112, 85);
            this.textBox_Atoms.Name = "textBox_Atoms";
            this.textBox_Atoms.ReadOnly = true;
            this.textBox_Atoms.Size = new System.Drawing.Size(53, 20);
            this.textBox_Atoms.TabIndex = 26;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(77, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "Atom:";
            // 
            // textBox_RecaptureRate
            // 
            this.textBox_RecaptureRate.Location = new System.Drawing.Point(112, 62);
            this.textBox_RecaptureRate.Name = "textBox_RecaptureRate";
            this.textBox_RecaptureRate.ReadOnly = true;
            this.textBox_RecaptureRate.Size = new System.Drawing.Size(53, 20);
            this.textBox_RecaptureRate.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Recapture Rate:";
            // 
            // textBox_totalRuns
            // 
            this.textBox_totalRuns.Location = new System.Drawing.Point(112, 36);
            this.textBox_totalRuns.Name = "textBox_totalRuns";
            this.textBox_totalRuns.ReadOnly = true;
            this.textBox_totalRuns.Size = new System.Drawing.Size(53, 20);
            this.textBox_totalRuns.TabIndex = 22;
            // 
            // textBox_runsDone
            // 
            this.textBox_runsDone.Location = new System.Drawing.Point(58, 36);
            this.textBox_runsDone.Name = "textBox_runsDone";
            this.textBox_runsDone.ReadOnly = true;
            this.textBox_runsDone.Size = new System.Drawing.Size(53, 20);
            this.textBox_runsDone.TabIndex = 21;
            // 
            // textBox_CyclesDone
            // 
            this.textBox_CyclesDone.Location = new System.Drawing.Point(4, 36);
            this.textBox_CyclesDone.Name = "textBox_CyclesDone";
            this.textBox_CyclesDone.ReadOnly = true;
            this.textBox_CyclesDone.Size = new System.Drawing.Size(53, 20);
            this.textBox_CyclesDone.TabIndex = 20;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(130, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Runs";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(96, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "out of";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(68, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Run";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(50, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "of";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Cycle";
            // 
            // apdHistogram
            // 
            this.apdHistogram.AllowPan = true;
            this.apdHistogram.AllowZoom = true;
            this.apdHistogram.AntiAliasLevel = ((uint)(0u));
            this.apdHistogram.AutoShrinkYAxesGap = true;
            this.apdHistogram.AutoYFit = ((Arction.LightningChartBasic.AutoYFit)(resources.GetObject("apdHistogram.AutoYFit")));
            this.apdHistogram.BackColor = System.Drawing.Color.LightGray;
            this.apdHistogram.Background = ((Arction.LightningChartBasic.Fill)(resources.GetObject("apdHistogram.Background")));
            this.apdHistogram.Bands = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.Band>)(resources.GetObject("apdHistogram.Bands")));
            this.apdHistogram.BarSeries = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.BarSeries>)(resources.GetObject("apdHistogram.BarSeries")));
            this.apdHistogram.ChartEventMarkers = ((System.Collections.Generic.List<Arction.LightningChartBasic.EventMarkers.ChartEventMarker>)(resources.GetObject("apdHistogram.ChartEventMarkers")));
            this.apdHistogram.ConstantLines = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.ConstantLine>)(resources.GetObject("apdHistogram.ConstantLines")));
            this.apdHistogram.DropOldEventMarkers = true;
            this.apdHistogram.DropOldSeriesData = true;
            this.apdHistogram.FreeformPointLineSeries = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.FreeformPointLineSeries>)(resources.GetObject("apdHistogram.FreeformPointLineSeries")));
            this.apdHistogram.GraphBackground = ((Arction.LightningChartBasic.Fill)(resources.GetObject("apdHistogram.GraphBackground")));
            this.apdHistogram.GraphMargins = new System.Windows.Forms.Padding(80, 20, 30, 60);
            this.apdHistogram.LegendBox = ((Arction.LightningChartBasic.LegendBox)(resources.GetObject("apdHistogram.LegendBox")));
            this.apdHistogram.LineSeriesCursors = ((System.Collections.Generic.List<Arction.LightningChartBasic.LineSeriesCursor>)(resources.GetObject("apdHistogram.LineSeriesCursors")));
            this.apdHistogram.Location = new System.Drawing.Point(186, 485);
            this.apdHistogram.MinimumSize = new System.Drawing.Size(120, 90);
            this.apdHistogram.MouseInteraction = true;
            this.apdHistogram.Name = "apdHistogram";
            this.apdHistogram.PointLineSeries = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.PointLineSeries>)(resources.GetObject("apdHistogram.PointLineSeries")));
            this.apdHistogram.RaiseEventsAfterDraw = false;
            this.apdHistogram.ScrollBars = ((System.Collections.Generic.List<Arction.LightningChartBasic.ScrollBar>)(resources.GetObject("apdHistogram.ScrollBars")));
            this.apdHistogram.Size = new System.Drawing.Size(530, 332);
            this.apdHistogram.StackedYAxesGap = 20;
            this.apdHistogram.TabIndex = 4;
            this.apdHistogram.ThrowChartExceptions = false;
            this.apdHistogram.Title = ((Arction.LightningChartBasic.Titles.ChartTitle)(resources.GetObject("apdHistogram.Title")));
            this.apdHistogram.XAxis = ((Arction.LightningChartBasic.Axes.AxisX)(resources.GetObject("apdHistogram.XAxis")));
            this.apdHistogram.YAxes = ((System.Collections.Generic.List<Arction.LightningChartBasic.Axes.AxisY>)(resources.GetObject("apdHistogram.YAxes")));
            this.apdHistogram.YAxesLayout = Arction.LightningChartBasic.YAxesLayout.LayeredCommonXAxis;
            this.apdHistogram.ZoomFactor = 2D;
            this.apdHistogram.ZoomRectLine = ((Arction.LightningChartBasic.LineStyle)(resources.GetObject("apdHistogram.ZoomRectLine")));
            // 
            // ApdSignalUpdate
            // 
            this.ApdSignalUpdate.Interval = 20;
            this.ApdSignalUpdate.Tick += new System.EventHandler(this.ApdSignalUpdate_Tick);
            // 
            // ApdHistogramUpdate
            // 
            this.ApdHistogramUpdate.Interval = 1000;
            this.ApdHistogramUpdate.Tick += new System.EventHandler(this.ApdHistogramUpdate_Tick);
            // 
            // groupBox_Histogram
            // 
            this.groupBox_Histogram.Controls.Add(this.button_Rescale);
            this.groupBox_Histogram.Controls.Add(this.radioButton2);
            this.groupBox_Histogram.Controls.Add(this.radioButton1);
            this.groupBox_Histogram.Location = new System.Drawing.Point(12, 572);
            this.groupBox_Histogram.Name = "groupBox_Histogram";
            this.groupBox_Histogram.Size = new System.Drawing.Size(168, 86);
            this.groupBox_Histogram.TabIndex = 5;
            this.groupBox_Histogram.TabStop = false;
            this.groupBox_Histogram.Text = "Histogram";
            // 
            // button_Rescale
            // 
            this.button_Rescale.Location = new System.Drawing.Point(84, 55);
            this.button_Rescale.Name = "button_Rescale";
            this.button_Rescale.Size = new System.Drawing.Size(75, 23);
            this.button_Rescale.TabIndex = 2;
            this.button_Rescale.Text = "Rescale";
            this.button_Rescale.UseVisualStyleBackColor = true;
            this.button_Rescale.Click += new System.EventHandler(this.button_Rescale_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(75, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(90, 30);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Signal Histogram";
            this.radioButton2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_HistogramDisplay_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton1.Location = new System.Drawing.Point(13, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(56, 30);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Spectrum";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_HistogramDisplay_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 363);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 94);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Network";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_StopFrequency);
            this.groupBox2.Controls.Add(this.button_StartFrequency);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.textBox_Frequency);
            this.groupBox2.Location = new System.Drawing.Point(12, 463);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 95);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Frequency Generator";
            // 
            // button_StopFrequency
            // 
            this.button_StopFrequency.Location = new System.Drawing.Point(103, 66);
            this.button_StopFrequency.Name = "button_StopFrequency";
            this.button_StopFrequency.Size = new System.Drawing.Size(59, 23);
            this.button_StopFrequency.TabIndex = 17;
            this.button_StopFrequency.Text = "Stop";
            this.button_StopFrequency.UseVisualStyleBackColor = true;
            this.button_StopFrequency.Click += new System.EventHandler(this.button_StopFrequency_Click);
            // 
            // button_StartFrequency
            // 
            this.button_StartFrequency.Location = new System.Drawing.Point(6, 66);
            this.button_StartFrequency.Name = "button_StartFrequency";
            this.button_StartFrequency.Size = new System.Drawing.Size(59, 23);
            this.button_StartFrequency.TabIndex = 16;
            this.button_StartFrequency.Text = "Start";
            this.button_StartFrequency.UseVisualStyleBackColor = true;
            this.button_StartFrequency.Click += new System.EventHandler(this.button_StartFrequency_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(18, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "Frequency (Hz):";
            // 
            // textBox_Frequency
            // 
            this.textBox_Frequency.Location = new System.Drawing.Point(109, 13);
            this.textBox_Frequency.Name = "textBox_Frequency";
            this.textBox_Frequency.Size = new System.Drawing.Size(53, 20);
            this.textBox_Frequency.TabIndex = 20;
            this.textBox_Frequency.Text = "1000";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 829);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_Histogram);
            this.Controls.Add(this.apdHistogram);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.apdSignal);
            this.Controls.Add(this.groupBox_Recapture);
            this.Controls.Add(this.groupBox_Trigger);
            this.Name = "MainWindow";
            this.Text = "APD Trigger";
            this.groupBox_Trigger.ResumeLayout(false);
            this.groupBox_Trigger.PerformLayout();
            this.groupBox_Recapture.ResumeLayout(false);
            this.groupBox_Recapture.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox_Histogram.ResumeLayout(false);
            this.groupBox_Histogram.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Trigger;
        private System.Windows.Forms.GroupBox groupBox_Recapture;
        private Arction.LightningChartBasic.LightningChartBasic apdSignal;
        private System.Windows.Forms.RadioButton radioButton_Monitor;
        private System.Windows.Forms.RadioButton radioButton_Measurement;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_detectionInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox thresholdInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_binningInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_cyclesInput;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_runsInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_acquireInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_apdInput;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox_NoAtoms;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_Atoms;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_RecaptureRate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_totalRuns;
        private System.Windows.Forms.TextBox textBox_runsDone;
        private System.Windows.Forms.TextBox textBox_CyclesDone;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private Arction.LightningChartBasic.LightningChartBasic apdHistogram;
        private System.Windows.Forms.Timer ApdSignalUpdate;
        private System.Windows.Forms.CheckBox CheckBox_SaveSignal;
        private System.Windows.Forms.Timer ApdHistogramUpdate;
        private System.Windows.Forms.CheckBox checkBox_SaveHistogram;
        private System.Windows.Forms.GroupBox groupBox_Histogram;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button_Rescale;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_Network;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_StopFrequency;
        private System.Windows.Forms.Button button_StartFrequency;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox_Frequency;
    }
}

