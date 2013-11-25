namespace Hulahoop.GUI
{
    partial class EveryXRunGUI
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
            this.label_RunEvery = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label_Close = new System.Windows.Forms.Label();
            this.textBox_XRun = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(3, 5);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(35, 13);
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "Name";
            // 
            // label_RunEvery
            // 
            this.label_RunEvery.AutoSize = true;
            this.label_RunEvery.Location = new System.Drawing.Point(109, 5);
            this.label_RunEvery.Name = "label_RunEvery";
            this.label_RunEvery.Size = new System.Drawing.Size(62, 13);
            this.label_RunEvery.TabIndex = 1;
            this.label_RunEvery.Text = "Every X run";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(6, 21);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(100, 20);
            this.textBox_Name.TabIndex = 2;
            // 
            // label_Close
            // 
            this.label_Close.AutoSize = true;
            this.label_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(20)))), ((int)(((byte)(94)))));
            this.label_Close.Location = new System.Drawing.Point(176, 0);
            this.label_Close.Name = "label_Close";
            this.label_Close.Size = new System.Drawing.Size(14, 13);
            this.label_Close.TabIndex = 11;
            this.label_Close.Text = "X";
            // 
            // textBox_XRun
            // 
            this.textBox_XRun.Location = new System.Drawing.Point(112, 21);
            this.textBox_XRun.Name = "textBox_XRun";
            this.textBox_XRun.Size = new System.Drawing.Size(53, 20);
            this.textBox_XRun.TabIndex = 12;
            // 
            // EveryXRunGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.textBox_XRun);
            this.Controls.Add(this.label_Close);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label_RunEvery);
            this.Controls.Add(this.label_Name);
            this.Name = "EveryXRunGUI";
            this.Size = new System.Drawing.Size(189, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_RunEvery;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label_Close;
        private System.Windows.Forms.TextBox textBox_XRun;
    }
}
