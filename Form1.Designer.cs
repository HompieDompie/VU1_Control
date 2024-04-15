
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
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbRightCalibration = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbLeftCalibration = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRightDial = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLeftDial = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.tcSetup = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbAutoSwitch = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbAutoOffTime = new System.Windows.Forms.TextBox();
            this.panSetup = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbAutoSensitivityTime = new System.Windows.Forms.TextBox();
            this.cbAutoSensitivity = new System.Windows.Forms.CheckBox();
            this.paColor = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.cbOutputs = new System.Windows.Forms.ComboBox();
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
            this.groupBox3.SuspendLayout();
            this.tcSetup.SuspendLayout();
            this.panSetup.SuspendLayout();
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
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(327, 399);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(52, 26);
            this.btnStart.TabIndex = 27;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.tbRightCalibration);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.tbLeftCalibration);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.tbRightDial);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.tbLeftDial);
            this.groupBox3.Location = new System.Drawing.Point(12, 330);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(215, 95);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(139, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(2, 64);
            this.label6.TabIndex = 53;
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
            // tbRightCalibration
            // 
            this.tbRightCalibration.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbRightCalibration.Location = new System.Drawing.Point(163, 59);
            this.tbRightCalibration.MaxLength = 3;
            this.tbRightCalibration.Name = "tbRightCalibration";
            this.tbRightCalibration.Size = new System.Drawing.Size(36, 20);
            this.tbRightCalibration.TabIndex = 48;
            this.tbRightCalibration.TextChanged += new System.EventHandler(this.tbRightCalibration_TextChanged);
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
            this.tbLeftCalibration.Size = new System.Drawing.Size(36, 20);
            this.tbLeftCalibration.TabIndex = 46;
            this.tbLeftCalibration.TextChanged += new System.EventHandler(this.tbLeftCalibration_TextChanged);
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
            this.tbRightDial.Size = new System.Drawing.Size(36, 20);
            this.tbRightDial.TabIndex = 44;
            this.tbRightDial.TextChanged += new System.EventHandler(this.tbRightDial_TextChanged);
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
            this.tbLeftDial.Size = new System.Drawing.Size(36, 20);
            this.tbLeftDial.TabIndex = 42;
            this.tbLeftDial.WordWrap = false;
            this.tbLeftDial.TextChanged += new System.EventHandler(this.tbLeftDial_TextChanged);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(257, 399);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(52, 26);
            this.btnReset.TabIndex = 34;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tcSetup
            // 
            this.tcSetup.Controls.Add(this.tabPage1);
            this.tcSetup.Controls.Add(this.tabPage2);
            this.tcSetup.Controls.Add(this.tabPage3);
            this.tcSetup.Location = new System.Drawing.Point(12, 45);
            this.tcSetup.Name = "tcSetup";
            this.tcSetup.SelectedIndex = 0;
            this.tcSetup.Size = new System.Drawing.Size(367, 274);
            this.tcSetup.TabIndex = 35;
            this.tcSetup.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcSetup_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(359, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Setup 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(359, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Setup 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(359, 248);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Setup 3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cbAutoSwitch
            // 
            this.cbAutoSwitch.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAutoSwitch.Location = new System.Drawing.Point(253, 344);
            this.cbAutoSwitch.Name = "cbAutoSwitch";
            this.cbAutoSwitch.Size = new System.Drawing.Size(122, 17);
            this.cbAutoSwitch.TabIndex = 52;
            this.cbAutoSwitch.Text = "Auto input switch";
            this.cbAutoSwitch.UseVisualStyleBackColor = true;
            this.cbAutoSwitch.CheckedChanged += new System.EventHandler(this.cbAutoSwitch_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(376, 365);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(12, 13);
            this.label14.TabIndex = 55;
            this.label14.Text = "s";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(254, 365);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 54;
            this.label13.Text = "Auto-off time";
            // 
            // tbAutoOffTime
            // 
            this.tbAutoOffTime.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAutoOffTime.Location = new System.Drawing.Point(339, 362);
            this.tbAutoOffTime.MaxLength = 3;
            this.tbAutoOffTime.Name = "tbAutoOffTime";
            this.tbAutoOffTime.Size = new System.Drawing.Size(36, 20);
            this.tbAutoOffTime.TabIndex = 53;
            this.tbAutoOffTime.TextChanged += new System.EventHandler(this.tbAutoOffTime_TextChanged);
            // 
            // panSetup
            // 
            this.panSetup.Controls.Add(this.label18);
            this.panSetup.Controls.Add(this.label11);
            this.panSetup.Controls.Add(this.tbAutoSensitivityTime);
            this.panSetup.Controls.Add(this.cbAutoSensitivity);
            this.panSetup.Controls.Add(this.paColor);
            this.panSetup.Controls.Add(this.label10);
            this.panSetup.Controls.Add(this.cbOutputs);
            this.panSetup.Controls.Add(this.label16);
            this.panSetup.Controls.Add(this.label17);
            this.panSetup.Controls.Add(this.tbAutoSwitchPerc);
            this.panSetup.Controls.Add(this.txtSmoothness);
            this.panSetup.Controls.Add(this.txtSensitiviy);
            this.panSetup.Controls.Add(this.label1);
            this.panSetup.Controls.Add(this.sbSensitivity);
            this.panSetup.Controls.Add(this.label5);
            this.panSetup.Controls.Add(this.label4);
            this.panSetup.Controls.Add(this.label3);
            this.panSetup.Controls.Add(this.label2);
            this.panSetup.Controls.Add(this.sbBlue);
            this.panSetup.Controls.Add(this.sbGreen);
            this.panSetup.Controls.Add(this.sbRed);
            this.panSetup.Controls.Add(this.sbSmoothness);
            this.panSetup.Location = new System.Drawing.Point(14, 67);
            this.panSetup.Name = "panSetup";
            this.panSetup.Size = new System.Drawing.Size(363, 250);
            this.panSetup.TabIndex = 56;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(240, 214);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(12, 13);
            this.label18.TabIndex = 82;
            this.label18.Text = "s";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(134, 214);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 81;
            this.label11.Text = "Time Period";
            // 
            // tbAutoSensitivityTime
            // 
            this.tbAutoSensitivityTime.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAutoSensitivityTime.Location = new System.Drawing.Point(203, 211);
            this.tbAutoSensitivityTime.MaxLength = 3;
            this.tbAutoSensitivityTime.Name = "tbAutoSensitivityTime";
            this.tbAutoSensitivityTime.Size = new System.Drawing.Size(36, 20);
            this.tbAutoSensitivityTime.TabIndex = 80;
            this.tbAutoSensitivityTime.TextChanged += new System.EventHandler(this.tbAutoSensitivityTime_TextChanged);
            // 
            // cbAutoSensitivity
            // 
            this.cbAutoSensitivity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAutoSensitivity.Location = new System.Drawing.Point(12, 213);
            this.cbAutoSensitivity.Name = "cbAutoSensitivity";
            this.cbAutoSensitivity.Size = new System.Drawing.Size(106, 17);
            this.cbAutoSensitivity.TabIndex = 79;
            this.cbAutoSensitivity.Text = "Auto Sensitivity";
            this.cbAutoSensitivity.UseVisualStyleBackColor = true;
            this.cbAutoSensitivity.CheckedChanged += new System.EventHandler(this.cbAutoSensitivity_CheckedChanged);
            // 
            // paColor
            // 
            this.paColor.Location = new System.Drawing.Point(335, 103);
            this.paColor.Name = "paColor";
            this.paColor.Size = new System.Drawing.Size(8, 72);
            this.paColor.TabIndex = 78;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(36, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 77;
            this.label10.Text = "Device";
            // 
            // cbOutputs
            // 
            this.cbOutputs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbOutputs.FormattingEnabled = true;
            this.cbOutputs.Location = new System.Drawing.Point(82, 17);
            this.cbOutputs.Name = "cbOutputs";
            this.cbOutputs.Size = new System.Drawing.Size(247, 21);
            this.cbOutputs.TabIndex = 76;
            this.cbOutputs.SelectedIndexChanged += new System.EventHandler(this.cbOutputs_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(119, 188);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 13);
            this.label16.TabIndex = 75;
            this.label16.Text = "%";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(13, 188);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 74;
            this.label17.Text = "Off threshold";
            // 
            // tbAutoSwitchPerc
            // 
            this.tbAutoSwitchPerc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAutoSwitchPerc.Location = new System.Drawing.Point(82, 185);
            this.tbAutoSwitchPerc.MaxLength = 3;
            this.tbAutoSwitchPerc.Name = "tbAutoSwitchPerc";
            this.tbAutoSwitchPerc.Size = new System.Drawing.Size(36, 20);
            this.tbAutoSwitchPerc.TabIndex = 73;
            // 
            // txtSmoothness
            // 
            this.txtSmoothness.AutoSize = true;
            this.txtSmoothness.Location = new System.Drawing.Point(332, 79);
            this.txtSmoothness.Name = "txtSmoothness";
            this.txtSmoothness.Size = new System.Drawing.Size(13, 13);
            this.txtSmoothness.TabIndex = 72;
            this.txtSmoothness.Text = "0";
            // 
            // txtSensitiviy
            // 
            this.txtSensitiviy.AutoSize = true;
            this.txtSensitiviy.Location = new System.Drawing.Point(332, 52);
            this.txtSensitiviy.Name = "txtSensitiviy";
            this.txtSensitiviy.Size = new System.Drawing.Size(13, 13);
            this.txtSensitiviy.TabIndex = 71;
            this.txtSensitiviy.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Sensitivity";
            // 
            // sbSensitivity
            // 
            this.sbSensitivity.Location = new System.Drawing.Point(82, 48);
            this.sbSensitivity.Minimum = 1;
            this.sbSensitivity.Name = "sbSensitivity";
            this.sbSensitivity.Size = new System.Drawing.Size(247, 17);
            this.sbSensitivity.TabIndex = 61;
            this.sbSensitivity.Value = 1;
            this.sbSensitivity.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbSensitivity_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 70;
            this.label5.Text = "Damping";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 67;
            this.label4.Text = "B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "G";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "R";
            // 
            // sbBlue
            // 
            this.sbBlue.Location = new System.Drawing.Point(82, 158);
            this.sbBlue.Maximum = 127;
            this.sbBlue.Name = "sbBlue";
            this.sbBlue.Size = new System.Drawing.Size(247, 17);
            this.sbBlue.SmallChange = 5;
            this.sbBlue.TabIndex = 64;
            this.sbBlue.Value = 1;
            this.sbBlue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbBlue_Scroll);
            // 
            // sbGreen
            // 
            this.sbGreen.Location = new System.Drawing.Point(82, 131);
            this.sbGreen.Maximum = 127;
            this.sbGreen.Name = "sbGreen";
            this.sbGreen.Size = new System.Drawing.Size(247, 17);
            this.sbGreen.SmallChange = 5;
            this.sbGreen.TabIndex = 63;
            this.sbGreen.Value = 1;
            this.sbGreen.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbGreen_Scroll);
            // 
            // sbRed
            // 
            this.sbRed.Location = new System.Drawing.Point(82, 102);
            this.sbRed.Maximum = 127;
            this.sbRed.Name = "sbRed";
            this.sbRed.Size = new System.Drawing.Size(247, 17);
            this.sbRed.SmallChange = 5;
            this.sbRed.TabIndex = 62;
            this.sbRed.Value = 1;
            this.sbRed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbRed_Scroll);
            // 
            // sbSmoothness
            // 
            this.sbSmoothness.Location = new System.Drawing.Point(82, 75);
            this.sbSmoothness.Minimum = 1;
            this.sbSmoothness.Name = "sbSmoothness";
            this.sbSmoothness.Size = new System.Drawing.Size(247, 17);
            this.sbSmoothness.TabIndex = 69;
            this.sbSmoothness.Value = 1;
            this.sbSmoothness.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbSmoothness_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(393, 436);
            this.Controls.Add(this.panSetup);
            this.Controls.Add(this.cbAutoSwitch);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbAutoOffTime);
            this.Controls.Add(this.tcSetup);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label7);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "VU1 Dials";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Close);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tcSetup.ResumeLayout(false);
            this.panSetup.ResumeLayout(false);
            this.panSetup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbRightCalibration;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbLeftCalibration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbRightDial;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbLeftDial;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TabControl tcSetup;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox cbAutoSwitch;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbAutoOffTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panSetup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbOutputs;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbAutoSwitchPerc;
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
        private System.Windows.Forms.Panel paColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbAutoSensitivityTime;
        private System.Windows.Forms.CheckBox cbAutoSensitivity;
        private System.Windows.Forms.Label label18;
    }
}

