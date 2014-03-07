namespace DigitalOutput
{
    partial class DigitalMainwindow
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
            this.TabPanel_pattern = new System.Windows.Forms.TabControl();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Load = new System.Windows.Forms.Button();
            this.label_Buffer = new System.Windows.Forms.Label();
            this.button_Synchronize = new System.Windows.Forms.Button();
            this.groupBox_Buffer = new System.Windows.Forms.GroupBox();
            this.groupBox_Information = new System.Windows.Forms.GroupBox();
            this.label_CyclesPerRun = new System.Windows.Forms.Label();
            this.label_Of = new System.Windows.Forms.Label();
            this.label_RunCounter = new System.Windows.Forms.Label();
            this.label_Run = new System.Windows.Forms.Label();
            this.label_CycleCounter = new System.Windows.Forms.Label();
            this.label_Cycle = new System.Windows.Forms.Label();
            this.label_Status = new System.Windows.Forms.Label();
            this.groupBox_Network = new System.Windows.Forms.GroupBox();
            this.button_HulaHoop = new System.Windows.Forms.Button();
            this.groupBox_Loops = new System.Windows.Forms.GroupBox();
            this.groupBox_Buffer.SuspendLayout();
            this.groupBox_Information.SuspendLayout();
            this.groupBox_Loops.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabPanel_pattern
            // 
            this.TabPanel_pattern.Location = new System.Drawing.Point(12, 146);
            this.TabPanel_pattern.Name = "TabPanel_pattern";
            this.TabPanel_pattern.SelectedIndex = 0;
            this.TabPanel_pattern.Size = new System.Drawing.Size(1260, 789);
            this.TabPanel_pattern.TabIndex = 0;
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(1193, 30);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(75, 23);
            this.button_Start.TabIndex = 1;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(1193, 59);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(75, 23);
            this.button_Stop.TabIndex = 2;
            this.button_Stop.Text = "Stop";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(1193, 88);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(1193, 117);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(75, 23);
            this.button_Load.TabIndex = 4;
            this.button_Load.Text = "Load";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // label_Buffer
            // 
            this.label_Buffer.AutoSize = true;
            this.label_Buffer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(20)))), ((int)(((byte)(94)))));
            this.label_Buffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Buffer.Location = new System.Drawing.Point(7, 18);
            this.label_Buffer.MaximumSize = new System.Drawing.Size(74, 24);
            this.label_Buffer.MinimumSize = new System.Drawing.Size(74, 24);
            this.label_Buffer.Name = "label_Buffer";
            this.label_Buffer.Size = new System.Drawing.Size(74, 24);
            this.label_Buffer.TabIndex = 5;
            this.label_Buffer.Text = "Nope";
            this.label_Buffer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button_Synchronize
            // 
            this.button_Synchronize.Location = new System.Drawing.Point(6, 47);
            this.button_Synchronize.Name = "button_Synchronize";
            this.button_Synchronize.Size = new System.Drawing.Size(75, 23);
            this.button_Synchronize.TabIndex = 6;
            this.button_Synchronize.Text = "Synchronize";
            this.button_Synchronize.UseVisualStyleBackColor = true;
            this.button_Synchronize.Click += new System.EventHandler(this.button_Synchronize_Click);
            // 
            // groupBox_Buffer
            // 
            this.groupBox_Buffer.Controls.Add(this.label_Buffer);
            this.groupBox_Buffer.Controls.Add(this.button_Synchronize);
            this.groupBox_Buffer.Location = new System.Drawing.Point(1100, 12);
            this.groupBox_Buffer.Name = "groupBox_Buffer";
            this.groupBox_Buffer.Size = new System.Drawing.Size(87, 128);
            this.groupBox_Buffer.TabIndex = 7;
            this.groupBox_Buffer.TabStop = false;
            this.groupBox_Buffer.Text = "Buffer";
            // 
            // groupBox_Information
            // 
            this.groupBox_Information.Controls.Add(this.label_CyclesPerRun);
            this.groupBox_Information.Controls.Add(this.label_Of);
            this.groupBox_Information.Controls.Add(this.label_RunCounter);
            this.groupBox_Information.Controls.Add(this.label_Run);
            this.groupBox_Information.Controls.Add(this.label_CycleCounter);
            this.groupBox_Information.Controls.Add(this.label_Cycle);
            this.groupBox_Information.Controls.Add(this.label_Status);
            this.groupBox_Information.Location = new System.Drawing.Point(894, 12);
            this.groupBox_Information.Name = "groupBox_Information";
            this.groupBox_Information.Size = new System.Drawing.Size(200, 128);
            this.groupBox_Information.TabIndex = 8;
            this.groupBox_Information.TabStop = false;
            this.groupBox_Information.Text = "Information";
            // 
            // label_CyclesPerRun
            // 
            this.label_CyclesPerRun.AutoSize = true;
            this.label_CyclesPerRun.Location = new System.Drawing.Point(149, 57);
            this.label_CyclesPerRun.Name = "label_CyclesPerRun";
            this.label_CyclesPerRun.Size = new System.Drawing.Size(13, 13);
            this.label_CyclesPerRun.TabIndex = 14;
            this.label_CyclesPerRun.Text = "0";
            // 
            // label_Of
            // 
            this.label_Of.AutoSize = true;
            this.label_Of.Location = new System.Drawing.Point(131, 57);
            this.label_Of.Name = "label_Of";
            this.label_Of.Size = new System.Drawing.Size(12, 13);
            this.label_Of.TabIndex = 13;
            this.label_Of.Text = "/";
            // 
            // label_RunCounter
            // 
            this.label_RunCounter.AutoSize = true;
            this.label_RunCounter.Location = new System.Drawing.Point(81, 75);
            this.label_RunCounter.Name = "label_RunCounter";
            this.label_RunCounter.Size = new System.Drawing.Size(13, 13);
            this.label_RunCounter.TabIndex = 12;
            this.label_RunCounter.Text = "0";
            // 
            // label_Run
            // 
            this.label_Run.AutoSize = true;
            this.label_Run.Location = new System.Drawing.Point(7, 75);
            this.label_Run.Name = "label_Run";
            this.label_Run.Size = new System.Drawing.Size(62, 13);
            this.label_Run.TabIndex = 11;
            this.label_Run.Text = "Runs done:";
            // 
            // label_CycleCounter
            // 
            this.label_CycleCounter.AutoSize = true;
            this.label_CycleCounter.Location = new System.Drawing.Point(81, 57);
            this.label_CycleCounter.Name = "label_CycleCounter";
            this.label_CycleCounter.Size = new System.Drawing.Size(13, 13);
            this.label_CycleCounter.TabIndex = 10;
            this.label_CycleCounter.Text = "0";
            // 
            // label_Cycle
            // 
            this.label_Cycle.AutoSize = true;
            this.label_Cycle.Location = new System.Drawing.Point(7, 57);
            this.label_Cycle.Name = "label_Cycle";
            this.label_Cycle.Size = new System.Drawing.Size(68, 13);
            this.label_Cycle.TabIndex = 9;
            this.label_Cycle.Text = "Cycles done:";
            // 
            // label_Status
            // 
            this.label_Status.AutoSize = true;
            this.label_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(20)))), ((int)(((byte)(94)))));
            this.label_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Status.Location = new System.Drawing.Point(6, 18);
            this.label_Status.MaximumSize = new System.Drawing.Size(85, 24);
            this.label_Status.MinimumSize = new System.Drawing.Size(85, 24);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(85, 24);
            this.label_Status.TabIndex = 8;
            this.label_Status.Text = "Stopped";
            this.label_Status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox_Network
            // 
            this.groupBox_Network.Location = new System.Drawing.Point(688, 12);
            this.groupBox_Network.Name = "groupBox_Network";
            this.groupBox_Network.Size = new System.Drawing.Size(200, 128);
            this.groupBox_Network.TabIndex = 9;
            this.groupBox_Network.TabStop = false;
            this.groupBox_Network.Text = "Network";
            // 
            // button_HulaHoop
            // 
            this.button_HulaHoop.Location = new System.Drawing.Point(6, 19);
            this.button_HulaHoop.Name = "button_HulaHoop";
            this.button_HulaHoop.Size = new System.Drawing.Size(88, 23);
            this.button_HulaHoop.TabIndex = 10;
            this.button_HulaHoop.Text = "Open Window";
            this.button_HulaHoop.UseVisualStyleBackColor = true;
            this.button_HulaHoop.Click += new System.EventHandler(this.button_HulaHoop_Click);
            // 
            // groupBox_Loops
            // 
            this.groupBox_Loops.Controls.Add(this.button_HulaHoop);
            this.groupBox_Loops.Location = new System.Drawing.Point(582, 12);
            this.groupBox_Loops.Name = "groupBox_Loops";
            this.groupBox_Loops.Size = new System.Drawing.Size(100, 128);
            this.groupBox_Loops.TabIndex = 11;
            this.groupBox_Loops.TabStop = false;
            this.groupBox_Loops.Text = "Hula Hoops";
            // 
            // DigitalMainwindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 1006);
            this.Controls.Add(this.groupBox_Loops);
            this.Controls.Add(this.groupBox_Network);
            this.Controls.Add(this.groupBox_Information);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.groupBox_Buffer);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.TabPanel_pattern);
            this.Name = "DigitalMainwindow";
            this.Text = "DigitalOutput";
            this.groupBox_Buffer.ResumeLayout(false);
            this.groupBox_Buffer.PerformLayout();
            this.groupBox_Information.ResumeLayout(false);
            this.groupBox_Information.PerformLayout();
            this.groupBox_Loops.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabPanel_pattern;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Label label_Buffer;
        private System.Windows.Forms.Button button_Synchronize;
        private System.Windows.Forms.GroupBox groupBox_Buffer;
        private System.Windows.Forms.GroupBox groupBox_Information;
        private System.Windows.Forms.GroupBox groupBox_Network;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Button button_HulaHoop;
        private System.Windows.Forms.GroupBox groupBox_Loops;
        private System.Windows.Forms.Label label_CycleCounter;
        private System.Windows.Forms.Label label_Cycle;
        private System.Windows.Forms.Label label_RunCounter;
        private System.Windows.Forms.Label label_Run;
        private System.Windows.Forms.Label label_CyclesPerRun;
        private System.Windows.Forms.Label label_Of;
    }
}

