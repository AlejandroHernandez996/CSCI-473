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
            richTextBox1.Clear();

            string major = comboBox2.Text;
            string courseText = textBox3.Text;
            string[] courseSplit = courseText.Split(' ');

            if (courseSplit.Length == 2)
            {

                richTextBox1.AppendText("Fail Report for majors (" + major + ") in "+ courseText + Environment.NewLine);
                richTextBox1.AppendText("------------------------------------------------------------- " + Environment.NewLine);

                var courses =
                    from C in Globals.coursePool
                    where C.departmentCode.Equals(courseSplit[0]) && C.courseNumber.ToString().Equals(courseSplit[1])
                    select C;

                if (!courses.Any())
                {
                    richTextBox1.Text = "Course was not found please try again.";
                    return;
                }

                foreach (var course in courses)
                {

                    foreach (KeyValuePair<uint, List<string>> entry in course.grades)
                    {
                        foreach(Student s in Globals.studentPool)
                        {
                            if(s.id == entry.Key && s.major.Equals(major) && 
                                (entry.Value[0].Equals("F") || entry.Value[0].Equals("F-") || entry.Value[0].Equals("F--")  || entry.Value[0].Equals("F+") || entry.Value[0].Equals("F++")))
                            {
                                richTextBox1.AppendText("z" + entry.Key + "  |  " + entry.Value[1] + "-" + entry.Value[2] + "  |  " + entry.Value[0] + Environment.NewLine);
                                break;
                            }
                        }

                    }
                }

            }
            else
            {
                richTextBox1.Text = "Enter DepCode and CourseNum (XXXX XXX)";

            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            string courseText = textBox4.Text.ToUpper();
            string[] courseSplit = courseText.Split(' ');

            if (courseSplit.Length == 2)
            {
                
                richTextBox1.AppendText("Grade Report for (" + courseText + ")" + Environment.NewLine);
                richTextBox1.AppendText("------------------------------------------------------------- " + Environment.NewLine);

                var courses =
                    from C in Globals.coursePool
                    where (C.departmentCode.Equals(courseSplit[0]) && C.courseNumber.ToString().Equals(courseSplit[1]))
                    select C;

                if (!courses.Any())
                {
                    richTextBox1.Text = "Course was not found please try again.";
                    return;
                }

                foreach (var course in courses)
                {

                    foreach (KeyValuePair<uint, List<string>> entry in course.grades)
                    {
                        richTextBox1.AppendText("z" + entry.Key + "  |  " + entry.Value[1] + "-" + entry.Value[2] + "  |  " + entry.Value[0] + Environment.NewLine);

                    }
                }

            }
            else
            {
                richTextBox1.Text = "Enter DepCode and CourseNum (XXXX XXX)";

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            string gradeText = comboBox1.Text;
            string[] gradeSplit = new String[2];
            string courseText = textBox2.Text.ToUpper();
            string[] courseSplit = courseText.Split(' ');
            var checkedButton = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (gradeText.Length > 1)
            {
                gradeSplit[0] = gradeText.Substring(0, 1);
                gradeSplit[1] = gradeText.Substring(1);
            }

            var courses =
                from C in Globals.coursePool
                where (C.departmentCode.Equals(courseSplit[0]) && C.courseNumber.ToString().Equals(courseSplit[1]))
                select C;

            if (!courses.Any())
            {
                richTextBox1.Text = "Course was not found please try again.";
                return;
            }

            richTextBox1.Text += "Grade threshold report for (" + courseSplit[0] + ")\n";
            richTextBox1.Text += "---------------------------------------------------------------------------------------\n";

            foreach (var course in courses)
            {
                var filteredStudents =
                    from C in course.grades
                    where ((C.Value[0].Substring(0).CompareTo(gradeSplit[0]) == 0 || C.Value[0].Substring(0).CompareTo(gradeSplit[0]) == 1 ) 
                            && (C.Value[0].Substring(1).CompareTo(gradeSplit[1]) == 0 || C.Value[0].Substring(1).CompareTo(gradeSplit[1]) == -1 ))      // this part doesn't work
                    select C;

                foreach (var filtered in filteredStudents)
                {
                    richTextBox1.Text += string.Format("{0}  |  {1}-{2}  |  {3}\n", filtered.Key, filtered.Value[1], filtered.Value[2], filtered.Value[0]);
                }

            }


            // checks what button is selected. Could be a better way of doing this.
            if (checkedButton.Text.Contains("Less"))
            {
                richTextBox1.Text += "\n*Less than*\n";
            }
            else
            {
                richTextBox1.Text += "\n*Greater than*\n";
            }
            
        }
    }
}
