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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl_pattern.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_pattern
            // 
            this.tabControl_pattern.Controls.Add(this.tabPage1);
            this.tabControl_pattern.Location = new System.Drawing.Point(12, 113);
            this.tabControl_pattern.Name = "tabControl_pattern";
            this.tabControl_pattern.SelectedIndex = 0;
            this.tabControl_pattern.Size = new System.Drawing.Size(1239, 837);
            this.tabControl_pattern.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1231, 811);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // AnalogOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 962);
            this.Controls.Add(this.tabControl_pattern);
            this.Name = "AnalogOutput";
            this.Text = "Form1";
            this.tabControl_pattern.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_pattern;
        private System.Windows.Forms.TabPage tabPage1;
    }
}

