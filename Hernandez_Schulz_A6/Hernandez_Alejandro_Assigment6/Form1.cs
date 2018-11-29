using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hernandez_Alejandro_Assigment6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PIe pieForm = new PIe(this);
            this.Hide();
            pieForm.Show();

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bar barForm = new Bar(this);
            this.Hide();
            barForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Line lineForm = new Line(this);
            this.Hide();
            lineForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Radar radarForm = new Radar(this);
            this.Hide();
            radarForm.Show();

        }
    }
}
