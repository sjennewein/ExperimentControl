namespace AnalogOutput.GUI
{
    partial class Networking
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
            this.label_Connection = new System.Windows.Forms.Label();
            this.checkBox_Network = new System.Windows.Forms.CheckBox();
            this.label_Ip = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label_colon = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_Connection
            // 
            this.label_Connection.AutoSize = true;
            this.label_Connection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(20)))), ((int)(((byte)(94)))));
            this.label_Connection.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Connection.Location = new System.Drawing.Point(3, 26);
            this.label_Connection.Name = "label_Connection";
            this.label_Connection.Size = new System.Drawing.Size(126, 24);
            this.label_Connection.TabIndex = 0;
            this.label_Connection.Text = "Disconnected";
            // 
            // checkBox_Network
            // 
            this.checkBox_Network.AutoSize = true;
            this.checkBox_Network.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_Network.Location = new System.Drawing.Point(5, 6);
            this.checkBox_Network.Name = "checkBox_Network";
            this.checkBox_Network.Size = new System.Drawing.Size(66, 17);
            this.checkBox_Network.TabIndex = 1;
            this.checkBox_Network.Text = "Network";
            this.checkBox_Network.UseVisualStyleBackColor = true;
            this.checkBox_Network.CheckedChanged += new System.EventHandler(this.checkBox_Network_CheckedChanged);
            // 
            // label_Ip
            // 
            this.label_Ip.AutoSize = true;
            this.label_Ip.Location = new System.Drawing.Point(4, 58);
            this.label_Ip.Name = "label_Ip";
            this.label_Ip.Size = new System.Drawing.Size(54, 13);
            this.label_Ip.TabIndex = 2;
            this.label_Ip.Text = "Server IP:";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Enabled = false;
            this.textBox_IP.Location = new System.Drawing.Point(64, 55);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(100, 20);
            this.textBox_IP.TabIndex = 3;
            // 
            // label_colon
            // 
            this.label_colon.AutoSize = true;
            this.label_colon.Location = new System.Drawing.Point(162, 58);
            this.label_colon.Name = "label_colon";
            this.label_colon.Size = new System.Drawing.Size(10, 13);
            this.label_colon.TabIndex = 4;
            this.label_colon.Text = ":";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Enabled = false;
            this.textBox_Port.Location = new System.Drawing.Point(170, 55);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(43, 20);
            this.textBox_Port.TabIndex = 5;
            // 
            // Networking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_Network);
            this.Controls.Add(this.label_Connection);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.label_Ip);
            this.Controls.Add(this.label_colon);
            this.Name = "Networking";
            this.Size = new System.Drawing.Size(218, 84);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Connection;
        private System.Windows.Forms.CheckBox checkBox_Network;
        private System.Windows.Forms.Label label_Ip;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label_colon;
        private System.Windows.Forms.TextBox textBox_Port;
    }
}
