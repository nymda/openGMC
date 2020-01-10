using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace openGMC
{
    public partial class Form1 : Form
    {
        public Form1()
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void beginData(string comPort)
        {
            timer1.Start();
            string VER_Command = "VER\r";

            if (SPORT is SerialPort)
            {
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
                    SPORT.Write(VER_Command);
                }
                catch (Exception exc)
                {
                }
            }
        }

        private void responseHandler(object sender, SerialDataReceivedEventArgs args)
        {
            int x = SPORT.ReadByte();
            evn = !evn;
            if (evn)
            {
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
            }
        }

        public List<int> arr = new List<int> { };
        public int[] graphData = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public void shiftReg(int inst)
        {
            //master int uwu
            int listLen = 16;

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

            string prnt = "| ";
            for(int countVarString = 0; countVarString < listLen; countVarString++)
            {
                prnt += arr[countVarString] + " | ";
            }
            drawGraphFromString(prnt);
            Console.WriteLine(prnt);
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

            string processed = gstring.Replace("|", "");
            processed = processed.Replace(" ", "");
            int length = processed.Length;

            Graphics g = Graphics.FromImage(grph);

            g.FillRectangle(Brushes.Gray, 0, 0, 700, 300);

            int barWidth = 700 / length;

            int currentPos = barWidth / 2;


            Point curPoint = new Point();

            for(int i = 0; i < length; i++)
            {           
                points.Add(new Point(690 - currentPos, 280 - Int32.Parse(processed[i].ToString()) * zoom));
                values.Add(processed[i].ToString());
                currentPos += barWidth;
            }

            int counter = 0;
            foreach (Point p in points)
            {
                g.DrawString(values[counter], font, Brushes.Black, new Point(p.X - 6, 285));

                g.DrawRectangle(Pens.Black, p.X, p.Y, 2, 2);

                counter++;
            }

            for(int o = 1; o < points.Count; o++)
            {
                g.DrawLine(Pens.Black, points[o], points[o - 1]);
            }

            g.DrawString(time, font, Brushes.Black, new Point(10, 10));

            pictureBox1.Image = grph;
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
            string time = DateTime.Now.ToString();
            string timeProcessed = time.Replace(" ", "-");
            timeProcessed = timeProcessed.Replace(":", "-");
            timeProcessed = timeProcessed.Replace("/", "-");
            pictureBox1.Image.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + timeProcessed + ".png");
            label4.Text = "Documents/" + timeProcessed + ".PNG";
        }
    }
}
