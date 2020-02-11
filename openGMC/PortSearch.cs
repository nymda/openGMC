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
    public partial class PortSearch : Form
    {
        public PortSearch()
        {
            InitializeComponent();
        }

        public int LOW = 10;
        public int HIGH = 255;
        public SerialPort SPORT = new SerialPort();

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Scanning...";
            List<Int32> ports = new List<Int32> { -1 };
            if (radioButton1.Checked) { ports = scanPorts(LOW); }
            else{ ports = scanPorts(HIGH); }

            foreach(int i in ports)
            {
                listBox1.Items.Insert(0, "COM" + i + " OPEN");
            }

            label1.Text = "Done.";
        }

        public List<Int32> scanPorts(int num)
        {
            List<Int32> ports = new List<Int32> { };
            for(int i = 0; i < num; i++)
            {
                SPORT.PortName = "COM" + i;
                try
                {
                    SPORT.Open();
                    ports.Add(i);
                }
                catch { }
            }
            return ports;
        }
    }
}
