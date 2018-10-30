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
        // FAIL REPORT
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            string major = comboBox2.Text;
            string courseText = textBox3.Text;
            string[] courseSplit = courseText.Split(' ');

            // Checks if real input given
            if (courseSplit.Length == 2)
            {

                // Header
                richTextBox1.AppendText("Fail Report for majors (" + major + ") in "+ courseText + Environment.NewLine);
                richTextBox1.AppendText("------------------------------------------------------------- " + Environment.NewLine);

                // LINQ query for courses
                var courses =
                    from C in Globals.coursePool
                    where C.departmentCode.Equals(courseSplit[0]) && C.courseNumber.ToString().Equals(courseSplit[1])
                    select C;

                if (!courses.Any())
                {
                    richTextBox1.Text = "Course was not found please try again.";
                    return;
                }

                //Prints to query box
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

            //Check if number input
            if (Regex.IsMatch(textBox1.Text, @"^\d+$"))
            {
                //Hheader
                uint id = Convert.ToUInt32(textBox1.Text);
                richTextBox1.AppendText("Single Student Grade Report (z"+ textBox1.Text + ")" + Environment.NewLine);
                richTextBox1.AppendText("------------------------------------------------------------- " + Environment.NewLine);

                //Linq search for students
                var studentGrades =
                    from C in Globals.coursePool
                    where (C.grades.ContainsKey(id))
                    select C.grades[id];

                if (!studentGrades.Any())
                {
                    richTextBox1.Text = "ID was not found please try again.";
                    return;
                }

                //Print students found
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

        // Grade report for a specific course
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            string courseText = textBox4.Text;
            string[] courseSplit = courseText.Split(' ');

            if (courseSplit.Length == 2)
            {
                // Header
                richTextBox1.AppendText("Grade Report for (" + courseText + ")" + Environment.NewLine);
                richTextBox1.AppendText("------------------------------------------------------------- " + Environment.NewLine);

                // Linq query for courses
                var courses =
                    from C in Globals.coursePool
                    where (C.departmentCode.Equals(courseSplit[0]) && C.courseNumber.ToString().Equals(courseSplit[1]))
                    select C;

                if (!courses.Any())
                {
                    richTextBox1.Text = "Course was not found please try again.";
                    return;
                }

                //PRint courses found
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

        // Grade Threshhold for class
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            string courseText = textBox2.Text;
            string[] courseSplit = courseText.Split(' ');
            string grade = comboBox1.Text;
            bool isLess = false;
            bool isMore = false;

            if (radioButton1.Checked)
                isLess = true;
            if (radioButton3.Checked)
                isMore = true;
            if (!isLess && !isMore)
            {
                richTextBox1.AppendText("Choose threshold" + Environment.NewLine);
            }
            //Check if valid input
            if (courseSplit.Length == 2)
            {

                //Header
                richTextBox1.AppendText("Grade Threshold Report for (" + courseText + ") " + grade + Environment.NewLine );
                richTextBox1.AppendText("------------------------------------------------------------- " + Environment.NewLine);

                //LINQ search for course
                var courses =
                    from C in Globals.coursePool
                    where (C.departmentCode.Equals(courseSplit[0]) && C.courseNumber.ToString().Equals(courseSplit[1]))
                    select C;

                if (!courses.Any())
                {
                    richTextBox1.Text = "Course was not found please try again.";
                    return;
                }
                //Find students in order
                List<KeyValuePair<uint, List<string>>> values = new List<KeyValuePair<uint, List<string>>>();
                foreach (var course in courses)
                {
                    foreach (KeyValuePair<uint, List<string>> entry in course.grades)
                    {
                        if (isLess && entry.Value[0].CompareTo(grade) >= 0)
                            values.Add(entry);
                        else if (isMore && entry.Value[0].CompareTo(grade) <= 0)
                            values.Add(entry);
                        
                    }
                }
                values.Sort(delegate (KeyValuePair<uint, List<string>> p1, KeyValuePair<uint, List<string>> p2) {
                    return p1.Value[0].CompareTo(p2.Value[0]);
                });
                //Print students
                foreach (KeyValuePair<uint, List<string>> entry in values)
                {
                    richTextBox1.AppendText("z" + entry.Key + "  |  " + entry.Value[1] + "-" + entry.Value[2] + "  |  " + entry.Value[0] + Environment.NewLine);

                }

            }
            else
            {
                richTextBox1.Text = "Enter DepCode and CourseNum (XXXX XXX)";

            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        // Failed threshold
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            double percent = Convert.ToDouble(textBox5.Text);
            bool isLess = false;
            bool isMore = false;
            if(!isLess && !isMore)
            {
                richTextBox1.AppendText("Choose threshold" + Environment.NewLine);
            }
            if (radioButton4.Checked)
                isLess = true;
            if (radioButton2.Checked)
                isMore = true;

            if (percent >= 0 && percent <= 100)
            {

                //Header
                richTextBox1.AppendText("Fail Threshold Report for all courses " + Environment.NewLine);
                richTextBox1.AppendText("------------------------------------------------------------- " + Environment.NewLine);

                foreach(Course c in Globals.coursePool){

                    //Linq query for failed students
                    var failures =
                        from student in c.grades
                        where student.Value[0][0] == 'F'
                        select student;

                    double fCount = failures.Count()*100;
                    double totalCount = c.grades.Count();
                    double ratio = fCount / totalCount;
                    if(isLess && ratio <= percent || isMore && ratio >= percent)
                    richTextBox1.AppendText(c.departmentCode + "-" + c.sectionNumber + "  |  %" + string.Format("{0:0.00}", ratio) + " of students failed out of "+ c.grades.Count + Environment.NewLine);


                }


            }
            else
            {
                richTextBox1.Text = "Percent out of range 0-100";

            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
        //Pass students
        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            bool isLess = false;
            bool isMore = false;
            string grade = comboBox4.Text;

            if (radioButton6.Checked)
                isLess = true;
            if (radioButton5.Checked)
                isMore = true;
            if (!isLess && !isMore)
            {
                richTextBox1.AppendText("Choose threshold" + Environment.NewLine);
            }
            //Header
            richTextBox1.AppendText("Pass Threshold Report for all courses " + Environment.NewLine);
                richTextBox1.AppendText("------------------------------------------------------------- " + Environment.NewLine);

            // Same as for failed threshold but instead check thats not an F and >= or <= threshold given
                foreach (Course c in Globals.coursePool)
                {
                if (isMore)
                {
                    var passes =
                        from student in c.grades
                        where student.Value[0][0] <= grade[0] && student.Value[0][0] != 'F'
                        select student;
                    double pCount = passes.Count() * 100;
                    double totalCount = c.grades.Count();
                    double ratio = pCount / totalCount;
                    richTextBox1.AppendText(c.departmentCode + "-" + c.sectionNumber + "  |  %" + string.Format("{0:0.00}", ratio) + " of students passed of " + c.grades.Count + Environment.NewLine);

                }
                else
                {
                    var passes =
                        from student in c.grades
                        where student.Value[0][0] >= grade[0] && student.Value[0][0] != 'F'
                        select student;
                    double pCount = passes.Count() * 100;
                    double totalCount = c.grades.Count();
                    double ratio = pCount / totalCount;
                    richTextBox1.AppendText(c.departmentCode + "-" + c.sectionNumber + "  |  %" + string.Format("{0:0.00}", ratio) + " of students passed of " + c.grades.Count + Environment.NewLine);

                }


            }

        }
    }
}
