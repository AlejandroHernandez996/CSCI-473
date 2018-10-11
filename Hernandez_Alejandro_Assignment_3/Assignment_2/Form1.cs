using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Assignment_2
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        // Show Results for all grades of one student ID
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            if (Regex.IsMatch(textBox1.Text, @"^\d+$"))
            {
                uint id = Convert.ToUInt32(textBox1.Text);
                richTextBox1.AppendText("Single Student Grade Report (z"+ textBox1.Text + ")" + Environment.NewLine);
                richTextBox1.AppendText("------------------------------------------------------------- " + Environment.NewLine);

                var studentGrades =
                    from C in Globals.coursePool
                    where (C.grades.ContainsKey(id))
                    select C.grades[id];

                if (!studentGrades.Any())
                {
                    richTextBox1.Text = "ID was not found please try again.";
                    return;
                }

                foreach (var grade in studentGrades)
                {
                    richTextBox1.AppendText("z"+textBox1.Text + "  |  " + grade[1] + "-" + grade[2] + "  |  " + grade[0] + Environment.NewLine);
                }

            }
            else
            {
                richTextBox1.Text = "ID must be a number";

            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
