namespace AspherixGPIB
{
    partial class GPIBWindow
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
            this.textBox_GPCommands = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_GPAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_arbAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_amplitude = new System.Windows.Forms.TextBox();
            this.textBox_A = new System.Windows.Forms.TextBox();
            this.textBox_sampling = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_x0 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_sigma = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_x = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.button_setGeneric = new System.Windows.Forms.Button();
            this.button_setArb = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_GPCommands
            // 
            this.textBox_GPCommands.Location = new System.Drawing.Point(12, 59);
            this.textBox_GPCommands.Multiline = true;
            this.textBox_GPCommands.Name = "textBox_GPCommands";
            this.textBox_GPCommands.Size = new System.Drawing.Size(183, 213);
            this.textBox_GPCommands.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Address:";
            // 
            // textBox_GPAddress
            // 
            this.textBox_GPAddress.Location = new System.Drawing.Point(66, 33);
            this.textBox_GPAddress.Name = "textBox_GPAddress";
            this.textBox_GPAddress.Size = new System.Drawing.Size(129, 20);
            this.textBox_GPAddress.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(201, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(2, 300);
            this.label2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "General Purpose";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(209, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Arb Waveform Gaussian ";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(401, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(2, 300);
            this.label6.TabIndex = 8;
            // 
            // textBox_arbAddress
            // 
            this.textBox_arbAddress.Location = new System.Drawing.Point(263, 33);
            this.textBox_arbAddress.Name = "textBox_arbAddress";
            this.textBox_arbAddress.Size = new System.Drawing.Size(129, 20);
            this.textBox_arbAddress.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(409, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Available Variables";
            // 
            // textBox_amplitude
            // 
            this.textBox_amplitude.Location = new System.Drawing.Point(299, 59);
            this.textBox_amplitude.Name = "textBox_amplitude";
            this.textBox_amplitude.Size = new System.Drawing.Size(93, 20);
            this.textBox_amplitude.TabIndex = 11;
            // 
            // textBox_A
            // 
            this.textBox_A.Location = new System.Drawing.Point(299, 174);
            this.textBox_A.Name = "textBox_A";
            this.textBox_A.Size = new System.Drawing.Size(93, 20);
            this.textBox_A.TabIndex = 12;
            // 
            // textBox_sampling
            // 
            this.textBox_sampling.Location = new System.Drawing.Point(299, 85);
            this.textBox_sampling.Name = "textBox_sampling";
            this.textBox_sampling.Size = new System.Drawing.Size(93, 20);
            this.textBox_sampling.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(209, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Amplitude [Vpp]:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(209, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "A:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(209, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Sampling [Hz]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(280, 122);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Gaussian";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(209, 203);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "x0:";
            // 
            // textBox_x0
            // 
            this.textBox_x0.Location = new System.Drawing.Point(299, 200);
            this.textBox_x0.Name = "textBox_x0";
            this.textBox_x0.Size = new System.Drawing.Size(93, 20);
            this.textBox_x0.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(209, 229);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Sigma:";
            // 
            // textBox_sigma
            // 
            this.textBox_sigma.Location = new System.Drawing.Point(299, 226);
            this.textBox_sigma.Name = "textBox_sigma";
            this.textBox_sigma.Size = new System.Drawing.Size(93, 20);
            this.textBox_sigma.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(209, 255);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "x (Nr of samples):";
            // 
            // textBox_x
            // 
            this.textBox_x.Location = new System.Drawing.Point(299, 252);
            this.textBox_x.Name = "textBox_x";
            this.textBox_x.Size = new System.Drawing.Size(93, 20);
            this.textBox_x.TabIndex = 22;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(203, 146);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(161, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "f(x)=A * exp(-(x-x0)^2/2*sigma^2)";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Control;
            this.listView1.Location = new System.Drawing.Point(409, 60);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(150, 241);
            this.listView1.TabIndex = 25;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button_Refresh
            // 
            this.button_Refresh.Location = new System.Drawing.Point(484, 31);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(75, 23);
            this.button_Refresh.TabIndex = 26;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // button_setGeneric
            // 
            this.button_setGeneric.Location = new System.Drawing.Point(120, 278);
            this.button_setGeneric.Name = "button_setGeneric";
            this.button_setGeneric.Size = new System.Drawing.Size(75, 23);
            this.button_setGeneric.TabIndex = 27;
            this.button_setGeneric.Text = "Set";
            this.button_setGeneric.UseVisualStyleBackColor = true;
            this.button_setGeneric.Click += new System.EventHandler(this.button_setGeneric_Click);
            // 
            // button_setArb
            // 
            this.button_setArb.Location = new System.Drawing.Point(317, 278);
            this.button_setArb.Name = "button_setArb";
            this.button_setArb.Size = new System.Drawing.Size(75, 23);
            this.button_setArb.TabIndex = 28;
            this.button_setArb.Text = "Set";
            this.button_setArb.UseVisualStyleBackColor = true;
            this.button_setArb.Click += new System.EventHandler(this.button_setArb_Click);
            // 
            // GPIBWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 316);
            this.Controls.Add(this.button_setArb);
            this.Controls.Add(this.button_setGeneric);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBox_x);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox_sigma);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBox_x0);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_sampling);
            this.Controls.Add(this.textBox_A);
            this.Controls.Add(this.textBox_amplitude);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_arbAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_GPAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_GPCommands);
            this.Name = "GPIBWindow";
            this.Text = "GPIB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_GPCommands;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_GPAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_arbAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_amplitude;
        private System.Windows.Forms.TextBox textBox_A;
        private System.Windows.Forms.TextBox textBox_sampling;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_x0;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_sigma;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_x;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.Button button_setGeneric;
        private System.Windows.Forms.Button button_setArb;
    }
}

