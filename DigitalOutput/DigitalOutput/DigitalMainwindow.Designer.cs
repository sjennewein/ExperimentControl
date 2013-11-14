﻿namespace DigitalOutput
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
            this.TabPanel = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox_ProgramFlow = new System.Windows.Forms.GroupBox();
            this.textBox_Flow = new System.Windows.Forms.TextBox();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Load = new System.Windows.Forms.Button();
            this.label_Buffer = new System.Windows.Forms.Label();
            this.button_Synchronize = new System.Windows.Forms.Button();
            this.groupBox_Buffer = new System.Windows.Forms.GroupBox();
            this.button_Undo = new System.Windows.Forms.Button();
            this.groupBox_Information = new System.Windows.Forms.GroupBox();
            this.label_CycleDone = new System.Windows.Forms.Label();
            this.label_Cycle = new System.Windows.Forms.Label();
            this.label_Status = new System.Windows.Forms.Label();
            this.groupBox_Network = new System.Windows.Forms.GroupBox();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.button_Disconnect = new System.Windows.Forms.Button();
            this.button_Connect = new System.Windows.Forms.Button();
            this.label_Network = new System.Windows.Forms.Label();
            this.checkBox_Network = new System.Windows.Forms.CheckBox();
            this.label_Server = new System.Windows.Forms.Label();
            this.textBox_Ip = new System.Windows.Forms.TextBox();
            this.label_collon = new System.Windows.Forms.Label();
            this.button_HulaHoop = new System.Windows.Forms.Button();
            this.groupBox_Loops = new System.Windows.Forms.GroupBox();
            this.label_Run = new System.Windows.Forms.Label();
            this.label_RunsDone = new System.Windows.Forms.Label();
            this.label_Of = new System.Windows.Forms.Label();
            this.label_CyclesPerRun = new System.Windows.Forms.Label();
            this.TabPanel.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox_ProgramFlow.SuspendLayout();
            this.groupBox_Buffer.SuspendLayout();
            this.groupBox_Information.SuspendLayout();
            this.groupBox_Network.SuspendLayout();
            this.groupBox_Loops.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabPanel
            // 
            this.TabPanel.Controls.Add(this.tabPage1);
            this.TabPanel.Location = new System.Drawing.Point(12, 146);
            this.TabPanel.Name = "TabPanel";
            this.TabPanel.SelectedIndex = 0;
            this.TabPanel.Size = new System.Drawing.Size(1260, 789);
            this.TabPanel.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox_ProgramFlow);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1252, 763);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox_ProgramFlow
            // 
            this.groupBox_ProgramFlow.Controls.Add(this.textBox_Flow);
            this.groupBox_ProgramFlow.Location = new System.Drawing.Point(18, 89);
            this.groupBox_ProgramFlow.Name = "groupBox_ProgramFlow";
            this.groupBox_ProgramFlow.Size = new System.Drawing.Size(274, 484);
            this.groupBox_ProgramFlow.TabIndex = 2;
            this.groupBox_ProgramFlow.TabStop = false;
            this.groupBox_ProgramFlow.Text = "Program flow:";
            // 
            // textBox_Flow
            // 
            this.textBox_Flow.Location = new System.Drawing.Point(6, 39);
            this.textBox_Flow.Multiline = true;
            this.textBox_Flow.Name = "textBox_Flow";
            this.textBox_Flow.Size = new System.Drawing.Size(176, 439);
            this.textBox_Flow.TabIndex = 0;
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
            this.groupBox_Buffer.Controls.Add(this.button_Undo);
            this.groupBox_Buffer.Controls.Add(this.label_Buffer);
            this.groupBox_Buffer.Controls.Add(this.button_Synchronize);
            this.groupBox_Buffer.Location = new System.Drawing.Point(1100, 12);
            this.groupBox_Buffer.Name = "groupBox_Buffer";
            this.groupBox_Buffer.Size = new System.Drawing.Size(87, 128);
            this.groupBox_Buffer.TabIndex = 7;
            this.groupBox_Buffer.TabStop = false;
            this.groupBox_Buffer.Text = "Buffer";
            // 
            // button_Undo
            // 
            this.button_Undo.Location = new System.Drawing.Point(7, 75);
            this.button_Undo.Name = "button_Undo";
            this.button_Undo.Size = new System.Drawing.Size(75, 23);
            this.button_Undo.TabIndex = 7;
            this.button_Undo.Text = "Undo";
            this.button_Undo.UseVisualStyleBackColor = true;
            this.button_Undo.Click += new System.EventHandler(this.button_Undo_Click);
            // 
            // groupBox_Information
            // 
            this.groupBox_Information.Controls.Add(this.label_CyclesPerRun);
            this.groupBox_Information.Controls.Add(this.label_Of);
            this.groupBox_Information.Controls.Add(this.label_RunsDone);
            this.groupBox_Information.Controls.Add(this.label_Run);
            this.groupBox_Information.Controls.Add(this.label_CycleDone);
            this.groupBox_Information.Controls.Add(this.label_Cycle);
            this.groupBox_Information.Controls.Add(this.label_Status);
            this.groupBox_Information.Location = new System.Drawing.Point(894, 12);
            this.groupBox_Information.Name = "groupBox_Information";
            this.groupBox_Information.Size = new System.Drawing.Size(200, 128);
            this.groupBox_Information.TabIndex = 8;
            this.groupBox_Information.TabStop = false;
            this.groupBox_Information.Text = "Information";
            // 
            // label_CycleDone
            // 
            this.label_CycleDone.AutoSize = true;
            this.label_CycleDone.Location = new System.Drawing.Point(81, 57);
            this.label_CycleDone.Name = "label_CycleDone";
            this.label_CycleDone.Size = new System.Drawing.Size(13, 13);
            this.label_CycleDone.TabIndex = 10;
            this.label_CycleDone.Text = "0";
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
            this.groupBox_Network.Controls.Add(this.textBox_Port);
            this.groupBox_Network.Controls.Add(this.button_Disconnect);
            this.groupBox_Network.Controls.Add(this.button_Connect);
            this.groupBox_Network.Controls.Add(this.label_Network);
            this.groupBox_Network.Controls.Add(this.checkBox_Network);
            this.groupBox_Network.Controls.Add(this.label_Server);
            this.groupBox_Network.Controls.Add(this.textBox_Ip);
            this.groupBox_Network.Controls.Add(this.label_collon);
            this.groupBox_Network.Location = new System.Drawing.Point(688, 12);
            this.groupBox_Network.Name = "groupBox_Network";
            this.groupBox_Network.Size = new System.Drawing.Size(200, 128);
            this.groupBox_Network.TabIndex = 9;
            this.groupBox_Network.TabStop = false;
            this.groupBox_Network.Text = "Network";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Enabled = false;
            this.textBox_Port.Location = new System.Drawing.Point(154, 73);
            this.textBox_Port.MaxLength = 3;
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(40, 20);
            this.textBox_Port.TabIndex = 15;
            // 
            // button_Disconnect
            // 
            this.button_Disconnect.Enabled = false;
            this.button_Disconnect.Location = new System.Drawing.Point(121, 99);
            this.button_Disconnect.Name = "button_Disconnect";
            this.button_Disconnect.Size = new System.Drawing.Size(75, 23);
            this.button_Disconnect.TabIndex = 14;
            this.button_Disconnect.Text = "Disconnect";
            this.button_Disconnect.UseVisualStyleBackColor = true;
            this.button_Disconnect.Click += new System.EventHandler(this.button_Disconnect_Click);
            // 
            // button_Connect
            // 
            this.button_Connect.Enabled = false;
            this.button_Connect.Location = new System.Drawing.Point(40, 99);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(75, 23);
            this.button_Connect.TabIndex = 13;
            this.button_Connect.Text = "Connect";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // label_Network
            // 
            this.label_Network.AutoSize = true;
            this.label_Network.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(20)))), ((int)(((byte)(94)))));
            this.label_Network.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Network.Location = new System.Drawing.Point(10, 18);
            this.label_Network.Margin = new System.Windows.Forms.Padding(0);
            this.label_Network.MaximumSize = new System.Drawing.Size(130, 24);
            this.label_Network.MinimumSize = new System.Drawing.Size(110, 24);
            this.label_Network.Name = "label_Network";
            this.label_Network.Size = new System.Drawing.Size(126, 24);
            this.label_Network.TabIndex = 12;
            this.label_Network.Text = "Disconnected";
            this.label_Network.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_Network
            // 
            this.checkBox_Network.AutoSize = true;
            this.checkBox_Network.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_Network.Location = new System.Drawing.Point(6, 51);
            this.checkBox_Network.Name = "checkBox_Network";
            this.checkBox_Network.Size = new System.Drawing.Size(90, 17);
            this.checkBox_Network.TabIndex = 11;
            this.checkBox_Network.Text = "Network:       ";
            this.checkBox_Network.UseVisualStyleBackColor = true;
            this.checkBox_Network.CheckedChanged += new System.EventHandler(this.checkBox_Network_CheckedChanged);
            // 
            // label_Server
            // 
            this.label_Server.AutoSize = true;
            this.label_Server.Location = new System.Drawing.Point(6, 76);
            this.label_Server.Name = "label_Server";
            this.label_Server.Size = new System.Drawing.Size(54, 13);
            this.label_Server.TabIndex = 10;
            this.label_Server.Text = "Server IP:";
            // 
            // textBox_Ip
            // 
            this.textBox_Ip.Enabled = false;
            this.textBox_Ip.Location = new System.Drawing.Point(66, 73);
            this.textBox_Ip.MaxLength = 3;
            this.textBox_Ip.Name = "textBox_Ip";
            this.textBox_Ip.Size = new System.Drawing.Size(82, 20);
            this.textBox_Ip.TabIndex = 0;
            // 
            // label_collon
            // 
            this.label_collon.AutoSize = true;
            this.label_collon.Location = new System.Drawing.Point(146, 76);
            this.label_collon.Name = "label_collon";
            this.label_collon.Size = new System.Drawing.Size(10, 13);
            this.label_collon.TabIndex = 9;
            this.label_collon.Text = ":";
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
            // label_Run
            // 
            this.label_Run.AutoSize = true;
            this.label_Run.Location = new System.Drawing.Point(7, 75);
            this.label_Run.Name = "label_Run";
            this.label_Run.Size = new System.Drawing.Size(62, 13);
            this.label_Run.TabIndex = 11;
            this.label_Run.Text = "Runs done:";
            // 
            // label_RunsDone
            // 
            this.label_RunsDone.AutoSize = true;
            this.label_RunsDone.Location = new System.Drawing.Point(81, 75);
            this.label_RunsDone.Name = "label_RunsDone";
            this.label_RunsDone.Size = new System.Drawing.Size(13, 13);
            this.label_RunsDone.TabIndex = 12;
            this.label_RunsDone.Text = "0";
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
            // label_CyclesPerRun
            // 
            this.label_CyclesPerRun.AutoSize = true;
            this.label_CyclesPerRun.Location = new System.Drawing.Point(149, 57);
            this.label_CyclesPerRun.Name = "label_CyclesPerRun";
            this.label_CyclesPerRun.Size = new System.Drawing.Size(13, 13);
            this.label_CyclesPerRun.TabIndex = 14;
            this.label_CyclesPerRun.Text = "0";
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
            this.Controls.Add(this.TabPanel);
            this.Name = "DigitalMainwindow";
            this.Text = "DigitalOutput";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DigitalMainwindow_FormClosing);
            this.TabPanel.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox_ProgramFlow.ResumeLayout(false);
            this.groupBox_ProgramFlow.PerformLayout();
            this.groupBox_Buffer.ResumeLayout(false);
            this.groupBox_Buffer.PerformLayout();
            this.groupBox_Information.ResumeLayout(false);
            this.groupBox_Information.PerformLayout();
            this.groupBox_Network.ResumeLayout(false);
            this.groupBox_Network.PerformLayout();
            this.groupBox_Loops.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabPanel;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Label label_Buffer;
        private System.Windows.Forms.Button button_Synchronize;
        private System.Windows.Forms.GroupBox groupBox_Buffer;
        private System.Windows.Forms.GroupBox groupBox_Information;
        private System.Windows.Forms.GroupBox groupBox_Network;
        private System.Windows.Forms.TextBox textBox_Ip;
        private System.Windows.Forms.CheckBox checkBox_Network;
        private System.Windows.Forms.Label label_Server;
        private System.Windows.Forms.Label label_Network;
        private System.Windows.Forms.TextBox textBox_Flow;
        private System.Windows.Forms.GroupBox groupBox_ProgramFlow;
        private System.Windows.Forms.Button button_Undo;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Button button_HulaHoop;
        private System.Windows.Forms.GroupBox groupBox_Loops;
        private System.Windows.Forms.Button button_Disconnect;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.Label label_collon;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label_CycleDone;
        private System.Windows.Forms.Label label_Cycle;
        private System.Windows.Forms.Label label_RunsDone;
        private System.Windows.Forms.Label label_Run;
        private System.Windows.Forms.Label label_CyclesPerRun;
        private System.Windows.Forms.Label label_Of;
    }
}

