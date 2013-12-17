namespace AnalogOutput.GUI
{
    partial class ChannelHeader
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
            this.label_Name = new System.Windows.Forms.Label();
            this.label_IntialValue = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_Value = new System.Windows.Forms.TextBox();
            this.button_Calibrate = new System.Windows.Forms.Button();
            this.button_Reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(3, 10);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(38, 13);
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "Name:";
            // 
            // label_IntialValue
            // 
            this.label_IntialValue.AutoSize = true;
            this.label_IntialValue.Location = new System.Drawing.Point(3, 36);
            this.label_IntialValue.Name = "label_IntialValue";
            this.label_IntialValue.Size = new System.Drawing.Size(64, 13);
            this.label_IntialValue.TabIndex = 1;
            this.label_IntialValue.Text = "Initial Value:";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(47, 7);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(78, 20);
            this.textBox_Name.TabIndex = 2;
            this.textBox_Name.Text = "Channel X";
            // 
            // textBox_Value
            // 
            this.textBox_Value.Location = new System.Drawing.Point(73, 33);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.Size = new System.Drawing.Size(52, 20);
            this.textBox_Value.TabIndex = 3;
            this.textBox_Value.Text = "0";
            // 
            // button_Calibrate
            // 
            this.button_Calibrate.Location = new System.Drawing.Point(60, 77);
            this.button_Calibrate.Name = "button_Calibrate";
            this.button_Calibrate.Size = new System.Drawing.Size(63, 23);
            this.button_Calibrate.TabIndex = 4;
            this.button_Calibrate.Text = "Calibrate";
            this.button_Calibrate.UseVisualStyleBackColor = true;
            this.button_Calibrate.Click += new System.EventHandler(this.button_Calibrate_Click);
            // 
            // button_Reset
            // 
            this.button_Reset.Location = new System.Drawing.Point(3, 77);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(51, 23);
            this.button_Reset.TabIndex = 5;
            this.button_Reset.Text = "Reset";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // ChannelHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.button_Calibrate);
            this.Controls.Add(this.textBox_Value);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label_IntialValue);
            this.Controls.Add(this.label_Name);
            this.Name = "ChannelHeader";
            this.Size = new System.Drawing.Size(126, 103);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_IntialValue;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.TextBox textBox_Value;
        private System.Windows.Forms.Button button_Calibrate;
        private System.Windows.Forms.Button button_Reset;
    }
}
