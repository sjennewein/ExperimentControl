namespace AnalogOutput
{
    partial class AnalogOutput
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
            this.tabControl_pattern = new System.Windows.Forms.TabControl();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_Load = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.panel_Network = new System.Windows.Forms.Panel();
            this.button_Hoops = new System.Windows.Forms.Button();
            this.label_Sync = new System.Windows.Forms.Label();
            this.button_Sync = new System.Windows.Forms.Button();
            this.groupBox_Info = new System.Windows.Forms.GroupBox();
            this.label_RunCounter = new System.Windows.Forms.Label();
            this.label_run = new System.Windows.Forms.Label();
            this.label_CyclePerRun = new System.Windows.Forms.Label();
            this.label_slash = new System.Windows.Forms.Label();
            this.label_CycleCounter = new System.Windows.Forms.Label();
            this.label_Cycle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_pattern
            // 
            this.tabControl_pattern.Location = new System.Drawing.Point(12, 104);
            this.tabControl_pattern.Name = "tabControl_pattern";
            this.tabControl_pattern.SelectedIndex = 0;
            this.tabControl_pattern.Size = new System.Drawing.Size(1260, 851);
            this.tabControl_pattern.TabIndex = 0;
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(1197, 12);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(75, 23);
            this.button_Start.TabIndex = 1;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(1197, 41);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(75, 23);
            this.button_Stop.TabIndex = 2;
            this.button_Stop.Text = "Stop";
            this.button_Stop.UseVisualStyleBackColor = true;
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(1116, 41);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(75, 23);
            this.button_Load.TabIndex = 3;
            this.button_Load.Text = "Load";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(1116, 12);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 4;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // panel_Network
            // 
            this.panel_Network.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Network.Location = new System.Drawing.Point(499, 12);
            this.panel_Network.Name = "panel_Network";
            this.panel_Network.Size = new System.Drawing.Size(245, 86);
            this.panel_Network.TabIndex = 5;
            // 
            // button_Hoops
            // 
            this.button_Hoops.Location = new System.Drawing.Point(418, 12);
            this.button_Hoops.Name = "button_Hoops";
            this.button_Hoops.Size = new System.Drawing.Size(75, 23);
            this.button_Hoops.TabIndex = 6;
            this.button_Hoops.Text = "Hooping";
            this.button_Hoops.UseVisualStyleBackColor = true;
            this.button_Hoops.Click += new System.EventHandler(this.button_Hoops_Click);
            // 
            // label_Sync
            // 
            this.label_Sync.AutoSize = true;
            this.label_Sync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(20)))), ((int)(((byte)(94)))));
            this.label_Sync.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Sync.Location = new System.Drawing.Point(1015, 14);
            this.label_Sync.Name = "label_Sync";
            this.label_Sync.Size = new System.Drawing.Size(95, 24);
            this.label_Sync.TabIndex = 7;
            this.label_Sync.Text = "Unsynced";
            // 
            // button_Sync
            // 
            this.button_Sync.Location = new System.Drawing.Point(1033, 41);
            this.button_Sync.Name = "button_Sync";
            this.button_Sync.Size = new System.Drawing.Size(61, 23);
            this.button_Sync.TabIndex = 8;
            this.button_Sync.Text = "Sync";
            this.button_Sync.UseVisualStyleBackColor = true;
            this.button_Sync.Click += new System.EventHandler(this.button_Sync_Click);
            // 
            // groupBox_Info
            // 
            this.groupBox_Info.Controls.Add(this.label_RunCounter);
            this.groupBox_Info.Controls.Add(this.label_run);
            this.groupBox_Info.Controls.Add(this.label_CyclePerRun);
            this.groupBox_Info.Controls.Add(this.label_slash);
            this.groupBox_Info.Controls.Add(this.label_CycleCounter);
            this.groupBox_Info.Controls.Add(this.label_Cycle);
            this.groupBox_Info.Controls.Add(this.label1);
            this.groupBox_Info.Location = new System.Drawing.Point(809, 12);
            this.groupBox_Info.Name = "groupBox_Info";
            this.groupBox_Info.Size = new System.Drawing.Size(200, 86);
            this.groupBox_Info.TabIndex = 9;
            this.groupBox_Info.TabStop = false;
            this.groupBox_Info.Text = "Information";
            // 
            // label_RunCounter
            // 
            this.label_RunCounter.AutoSize = true;
            this.label_RunCounter.Location = new System.Drawing.Point(84, 61);
            this.label_RunCounter.Name = "label_RunCounter";
            this.label_RunCounter.Size = new System.Drawing.Size(13, 13);
            this.label_RunCounter.TabIndex = 16;
            this.label_RunCounter.Text = "0";
            // 
            // label_run
            // 
            this.label_run.AutoSize = true;
            this.label_run.Location = new System.Drawing.Point(10, 61);
            this.label_run.Name = "label_run";
            this.label_run.Size = new System.Drawing.Size(62, 13);
            this.label_run.TabIndex = 15;
            this.label_run.Text = "Runs done:";
            // 
            // label_CyclePerRun
            // 
            this.label_CyclePerRun.AutoSize = true;
            this.label_CyclePerRun.Location = new System.Drawing.Point(151, 44);
            this.label_CyclePerRun.Name = "label_CyclePerRun";
            this.label_CyclePerRun.Size = new System.Drawing.Size(13, 13);
            this.label_CyclePerRun.TabIndex = 14;
            this.label_CyclePerRun.Text = "0";
            // 
            // label_slash
            // 
            this.label_slash.AutoSize = true;
            this.label_slash.Location = new System.Drawing.Point(133, 44);
            this.label_slash.Name = "label_slash";
            this.label_slash.Size = new System.Drawing.Size(12, 13);
            this.label_slash.TabIndex = 13;
            this.label_slash.Text = "/";
            // 
            // label_CycleCounter
            // 
            this.label_CycleCounter.AutoSize = true;
            this.label_CycleCounter.Location = new System.Drawing.Point(84, 44);
            this.label_CycleCounter.Name = "label_CycleCounter";
            this.label_CycleCounter.Size = new System.Drawing.Size(13, 13);
            this.label_CycleCounter.TabIndex = 12;
            this.label_CycleCounter.Text = "0";
            // 
            // label_Cycle
            // 
            this.label_Cycle.AutoSize = true;
            this.label_Cycle.Location = new System.Drawing.Point(10, 44);
            this.label_Cycle.Name = "label_Cycle";
            this.label_Cycle.Size = new System.Drawing.Size(68, 13);
            this.label_Cycle.TabIndex = 11;
            this.label_Cycle.Text = "Cycles done:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(20)))), ((int)(((byte)(94)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "Stopped";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Sample rate: 500 kHz";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Shortest duration: 2 μs";
            // 
            // AnalogOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 976);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox_Info);
            this.Controls.Add(this.button_Sync);
            this.Controls.Add(this.label_Sync);
            this.Controls.Add(this.button_Hoops);
            this.Controls.Add(this.panel_Network);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.tabControl_pattern);
            this.Name = "AnalogOutput";
            this.Text = "Form1";
            this.groupBox_Info.ResumeLayout(false);
            this.groupBox_Info.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_pattern;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Panel panel_Network;
        private System.Windows.Forms.Button button_Hoops;
        private System.Windows.Forms.Label label_Sync;
        private System.Windows.Forms.Button button_Sync;
        private System.Windows.Forms.GroupBox groupBox_Info;
        private System.Windows.Forms.Label label_RunCounter;
        private System.Windows.Forms.Label label_run;
        private System.Windows.Forms.Label label_CyclePerRun;
        private System.Windows.Forms.Label label_slash;
        private System.Windows.Forms.Label label_CycleCounter;
        private System.Windows.Forms.Label label_Cycle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

