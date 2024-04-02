
namespace VU1_Control
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbOutputs = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tbAutoSwitchPerc = new System.Windows.Forms.TextBox();
            this.txtSmoothness = new System.Windows.Forms.Label();
            this.txtSensitiviy = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sbSensitivity = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sbBlue = new System.Windows.Forms.HScrollBar();
            this.sbGreen = new System.Windows.Forms.HScrollBar();
            this.sbRed = new System.Windows.Forms.HScrollBar();
            this.sbSmoothness = new System.Windows.Forms.HScrollBar();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbAutoOffTime = new System.Windows.Forms.TextBox();
            this.tbRightCalibration = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbLeftCalibration = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRightDial = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLeftDial = new System.Windows.Forms.TextBox();
            this.btnSetup1 = new System.Windows.Forms.Button();
            this.btnSetup2 = new System.Windows.Forms.Button();
            this.cbAutoSwitch = new System.Windows.Forms.CheckBox();
            this.btnSetup3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(68, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(257, 24);
            this.label7.TabIndex = 17;
            this.label7.Text = "Streacom VU1 Dial Control";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbOutputs);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(21, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 69);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select audio device";
            // 
            // cbOutputs
            // 
            this.cbOutputs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbOutputs.FormattingEnabled = true;
            this.cbOutputs.Location = new System.Drawing.Point(78, 28);
            this.cbOutputs.Name = "cbOutputs";
            this.cbOutputs.Size = new System.Drawing.Size(247, 21);
            this.cbOutputs.TabIndex = 25;
            this.cbOutputs.SelectedIndexChanged += new System.EventHandler(this.cbOutputs_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, -12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Output";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Device";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.tbAutoSwitchPerc);
            this.groupBox2.Controls.Add(this.txtSmoothness);
            this.groupBox2.Controls.Add(this.txtSensitiviy);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.sbSensitivity);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.sbBlue);
            this.groupBox2.Controls.Add(this.sbGreen);
            this.groupBox2.Controls.Add(this.sbRed);
            this.groupBox2.Controls.Add(this.sbSmoothness);
            this.groupBox2.Location = new System.Drawing.Point(21, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 188);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dial Setup";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(115, 162);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 13);
            this.label16.TabIndex = 58;
            this.label16.Text = "%";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 162);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 57;
            this.label17.Text = "Off threshold";
            // 
            // tbAutoSwitchPerc
            // 
            this.tbAutoSwitchPerc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAutoSwitchPerc.Location = new System.Drawing.Point(78, 159);
            this.tbAutoSwitchPerc.MaxLength = 3;
            this.tbAutoSwitchPerc.Name = "tbAutoSwitchPerc";
            this.tbAutoSwitchPerc.Size = new System.Drawing.Size(37, 20);
            this.tbAutoSwitchPerc.TabIndex = 56;
            this.tbAutoSwitchPerc.TextChanged += new System.EventHandler(this.tbAutoSwitchPerc_TextChanged);
            // 
            // txtSmoothness
            // 
            this.txtSmoothness.AutoSize = true;
            this.txtSmoothness.Location = new System.Drawing.Point(328, 53);
            this.txtSmoothness.Name = "txtSmoothness";
            this.txtSmoothness.Size = new System.Drawing.Size(13, 13);
            this.txtSmoothness.TabIndex = 38;
            this.txtSmoothness.Text = "0";
            // 
            // txtSensitiviy
            // 
            this.txtSensitiviy.AutoSize = true;
            this.txtSensitiviy.Location = new System.Drawing.Point(328, 26);
            this.txtSensitiviy.Name = "txtSensitiviy";
            this.txtSensitiviy.Size = new System.Drawing.Size(13, 13);
            this.txtSensitiviy.TabIndex = 37;
            this.txtSensitiviy.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Sensitivity";
            // 
            // sbSensitivity
            // 
            this.sbSensitivity.Location = new System.Drawing.Point(78, 22);
            this.sbSensitivity.Minimum = 1;
            this.sbSensitivity.Name = "sbSensitivity";
            this.sbSensitivity.Size = new System.Drawing.Size(247, 17);
            this.sbSensitivity.TabIndex = 23;
            this.sbSensitivity.Value = 1;
            this.sbSensitivity.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbSensitivity_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Damping";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "G";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "R";
            // 
            // sbBlue
            // 
            this.sbBlue.Location = new System.Drawing.Point(78, 132);
            this.sbBlue.Maximum = 127;
            this.sbBlue.Name = "sbBlue";
            this.sbBlue.Size = new System.Drawing.Size(247, 17);
            this.sbBlue.SmallChange = 5;
            this.sbBlue.TabIndex = 26;
            this.sbBlue.Value = 1;
            this.sbBlue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbBlue_Scroll);
            // 
            // sbGreen
            // 
            this.sbGreen.Location = new System.Drawing.Point(78, 105);
            this.sbGreen.Maximum = 127;
            this.sbGreen.Name = "sbGreen";
            this.sbGreen.Size = new System.Drawing.Size(247, 17);
            this.sbGreen.SmallChange = 5;
            this.sbGreen.TabIndex = 25;
            this.sbGreen.Value = 1;
            this.sbGreen.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbGreen_Scroll);
            // 
            // sbRed
            // 
            this.sbRed.Location = new System.Drawing.Point(78, 76);
            this.sbRed.Maximum = 127;
            this.sbRed.Name = "sbRed";
            this.sbRed.Size = new System.Drawing.Size(247, 17);
            this.sbRed.SmallChange = 5;
            this.sbRed.TabIndex = 24;
            this.sbRed.Value = 1;
            this.sbRed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbRed_Scroll);
            // 
            // sbSmoothness
            // 
            this.sbSmoothness.Location = new System.Drawing.Point(78, 49);
            this.sbSmoothness.Minimum = 1;
            this.sbSmoothness.Name = "sbSmoothness";
            this.sbSmoothness.Size = new System.Drawing.Size(247, 17);
            this.sbSmoothness.TabIndex = 31;
            this.sbSmoothness.Value = 1;
            this.sbSmoothness.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbSmoothness_Scroll);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(326, 496);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(52, 26);
            this.btnStart.TabIndex = 27;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.tbAutoOffTime);
            this.groupBox3.Controls.Add(this.tbRightCalibration);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.tbLeftCalibration);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.tbRightDial);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.tbLeftDial);
            this.groupBox3.Location = new System.Drawing.Point(21, 364);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(357, 119);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(75, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 52;
            this.label15.Text = "Left";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(115, 89);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(12, 13);
            this.label14.TabIndex = 51;
            this.label14.Text = "s";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 50;
            this.label13.Text = "Auto-off time";
            // 
            // tbAutoOffTime
            // 
            this.tbAutoOffTime.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAutoOffTime.Location = new System.Drawing.Point(78, 86);
            this.tbAutoOffTime.MaxLength = 3;
            this.tbAutoOffTime.Name = "tbAutoOffTime";
            this.tbAutoOffTime.Size = new System.Drawing.Size(37, 20);
            this.tbAutoOffTime.TabIndex = 49;
            this.tbAutoOffTime.TextChanged += new System.EventHandler(this.tbAutoOffTime_TextChanged);
            // 
            // tbRightCalibration
            // 
            this.tbRightCalibration.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbRightCalibration.Location = new System.Drawing.Point(163, 59);
            this.tbRightCalibration.MaxLength = 3;
            this.tbRightCalibration.Name = "tbRightCalibration";
            this.tbRightCalibration.Size = new System.Drawing.Size(37, 20);
            this.tbRightCalibration.TabIndex = 48;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 47;
            this.label12.Text = "Calibration";
            // 
            // tbLeftCalibration
            // 
            this.tbLeftCalibration.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbLeftCalibration.Location = new System.Drawing.Point(78, 59);
            this.tbLeftCalibration.MaxLength = 3;
            this.tbLeftCalibration.Name = "tbLeftCalibration";
            this.tbLeftCalibration.Size = new System.Drawing.Size(37, 20);
            this.tbLeftCalibration.TabIndex = 46;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(160, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Right";
            // 
            // tbRightDial
            // 
            this.tbRightDial.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbRightDial.Location = new System.Drawing.Point(163, 32);
            this.tbRightDial.MaxLength = 1;
            this.tbRightDial.Name = "tbRightDial";
            this.tbRightDial.Size = new System.Drawing.Size(24, 20);
            this.tbRightDial.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Dial Number";
            // 
            // tbLeftDial
            // 
            this.tbLeftDial.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbLeftDial.Location = new System.Drawing.Point(78, 32);
            this.tbLeftDial.MaxLength = 1;
            this.tbLeftDial.Name = "tbLeftDial";
            this.tbLeftDial.Size = new System.Drawing.Size(24, 20);
            this.tbLeftDial.TabIndex = 42;
            this.tbLeftDial.WordWrap = false;
            // 
            // btnSetup1
            // 
            this.btnSetup1.BackColor = System.Drawing.Color.PaleGreen;
            this.btnSetup1.Location = new System.Drawing.Point(78, 41);
            this.btnSetup1.Name = "btnSetup1";
            this.btnSetup1.Size = new System.Drawing.Size(29, 26);
            this.btnSetup1.TabIndex = 29;
            this.btnSetup1.Text = "1";
            this.btnSetup1.UseVisualStyleBackColor = false;
            this.btnSetup1.Click += new System.EventHandler(this.btnSetup1_Click);
            // 
            // btnSetup2
            // 
            this.btnSetup2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSetup2.Location = new System.Drawing.Point(113, 41);
            this.btnSetup2.Name = "btnSetup2";
            this.btnSetup2.Size = new System.Drawing.Size(29, 26);
            this.btnSetup2.TabIndex = 30;
            this.btnSetup2.Text = "2";
            this.btnSetup2.UseVisualStyleBackColor = false;
            this.btnSetup2.Click += new System.EventHandler(this.btnSetup2_Click);
            // 
            // cbAutoSwitch
            // 
            this.cbAutoSwitch.AutoSize = true;
            this.cbAutoSwitch.Location = new System.Drawing.Point(208, 46);
            this.cbAutoSwitch.Name = "cbAutoSwitch";
            this.cbAutoSwitch.Size = new System.Drawing.Size(107, 17);
            this.cbAutoSwitch.TabIndex = 31;
            this.cbAutoSwitch.Text = "Auto input switch";
            this.cbAutoSwitch.UseVisualStyleBackColor = true;
            this.cbAutoSwitch.CheckedChanged += new System.EventHandler(this.cbAutoSwitch_CheckedChanged);
            // 
            // btnSetup3
            // 
            this.btnSetup3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSetup3.Location = new System.Drawing.Point(148, 41);
            this.btnSetup3.Name = "btnSetup3";
            this.btnSetup3.Size = new System.Drawing.Size(29, 26);
            this.btnSetup3.TabIndex = 32;
            this.btnSetup3.Text = "3";
            this.btnSetup3.UseVisualStyleBackColor = false;
            this.btnSetup3.Click += new System.EventHandler(this.btnSetup3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Setup";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(263, 496);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(52, 26);
            this.btnReset.TabIndex = 34;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(400, 534);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSetup3);
            this.Controls.Add(this.cbAutoSwitch);
            this.Controls.Add(this.btnSetup2);
            this.Controls.Add(this.btnSetup1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "VU1 Dials";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Close);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbOutputs;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label txtSmoothness;
        private System.Windows.Forms.Label txtSensitiviy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar sbSensitivity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HScrollBar sbBlue;
        private System.Windows.Forms.HScrollBar sbGreen;
        private System.Windows.Forms.HScrollBar sbRed;
        private System.Windows.Forms.HScrollBar sbSmoothness;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbRightCalibration;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbLeftCalibration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbRightDial;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbLeftDial;
        private System.Windows.Forms.Button btnSetup1;
        private System.Windows.Forms.Button btnSetup2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbAutoOffTime;
        private System.Windows.Forms.CheckBox cbAutoSwitch;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnSetup3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbAutoSwitchPerc;
        private System.Windows.Forms.Button btnReset;
    }
}

