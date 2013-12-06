namespace AnalogOutput.GUI
{
    partial class Step
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Value = new System.Windows.Forms.Label();
            this.label_Duration = new System.Windows.Forms.Label();
            this.radioButton_File = new System.Windows.Forms.RadioButton();
            this.radioButton_GUI = new System.Windows.Forms.RadioButton();
            this.button_File = new System.Windows.Forms.Button();
            this.textBox_Voltage = new System.Windows.Forms.TextBox();
            this.textBox_Duration = new System.Windows.Forms.TextBox();
            this.label_Name = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_Value
            // 
            this.label_Value.AutoSize = true;
            this.label_Value.Location = new System.Drawing.Point(1, 59);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(62, 13);
            this.label_Value.TabIndex = 0;
            this.label_Value.Text = "Voltage [V]:";
            // 
            // label_Duration
            // 
            this.label_Duration.AutoSize = true;
            this.label_Duration.Location = new System.Drawing.Point(1, 83);
            this.label_Duration.Name = "label_Duration";
            this.label_Duration.Size = new System.Drawing.Size(70, 13);
            this.label_Duration.TabIndex = 1;
            this.label_Duration.Text = "Duration [µs]:";
            // 
            // radioButton_File
            // 
            this.radioButton_File.AutoSize = true;
            this.radioButton_File.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton_File.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton_File.Location = new System.Drawing.Point(3, 24);
            this.radioButton_File.Name = "radioButton_File";
            this.radioButton_File.Size = new System.Drawing.Size(27, 30);
            this.radioButton_File.TabIndex = 2;
            this.radioButton_File.Text = "File";
            this.radioButton_File.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.radioButton_File.UseVisualStyleBackColor = true;
            this.radioButton_File.CheckedChanged += new System.EventHandler(this.radioButton_InputMethod_CheckedChanged);
            // 
            // radioButton_GUI
            // 
            this.radioButton_GUI.AutoSize = true;
            this.radioButton_GUI.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton_GUI.Checked = true;
            this.radioButton_GUI.Location = new System.Drawing.Point(34, 24);
            this.radioButton_GUI.Name = "radioButton_GUI";
            this.radioButton_GUI.Size = new System.Drawing.Size(46, 30);
            this.radioButton_GUI.TabIndex = 3;
            this.radioButton_GUI.TabStop = true;
            this.radioButton_GUI.Text = "Manual";
            this.radioButton_GUI.UseVisualStyleBackColor = true;
            this.radioButton_GUI.CheckedChanged += new System.EventHandler(this.radioButton_InputMethod_CheckedChanged);
            // 
            // button_File
            // 
            this.button_File.Location = new System.Drawing.Point(82, 26);
            this.button_File.Name = "button_File";
            this.button_File.Size = new System.Drawing.Size(41, 26);
            this.button_File.TabIndex = 4;
            this.button_File.Text = "Load";
            this.button_File.UseVisualStyleBackColor = true;
            this.button_File.Visible = false;
            this.button_File.Click += new System.EventHandler(this.button_File_Click);
            // 
            // textBox_Voltage
            // 
            this.textBox_Voltage.Location = new System.Drawing.Point(70, 56);
            this.textBox_Voltage.Name = "textBox_Voltage";
            this.textBox_Voltage.Size = new System.Drawing.Size(53, 20);
            this.textBox_Voltage.TabIndex = 5;
            this.textBox_Voltage.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Voltage_Validating);
            // 
            // textBox_Duration
            // 
            this.textBox_Duration.Location = new System.Drawing.Point(70, 80);
            this.textBox_Duration.Name = "textBox_Duration";
            this.textBox_Duration.Size = new System.Drawing.Size(53, 20);
            this.textBox_Duration.TabIndex = 6;
            this.textBox_Duration.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Duration_Validating);
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(3, 6);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(38, 13);
            this.label_Name.TabIndex = 7;
            this.label_Name.Text = "Name:";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(44, 3);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(79, 20);
            this.textBox_Name.TabIndex = 8;
            // 
            // Step
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label_Name);
            this.Controls.Add(this.textBox_Duration);
            this.Controls.Add(this.textBox_Voltage);
            this.Controls.Add(this.button_File);
            this.Controls.Add(this.radioButton_GUI);
            this.Controls.Add(this.radioButton_File);
            this.Controls.Add(this.label_Duration);
            this.Controls.Add(this.label_Value);
            this.Name = "Step";
            this.Size = new System.Drawing.Size(126, 103);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Value;
        private System.Windows.Forms.Label label_Duration;
        private System.Windows.Forms.RadioButton radioButton_File;
        private System.Windows.Forms.RadioButton radioButton_GUI;
        private System.Windows.Forms.Button button_File;
        private System.Windows.Forms.TextBox textBox_Voltage;
        private System.Windows.Forms.TextBox textBox_Duration;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_Name;
    }
}
