﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace openGMC
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public SerialPort SPORT = new SerialPort();
        public bool evn = true;
        public int totalCount = 0;
        public double CPM = 0;
        public double secondsElapsed = 1;
        public double minutesElapsed = 0;
        public int s5Count = 0;
        public int maxCPS = 0;
        public string savePath = "";
        bool logging = false;
        bool logPahtSet = false;
        public List<String> bars = new List<String> { };
        public bool useAutoZoom = true;
        public bool highCps = false;
        public bool showNumeric = true;
        public bool evnText = false;
        public bool barGraph = false;

        public int awaitAutoCount = 0;

        public int listLen = 32;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void beginData(string comPort)
        {
            bars.Add("openGMC | Logging started");

            timer1.Start();
            SPORT.PortName = "COM" + comPort;
            SPORT.DataBits = 8;
            SPORT.Parity = Parity.None;
            SPORT.StopBits = StopBits.One;
            SPORT.BaudRate = 57600;

            try
            {
                SPORT.Open();
                SPORT.DiscardOutBuffer();
                SPORT.DiscardInBuffer();
                SPORT.DataReceived += new SerialDataReceivedEventHandler(responseHandler);
            }
            catch
            {

            }

            sendCommand("<HEARTBEAT1>>");
            
        }

        public void sendCommand(string command)
        {
            try
            {
                SPORT.Write(command);
                button1.Enabled = false;
            }
            catch
            {
                alert("Error", "Error communicating with port");
            }

        }

        private void responseHandler(object sender, SerialDataReceivedEventArgs args)
        {
            int x = int.Parse(SPORT.ReadByte().ToString(), System.Globalization.NumberStyles.HexNumber);

            evn = !evn;
            if (evn)
            {
                if (x > maxCPS)
                {
                    maxCPS = x;
                    awaitAutoCount = 11;
                }

                if (awaitAutoCount > 0)
                {
                    if (awaitAutoCount == 1)
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            snapshot();
                        }));
                        awaitAutoCount--;
                    }
                    else
                    {
                        awaitAutoCount--;
                    }
                }

                s5Count++;
                totalCount += x;
                minutesElapsed = secondsElapsed / 60;
                CPM = Math.Ceiling(totalCount / minutesElapsed);

                shiftReg(x);

                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        label2.Text = "Total counts: " + totalCount.ToString();
                    }));
                }
                catch {}

                if (logging && logPahtSet)
                {
                    string tickBar = "";
                    for(int e = 0; e < x; e++)
                    {
                        tickBar += "#";
                    }
                    bars.Add(DateTime.Now + " : " + tickBar);
                    File.WriteAllLines(savePath, bars);
                }
            }
        }

        public List<int> arr = new List<int> { };

        public void shiftReg(int inst)
        {
            List<Int32> data = new List<Int32> { };

            int pxOneBar = 700 / listLen;

            if(arr.Count() < listLen)
            {
                for (int countVarAdding = 0; countVarAdding < listLen; countVarAdding++)
                {
                    arr.Add(0);
                }
            }

            int pos = listLen - 1;

            for (int countVarShift = 0; countVarShift < (listLen - 1); countVarShift++)
            {
                if(pos > 0)
                {
                    arr[pos] = arr[pos - 1];
                    pos--;
                }
                if(pos == 0)
                {
                    arr[pos] = inst;
                    pos--;
                }
            }

            for(int countVarString = 0; countVarString < listLen; countVarString++)
            {
                data.Add(arr[countVarString]);
            }

            bool autoZoom = checkBox3.Checked;
            bool barGraph = BarRB.Checked;

            graph_draw(data.ToArray(), showNumeric, barGraph, autoZoom);

        }

        public Bitmap grph = new Bitmap(700, 300);

        public void graph_draw(int[] data, bool showNum, bool barGraph, bool autoZoom)
        {
            int zoom = 0;
            this.Invoke(new MethodInvoker(delegate (){ zoom = trackBar1.Value; }));

            int points_num = data.Length;
            Pen thiccBlack = new Pen(Color.Black, 2);
            Pen thiccRed = new Pen(Color.Red, 2);
            Graphics g = Graphics.FromImage(grph);
            int centerPoint = grph.Width / 2;
            int fits = 700 / points_num;
            Console.WriteLine(fits);
            Font font = new Font("Lucida Console", 10.0f);
            string time = DateTime.Now.ToString();
            List<Point> points = new List<Point> { };
            int halfNum = points_num / 2;
            int width = 0;
            int externalPointer = 0;

            g.FillRectangle(Brushes.LightGray, 0, 0, 700, 300);
            g.DrawString(time, font, Brushes.Black, 5, 5);

            for (int i = 0; i < halfNum; i++)
            {
                points.Add(new Point(centerPoint + width, 0));
                externalPointer++;
                width += fits;
            }
            width = 0;
            for (int i = 0; i < halfNum; i++)
            {
                points.Add(new Point(centerPoint - width, 0));
                externalPointer++;
                width += fits;
            }
            points = points.OrderByDescending(p => p.X).ToList();
            points = points.Distinct().ToList();
            int redoCount = 0;
            int addTobarwidth = 0;
            if (barGraph)
            {
                int highestnum = 0;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                g.DrawLine(thiccBlack, new Point(0, 281), new Point(700, 281));
                g.DrawLine(thiccBlack, new Point(0, 282), new Point(700, 282));
                g.DrawLine(thiccBlack, new Point(0, 283), new Point(700, 283));

                int prevLeftPoint = 0;
                foreach (Point p in points)
                {
                    try
                    {
                        if (data[redoCount - 1] > highestnum) { highestnum = data[redoCount - 1]; }
                        int height = 278 - data[redoCount - 1] * zoom;
                        if (height < 25 && autoZoom) { this.Invoke(new MethodInvoker(delegate () { trackBar1.Value -= 1; })); }
                    }
                    catch { }
                    redoCount++;
                    if (showNum)
                    {
                        if (data[redoCount - 1].ToString().Length > 1) { g.DrawString(data[redoCount - 1].ToString(), font, Brushes.Black, new Point(p.X - 10, 285 - p.Y)); }
                        else { g.DrawString(data[redoCount - 1].ToString(), font, Brushes.Black, new Point(p.X - 5, 285 - p.Y)); }
                    }

                    //g.DrawRectangle(Pens.Black, )

                    Rectangle r = new Rectangle((p.X - (fits + addTobarwidth) / 2), (280 - data[redoCount - 1] * zoom), 5, 5);

                    Point upperLeft = new Point(p.X - (fits + addTobarwidth) / 2, 280 - data[redoCount - 1] * zoom);
                    Point lowerRight = new Point(p.X + fits / 2, 300);

                    g.DrawRectangle(thiccBlack, upperLeft.X, upperLeft.Y, lowerRight.X - upperLeft.X, upperLeft.Y);

                    //g.DrawLine(thiccBlack, new Point(p.X - (fits + addTobarwidth) / 2, 280 - data[redoCount - 1] * zoom), new Point(p.X - fits / 2, 300));
                    //g.DrawLine(thiccBlack, new Point(p.X + (fits + addTobarwidth) / 2, 280 - data[redoCount - 1] * zoom), new Point(p.X + fits / 2, 300));
                    //g.DrawLine(thiccBlack, new Point(p.X - (fits + addTobarwidth) / 2, 280 - data[redoCount - 1] * zoom), new Point(p.X + (fits + addTobarwidth) / 2, 280 - data[redoCount - 1] * zoom));
                    if (p.X - fits / 2 != prevLeftPoint) { addTobarwidth = 1; }
                    else { addTobarwidth = 0; }
                    prevLeftPoint = p.X + fits / 2;
                }
            }
            else
            {
                int highestnum = 0;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Point prevPoint = new Point(0, 0);
                foreach (Point p in points)
                {
                    try
                    {
                        if (data[redoCount - 1] > highestnum) { highestnum = data[redoCount - 1]; }
                        int height = 278 - data[redoCount - 1] * zoom;
                        if (height < 25 && autoZoom) { this.Invoke(new MethodInvoker(delegate () { trackBar1.Value -= 1; })); }
                    }
                    catch { }
                    redoCount++;
                    if (showNum)
                    {
                        if (data[redoCount - 1].ToString().Length > 1) { g.DrawString(data[redoCount - 1].ToString(), font, Brushes.Black, new Point(p.X - 10, 283 - p.Y)); }
                        else { g.DrawString(data[redoCount - 1].ToString(), font, Brushes.Black, new Point(p.X - 5, 283 - p.Y)); }
                    }
                    g.DrawLine(thiccBlack, new Point(p.X, 278 - data[redoCount - 1] * zoom), new Point(p.X, 280));
                    if (prevPoint != new Point(0, 0))
                    {
                        g.DrawLine(thiccRed, new Point(p.X, 278 - data[redoCount - 1] * zoom), prevPoint);
                    }
                    prevPoint = new Point(p.X, 278 - data[redoCount - 1] * zoom);
                }
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.DrawLine(thiccBlack, new Point(0, 281), new Point(700, 281));
                g.DrawLine(thiccBlack, new Point(0, 282), new Point(700, 282));
                g.DrawLine(thiccBlack, new Point(0, 283), new Point(700, 283));
            }
            GraphPB.Image = grph;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            beginData(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            snapshot();
        }

        public void snapshot()
        {
            try
            {
                string time = DateTime.Now.ToString();
                string timeProcessed = time.Replace(" ", "-");
                timeProcessed = timeProcessed.Replace(":", "-");
                timeProcessed = timeProcessed.Replace("/", "-");
                GraphPB.Image.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + timeProcessed + ".png");
                label4.Text = "Documents/" + timeProcessed + ".PNG";
            }
            catch {}
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            listLen = Convert.ToInt32(numericUpDown1.Value);
        }

        private void chart1_Click(object sender, EventArgs e) {}

        private void trackBar1_Scroll(object sender, EventArgs e) {}

        private void DVOn_Click(object sender, EventArgs e)
        {
            disableBtns();
            sendCommand("<POWERON>>");
            System.Threading.Thread.Sleep(1000);
            sendCommand("<HEARTBEAT1>>");
            enablBtns();
        }

        private void DVOff_Click(object sender, EventArgs e)
        {
            disableBtns();
            sendCommand("<HEARTBEAT0>>");
            System.Threading.Thread.Sleep(1000);
            sendCommand("<POWEROFF>>");
            enablBtns();
        }

        private void FEEDStop_Click(object sender, EventArgs e)
        {
            disableBtns();
            sendCommand("<HEARTBEAT0>>");
            System.Threading.Thread.Sleep(1000);
            enablBtns();
        }

        private void FEEDStart_Click(object sender, EventArgs e)
        {
            disableBtns();
            sendCommand("<HEARTBEAT1>>");
            System.Threading.Thread.Sleep(1000);
            enablBtns();
        }

        public void enablBtns()
        {
            DV_On.Enabled = true;
            DV_Off.Enabled = true;
            FEEDStop.Enabled = true;
            FEEDStart.Enabled = true;
        }

        public void disableBtns()
        {
            DV_On.Enabled = false;
            DV_Off.Enabled = false;
            FEEDStop.Enabled = false;
            FEEDStart.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Open Output File";
                dlg.Filter = "Text Files | *.txt";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    label6.Text = Path.GetFileName(dlg.FileName); 
                    savePath = dlg.FileName;
                    logPahtSet = true;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                logging = true;
            }
            else
            {
                logging = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                useAutoZoom = true;
            }
            else
            {
                useAutoZoom = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                showNumeric = true;
            }
            else
            {
                showNumeric = false;
            }
        }

        public void alert(string head, string body)
        {
            Form msg = new Msg(head, body);
            msg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            alert("info", "sh_info");
        }

        private void LineRB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BarRB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form PortSearch = new PortSearch();
            PortSearch.Show();

        }
    }
}
