using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public int awaitAutoCount = 0;

        public int listLen = 57;

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
            SPORT.Write(command);
        }

        private void responseHandler(object sender, SerialDataReceivedEventArgs args)
        {
            int x = SPORT.ReadByte();

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
                catch
                {

                }

                if (s5Count == 5)
                {
                    s5Count = 0;
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        label1.Text = "CPM: " + CPM.ToString();
                    }));
                }

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

            int pxOneBar = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(700 / Convert.ToDouble(listLen))));

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

            string prnt = "";
            for(int countVarString = 0; countVarString < listLen; countVarString++)
            {
                prnt += arr[countVarString];
            }
            drawGraphFromString(prnt);
        }

        public Bitmap grph = new Bitmap(700, 300);

        public void drawGraphFromString(string gstring)
        {
            int zoom = 25;
            Font font = new Font("Lucida Console", 10.0f);
            string time = DateTime.Now.ToString();
            this.Invoke(new MethodInvoker(delegate ()
            {
                zoom = trackBar1.Value;
            }));
            List<Point> points = new List<Point> { };
            List<String> values = new List<String> { };
            string processed = gstring;
            int length = processed.Length;
            Graphics g = Graphics.FromImage(grph);
            g.FillRectangle(Brushes.LightGray, 0, 0, 700, 300);
            int barWidth = 700 / length;
            int currentPos = barWidth / 2;
            for(int i = 0; i < length; i++)
            {   
                points.Add(new Point(690 - currentPos, 280 - Int32.Parse(processed[i].ToString()) * zoom));
                values.Add(processed[i].ToString());
                currentPos += barWidth;
            }
            int counter = 0;
            foreach (Point p in points)
            {
                try
                {             
                    g.DrawString(values[counter], font, Brushes.Black, new Point(p.X - 6, 285));
                    g.DrawLine(Pens.Black, p, new Point(p.X, 283));
                    counter++;
                    Console.WriteLine(counter + " " + Convert.ToString(counter - 1) + " : " + points.Count());
                    g.DrawLine(Pens.OrangeRed, points[counter - 1], points[counter - 2]);
                }
                catch
                {

                }
            }

            g.DrawString("Z: " + zoom, font, Brushes.Black, new Point(10, 25));
            g.DrawString(time, font, Brushes.Black, new Point(10, 10));
            GraphPB.Image = grph;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            beginData(textBox1.Text);
            button1.Enabled = false;
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
            catch
            {

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            listLen = Convert.ToInt32(numericUpDown1.Value);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

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
    }
}
