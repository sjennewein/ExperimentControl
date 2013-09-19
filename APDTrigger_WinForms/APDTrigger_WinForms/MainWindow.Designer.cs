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
            this.saveCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_runsInput = new System.Windows.Forms.TextBox();
            this.radioButton_Endless = new System.Windows.Forms.RadioButton();
            this.radioButton_triggered = new System.Windows.Forms.RadioButton();
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
            this.label7 = new System.Windows.Forms.Label();
            this.radioButton_No = new System.Windows.Forms.RadioButton();
            this.radioButton_Yes = new System.Windows.Forms.RadioButton();
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
            this.groupBox_Trigger.SuspendLayout();
            this.groupBox_Recapture.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Trigger
            // 
            this.groupBox_Trigger.Controls.Add(this.saveCheckBox);
            this.groupBox_Trigger.Controls.Add(this.label8);
            this.groupBox_Trigger.Controls.Add(this.textBox_runsInput);
            this.groupBox_Trigger.Controls.Add(this.radioButton_Endless);
            this.groupBox_Trigger.Controls.Add(this.radioButton_triggered);
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
            this.groupBox_Trigger.Size = new System.Drawing.Size(168, 231);
            this.groupBox_Trigger.TabIndex = 0;
            this.groupBox_Trigger.TabStop = false;
            this.groupBox_Trigger.Text = "Trigger Settings";
            // 
            // saveCheckBox
            // 
            this.saveCheckBox.AutoSize = true;
            this.saveCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveCheckBox.Location = new System.Drawing.Point(64, 138);
            this.saveCheckBox.Name = "saveCheckBox";
            this.saveCheckBox.Size = new System.Drawing.Size(57, 17);
            this.saveCheckBox.TabIndex = 14;
            this.saveCheckBox.Text = "Save: ";
            this.saveCheckBox.UseVisualStyleBackColor = true;
            this.saveCheckBox.CheckedChanged += new System.EventHandler(this.saveCheckBox_CheckedChanged);
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
            // radioButton_Endless
            // 
            this.radioButton_Endless.Checked = true;
            this.radioButton_Endless.Location = new System.Drawing.Point(96, 169);
            this.radioButton_Endless.Name = "radioButton_Endless";
            this.radioButton_Endless.Size = new System.Drawing.Size(65, 17);
            this.radioButton_Endless.TabIndex = 0;
            this.radioButton_Endless.TabStop = true;
            this.radioButton_Endless.Text = "Endless";
            this.radioButton_Endless.UseVisualStyleBackColor = true;
            this.radioButton_Endless.CheckedChanged += new System.EventHandler(this.triggerRadioButton_CheckedChanged);
            // 
            // radioButton_triggered
            // 
            this.radioButton_triggered.AutoSize = true;
            this.radioButton_triggered.Location = new System.Drawing.Point(6, 169);
            this.radioButton_triggered.Name = "radioButton_triggered";
            this.radioButton_triggered.Size = new System.Drawing.Size(70, 17);
            this.radioButton_triggered.TabIndex = 8;
            this.radioButton_triggered.Text = "Triggered";
            this.radioButton_triggered.UseVisualStyleBackColor = true;
            this.radioButton_triggered.CheckedChanged += new System.EventHandler(this.triggerRadioButton_CheckedChanged);
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(106, 200);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(59, 23);
            this.stop_button.TabIndex = 7;
            this.stop_button.Text = "Stop";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(6, 200);
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
            this.groupBox_Recapture.Controls.Add(this.label7);
            this.groupBox_Recapture.Controls.Add(this.radioButton_No);
            this.groupBox_Recapture.Controls.Add(this.radioButton_Yes);
            this.groupBox_Recapture.Location = new System.Drawing.Point(12, 311);
            this.groupBox_Recapture.Name = "groupBox_Recapture";
            this.groupBox_Recapture.Size = new System.Drawing.Size(168, 103);
            this.groupBox_Recapture.TabIndex = 1;
            this.groupBox_Recapture.TabStop = false;
            this.groupBox_Recapture.Text = "Recapture Settings";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Samples to acquire:";
            // 
            // textBox_acquireInput
            // 
            this.textBox_acquireInput.Location = new System.Drawing.Point(109, 77);
            this.textBox_acquireInput.Name = "textBox_acquireInput";
            this.textBox_acquireInput.Size = new System.Drawing.Size(53, 20);
            this.textBox_acquireInput.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "APD Binsize:";
            // 
            // textBox_apdInput
            // 
            this.textBox_apdInput.Location = new System.Drawing.Point(109, 55);
            this.textBox_apdInput.Name = "textBox_apdInput";
            this.textBox_apdInput.Size = new System.Drawing.Size(53, 20);
            this.textBox_apdInput.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Recaputre:";
            // 
            // radioButton_No
            // 
            this.radioButton_No.AutoSize = true;
            this.radioButton_No.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton_No.Checked = true;
            this.radioButton_No.Location = new System.Drawing.Point(135, 19);
            this.radioButton_No.Name = "radioButton_No";
            this.radioButton_No.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButton_No.Size = new System.Drawing.Size(25, 30);
            this.radioButton_No.TabIndex = 11;
            this.radioButton_No.TabStop = true;
            this.radioButton_No.Text = "No";
            this.radioButton_No.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton_No.UseVisualStyleBackColor = true;
            this.radioButton_No.CheckedChanged += new System.EventHandler(this.radioButton_Recapture_CheckedChanged);
            // 
            // radioButton_Yes
            // 
            this.radioButton_Yes.AutoSize = true;
            this.radioButton_Yes.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton_Yes.Location = new System.Drawing.Point(104, 19);
            this.radioButton_Yes.Name = "radioButton_Yes";
            this.radioButton_Yes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButton_Yes.Size = new System.Drawing.Size(29, 30);
            this.radioButton_Yes.TabIndex = 9;
            this.radioButton_Yes.Text = "Yes";
            this.radioButton_Yes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton_Yes.UseVisualStyleBackColor = true;
            this.radioButton_Yes.CheckedChanged += new System.EventHandler(this.radioButton_Recapture_CheckedChanged);
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
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 829);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Trigger;
        private System.Windows.Forms.GroupBox groupBox_Recapture;
        private Arction.LightningChartBasic.LightningChartBasic apdSignal;
        private System.Windows.Forms.RadioButton radioButton_Endless;
        private System.Windows.Forms.RadioButton radioButton_triggered;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_detectionInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox thresholdInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_binningInput;
        private System.Windows.Forms.RadioButton radioButton_Yes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_cyclesInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton_No;
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
        private System.Windows.Forms.CheckBox saveCheckBox;
        private System.Windows.Forms.Timer ApdHistogramUpdate;
    }
}

