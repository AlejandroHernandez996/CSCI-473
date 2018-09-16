using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2
{
    //Static class that contains students and courses
    public static class Globals
    {
        public static List<Student> studentPool = new List<Student>();
        public static List<Course> coursePool = new List<Course>();

    }

    //College years enums
    public enum Year { Freshman, Sophmore, Junior, Senior, PostBacc }

    //Course class 
    //Stores students enrolled in class and other course attributes
    public class Course
    {
        //Variables
        private string DepartmentCode;
        public string departmentCode
        {
            get
            {
                return DepartmentCode;
            }
            set
            {
                if (value.Length <= 4)
                {
                    DepartmentCode = value.ToUpper();
                }
            }
        }
        private uint CourseNumber;
        public uint courseNumber
        {
            get
            {
                return CourseNumber;
            }
            set
            {
                if (value >= 100 && value <= 499)
                {
                    CourseNumber = value;
                }
            }
        }
        private string SectionNumber;
        public string sectionNumber
        {
            get
            {
                return SectionNumber;
            }
            set
            {
                if (value.Length == 4)
                {
                    SectionNumber = value;
                }
            }
        }
        private ushort CreditHours;
        public ushort creditHours
        {
            get
            {
                return CreditHours;
            }
            set
            {
                if (value >= 0 && value <= 6)
                {
                    CreditHours = value;
                }
            }
        }

        public List<uint> idList;

        private ushort NumStudentsEnrolled;
        public ushort numStudentsEnrolled
        {
            get
            {
                return NumStudentsEnrolled;
            }
            set
            {
                if (value >= 0)
                {
                    NumStudentsEnrolled = value;
                }
            }
        }
        public ushort maxNumStudents
        {
            get
            {
                return MaxNumStudents;
            }
            set
            {
                MaxNumStudents = value;
            }
        }
        private ushort MaxNumStudents;

        public Course() { }
        //Initialize course variables
        public Course(string depCode, uint courseNum, string secNum, ushort hours, ushort max)
        {
            departmentCode = depCode;
            courseNumber = courseNum;
            sectionNumber = secNum;
            creditHours = hours;
            maxNumStudents = max;
            idList = new List<uint>(maxNumStudents);
        }
        //Used to compare against another course
        //First compares the department code then if they are equal
        //compare the course number
        public int CompareTo(Course course)
        {
            int depCompare = departmentCode.CompareTo(course.departmentCode);

            if (depCompare == 0)
            {
                return courseNumber.CompareTo(course.courseNumber);
            }
            else
            {
                return depCompare;
            }
        }
        //Print function to display students enrolled in course
        public List<string> PrintRoster()
        {
            List<string> lines = new List<string>();
            string temp_line;

            temp_line = "Course: " + ToString() + "\n---------------------------------------------------------------\n";
            lines.Add(temp_line);

            foreach (uint i in idList)
            {
                for (int x = 0; x < Globals.studentPool.Count; x++)
                {
                    if (Globals.studentPool[x].id == i)
                    {
                        temp_line = Globals.studentPool[x].ToRoster();
                        lines.Add(temp_line);
                    }
                }
            }
            return lines;
        }
        public override string ToString()
        {
            return String.Format("{0} {1}-{2} ({3}/{4})", departmentCode, courseNumber, sectionNumber, numStudentsEnrolled, maxNumStudents);
            //return String.Format("Course: {0} {1}-{2} ({3}/{4})", departmentCode, courseNumber, sectionNumber, numStudentsEnrolled, maxNumStudents);

        }
    }
    //Student class holds variables of a student
    //Allows to enroll/drop student into a given course
    public class Student
    {
        public uint id
        {
            get
            {
                return Id;
            }
        }
        private readonly uint Id;

        public string firstName
        {
            get
            {
                return FirstName;
            }
            set
            {
                FirstName = value;
            }
        }
        private string FirstName;

        public string lastName
        {
            get
            {
                return LastName;
            }
            set
            {
                LastName = value;

            }
        }
        private string LastName;

        public string major
        {
            get
            {
                return Major;
            }
            set
            {
                Major = value;
            }
        }
        private string Major;

        public Year year
        {
            get
            {
                return Year;
            }
            set
            {
                Year = value;
            }
        }
        private Year Year;

        private float GPA;
        public float gpa
        {
            get
            {
                return GPA;
            }
            set
            {
                if (value >= 0 && value <= 4.000)
                {
                    //Console.WriteLine("Valid GPA entered");
                    GPA = value;
                }
            }
        }

        public ushort creditHours
        {
            get
            {
                return CreditHours;
            }
            set
            {
                if (value >= 0 && value <= 18)
                {
                    CreditHours = value;
                }
            }
        }
        private ushort CreditHours;

        public Student() { }
        //Initialize Student variables
        public Student(uint zID, string fName, string lName, string maj, Year academicYear, float grade)
        {

            major = maj;
            firstName = fName;
            lastName = lName;
            year = academicYear;
            gpa = grade;
            creditHours = 0;
            if (zID > 1000000)
            {
                Id = zID;
            }

        }
        //Compares students by ZID
        public int CompareTo(Student student)
        {
            if (id > student.id)
            {
                return 1;
            }
            else if (id < student.id)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        //Checks if student is able to enroll into course
        //If return different error codes
        public int Enroll(Course newCourse)
        {

            if (newCourse.numStudentsEnrolled >= newCourse.maxNumStudents)
            {
                return 5;
            }
            foreach (uint i in newCourse.idList)
            {
                if (i == id)
                {
                    return 10;
                }
            }
            if (creditHours + newCourse.creditHours > 18)
            {
                return 15;
            }

            newCourse.idList.Add(id);
            newCourse.numStudentsEnrolled++;
            return 0;

        }
        //Drops student from a course he is in already
        public int Drop(Course newCourse)
        {
            if (newCourse.idList.Remove(id))
            {
                return 0;
            }
            else
            {
                return 20;
            }
        }

        public override string ToString()
        {
            //return String.Format("{0} -- {1}, {2}, [{3}], ({4}) |{5}|", id, lastName, firstName, year, major, gpa);
            return String.Format("{0} -- {1}, {2}", id, lastName, firstName);
        }
        public string ToRoster()
        {
            return String.Format("{0} {1} {2} {3}", id, lastName, firstName, major);

        }

    }



    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        //Returns a student when given an input line from a student file
        public static Student CreateStudent(string line)
        {
            string[] studentArr = line.Split(',');
            Student student = new Student(Convert.ToUInt32(studentArr[0]), studentArr[2], studentArr[1], studentArr[3], (Year)Convert.ToInt16(studentArr[4]), float.Parse(studentArr[5]));
            return student;
        }
        //Returns a course when given an input line from a course file
        public static Course CreateCourse(string line)
        {
            string[] courseArr = line.Split(',');
            Course course = new Course(courseArr[0], Convert.ToUInt16(courseArr[1]), courseArr[2], ushort.Parse(courseArr[3]), ushort.Parse(courseArr[4]));
            return course;
        }

        static void Main()
        {
            //Loops through file and creates a new student for every line given then adds it to the studentPool
            string inLine;
            using (StreamReader inFile = new StreamReader(@"..\..\students.txt"))
            {

                while ((inLine = inFile.ReadLine()) != null)
                {
                    Student temp = CreateStudent(inLine);
                    Globals.studentPool.Add(temp);
                }
            }
            //Loops through course file and creates a new course for every line then adds it to the coursePool
            using (StreamReader inFile = new StreamReader(@"..\..\courses.txt"))
            {
                while ((inLine = inFile.ReadLine()) != null)
                {
                    Course temp = CreateCourse(inLine);
                    Globals.coursePool.Add(temp);
                }
            }
            //Sort both pools
            Globals.studentPool.Sort((s1, s2) => s1.CompareTo(s2));
            Globals.coursePool.Sort((c1, c2) => c1.CompareTo(c2));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
