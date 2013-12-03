namespace Hulahoop
{
    partial class HulaHoopAnalog
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
            this.panel_Iterator = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // button_Iterator
            // 
            this.button_Iterator.Location = new System.Drawing.Point(1, 16);
            this.button_Iterator.Name = "button_Iterator";
            this.button_Iterator.Size = new System.Drawing.Size(75, 23);
            this.button_Iterator.TabIndex = 4;
            this.button_Iterator.Text = "Add Iterator";
            this.button_Iterator.UseVisualStyleBackColor = true;
            this.button_Iterator.Click += new System.EventHandler(this.button_Iterator_Click);
            // 
            // panel_Iterator
            // 
            this.panel_Iterator.AutoSize = true;
            this.panel_Iterator.Location = new System.Drawing.Point(1, 45);
            this.panel_Iterator.Name = "panel_Iterator";
            this.panel_Iterator.Size = new System.Drawing.Size(374, 209);
            this.panel_Iterator.TabIndex = 3;
            // 
            // HulaHoopAnalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 269);
            this.Controls.Add(this.button_Iterator);
            this.Controls.Add(this.panel_Iterator);
            this.Name = "HulaHoopAnalog";
            this.Text = "HulaHoopAnalog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Iterator;
        private System.Windows.Forms.Panel panel_Iterator;
    }
}