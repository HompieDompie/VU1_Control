
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
            this.label6 = new System.Windows.Forms.Label();
            this.cbInputs = new System.Windows.Forms.ComboBox();
            this.cbOutputs = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSmoothness = new System.Windows.Forms.Label();
            this.txtSensitiviy = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sbSensitivity = new System.Windows.Forms.HScrollBar();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRightDial = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLeftDial = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sbBlue = new System.Windows.Forms.HScrollBar();
            this.sbGreen = new System.Windows.Forms.HScrollBar();
            this.sbRed = new System.Windows.Forms.HScrollBar();
            this.sbSmoothness = new System.Windows.Forms.HScrollBar();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbInputs);
            this.groupBox1.Controls.Add(this.cbOutputs);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(18, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 91);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select capture device";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Input";
            // 
            // cbInputs
            // 
            this.cbInputs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbInputs.FormattingEnabled = true;
            this.cbInputs.Location = new System.Drawing.Point(78, 57);
            this.cbInputs.Name = "cbInputs";
            this.cbInputs.Size = new System.Drawing.Size(247, 21);
            this.cbInputs.TabIndex = 27;
            this.cbInputs.SelectedIndexChanged += new System.EventHandler(this.cbInputs_SelectedIndexChanged);
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
            this.label10.Location = new System.Drawing.Point(30, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Output";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSmoothness);
            this.groupBox2.Controls.Add(this.txtSensitiviy);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.sbSensitivity);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbRightDial);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tbLeftDial);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.sbBlue);
            this.groupBox2.Controls.Add(this.sbGreen);
            this.groupBox2.Controls.Add(this.sbRed);
            this.groupBox2.Controls.Add(this.sbSmoothness);
            this.groupBox2.Location = new System.Drawing.Point(18, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 198);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dial Setup";
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
            this.label1.Location = new System.Drawing.Point(15, 26);
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(126, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Right";
            // 
            // tbRightDial
            // 
            this.tbRightDial.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbRightDial.Location = new System.Drawing.Point(164, 158);
            this.tbRightDial.MaxLength = 1;
            this.tbRightDial.Name = "tbRightDial";
            this.tbRightDial.Size = new System.Drawing.Size(24, 20);
            this.tbRightDial.TabIndex = 35;
            this.tbRightDial.TextChanged += new System.EventHandler(this.tbRightDial_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Left Dial Nr";
            // 
            // tbLeftDial
            // 
            this.tbLeftDial.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbLeftDial.Location = new System.Drawing.Point(78, 158);
            this.tbLeftDial.MaxLength = 1;
            this.tbLeftDial.Name = "tbLeftDial";
            this.tbLeftDial.Size = new System.Drawing.Size(24, 20);
            this.tbLeftDial.TabIndex = 33;
            this.tbLeftDial.WordWrap = false;
            this.tbLeftDial.TextChanged += new System.EventHandler(this.tbLeftDial_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Damping";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "G";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 80);
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
            this.btnStart.Location = new System.Drawing.Point(319, 363);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(52, 38);
            this.btnStart.TabIndex = 27;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(387, 409);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbInputs;
        private System.Windows.Forms.ComboBox cbOutputs;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label txtSmoothness;
        private System.Windows.Forms.Label txtSensitiviy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar sbSensitivity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbRightDial;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbLeftDial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HScrollBar sbBlue;
        private System.Windows.Forms.HScrollBar sbGreen;
        private System.Windows.Forms.HScrollBar sbRed;
        private System.Windows.Forms.HScrollBar sbSmoothness;
        private System.Windows.Forms.Button btnStart;
    }
}

