namespace Hulahoop.GUI
{
    partial class IteratorGUI
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
            this.label_Start = new System.Windows.Forms.Label();
            this.label_Stepsize = new System.Windows.Forms.Label();
            this.label_Stop = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_Stop = new System.Windows.Forms.TextBox();
            this.textBox_Stepsize = new System.Windows.Forms.TextBox();
            this.textBox_Start = new System.Windows.Forms.TextBox();
            this.label_Close = new System.Windows.Forms.Label();
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
            // label_Start
            // 
            this.label_Start.AutoSize = true;
            this.label_Start.Location = new System.Drawing.Point(106, 5);
            this.label_Start.Name = "label_Start";
            this.label_Start.Size = new System.Drawing.Size(29, 13);
            this.label_Start.TabIndex = 1;
            this.label_Start.Text = "Start";
            // 
            // label_Stepsize
            // 
            this.label_Stepsize.AutoSize = true;
            this.label_Stepsize.Location = new System.Drawing.Point(173, 5);
            this.label_Stepsize.Name = "label_Stepsize";
            this.label_Stepsize.Size = new System.Drawing.Size(47, 13);
            this.label_Stepsize.TabIndex = 2;
            this.label_Stepsize.Text = "Stepsize";
            // 
            // label_Stop
            // 
            this.label_Stop.AutoSize = true;
            this.label_Stop.Location = new System.Drawing.Point(240, 5);
            this.label_Stop.Name = "label_Stop";
            this.label_Stop.Size = new System.Drawing.Size(29, 13);
            this.label_Stop.TabIndex = 3;
            this.label_Stop.Text = "Stop";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(3, 21);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(100, 20);
            this.textBox_Name.TabIndex = 4;
            // 
            // textBox_Stop
            // 
            this.textBox_Stop.Location = new System.Drawing.Point(243, 21);
            this.textBox_Stop.Name = "textBox_Stop";
            this.textBox_Stop.Size = new System.Drawing.Size(61, 20);
            this.textBox_Stop.TabIndex = 7;
            // 
            // textBox_Stepsize
            // 
            this.textBox_Stepsize.Location = new System.Drawing.Point(176, 21);
            this.textBox_Stepsize.Name = "textBox_Stepsize";
            this.textBox_Stepsize.Size = new System.Drawing.Size(61, 20);
            this.textBox_Stepsize.TabIndex = 8;
            // 
            // textBox_Start
            // 
            this.textBox_Start.Location = new System.Drawing.Point(109, 21);
            this.textBox_Start.Name = "textBox_Start";
            this.textBox_Start.Size = new System.Drawing.Size(61, 20);
            this.textBox_Start.TabIndex = 9;
            // 
            // label_Close
            // 
            this.label_Close.AutoSize = true;
            this.label_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(20)))), ((int)(((byte)(94)))));
            this.label_Close.Location = new System.Drawing.Point(294, 0);
            this.label_Close.Name = "label_Close";
            this.label_Close.Size = new System.Drawing.Size(14, 13);
            this.label_Close.TabIndex = 10;
            this.label_Close.Text = "X";
            // 
            // IteratorGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label_Close);
            this.Controls.Add(this.textBox_Start);
            this.Controls.Add(this.textBox_Stepsize);
            this.Controls.Add(this.textBox_Stop);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label_Stop);
            this.Controls.Add(this.label_Stepsize);
            this.Controls.Add(this.label_Start);
            this.Controls.Add(this.label_Name);
            this.Name = "IteratorGUI";
            this.Size = new System.Drawing.Size(307, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_Start;
        private System.Windows.Forms.Label label_Stepsize;
        private System.Windows.Forms.Label label_Stop;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.TextBox textBox_Stop;
        private System.Windows.Forms.TextBox textBox_Stepsize;
        private System.Windows.Forms.TextBox textBox_Start;
        private System.Windows.Forms.Label label_Close;
    }
}
