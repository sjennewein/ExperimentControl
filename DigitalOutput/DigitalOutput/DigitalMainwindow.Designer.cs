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
            this.TabPanel = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Load = new System.Windows.Forms.Button();
            this.TabPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabPanel
            // 
            this.TabPanel.Controls.Add(this.tabPage1);
            this.TabPanel.Location = new System.Drawing.Point(12, 146);
            this.TabPanel.Name = "TabPanel";
            this.TabPanel.SelectedIndex = 0;
            this.TabPanel.Size = new System.Drawing.Size(1260, 704);
            this.TabPanel.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1252, 678);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(1193, 12);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(75, 23);
            this.button_Start.TabIndex = 1;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(1193, 41);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(75, 23);
            this.button_Stop.TabIndex = 2;
            this.button_Stop.Text = "Stop";
            this.button_Stop.UseVisualStyleBackColor = true;
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(1193, 70);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(1193, 99);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(75, 23);
            this.button_Load.TabIndex = 4;
            this.button_Load.Text = "Load";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // DigitalMainwindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 862);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.TabPanel);
            this.Name = "DigitalMainwindow";
            this.Text = "DigitalOutput";
            this.TabPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabPanel;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Load;
    }
}

