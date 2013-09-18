namespace APDTrigger_WinForms
{
    partial class ApdSignalContextMenu
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
            this.autoscaleCheckbox = new System.Windows.Forms.CheckBox();
            this.yMinBox = new System.Windows.Forms.TextBox();
            this.yMaxBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // autoscaleCheckbox
            // 
            this.autoscaleCheckbox.AutoSize = true;
            this.autoscaleCheckbox.Location = new System.Drawing.Point(50, 75);
            this.autoscaleCheckbox.Name = "autoscaleCheckbox";
            this.autoscaleCheckbox.Size = new System.Drawing.Size(73, 17);
            this.autoscaleCheckbox.TabIndex = 0;
            this.autoscaleCheckbox.Text = "Autoscale";
            this.autoscaleCheckbox.UseVisualStyleBackColor = true;
            this.autoscaleCheckbox.CheckedChanged += new System.EventHandler(this.autoscaleCheckbox_CheckedChanged);
            // 
            // yMinBox
            // 
            this.yMinBox.Location = new System.Drawing.Point(55, 12);
            this.yMinBox.Name = "yMinBox";
            this.yMinBox.Size = new System.Drawing.Size(64, 20);
            this.yMinBox.TabIndex = 1;
            this.yMinBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.yMinBox_KeyUp);
            // 
            // yMaxBox
            // 
            this.yMaxBox.Location = new System.Drawing.Point(55, 38);
            this.yMaxBox.Name = "yMaxBox";
            this.yMaxBox.Size = new System.Drawing.Size(64, 20);
            this.yMaxBox.TabIndex = 2;
            this.yMaxBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.yMaxBox_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Y-Min:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Y-Max:";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(44, 104);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // ApdSignalContextMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(135, 139);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.yMaxBox);
            this.Controls.Add(this.yMinBox);
            this.Controls.Add(this.autoscaleCheckbox);
            this.MaximumSize = new System.Drawing.Size(143, 166);
            this.MinimumSize = new System.Drawing.Size(143, 166);
            this.Name = "ApdSignalContextMenu";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ChartContext";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox autoscaleCheckbox;
        private System.Windows.Forms.TextBox yMinBox;
        private System.Windows.Forms.TextBox yMaxBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button updateButton;
    }
}