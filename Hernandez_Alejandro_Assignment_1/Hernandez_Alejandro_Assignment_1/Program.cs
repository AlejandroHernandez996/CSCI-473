using System;
using System.Collections.Generic;
using System.IO;

namespace Hernandez_Alejandro_Assignment_1
{
    public static class Globals
    {
        public static List<Student> studentPool = new List<Student>();
        public static List<Course> coursePool = new List<Course>();

    }

    public enum Year { Freshman,Sophmore,Junior,Senior,PostBacc}
    public class Course
    {

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
        public ushort maxNumStudents;

        public Course() { }
        public Course(string depCode, uint courseNum, string secNum, ushort hours, ushort max)
        {
            departmentCode = depCode;
            courseNumber = courseNum;
            sectionNumber = secNum;
            creditHours = hours;
            maxNumStudents = max;
            idList = new List<uint>(maxNumStudents);
        }
        public int CompareTo(Course course)
        {
            int depCompare = departmentCode.CompareTo(course.departmentCode);

            if(depCompare == 0)
            {
                return courseNumber.CompareTo(course.courseNumber);
            }
            else
            {
                return depCompare;
            }
        }
        public void PrintRoster()
        {
            Console.WriteLine("Course: " + ToString());
            Console.WriteLine("---------------------------------------------------------------");

            foreach(uint i in idList)
            {
                for(int x =0;x < Globals.studentPool.Count; x++)
                {
                    if(Globals.studentPool[x].id == i)
                    {
                        Console.WriteLine(Globals.studentPool[x].id);
                    }
                }

            }


        }
        public override string ToString()
        {
            return String.Format("Course: {0} {1}-{2} ({3}/{4})", departmentCode, courseNumber, sectionNumber, numStudentsEnrolled, maxNumStudents);

        }



    }
    public class Student{

        public readonly uint id;
        public string firstName;
        public string lastName;
        public string major;
        public Year year;

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

        public ushort creditHours;

        public Student() { }

        public Student(uint zID, string fName, string lName,string maj, Year academicYear, float grade)
        {

            major = maj;
            firstName = fName;
            lastName = lName;
            year = academicYear;
            gpa = grade;
            creditHours = 0;
            if (zID > 1000000)
            {
                id = zID;
            }

        }

        public int CompareTo(Student student)
        {
            if(id > student.id)
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

        public int Enroll(Course newCourse)
        {

            if(newCourse.numStudentsEnrolled >= newCourse.maxNumStudents)
            {
                return 5;
            }
            foreach(uint i in newCourse.idList)
            {
                if(i == id)
                {
                    return 10;
                }
            }
            if(creditHours + newCourse.creditHours > 18)
            {
                return 15;
            }

            newCourse.idList.Add(id);
            newCourse.numStudentsEnrolled++;
            return 0;

        }

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
            return String.Format("{0} -- {1}, {2}, [{3}], ({4}) |{5}|", id, lastName, firstName, year, major, gpa);
        }

    }
    class Program
    {
        public static Student CreateStudent(string line)
        {
            string[] studentArr = line.Split(',');
            Student student = new Student(Convert.ToUInt32(studentArr[0]), studentArr[2], studentArr[1], studentArr[3], (Year)Convert.ToInt16(studentArr[4]), float.Parse(studentArr[5]));
            return student;
        }
        public static Course CreateCourse(string line)
        {
            string[] courseArr = line.Split(',');
            Course course = new Course(courseArr[0], Convert.ToUInt16(courseArr[1]), courseArr[2], ushort.Parse(courseArr[3]), ushort.Parse(courseArr[4]));
            return course;
        }
        static void Main(string[] args)
        {

            string inLine;
            using (StreamReader inFile = new StreamReader(@"..\..\..\..\students.txt"))
            {
                
                while ((inLine = inFile.ReadLine()) != null) 
                {
                    Student temp = CreateStudent(inLine);
                    Globals.studentPool.Add(temp);
                }
            }
            using (StreamReader inFile = new StreamReader(@"..\..\..\..\courses.txt"))
            {
                while ((inLine = inFile.ReadLine()) != null)
                {
                    Course temp = CreateCourse(inLine);
                    Globals.coursePool.Add(temp);
                }
            }
            Globals.studentPool.Sort((s1, s2) => s1.CompareTo(s2));
            Globals.coursePool.Sort((c1, c2) => c1.CompareTo(c2));

            while (true)
            {

                Console.WriteLine(String.Format("We have {0} students and {1} classes", Globals.studentPool.Count, Globals.coursePool.Count));
                Console.WriteLine("Please choose from the following options");
                Console.WriteLine("1. Print Student List <All>");
                Console.WriteLine("2. Print Student List <Major>");
                Console.WriteLine("3. Print Student List <Academic Year>");
                Console.WriteLine("4. Print Course List");
                Console.WriteLine("5. Print Course Roster");
                Console.WriteLine("6. Enroll Student");
                Console.WriteLine("7. Drop Student");
                Console.WriteLine("8. Quit");

                char x = Char.ToLower(Console.ReadKey(true).KeyChar);
                Console.Clear();
                if(x == '1')
                {

                    foreach(Student s in Globals.studentPool)
                    {
                        Console.WriteLine(s.ToString());
                    }
                    
                }
                else if (x == '2')
                {
                    Console.WriteLine("Type Major and Press Enter");
                    string line = Console.ReadLine();
                    foreach (Student s in Globals.studentPool)
                    {
                        if(s.major.ToLower().Equals(line.ToLower()))
                            Console.WriteLine(s.ToString());
                    }

                }
                else if (x == '3')
                {
                    Console.WriteLine("Type Year and Press Enter ( Freshman, Sophmore, Junior, Senior, PostBacc )");
                    string line = Console.ReadLine();
                    int year = -1;
                    if (line.ToLower().Equals("freshman"))
                        year = 0;
                    else if (line.ToLower().Equals("sophmore"))
                        year = 1;
                    else if (line.ToLower().Equals("junior"))
                        year = 2;
                    else if (line.ToLower().Equals("senior"))
                        year = 3;
                    else if (line.ToLower().Equals("postbacc"))
                        year = 4;

                    foreach (Student s in Globals.studentPool)
                    {
                        if (year != -1 && s.year == (Year)year)
                            Console.WriteLine(s.ToString());
                    }

                }
                else if (x == '4')
                {

                    foreach (Course c in Globals.coursePool)
                    {
                        Console.WriteLine(c.ToString());
                    }

                }
                else if (x == '5')
                {

                    Console.WriteLine("Type Department Code, Course Number, Section Number(XXXX XXX XXXX)");
                    string[] line = Console.ReadLine().Split(' ');

                    Course foundCourse;
                    if (line.Length == 3) {
                        foreach (Course c in Globals.coursePool) {

                            if (line[0].Equals(c.departmentCode) && Convert.ToUInt32(line[1]) == c.courseNumber && line[2].Equals(c.sectionNumber))
                            {
                                foundCourse = c;
                                foreach(uint i in c.idList)
                                {
                                    foreach (Student s in Globals.studentPool)
                                    {
                                        if(i == s.id)
                                            Console.WriteLine(s.ToString());
                                    }
                                }
                            }
    

                        }
                    }


                }
                else if (x == '6')
                {
                    Console.WriteLine("Type ZID and Department Code, Course Number, Section Number(ZID XXXX XXX XXXX)");
                    string[] line = Console.ReadLine().Split(' ');

                    Student tempStudent = null;
                    Course tempCourse = null;
                    if (line.Length == 4)
                    {
                        foreach (Student foundStudent in Globals.studentPool)
                        {
                            if (line[0].Equals(foundStudent.id.ToString()))
                            {
                                tempStudent = foundStudent;
                                break;
                            }
                        }
                        foreach (Course foundCourse in Globals.coursePool)
                        {
                            if (line[1].Equals(foundCourse.departmentCode) && Convert.ToUInt32(line[2]) == foundCourse.courseNumber && line[3].Equals(foundCourse.sectionNumber))
                            {
                                tempCourse = foundCourse;
                                break;
                            }
                        }
                    }
                    if(tempStudent != null && tempCourse != null)
                    {
                        tempStudent.Enroll(tempCourse);
                        Console.WriteLine("Enrolled Succesful!");
                    }
                    else
                    {
                        Console.WriteLine("Could not enroll please try again");

                    }


                }
                else if (x == '7')
                {
                    Console.WriteLine("Type ZID and Department Code, Course Number, Section Number(ZID XXXX XXX XXXX)");
                    string[] line = Console.ReadLine().Split(' ');

                    Student tempStudent = null;
                    Course tempCourse = null;
                    if (line.Length == 4)
                    {
                        foreach (Student foundStudent in Globals.studentPool)
                        {
                            if (line[0].Equals(foundStudent.id.ToString()))
                            {
                                tempStudent = foundStudent;
                                break;
                            }
                        }
                        foreach (Course foundCourse in Globals.coursePool)
                        {
                            if (line[1].Equals(foundCourse.departmentCode) && Convert.ToUInt32(line[2]) == foundCourse.courseNumber && line[3].Equals(foundCourse.sectionNumber))
                            {
                                tempCourse = foundCourse;
                                break;
                            }
                        }
                    }
                    if (tempStudent != null && tempCourse != null)
                    {
                        tempStudent.Drop(tempCourse);
                        Console.WriteLine("Drop Succesful!");
                    }
                    else
                    {
                        Console.WriteLine("Could not drop please try again");

                    }


                }else if(x == 'q' || x == 'Q'||  x == '8' || x == 'h')
                {
                    break;
                }
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey(true);
                Console.Clear();
            }

        }
        

    }
}
