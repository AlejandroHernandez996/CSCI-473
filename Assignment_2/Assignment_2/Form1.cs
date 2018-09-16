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
            foreach(Student s in Globals.studentPool)
            {
                listBox_students.Items.Add(s);
            }

            foreach(Course c in Globals.coursePool)
            {
                listBox_courses.Items.Add(c);
            }

        }

        private void button_enroll_student_Click(object sender, EventArgs e)
        {
            if(listBox_students.SelectedItems.Count > 0 && listBox_courses.SelectedItems.Count > 0)
            {
                bool previously_enrolled = false;

                richTextBox_messages.Clear();
                foreach(Student s in listBox_students.SelectedItems)
                {
                    foreach(Course c in listBox_courses.SelectedItems)
                    {
                        foreach(uint u in c.idList)
                        {
                            if(s.id == u)
                            {
                                previously_enrolled = true;
                            }
                        }

                        if (!previously_enrolled)
                        {
                            if (s.Enroll(c) == 0)
                            {
                                richTextBox_messages.Text += "Enrolled " + s.firstName + " in " + c.departmentCode + c.courseNumber + "\n";
                            }
                        }
                        previously_enrolled = false;
                    }
                }
                listBox_courses.Items.Clear();
                foreach(Course c in Globals.coursePool)
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

                        if (enrolled)
                        {
                            if (s.Drop(c) == 0)
                            {
                                richTextBox_messages.Text += "Dropped " + s.firstName + " from " + c.departmentCode + c.courseNumber + "\n";
                                c.numStudentsEnrolled--;
                            }
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
            foreach(Course c in listBox_courses.SelectedItems)
            {
                foreach(string s in c.PrintRoster())
                {
                    richTextBox_messages.Text += s + "\n";
                }
            }
        }
    }
}
