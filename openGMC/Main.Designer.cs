namespace openGMC
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.GraphPB = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DV_On = new System.Windows.Forms.Button();
            this.DV_Off = new System.Windows.Forms.Button();
            this.FEEDStop = new System.Windows.Forms.Button();
            this.FEEDStart = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CB_Autozoom = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GraphPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 20F);
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "CPS: --";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 20F);
            this.label2.Location = new System.Drawing.Point(18, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(401, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total counts: --";
            // 
            // GraphPB
            // 
            this.GraphPB.BackColor = System.Drawing.Color.LightGray;
            this.GraphPB.Location = new System.Drawing.Point(24, 104);
            this.GraphPB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GraphPB.Name = "GraphPB";
            this.GraphPB.Size = new System.Drawing.Size(1152, 491);
            this.GraphPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GraphPB.TabIndex = 2;
            this.GraphPB.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(100, 23);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(79, 27);
            this.textBox1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "COM port:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 61);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(1185, 104);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar1.Maximum = 75;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(69, 491);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Value = 25;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1353, 20);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 34);
            this.button2.TabIndex = 7;
            this.button2.Text = "Snap";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.label4.Location = new System.Drawing.Point(1041, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(284, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "Documents/00-00-0000-00-00-00-00.PNG";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(1154, 69);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(81, 27);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            57,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(1244, 69);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(190, 104);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1040, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Graph Width:";
            // 
            // DV_On
            // 
            this.DV_On.Location = new System.Drawing.Point(1257, 181);
            this.DV_On.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DV_On.Name = "DV_On";
            this.DV_On.Size = new System.Drawing.Size(165, 34);
            this.DV_On.TabIndex = 6;
            this.DV_On.Text = "DEVICE ON";
            this.DV_On.UseVisualStyleBackColor = true;
            this.DV_On.Click += new System.EventHandler(this.DVOn_Click);
            // 
            // DV_Off
            // 
            this.DV_Off.Location = new System.Drawing.Point(1257, 224);
            this.DV_Off.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DV_Off.Name = "DV_Off";
            this.DV_Off.Size = new System.Drawing.Size(165, 34);
            this.DV_Off.TabIndex = 7;
            this.DV_Off.Text = "DEVICE OFF";
            this.DV_Off.UseVisualStyleBackColor = true;
            this.DV_Off.Click += new System.EventHandler(this.DVOff_Click);
            // 
            // FEEDStop
            // 
            this.FEEDStop.Location = new System.Drawing.Point(1257, 308);
            this.FEEDStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FEEDStop.Name = "FEEDStop";
            this.FEEDStop.Size = new System.Drawing.Size(165, 34);
            this.FEEDStop.TabIndex = 12;
            this.FEEDStop.Text = "STOP FEED";
            this.FEEDStop.UseVisualStyleBackColor = true;
            this.FEEDStop.Click += new System.EventHandler(this.FEEDStop_Click);
            // 
            // FEEDStart
            // 
            this.FEEDStart.Location = new System.Drawing.Point(1257, 266);
            this.FEEDStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FEEDStart.Name = "FEEDStart";
            this.FEEDStart.Size = new System.Drawing.Size(165, 34);
            this.FEEDStart.TabIndex = 13;
            this.FEEDStart.Text = "START FEED";
            this.FEEDStart.UseVisualStyleBackColor = true;
            this.FEEDStart.Click += new System.EventHandler(this.FEEDStart_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(45, 61);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(105, 34);
            this.button7.TabIndex = 14;
            this.button7.Text = "Set file";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 28);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(141, 23);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Log data to file";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 99);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 19);
            this.label6.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Location = new System.Drawing.Point(1257, 488);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(164, 107);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logging";
            // 
            // CB_Autozoom
            // 
            this.CB_Autozoom.AutoSize = true;
            this.CB_Autozoom.Checked = true;
            this.CB_Autozoom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_Autozoom.Location = new System.Drawing.Point(1258, 349);
            this.CB_Autozoom.Name = "CB_Autozoom";
            this.CB_Autozoom.Size = new System.Drawing.Size(113, 23);
            this.CB_Autozoom.TabIndex = 18;
            this.CB_Autozoom.Text = "Auto zoom";
            this.CB_Autozoom.UseVisualStyleBackColor = true;
            this.CB_Autozoom.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 618);
            this.Controls.Add(this.CB_Autozoom);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FEEDStart);
            this.Controls.Add(this.FEEDStop);
            this.Controls.Add(this.DV_On);
            this.Controls.Add(this.DV_Off);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.GraphPB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Main";
            this.Text = "openGMC";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GraphPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox GraphPB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button DV_On;
        private System.Windows.Forms.Button DV_Off;
        private System.Windows.Forms.Button FEEDStop;
        private System.Windows.Forms.Button FEEDStart;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox CB_Autozoom;
    }
}

