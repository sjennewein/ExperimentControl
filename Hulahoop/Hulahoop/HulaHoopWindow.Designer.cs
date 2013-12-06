namespace Hulahoop
{
    partial class HulaHoopWindow
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
            this.button_Iterator = new System.Windows.Forms.Button();
            this.panel_LinearIterator = new System.Windows.Forms.Panel();
            this.button_FileIterator = new System.Windows.Forms.Button();
            this.panel_FileIterator = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // button_Iterator
            // 
            this.button_Iterator.Location = new System.Drawing.Point(12, 16);
            this.button_Iterator.Name = "button_Iterator";
            this.button_Iterator.Size = new System.Drawing.Size(102, 23);
            this.button_Iterator.TabIndex = 4;
            this.button_Iterator.Text = "Add linear iterator";
            this.button_Iterator.UseVisualStyleBackColor = true;
            this.button_Iterator.Click += new System.EventHandler(this.button_Iterator_Click);
            // 
            // panel_LinearIterator
            // 
            this.panel_LinearIterator.AutoSize = true;
            this.panel_LinearIterator.Location = new System.Drawing.Point(12, 45);
            this.panel_LinearIterator.Name = "panel_LinearIterator";
            this.panel_LinearIterator.Size = new System.Drawing.Size(384, 236);
            this.panel_LinearIterator.TabIndex = 3;
            // 
            // button_FileIterator
            // 
            this.button_FileIterator.Location = new System.Drawing.Point(770, 12);
            this.button_FileIterator.Name = "button_FileIterator";
            this.button_FileIterator.Size = new System.Drawing.Size(88, 23);
            this.button_FileIterator.TabIndex = 5;
            this.button_FileIterator.Text = "Add file iterator";
            this.button_FileIterator.UseVisualStyleBackColor = true;
            this.button_FileIterator.Click += new System.EventHandler(this.button_FileIterator_Click);
            // 
            // panel_FileIterator
            // 
            this.panel_FileIterator.AutoSize = true;
            this.panel_FileIterator.Location = new System.Drawing.Point(414, 45);
            this.panel_FileIterator.Name = "panel_FileIterator";
            this.panel_FileIterator.Size = new System.Drawing.Size(444, 236);
            this.panel_FileIterator.TabIndex = 4;
            // 
            // HulaHoopWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 293);
            this.Controls.Add(this.panel_FileIterator);
            this.Controls.Add(this.button_FileIterator);
            this.Controls.Add(this.button_Iterator);
            this.Controls.Add(this.panel_LinearIterator);
            this.Name = "HulaHoopWindow";
            this.Text = "HulaHoopWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HulaHoopWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Iterator;
        private System.Windows.Forms.Panel panel_LinearIterator;
        private System.Windows.Forms.Button button_FileIterator;
        private System.Windows.Forms.Panel panel_FileIterator;
    }
}