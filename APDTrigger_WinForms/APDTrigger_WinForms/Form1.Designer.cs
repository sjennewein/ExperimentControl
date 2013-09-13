namespace APDTrigger_WinForms
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.runsInput = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.stop_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.detectionInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.thresholdInput = new System.Windows.Forms.TextBox();
            this.cyclesInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.binningInput = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.acquireInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.apdInput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.lightningChartBasic1 = new Arction.LightningChartBasic.LightningChartBasic();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.runsInput);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.stop_button);
            this.groupBox1.Controls.Add(this.start_button);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.detectionInput);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.thresholdInput);
            this.groupBox1.Controls.Add(this.cyclesInput);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.binningInput);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 199);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Trigger Settings";
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
            // runsInput
            // 
            this.runsInput.Location = new System.Drawing.Point(106, 112);
            this.runsInput.Name = "runsInput";
            this.runsInput.Size = new System.Drawing.Size(53, 20);
            this.runsInput.TabIndex = 11;
            // 
            // radioButton2
            // 
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(96, 145);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 17);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Endless";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.triggerRadioButton_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 145);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(70, 17);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.Text = "Triggered";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.triggerRadioButton_CheckedChanged);
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(103, 170);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(59, 23);
            this.stop_button.TabIndex = 7;
            this.stop_button.Text = "Stop";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(6, 170);
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
            // detectionInput
            // 
            this.detectionInput.Location = new System.Drawing.Point(106, 64);
            this.detectionInput.Name = "detectionInput";
            this.detectionInput.Size = new System.Drawing.Size(53, 20);
            this.detectionInput.TabIndex = 4;
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
            // cyclesInput
            // 
            this.cyclesInput.Location = new System.Drawing.Point(106, 90);
            this.cyclesInput.Name = "cyclesInput";
            this.cyclesInput.Size = new System.Drawing.Size(53, 20);
            this.cyclesInput.TabIndex = 9;
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
            // binningInput
            // 
            this.binningInput.Location = new System.Drawing.Point(106, 19);
            this.binningInput.Name = "binningInput";
            this.binningInput.Size = new System.Drawing.Size(53, 20);
            this.binningInput.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.acquireInput);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.apdInput);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Location = new System.Drawing.Point(12, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 103);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Recapture Settings";
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
            // acquireInput
            // 
            this.acquireInput.Location = new System.Drawing.Point(109, 77);
            this.acquireInput.Name = "acquireInput";
            this.acquireInput.Size = new System.Drawing.Size(53, 20);
            this.acquireInput.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "APD Binsize:";
            // 
            // apdInput
            // 
            this.apdInput.Location = new System.Drawing.Point(109, 55);
            this.apdInput.Name = "apdInput";
            this.apdInput.Size = new System.Drawing.Size(53, 20);
            this.apdInput.TabIndex = 16;
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
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(135, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButton4.Size = new System.Drawing.Size(25, 30);
            this.radioButton4.TabIndex = 11;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "No";
            this.radioButton4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton3.Location = new System.Drawing.Point(104, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButton3.Size = new System.Drawing.Size(29, 30);
            this.radioButton3.TabIndex = 9;
            this.radioButton3.Text = "Yes";
            this.radioButton3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // lightningChartBasic1
            // 
            this.lightningChartBasic1.AllowPan = true;
            this.lightningChartBasic1.AllowZoom = true;
            this.lightningChartBasic1.AntiAliasLevel = ((uint)(4u));
            this.lightningChartBasic1.AutoShrinkYAxesGap = true;
            this.lightningChartBasic1.AutoYFit = ((Arction.LightningChartBasic.AutoYFit)(resources.GetObject("lightningChartBasic1.AutoYFit")));
            this.lightningChartBasic1.BackColor = System.Drawing.Color.LightGray;
            this.lightningChartBasic1.Background = ((Arction.LightningChartBasic.Fill)(resources.GetObject("lightningChartBasic1.Background")));
            this.lightningChartBasic1.Bands = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.Band>)(resources.GetObject("lightningChartBasic1.Bands")));
            this.lightningChartBasic1.BarSeries = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.BarSeries>)(resources.GetObject("lightningChartBasic1.BarSeries")));
            this.lightningChartBasic1.ChartEventMarkers = ((System.Collections.Generic.List<Arction.LightningChartBasic.EventMarkers.ChartEventMarker>)(resources.GetObject("lightningChartBasic1.ChartEventMarkers")));
            this.lightningChartBasic1.ConstantLines = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.ConstantLine>)(resources.GetObject("lightningChartBasic1.ConstantLines")));
            this.lightningChartBasic1.DropOldEventMarkers = true;
            this.lightningChartBasic1.DropOldSeriesData = true;
            this.lightningChartBasic1.FreeformPointLineSeries = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.FreeformPointLineSeries>)(resources.GetObject("lightningChartBasic1.FreeformPointLineSeries")));
            this.lightningChartBasic1.GraphBackground = ((Arction.LightningChartBasic.Fill)(resources.GetObject("lightningChartBasic1.GraphBackground")));
            this.lightningChartBasic1.GraphMargins = new System.Windows.Forms.Padding(80, 20, 30, 60);
            this.lightningChartBasic1.LegendBox = ((Arction.LightningChartBasic.LegendBox)(resources.GetObject("lightningChartBasic1.LegendBox")));
            this.lightningChartBasic1.LineSeriesCursors = ((System.Collections.Generic.List<Arction.LightningChartBasic.LineSeriesCursor>)(resources.GetObject("lightningChartBasic1.LineSeriesCursors")));
            this.lightningChartBasic1.Location = new System.Drawing.Point(186, 12);
            this.lightningChartBasic1.MinimumSize = new System.Drawing.Size(120, 90);
            this.lightningChartBasic1.MouseInteraction = true;
            this.lightningChartBasic1.Name = "lightningChartBasic1";
            this.lightningChartBasic1.PointLineSeries = ((System.Collections.Generic.List<Arction.LightningChartBasic.Series.PointLineSeries>)(resources.GetObject("lightningChartBasic1.PointLineSeries")));
            this.lightningChartBasic1.RaiseEventsAfterDraw = false;
            this.lightningChartBasic1.ScrollBars = ((System.Collections.Generic.List<Arction.LightningChartBasic.ScrollBar>)(resources.GetObject("lightningChartBasic1.ScrollBars")));
            this.lightningChartBasic1.Size = new System.Drawing.Size(530, 467);
            this.lightningChartBasic1.StackedYAxesGap = 20;
            this.lightningChartBasic1.TabIndex = 2;
            this.lightningChartBasic1.ThrowChartExceptions = false;
            this.lightningChartBasic1.Title = ((Arction.LightningChartBasic.Titles.ChartTitle)(resources.GetObject("lightningChartBasic1.Title")));
            this.lightningChartBasic1.XAxis = ((Arction.LightningChartBasic.Axes.AxisX)(resources.GetObject("lightningChartBasic1.XAxis")));
            this.lightningChartBasic1.YAxes = ((System.Collections.Generic.List<Arction.LightningChartBasic.Axes.AxisY>)(resources.GetObject("lightningChartBasic1.YAxes")));
            this.lightningChartBasic1.YAxesLayout = Arction.LightningChartBasic.YAxesLayout.LayeredCommonXAxis;
            this.lightningChartBasic1.ZoomFactor = 2D;
            this.lightningChartBasic1.ZoomRectLine = ((Arction.LightningChartBasic.LineStyle)(resources.GetObject("lightningChartBasic1.ZoomRectLine")));
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox13);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.textBox12);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.textBox11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBox10);
            this.groupBox3.Controls.Add(this.textBox9);
            this.groupBox3.Controls.Add(this.textBox8);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(12, 326);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 153);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Statistics";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(112, 108);
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(53, 20);
            this.textBox13.TabIndex = 28;
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
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(112, 85);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(53, 20);
            this.textBox12.TabIndex = 26;
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
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(112, 62);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(53, 20);
            this.textBox11.TabIndex = 24;
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
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(112, 36);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(53, 20);
            this.textBox10.TabIndex = 22;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(58, 36);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(53, 20);
            this.textBox9.TabIndex = 21;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(4, 36);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(53, 20);
            this.textBox8.TabIndex = 20;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 491);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lightningChartBasic1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Arction.LightningChartBasic.LightningChartBasic lightningChartBasic1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox detectionInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox thresholdInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox binningInput;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cyclesInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox runsInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox acquireInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox apdInput;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
    }
}

