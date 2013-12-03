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
            this.button_Start.Location = new System.Drawing.Point(1197, 23);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(75, 23);
            this.button_Start.TabIndex = 1;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(1197, 52);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(75, 23);
            this.button_Stop.TabIndex = 2;
            this.button_Stop.Text = "Stop";
            this.button_Stop.UseVisualStyleBackColor = true;
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(1116, 52);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(75, 23);
            this.button_Load.TabIndex = 3;
            this.button_Load.Text = "Load";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(1116, 23);
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
            this.panel_Network.Location = new System.Drawing.Point(865, 12);
            this.panel_Network.Name = "panel_Network";
            this.panel_Network.Size = new System.Drawing.Size(245, 86);
            this.panel_Network.TabIndex = 5;
            // 
            // button_Hoops
            // 
            this.button_Hoops.Location = new System.Drawing.Point(735, 12);
            this.button_Hoops.Name = "button_Hoops";
            this.button_Hoops.Size = new System.Drawing.Size(75, 23);
            this.button_Hoops.TabIndex = 6;
            this.button_Hoops.Text = "Hooping";
            this.button_Hoops.UseVisualStyleBackColor = true;
            this.button_Hoops.Click += new System.EventHandler(this.button_Hoops_Click);
            // 
            // AnalogOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 976);
            this.Controls.Add(this.button_Hoops);
            this.Controls.Add(this.panel_Network);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.tabControl_pattern);
            this.Name = "AnalogOutput";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_pattern;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Panel panel_Network;
        private System.Windows.Forms.Button button_Hoops;
    }
}

