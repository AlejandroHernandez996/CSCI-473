using System;
using System.Collections.Generic;
using System.IO;

namespace Hernandez_Alejandro_Assignment_1
{
    public static class Globals
    {
        public static Dictionary<uint, Student> studentPool = new Dictionary<uint, Student>();
        public static Dictionary<string, Course> coursePool = new Dictionary<string, Course>();

    }

    public enum Year { Freshman,Sophmore,Junior,Senior,PostBacc}
    public class Course
    {

        public string departmentCode;
        public uint courseNumber;
        public string sectionNumber;
        public ushort creditHours;
        public List<uint> idList;
        public ushort numStudentsEnrolled;
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

                Console.WriteLine(Globals.studentPool[i].ToString());

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
        public float gpa;
        public ushort creditHours;

        public Student() { }

        public Student(uint zID, string fName, string lName,string maj, Year academicYear, float grade)
        {

            id = zID;
            major = maj;
            firstName = fName;
            lastName = lName;
            year = academicYear;
            gpa = grade;
            creditHours = 0;


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

            Student student = new Student();
            return student;
        }
        public static Course CreateCourse(string line)
        {

            Course student = new Course();
            return student;
        }
        static void Main(string[] args)
        {

            string inLine;
            using (StreamReader inFile = new StreamReader("..\\..\\students.txt"))
            {
                inLine = inFile.ReadLine(); 
                while (inLine != null) 
                {
                    inLine = inFile.ReadLine();
                    Student temp = CreateStudent(inLine);
                    Globals.studentPool[temp.id] = temp;
                }
            }
            using (StreamReader inFile = new StreamReader("..\\..\\courses.txt"))
            {
                inLine = inFile.ReadLine();
                while (inLine != null)
                {
                    inLine = inFile.ReadLine();
                    Course temp = CreateCourse(inLine);
                    Globals.coursePool[temp.id] = temp;
                }
            }

        }
        

    }
}
