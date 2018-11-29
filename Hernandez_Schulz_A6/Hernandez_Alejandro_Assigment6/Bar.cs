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
    public partial class Bar : Form
    {
        Form1 f;

        public Bar(Form1 f)
        {
            InitializeComponent();
            this.f = f;
            this.FormClosing += Bar_FormClosing;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.Show(); 
            this.Hide(); 
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Bar_Load(object sender, EventArgs e)
        {

        }
        
        private void Bar_FormClosing(object sender, FormClosingEventArgs e)
        {
            f.Show(); 
            this.Hide();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
