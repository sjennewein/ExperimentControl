using AspherixGPIB.GUI;

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
            this.textBox_arbAddress = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.button_setGeneric = new System.Windows.Forms.Button();
            this.textBox_x = new AspherixGPIB.GUI.DynamicTextBox();
            this.textBox_sigma = new AspherixGPIB.GUI.DynamicTextBox();
            this.textBox_x0 = new AspherixGPIB.GUI.DynamicTextBox();
            this.textBox_sampling = new AspherixGPIB.GUI.DynamicTextBox();
            this.textBox_A = new AspherixGPIB.GUI.DynamicTextBox();
            this.textBox_amplitude = new AspherixGPIB.GUI.DynamicTextBox();
            this.button_setArb = new System.Windows.Forms.Button();
            this.checkBox_GeneralPurpose = new System.Windows.Forms.CheckBox();
            this.checkBox_arb = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox_GPCommands
            // 
            this.textBox_GPCommands.Enabled = false;
            this.textBox_GPCommands.Location = new System.Drawing.Point(12, 100);
            this.textBox_GPCommands.Multiline = true;
            this.textBox_GPCommands.Name = "textBox_GPCommands";
            this.textBox_GPCommands.Size = new System.Drawing.Size(183, 213);
            this.textBox_GPCommands.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Address:";
            // 
            // textBox_GPAddress
            // 
            this.textBox_GPAddress.Enabled = false;
            this.textBox_GPAddress.Location = new System.Drawing.Point(66, 74);
            this.textBox_GPAddress.Name = "textBox_GPAddress";
            this.textBox_GPAddress.Size = new System.Drawing.Size(129, 20);
            this.textBox_GPAddress.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(201, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(2, 350);
            this.label2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 10);
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
            // textBox_arbAddress
            // 
            this.textBox_arbAddress.Enabled = false;
            this.textBox_arbAddress.Location = new System.Drawing.Point(266, 72);
            this.textBox_arbAddress.Name = "textBox_arbAddress";
            this.textBox_arbAddress.Size = new System.Drawing.Size(129, 20);
            this.textBox_arbAddress.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(212, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Amplitude [Vpp]:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(212, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "A:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(212, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Sampling [Hz]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(283, 161);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Gaussian";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(212, 242);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "x0:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(212, 268);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Sigma:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(212, 294);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "x (Nr of samples):";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(206, 185);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(161, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "f(x)=A * exp(-(x-x0)^2/2*sigma^2)";
            // 
            // button_setGeneric
            // 
            this.button_setGeneric.Location = new System.Drawing.Point(120, 319);
            this.button_setGeneric.Name = "button_setGeneric";
            this.button_setGeneric.Size = new System.Drawing.Size(75, 23);
            this.button_setGeneric.TabIndex = 27;
            this.button_setGeneric.Text = "Set";
            this.button_setGeneric.UseVisualStyleBackColor = true;
            this.button_setGeneric.Click += new System.EventHandler(this.button_setGeneric_Click);
            // 
            // textBox_x
            // 
            this.textBox_x.Enabled = false;
            this.textBox_x.Location = new System.Drawing.Point(302, 291);
            this.textBox_x.Name = "textBox_x";
            this.textBox_x.Size = new System.Drawing.Size(93, 20);
            this.textBox_x.TabIndex = 22;
            // 
            // textBox_sigma
            // 
            this.textBox_sigma.Enabled = false;
            this.textBox_sigma.Location = new System.Drawing.Point(302, 265);
            this.textBox_sigma.Name = "textBox_sigma";
            this.textBox_sigma.Size = new System.Drawing.Size(93, 20);
            this.textBox_sigma.TabIndex = 20;
            // 
            // textBox_x0
            // 
            this.textBox_x0.Enabled = false;
            this.textBox_x0.Location = new System.Drawing.Point(302, 239);
            this.textBox_x0.Name = "textBox_x0";
            this.textBox_x0.Size = new System.Drawing.Size(93, 20);
            this.textBox_x0.TabIndex = 18;
            // 
            // textBox_sampling
            // 
            this.textBox_sampling.Enabled = false;
            this.textBox_sampling.Location = new System.Drawing.Point(302, 124);
            this.textBox_sampling.Name = "textBox_sampling";
            this.textBox_sampling.Size = new System.Drawing.Size(93, 20);
            this.textBox_sampling.TabIndex = 13;
            // 
            // textBox_A
            // 
            this.textBox_A.Enabled = false;
            this.textBox_A.Location = new System.Drawing.Point(302, 213);
            this.textBox_A.Name = "textBox_A";
            this.textBox_A.Size = new System.Drawing.Size(93, 20);
            this.textBox_A.TabIndex = 12;
            // 
            // textBox_amplitude
            // 
            this.textBox_amplitude.Enabled = false;
            this.textBox_amplitude.Location = new System.Drawing.Point(302, 98);
            this.textBox_amplitude.Name = "textBox_amplitude";
            this.textBox_amplitude.Size = new System.Drawing.Size(93, 20);
            this.textBox_amplitude.TabIndex = 11;
            // 
            // button_setArb
            // 
            this.button_setArb.Location = new System.Drawing.Point(320, 317);
            this.button_setArb.Name = "button_setArb";
            this.button_setArb.Size = new System.Drawing.Size(75, 23);
            this.button_setArb.TabIndex = 28;
            this.button_setArb.Text = "Set";
            this.button_setArb.UseVisualStyleBackColor = true;
            this.button_setArb.Click += new System.EventHandler(this.button_setArb_Click);
            // 
            // checkBox_GeneralPurpose
            // 
            this.checkBox_GeneralPurpose.AutoSize = true;
            this.checkBox_GeneralPurpose.Location = new System.Drawing.Point(12, 38);
            this.checkBox_GeneralPurpose.Name = "checkBox_GeneralPurpose";
            this.checkBox_GeneralPurpose.Size = new System.Drawing.Size(65, 17);
            this.checkBox_GeneralPurpose.TabIndex = 29;
            this.checkBox_GeneralPurpose.Text = "Activate";
            this.checkBox_GeneralPurpose.UseVisualStyleBackColor = true;
            this.checkBox_GeneralPurpose.CheckedChanged += new System.EventHandler(this.checkBox_GeneralPurpose_CheckedChanged);
            // 
            // checkBox_arb
            // 
            this.checkBox_arb.AutoSize = true;
            this.checkBox_arb.Location = new System.Drawing.Point(212, 38);
            this.checkBox_arb.Name = "checkBox_arb";
            this.checkBox_arb.Size = new System.Drawing.Size(65, 17);
            this.checkBox_arb.TabIndex = 30;
            this.checkBox_arb.Text = "Activate";
            this.checkBox_arb.UseVisualStyleBackColor = true;
            this.checkBox_arb.CheckedChanged += new System.EventHandler(this.checkBox_arb_CheckedChanged);
            // 
            // GPIBWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 374);
            this.Controls.Add(this.checkBox_arb);
            this.Controls.Add(this.checkBox_GeneralPurpose);
            this.Controls.Add(this.button_setArb);
            this.Controls.Add(this.button_setGeneric);
            this.Controls.Add(this.label2);
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
            this.Controls.Add(this.textBox_arbAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_GPAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_GPCommands);
            this.Name = "GPIBWindow";
            this.Text = "GPIB";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GPIBWindow_FormClosing);
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
        private System.Windows.Forms.TextBox textBox_arbAddress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button_setGeneric;
        private DynamicTextBox textBox_amplitude;
        private DynamicTextBox textBox_A;
        private DynamicTextBox textBox_sampling;
        private DynamicTextBox textBox_x0;
        private DynamicTextBox textBox_sigma;
        private DynamicTextBox textBox_x;
        private System.Windows.Forms.Button button_setArb;
        private System.Windows.Forms.CheckBox checkBox_GeneralPurpose;
        private System.Windows.Forms.CheckBox checkBox_arb;
    }
}

