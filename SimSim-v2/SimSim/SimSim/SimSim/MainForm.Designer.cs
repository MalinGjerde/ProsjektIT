namespace SimSim
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbComPort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSendStatus = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.trbTermistor = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trbPotm1 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.trbPotm2 = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.trbTemp1 = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.trbTemp2 = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.chkAutoSend = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbIntervall = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbNodeNum = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trbTermistor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbPotm1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbPotm2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTemp1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTemp2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(548, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Innganger";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(694, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Utganger";
            // 
            // cbComPort
            // 
            this.cbComPort.FormattingEnabled = true;
            this.cbComPort.Location = new System.Drawing.Point(22, 52);
            this.cbComPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbComPort.Name = "cbComPort";
            this.cbComPort.Size = new System.Drawing.Size(114, 28);
            this.cbComPort.TabIndex = 2;
            this.cbComPort.SelectedIndexChanged += new System.EventHandler(this.cbComPort_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Velg ComPort";
            // 
            // btnSendStatus
            // 
            this.btnSendStatus.Location = new System.Drawing.Point(537, 494);
            this.btnSendStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSendStatus.Name = "btnSendStatus";
            this.btnSendStatus.Size = new System.Drawing.Size(112, 35);
            this.btnSendStatus.TabIndex = 4;
            this.btnSendStatus.Text = "Send status";
            this.btnSendStatus.UseVisualStyleBackColor = true;
            this.btnSendStatus.Click += new System.EventHandler(this.btnSendStatus_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(658, 494);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(112, 35);
            this.btnQuit.TabIndex = 5;
            this.btnQuit.Text = "Avslutt";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // trbTermistor
            // 
            this.trbTermistor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.trbTermistor.Location = new System.Drawing.Point(22, 140);
            this.trbTermistor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trbTermistor.Maximum = 1023;
            this.trbTermistor.Name = "trbTermistor";
            this.trbTermistor.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbTermistor.Size = new System.Drawing.Size(69, 354);
            this.trbTermistor.SmallChange = 10;
            this.trbTermistor.TabIndex = 8;
            this.trbTermistor.TickFrequency = 50;
            this.trbTermistor.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbTermistor.Value = 500;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Termistor";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(146, 115);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Potm. 1";
            // 
            // trbPotm1
            // 
            this.trbPotm1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.trbPotm1.Location = new System.Drawing.Point(144, 140);
            this.trbPotm1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trbPotm1.Maximum = 1023;
            this.trbPotm1.Name = "trbPotm1";
            this.trbPotm1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbPotm1.Size = new System.Drawing.Size(69, 354);
            this.trbPotm1.SmallChange = 10;
            this.trbPotm1.TabIndex = 10;
            this.trbPotm1.TickFrequency = 50;
            this.trbPotm1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbPotm1.Value = 500;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 115);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Potm. 2";
            // 
            // trbPotm2
            // 
            this.trbPotm2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.trbPotm2.Location = new System.Drawing.Point(220, 140);
            this.trbPotm2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trbPotm2.Maximum = 1023;
            this.trbPotm2.Name = "trbPotm2";
            this.trbPotm2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbPotm2.Size = new System.Drawing.Size(69, 354);
            this.trbPotm2.SmallChange = 10;
            this.trbPotm2.TabIndex = 12;
            this.trbPotm2.TickFrequency = 50;
            this.trbPotm2.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbPotm2.Value = 500;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(344, 115);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Termp. 1";
            // 
            // trbTemp1
            // 
            this.trbTemp1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.trbTemp1.Location = new System.Drawing.Point(346, 140);
            this.trbTemp1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trbTemp1.Maximum = 150;
            this.trbTemp1.Name = "trbTemp1";
            this.trbTemp1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbTemp1.Size = new System.Drawing.Size(69, 354);
            this.trbTemp1.SmallChange = 10;
            this.trbTemp1.TabIndex = 14;
            this.trbTemp1.TickFrequency = 10;
            this.trbTemp1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbTemp1.Value = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(420, 115);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Temp. 2";
            // 
            // trbTemp2
            // 
            this.trbTemp2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.trbTemp2.LargeChange = 10;
            this.trbTemp2.Location = new System.Drawing.Point(424, 140);
            this.trbTemp2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trbTemp2.Maximum = 150;
            this.trbTemp2.Name = "trbTemp2";
            this.trbTemp2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbTemp2.Size = new System.Drawing.Size(69, 354);
            this.trbTemp2.SmallChange = 5;
            this.trbTemp2.TabIndex = 16;
            this.trbTemp2.TickFrequency = 10;
            this.trbTemp2.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbTemp2.Value = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(376, 503);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "(0 - 150)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(176, 503);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 20);
            this.label11.TabIndex = 20;
            this.label11.Text = "(0 - 1023)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 503);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 20);
            this.label12.TabIndex = 21;
            this.label12.Text = "(0 - 1023)";
            // 
            // chkAutoSend
            // 
            this.chkAutoSend.AutoSize = true;
            this.chkAutoSend.Location = new System.Drawing.Point(472, 55);
            this.chkAutoSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkAutoSend.Name = "chkAutoSend";
            this.chkAutoSend.Size = new System.Drawing.Size(155, 24);
            this.chkAutoSend.TabIndex = 22;
            this.chkAutoSend.Text = "Send automatisk";
            this.chkAutoSend.UseVisualStyleBackColor = true;
            this.chkAutoSend.CheckedChanged += new System.EventHandler(this.chkAutoSend_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(639, 55);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 20);
            this.label10.TabIndex = 23;
            this.label10.Text = "Intervall (sek)";
            this.label10.Visible = false;
            // 
            // tbIntervall
            // 
            this.tbIntervall.Location = new System.Drawing.Point(742, 51);
            this.tbIntervall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbIntervall.Name = "tbIntervall";
            this.tbIntervall.Size = new System.Drawing.Size(37, 26);
            this.tbIntervall.TabIndex = 24;
            this.tbIntervall.Text = "5";
            this.tbIntervall.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbIntervall.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(162, 57);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 20);
            this.label13.TabIndex = 25;
            this.label13.Text = "Nodenummer";
            // 
            // tbNodeNum
            // 
            this.tbNodeNum.Location = new System.Drawing.Point(276, 52);
            this.tbNodeNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbNodeNum.Name = "tbNodeNum";
            this.tbNodeNum.ReadOnly = true;
            this.tbNodeNum.Size = new System.Drawing.Size(37, 26);
            this.tbNodeNum.TabIndex = 26;
            this.tbNodeNum.Text = "1";
            this.tbNodeNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 580);
            this.Controls.Add(this.tbNodeNum);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbIntervall);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chkAutoSend);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.trbTemp2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.trbTemp1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trbPotm2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trbPotm1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trbTermistor);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnSendStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbComPort);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Simulator for simulatorkort";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trbTermistor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbPotm1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbPotm2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTemp1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTemp2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbComPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSendStatus;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.TrackBar trbTermistor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trbPotm1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trbPotm2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trbTemp1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trbTemp2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkAutoSend;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbIntervall;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbNodeNum;
    }
}

