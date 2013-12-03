namespace Hulahoop.GUI
{
    partial class FileIteratorGui
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
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label_Close = new System.Windows.Forms.Label();
            this.button_Load = new System.Windows.Forms.Button();
            this.textBox_Filename = new System.Windows.Forms.TextBox();
            this.label_Filename = new System.Windows.Forms.Label();
            this.label_Length = new System.Windows.Forms.Label();
            this.label_Lines = new System.Windows.Forms.Label();
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
            this.label_Close.Location = new System.Drawing.Point(418, 0);
            this.label_Close.Name = "label_Close";
            this.label_Close.Size = new System.Drawing.Size(14, 13);
            this.label_Close.TabIndex = 11;
            this.label_Close.Text = "X";
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(353, 19);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(75, 23);
            this.button_Load.TabIndex = 12;
            this.button_Load.Text = "Load";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // textBox_Filename
            // 
            this.textBox_Filename.Enabled = false;
            this.textBox_Filename.Location = new System.Drawing.Point(112, 21);
            this.textBox_Filename.Name = "textBox_Filename";
            this.textBox_Filename.Size = new System.Drawing.Size(147, 20);
            this.textBox_Filename.TabIndex = 13;
            // 
            // label_Filename
            // 
            this.label_Filename.AutoSize = true;
            this.label_Filename.Location = new System.Drawing.Point(112, 5);
            this.label_Filename.Name = "label_Filename";
            this.label_Filename.Size = new System.Drawing.Size(49, 13);
            this.label_Filename.TabIndex = 14;
            this.label_Filename.Text = "Filename";
            // 
            // label_Length
            // 
            this.label_Length.AutoSize = true;
            this.label_Length.Location = new System.Drawing.Point(265, 24);
            this.label_Length.Name = "label_Length";
            this.label_Length.Size = new System.Drawing.Size(13, 13);
            this.label_Length.TabIndex = 15;
            this.label_Length.Text = "0";
            // 
            // label_Lines
            // 
            this.label_Lines.AutoSize = true;
            this.label_Lines.Location = new System.Drawing.Point(265, 5);
            this.label_Lines.Name = "label_Lines";
            this.label_Lines.Size = new System.Drawing.Size(32, 13);
            this.label_Lines.TabIndex = 16;
            this.label_Lines.Text = "Lines";
            // 
            // FileIteratorGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label_Lines);
            this.Controls.Add(this.label_Length);
            this.Controls.Add(this.label_Filename);
            this.Controls.Add(this.textBox_Filename);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.label_Close);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label_Name);
            this.Name = "FileIteratorGui";
            this.Size = new System.Drawing.Size(431, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label_Close;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.TextBox textBox_Filename;
        private System.Windows.Forms.Label label_Filename;
        private System.Windows.Forms.Label label_Length;
        private System.Windows.Forms.Label label_Lines;
    }
}
