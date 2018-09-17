namespace Assignment_2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_print_course_roster = new System.Windows.Forms.Button();
            this.button_enroll_student = new System.Windows.Forms.Button();
            this.button_drop_student = new System.Windows.Forms.Button();
            this.button_apply_search_criteria = new System.Windows.Forms.Button();
            this.button_add_student = new System.Windows.Forms.Button();
            this.button_add_course = new System.Windows.Forms.Button();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_zid = new System.Windows.Forms.TextBox();
            this.textBox_course_number = new System.Windows.Forms.TextBox();
            this.textBox_search_student = new System.Windows.Forms.TextBox();
            this.textBox_filter_courses = new System.Windows.Forms.TextBox();
            this.textBox_department_code = new System.Windows.Forms.TextBox();
            this.comboBox_year = new System.Windows.Forms.ComboBox();
            this.comboBox_major = new System.Windows.Forms.ComboBox();
            this.comboBox_section_number = new System.Windows.Forms.ComboBox();
            this.numericUpDown_capacity = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.richTextBox_messages = new System.Windows.Forms.RichTextBox();
            this.listBox_students = new System.Windows.Forms.ListBox();
            this.listBox_courses = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_capacity)).BeginInit();
            this.SuspendLayout();
            // 
            // button_print_course_roster
            // 
            this.button_print_course_roster.Location = new System.Drawing.Point(13, 13);
            this.button_print_course_roster.Name = "button_print_course_roster";
            this.button_print_course_roster.Size = new System.Drawing.Size(150, 28);
            this.button_print_course_roster.TabIndex = 0;
            this.button_print_course_roster.Text = "Print Course Roster";
            this.button_print_course_roster.UseVisualStyleBackColor = true;
            this.button_print_course_roster.Click += new System.EventHandler(this.button_print_course_roster_Click);
            // 
            // button_enroll_student
            // 
            this.button_enroll_student.Location = new System.Drawing.Point(13, 47);
            this.button_enroll_student.Name = "button_enroll_student";
            this.button_enroll_student.Size = new System.Drawing.Size(150, 28);
            this.button_enroll_student.TabIndex = 1;
            this.button_enroll_student.Text = "Enroll Student";
            this.button_enroll_student.UseVisualStyleBackColor = true;
            this.button_enroll_student.Click += new System.EventHandler(this.button_enroll_student_Click);
            // 
            // button_drop_student
            // 
            this.button_drop_student.Location = new System.Drawing.Point(13, 81);
            this.button_drop_student.Name = "button_drop_student";
            this.button_drop_student.Size = new System.Drawing.Size(150, 28);
            this.button_drop_student.TabIndex = 2;
            this.button_drop_student.Text = "Drop Student";
            this.button_drop_student.UseVisualStyleBackColor = true;
            this.button_drop_student.Click += new System.EventHandler(this.button_drop_student_Click);
            // 
            // button_apply_search_criteria
            // 
            this.button_apply_search_criteria.Location = new System.Drawing.Point(13, 115);
            this.button_apply_search_criteria.Name = "button_apply_search_criteria";
            this.button_apply_search_criteria.Size = new System.Drawing.Size(150, 28);
            this.button_apply_search_criteria.TabIndex = 3;
            this.button_apply_search_criteria.Text = "Apply Search Criteria";
            this.button_apply_search_criteria.UseVisualStyleBackColor = true;
            this.button_apply_search_criteria.Click += new System.EventHandler(this.button_apply_search_criteria_Click);
            // 
            // button_add_student
            // 
            this.button_add_student.Location = new System.Drawing.Point(188, 282);
            this.button_add_student.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.button_add_student.Name = "button_add_student";
            this.button_add_student.Size = new System.Drawing.Size(151, 28);
            this.button_add_student.TabIndex = 4;
            this.button_add_student.Text = "Add Student";
            this.button_add_student.UseVisualStyleBackColor = true;
            // 
            // button_add_course
            // 
            this.button_add_course.Location = new System.Drawing.Point(188, 431);
            this.button_add_course.Name = "button_add_course";
            this.button_add_course.Size = new System.Drawing.Size(151, 28);
            this.button_add_course.TabIndex = 5;
            this.button_add_course.Text = "Add Course";
            this.button_add_course.UseVisualStyleBackColor = true;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(13, 210);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(160, 20);
            this.textBox_name.TabIndex = 6;
            // 
            // textBox_zid
            // 
            this.textBox_zid.Location = new System.Drawing.Point(179, 210);
            this.textBox_zid.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.textBox_zid.Name = "textBox_zid";
            this.textBox_zid.Size = new System.Drawing.Size(160, 20);
            this.textBox_zid.TabIndex = 7;
            // 
            // textBox_course_number
            // 
            this.textBox_course_number.Location = new System.Drawing.Point(179, 360);
            this.textBox_course_number.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.textBox_course_number.Name = "textBox_course_number";
            this.textBox_course_number.Size = new System.Drawing.Size(160, 20);
            this.textBox_course_number.TabIndex = 8;
            // 
            // textBox_search_student
            // 
            this.textBox_search_student.Location = new System.Drawing.Point(179, 30);
            this.textBox_search_student.Name = "textBox_search_student";
            this.textBox_search_student.Size = new System.Drawing.Size(160, 20);
            this.textBox_search_student.TabIndex = 9;
            // 
            // textBox_filter_courses
            // 
            this.textBox_filter_courses.Location = new System.Drawing.Point(179, 79);
            this.textBox_filter_courses.Name = "textBox_filter_courses";
            this.textBox_filter_courses.Size = new System.Drawing.Size(160, 20);
            this.textBox_filter_courses.TabIndex = 10;
            // 
            // textBox_department_code
            // 
            this.textBox_department_code.Location = new System.Drawing.Point(14, 360);
            this.textBox_department_code.Name = "textBox_department_code";
            this.textBox_department_code.Size = new System.Drawing.Size(160, 20);
            this.textBox_department_code.TabIndex = 11;
            // 
            // comboBox_year
            // 
            this.comboBox_year.FormattingEnabled = true;
            this.comboBox_year.Location = new System.Drawing.Point(179, 252);
            this.comboBox_year.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.comboBox_year.Name = "comboBox_year";
            this.comboBox_year.Size = new System.Drawing.Size(160, 21);
            this.comboBox_year.TabIndex = 12;
            // 
            // comboBox_major
            // 
            this.comboBox_major.FormattingEnabled = true;
            this.comboBox_major.Location = new System.Drawing.Point(14, 252);
            this.comboBox_major.Name = "comboBox_major";
            this.comboBox_major.Size = new System.Drawing.Size(160, 21);
            this.comboBox_major.TabIndex = 13;
            // 
            // comboBox_section_number
            // 
            this.comboBox_section_number.FormattingEnabled = true;
            this.comboBox_section_number.Location = new System.Drawing.Point(14, 403);
            this.comboBox_section_number.Name = "comboBox_section_number";
            this.comboBox_section_number.Size = new System.Drawing.Size(160, 21);
            this.comboBox_section_number.TabIndex = 14;
            // 
            // numericUpDown_capacity
            // 
            this.numericUpDown_capacity.Location = new System.Drawing.Point(179, 402);
            this.numericUpDown_capacity.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.numericUpDown_capacity.Name = "numericUpDown_capacity";
            this.numericUpDown_capacity.Size = new System.Drawing.Size(160, 20);
            this.numericUpDown_capacity.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Search Student ( by Z-ID )";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Filter Courses ( by Dept )";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 194);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Last Name, First Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(176, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Z-ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Major";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(176, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Academic Year";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 344);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Department Code";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(176, 344);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Course Number";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 387);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Section Number";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(176, 386);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Capacity";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(10, 172);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 16);
            this.label11.TabIndex = 26;
            this.label11.Text = "New Student";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(11, 322);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 16);
            this.label12.TabIndex = 27;
            this.label12.Text = "New Course";
            // 
            // richTextBox_messages
            // 
            this.richTextBox_messages.Location = new System.Drawing.Point(14, 480);
            this.richTextBox_messages.Name = "richTextBox_messages";
            this.richTextBox_messages.Size = new System.Drawing.Size(970, 142);
            this.richTextBox_messages.TabIndex = 28;
            this.richTextBox_messages.Text = "";
            // 
            // listBox_students
            // 
            this.listBox_students.FormattingEnabled = true;
            this.listBox_students.Location = new System.Drawing.Point(382, 13);
            this.listBox_students.Name = "listBox_students";
            this.listBox_students.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_students.Size = new System.Drawing.Size(293, 446);
            this.listBox_students.TabIndex = 29;
            // 
            // listBox_courses
            // 
            this.listBox_courses.FormattingEnabled = true;
            this.listBox_courses.Location = new System.Drawing.Point(691, 13);
            this.listBox_courses.Name = "listBox_courses";
            this.listBox_courses.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_courses.Size = new System.Drawing.Size(293, 446);
            this.listBox_courses.TabIndex = 30;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 634);
            this.Controls.Add(this.listBox_courses);
            this.Controls.Add(this.listBox_students);
            this.Controls.Add(this.richTextBox_messages);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_capacity);
            this.Controls.Add(this.comboBox_section_number);
            this.Controls.Add(this.comboBox_major);
            this.Controls.Add(this.comboBox_year);
            this.Controls.Add(this.textBox_department_code);
            this.Controls.Add(this.textBox_filter_courses);
            this.Controls.Add(this.textBox_search_student);
            this.Controls.Add(this.textBox_course_number);
            this.Controls.Add(this.textBox_zid);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.button_add_course);
            this.Controls.Add(this.button_add_student);
            this.Controls.Add(this.button_apply_search_criteria);
            this.Controls.Add(this.button_drop_student);
            this.Controls.Add(this.button_enroll_student);
            this.Controls.Add(this.button_print_course_roster);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_capacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_print_course_roster;
        private System.Windows.Forms.Button button_enroll_student;
        private System.Windows.Forms.Button button_drop_student;
        private System.Windows.Forms.Button button_apply_search_criteria;
        private System.Windows.Forms.Button button_add_student;
        private System.Windows.Forms.Button button_add_course;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_zid;
        private System.Windows.Forms.TextBox textBox_course_number;
        private System.Windows.Forms.TextBox textBox_search_student;
        private System.Windows.Forms.TextBox textBox_filter_courses;
        private System.Windows.Forms.TextBox textBox_department_code;
        private System.Windows.Forms.ComboBox comboBox_year;
        private System.Windows.Forms.ComboBox comboBox_major;
        private System.Windows.Forms.ComboBox comboBox_section_number;
        private System.Windows.Forms.NumericUpDown numericUpDown_capacity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox richTextBox_messages;
        private System.Windows.Forms.ListBox listBox_students;
        private System.Windows.Forms.ListBox listBox_courses;
    }
}

