using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Student s in Globals.studentPool)
            {
                listBox_students.Items.Add(s);
            }

            foreach (Course c in Globals.coursePool)
            {
                listBox_courses.Items.Add(c);
            }

        }

        private void button_enroll_student_Click(object sender, EventArgs e)
        {
            if (listBox_students.SelectedItems.Count > 0 && listBox_courses.SelectedItems.Count > 0)
            {
                bool previously_enrolled = false;

                richTextBox_messages.Clear();
                foreach (Student s in listBox_students.SelectedItems)
                {
                    foreach (Course c in listBox_courses.SelectedItems)
                    {
                        foreach (uint u in c.idList)
                        {
                            if (s.id == u)
                            {
                                previously_enrolled = true;
                            }
                        }

                        if (!previously_enrolled && s.Enroll(c) == 0)
                        {
                            richTextBox_messages.Text += "Enrolled " + s.firstName + " in " + c.departmentCode + c.courseNumber + "\n";
                        }
                        else
                        {
                            richTextBox_messages.Text += "Failed to enroll " + s.firstName + " in " + c.departmentCode + c.courseNumber + "\n";
                        }
                        previously_enrolled = false;
                    }
                }
                listBox_courses.Items.Clear();
                foreach (Course c in Globals.coursePool)
                {
                    listBox_courses.Items.Add(c);
                }
            }
        }

        private void button_drop_student_Click(object sender, EventArgs e)
        {
            if (listBox_students.SelectedItems.Count > 0 && listBox_courses.SelectedItems.Count > 0)
            {
                bool enrolled = false;

                richTextBox_messages.Clear();
                foreach (Student s in listBox_students.SelectedItems)
                {
                    foreach (Course c in listBox_courses.SelectedItems)
                    {
                        foreach (uint u in c.idList)
                        {
                            if (s.id == u)
                            {
                                enrolled = true;
                            }
                        }

                        if (enrolled && s.Drop(c) == 0)
                        {
                            richTextBox_messages.Text += "Dropped " + s.firstName + " from " + c.departmentCode + c.courseNumber + "\n";
                        }
                        else
                        {
                            richTextBox_messages.Text += "Failed to drop " + s.firstName + " from " + c.departmentCode + c.courseNumber + "\n";
                        }
                        enrolled = false;
                    }
                }
                listBox_courses.Items.Clear();
                foreach (Course c in Globals.coursePool)
                {
                    listBox_courses.Items.Add(c);
                }
            }
        }

        private void button_print_course_roster_Click(object sender, EventArgs e)
        {
            richTextBox_messages.Clear();
            foreach (Course c in listBox_courses.SelectedItems)
            {
                foreach (string s in c.PrintRoster())
                {
                    richTextBox_messages.Text += s + "\n";
                }
            }
        }

        private void button_apply_search_criteria_Click(object sender, EventArgs e)
        {
            if (textBox_search_student.Text != "")
            {
                List<Student> foundStudent = new List<Student>();
                foreach (Student s in Globals.studentPool)
                {
                    if (s.id.ToString().Contains(textBox_search_student.Text))
                    {
                        foundStudent.Add(s);
                    }
                }
                if (foundStudent.Count > 0)
                {
                    listBox_students.Items.Clear();
                    richTextBox_messages.Text = "";
                    foreach (Student s in foundStudent)
                    {
                        listBox_students.Items.Add(s);
                    }
                }
                else if (listBox_students.Items.Count != Globals.studentPool.Count)
                {
                    listBox_students.Items.Clear();
                    foreach (Student s in Globals.studentPool)
                    {
                        listBox_students.Items.Add(s);
                    }
                }
                else
                {
                    richTextBox_messages.Text = "Could not find " + textBox_search_student.Text + " in database.";
                }
            }

            if (textBox_filter_courses.Text != null)
            {
                List<Course> foundCourse = new List<Course>();
                foreach (Course c in Globals.coursePool)
                {
                    if (c.ToString().Contains(textBox_filter_courses.Text))
                    {
                        foundCourse.Add(c);
                    }
                }
                if (foundCourse.Count > 0)
                {
                    listBox_courses.Items.Clear();
                    richTextBox_messages.Text = "";
                    foreach (Course c in foundCourse)
                    {
                        listBox_courses.Items.Add(c);
                    }
                }
                else if (listBox_courses.Items.Count != Globals.coursePool.Count)
                {
                    listBox_courses.Items.Clear();
                    foreach (Course c in Globals.coursePool)
                    {
                        listBox_students.Items.Add(c);
                    }
                }
                else
                {
                    richTextBox_messages.Text = "Could not find " + textBox_filter_courses.Text + " in database.";
                }
            }
        }

        private void search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                button_apply_search_criteria_Click(sender, e);
            }
            
        }

        private void button_add_student_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox_name.Text) &&
                !string.IsNullOrWhiteSpace(textBox_zid.Text) && 
                !string.IsNullOrWhiteSpace(comboBox_major.Text) &&
                !string.IsNullOrWhiteSpace(comboBox_year.Text))
            {
                richTextBox_messages.Text = "Entered things in all fields";
            }
        }
    }
}