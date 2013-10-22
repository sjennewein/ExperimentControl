namespace Hulahoop
{
    partial class HulahoopDigital
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
            this.panel_Iterator = new System.Windows.Forms.Panel();
            this.panel_everyRun = new System.Windows.Forms.Panel();
            this.button_Iterator = new System.Windows.Forms.Button();
            this.button_EveryXthRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel_Iterator
            // 
            this.panel_Iterator.AutoSize = true;
            this.panel_Iterator.Location = new System.Drawing.Point(12, 41);
            this.panel_Iterator.Name = "panel_Iterator";
            this.panel_Iterator.Size = new System.Drawing.Size(310, 209);
            this.panel_Iterator.TabIndex = 0;
            // 
            // panel_everyRun
            // 
            this.panel_everyRun.AutoSize = true;
            this.panel_everyRun.Location = new System.Drawing.Point(328, 41);
            this.panel_everyRun.Name = "panel_everyRun";
            this.panel_everyRun.Size = new System.Drawing.Size(189, 209);
            this.panel_everyRun.TabIndex = 1;
            // 
            // button_Iterator
            // 
            this.button_Iterator.Location = new System.Drawing.Point(12, 12);
            this.button_Iterator.Name = "button_Iterator";
            this.button_Iterator.Size = new System.Drawing.Size(75, 23);
            this.button_Iterator.TabIndex = 2;
            this.button_Iterator.Text = "Add Iterator";
            this.button_Iterator.UseVisualStyleBackColor = true;
            this.button_Iterator.Click += new System.EventHandler(this.button_Iterator_Click);
            // 
            // button_EveryXthRun
            // 
            this.button_EveryXthRun.Location = new System.Drawing.Point(328, 12);
            this.button_EveryXthRun.Name = "button_EveryXthRun";
            this.button_EveryXthRun.Size = new System.Drawing.Size(98, 23);
            this.button_EveryXthRun.TabIndex = 3;
            this.button_EveryXthRun.Text = "Add Every X Run";
            this.button_EveryXthRun.UseVisualStyleBackColor = true;
            this.button_EveryXthRun.Click += new System.EventHandler(this.button_EveryXthRun_Click);
            // 
            // HulahoopDigital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 256);
            this.Controls.Add(this.button_EveryXthRun);
            this.Controls.Add(this.button_Iterator);
            this.Controls.Add(this.panel_everyRun);
            this.Controls.Add(this.panel_Iterator);
            this.Name = "HulahoopDigital";
            this.Text = "Hula Hoop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HulahoopDigital_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_Iterator;
        private System.Windows.Forms.Panel panel_everyRun;
        private System.Windows.Forms.Button button_Iterator;
        private System.Windows.Forms.Button button_EveryXthRun;


    }
}

