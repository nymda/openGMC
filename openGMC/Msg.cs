using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace openGMC
{
    public partial class Msg : Form
    {
        public string head;
        public string body;

        public Msg(string headL, string bodyL)
        {
            InitializeComponent();
            head = headL;
            body = bodyL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Msg_Load(object sender, EventArgs e)
        {
            this.Text = head;

            if(body == "sh_info")
            {
                label1.Text = "OpenGMC - By knedit. \n\nTested on the GMC-300E \nApparently doesnt work on the 320+ \nAuto zoom: reduces zoom to show highest peak \nNumbers: shows numbers at the bottom of the graph";
            }
            else
            {
                label1.Text = body;
            }
        }
    }
}
