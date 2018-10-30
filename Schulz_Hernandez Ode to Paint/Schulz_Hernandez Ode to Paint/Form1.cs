using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Schulz_Hernandez_Ode_to_Paint
{

    

    public partial class Form1 : Form
    {

        //private SolidBrush brush;
        private Graphics g;
        private bool isPainting = false;
        private Color color1 = Color.Black;


        public Form1()
        {
            InitializeComponent();
            g = paintPanel.CreateGraphics();
        }

        int? initX = null;
        int? initY = null;

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void colorBox_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void colorBox_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            color1 = textBox.BackColor;
        }

        private void paintPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isPainting = true;
        }

        private void PaintPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isPainting = false;
            initX = null;
            initY = null;
        }

        private void paintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPainting == true)
            {
                Pen p = new Pen(color1, 1.0f);
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                initX = e.X;
                initY = e.Y;
            }
        }
    }
}
