using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Created by Benjamin Schulz and Alejandro Hernandez
 * CSCI 473 - Assignment 2
 * 
*/

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
            //Populate student listbox
            foreach (Student s in Globals.studentPool)
            {
                listBox_students.Items.Add(s);
            }

            //Populate course listbox
            foreach (Course c in Globals.coursePool)
            {
                listBox_courses.Items.Add(c);
            }

            //This bit just populates the 'year' combo box with the list of possible years
            for(int i = 0; i < Enum.GetNames(typeof(Year)).Length; i++)
            {
                comboBox_year.Items.Add(Enum.GetNames(typeof(Year))[i]);
            }

        }

        // Enrolls one or more student into one or more classes if possible (class is full, too many credit hours taken, etc)
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

        // Drops the student if possible
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

        // Prints one or more courses current rosters
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

        // Apply search critera entered by user upon button click or enter key(later in code)
        private void button_apply_search_criteria_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox_search_student.Text))
            {
                List<Student> foundStudent = new List<Student>();
                foreach (Student s in Globals.studentPool)
                {
                    if (s.id.ToString().StartsWith(textBox_search_student.Text))
                    {
                        foundStudent.Add(s);
                    }
                }
                if (foundStudent.Count > 0)
                {
                    listBox_students.Items.Clear();
                    richTextBox_messages.Clear();
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
            else
            {
                listBox_students.Items.Clear();
                foreach (Student s in Globals.studentPool)
                {
                    listBox_students.Items.Add(s);
                }
            }

            if (!string.IsNullOrWhiteSpace(textBox_filter_courses.Text))
            {
                List<Course> foundCourse = new List<Course>();
                foreach (Course c in Globals.coursePool)
                {
                    textBox_filter_courses.Text = textBox_filter_courses.Text.ToUpper();
                    if (c.departmentCode.Equals(textBox_filter_courses.Text))
                    {
                        foundCourse.Add(c);
                    }
                }
                if (foundCourse.Count > 0)
                {
                    listBox_courses.Items.Clear();
                    richTextBox_messages.Clear();
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
                        listBox_courses.Items.Add(c);
                    }
                }
                else
                {
                    richTextBox_messages.Text = "Could not find " + textBox_filter_courses.Text + " in database.";
                }
            }
            else
            {
                listBox_courses.Items.Clear();
                foreach(Course c in Globals.coursePool)
                {
                    listBox_courses.Items.Add(c);
                }
            }
        }

        // Applying search criteria with the enter key as another option to the user
        private void search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                button_apply_search_criteria_Click(sender, e);
            }
            
        }

        //ALOT of checks to make sure the input is valid before it ultimately adds a new student to the database
        private void button_add_student_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox_name.Text) &&
                !string.IsNullOrWhiteSpace(textBox_zid.Text) && 
                !string.IsNullOrWhiteSpace(comboBox_major.Text) &&
                !string.IsNullOrWhiteSpace(comboBox_year.Text))
            {
                //Student newStudent;
                string newStudentFirst = "Default";
                string newStudentLast = "Doe";
                string newStudentMajor = "Default";
                Year newStudentYear = Year.Freshman;
                uint newStudentId = 000;
                bool canMakeStudent = true;                

                string tempString;
                uint tempUint;

                //Get zID ready for new student
                if (textBox_zid.Text.Length == 8 && textBox_zid.Text.ToUpper().StartsWith("Z"))
                {
                    tempString = textBox_zid.Text.Substring(1);
                }
                else
                {
                    tempString = textBox_zid.Text;
                }

                if(tempString.Length == 7 && uint.TryParse(tempString, out tempUint))
                {
                    foreach(Student s in Globals.studentPool)
                    {
                        if(s.id == tempUint)
                        {
                            canMakeStudent = false;
                            richTextBox_messages.Text = "Failed to add new student - Student ZID already in database";
                        }
                    }
                    newStudentId = tempUint;
                }
                else
                {
                    richTextBox_messages.Text = "Failed to add new student - Invalid ZID entry";
                    canMakeStudent = false;
                }



                //Get name ready for new student to include proper formatting
                if (textBox_name.Text.Contains(","))
                {
                    string[] nameComponents = textBox_name.Text.Split(',');
                    if(nameComponents.Count() == 2)
                    {
                        newStudentLast = nameComponents[0].First().ToString().ToUpper() + nameComponents[0].Substring(1);
                        if(nameComponents[1].StartsWith(" ")){
                            nameComponents[1] = nameComponents[1].Substring(1);
                        }
                        newStudentFirst = nameComponents[1].First().ToString().ToUpper() + nameComponents[1].Substring(1);
                        //richTextBox_messages.Text = string.Format("last - '{0}'\tfirst - '{1}'", newStudentLast, newStudentFirst);
                    }
                    else
                    {
                        richTextBox_messages.Text = "Failed to add new student - Invalid name entry";
                        canMakeStudent = false;
                    }
                }
                else
                {
                    richTextBox_messages.Text = "Failed to add new student - Invalid name entry";
                    canMakeStudent = false;
                }
               
                //checking the combo boxes
                if(comboBox_major.SelectedIndex != -1)
                {
                    newStudentMajor = comboBox_major.Text;
                }
                else
                {
                    canMakeStudent = false;
                    richTextBox_messages.Text = "Failed to add new student - Invalid Major entry";
                }
                if(comboBox_year.SelectedIndex != -1)
                {
                    newStudentYear = (Year)comboBox_year.SelectedIndex;
                }
                else
                {
                    canMakeStudent = false;
                    richTextBox_messages.Text = "Failed to add new student - Invalid Year entry";
                }

                //If everything entered for the new student was valid and didn't set the flag, go ahead and make a new student object and add it to the database
                if (canMakeStudent == true)
                {
                    Student newStudent = new Student(newStudentId, newStudentFirst, newStudentLast, newStudentMajor, newStudentYear, 0.000f);

                    Globals.studentPool.Add(newStudent);
                    Globals.studentPool.Sort((s1, s2) => s1.CompareTo(s2));
                    listBox_students.Items.Clear();
                    foreach(Student s in Globals.studentPool)
                    {
                        listBox_students.Items.Add(s);
                    }

                    richTextBox_messages.Text = "Added new student to the database";
                }

            }
            else
            {
                richTextBox_messages.Text = "One or more of the required fields to create a student has been left empty";
            }
        }

        private void button_add_course_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox_course_number.Text) &&
                !string.IsNullOrWhiteSpace(comboBox_department_code.Text) &&
                !string.IsNullOrWhiteSpace(textBox_section_number.Text) &&
                !string.IsNullOrWhiteSpace(numericUpDown_capacity.Text))
            {
                Course newCourse;

                //The Department Code must be exactly four alphanumeric characters.
                //The Course number must be exactly three digits. There's a trick to doing this using regular expressions.
                //The Section number must be exactly four alphanumeric characters.
                //The capacity must be > 0.
                string newDepCode = comboBox_department_code.Text;
                string newCourseNum = textBox_course_number.Text;
                string newSecNum = textBox_section_number.Text;
                ushort newMax = ushort.Parse(numericUpDown_capacity.Text);
                ushort newCreditHours= ushort.Parse(textBox_credit_hours.Text);

                //Check if valid fields
                if (!Regex.IsMatch(newDepCode, @"^[a-zA-Z0-9]{1,4}$"))
                {
                    richTextBox_messages.Text = "Failed to add new course - Invalid Dep Code";
                    return;
                }
                if (!Regex.IsMatch(newCourseNum, @"^[0-9]{3}$"))
                {
                    richTextBox_messages.Text = "Failed to add new course - Invalid Course Number";
                    return;
                }
                if (!Regex.IsMatch(newSecNum, @"^[a-zA-Z0-9]{4}$"))
                {
                    richTextBox_messages.Text = "Failed to add new course - Invalid Sec Number";
                    return;
                }
                if (!Regex.IsMatch(textBox_credit_hours.Text, @"^[1-4]{1}")){
                    richTextBox_messages.Text = "Failed to add new course - Invalid credit hours";
                    return;
                }
                if (newMax < 1)
                {
                    richTextBox_messages.Text = "Failed to add new course - Invalid max capacity";
                    return;
                }

                //Create new course
                newCourse = new Course(newDepCode, uint.Parse(newCourseNum), newSecNum, newCreditHours, newMax);


                // Check if course is already in there
                foreach (Course c in Globals.coursePool)
                {

                    if (newCourse.CompareTo(c) == 0 && newCourse.sectionNumber.Equals(c.sectionNumber))
                    {
                        richTextBox_messages.Text = "Failed to add new course - Already Exists";
                        return;
                    }

                }

                richTextBox_messages.Text = "Added new course to the database";

                // Add new course and refresh list
                Globals.coursePool.Add(newCourse);
                Globals.coursePool.Sort((c1, c2) => c1.CompareTo(c2));
                listBox_courses.Items.Clear();
                foreach (Course c in Globals.coursePool)
                {
                    listBox_courses.Items.Add(c);
                }


            }
        }

        // This part displays the enrolled classes of a student when the user clicks on them in the listbox. Can display multiple students classes if desired. 
        private void listBox_students_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox_messages.Clear();
            foreach (Student s in listBox_students.SelectedItems)
            {
                richTextBox_messages.Text += string.Format("{0}--\t{1}, {2}\t[{3}] ({4}) |{5}|", s.id, s.lastName, s.firstName, s.year, s.major, s.gpa);
                richTextBox_messages.AppendText("\n-----------------------------------------------------------------------------------------------------------------------------------\n");
                foreach (Course c in s.currentlyEnrolled)
                {
                    richTextBox_messages.Text += string.Format("{0} {1}-{2} ({3}/{4})\n", c.departmentCode, c.courseNumber, c.sectionNumber, c.numStudentsEnrolled, c.maxNumStudents);
                }
                richTextBox_messages.AppendText("\n\n");
            }
        }


    }
}