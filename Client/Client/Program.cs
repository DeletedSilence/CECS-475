using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static IBuisnessLayer businessLayer = new BusinessLayer.BusinessLayer();
        static void Main(string[] args)
        {
            bool check = true;
            int input=0;
            while (check)
            {
                Console.WriteLine("[\t\tTeacher\t\t]");
                Console.WriteLine("(1) Add Teacher");
                Console.WriteLine("(2) Update Teacher");
                Console.WriteLine("(3) Remove Teacher");
                Console.WriteLine("(4) Display courses taught by Teacher");
                Console.WriteLine("(5) Display all Teachers");
                Console.WriteLine("(6) Get All Standards");
                Console.WriteLine("[\t\tCourse\t\t\t]");
                Console.WriteLine("(7) Add Course");
                Console.WriteLine("(8) Update Course");
                Console.WriteLine("(9) Remove Course");
                Console.WriteLine("(10) Get All Courses");
                //Console.WriteLine("[\t\tOther\t\t\t]");
                Console.WriteLine("(11) Exit");

                Console.Write("\nMenu Choice: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    menuChoice(input, ref check);
                }
                catch(NullReferenceException n)
                {
                    Console.WriteLine("The database is empty");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Invalid input! Please try again");
                }
            }
            
        }

        public static void menuChoice(int input, ref bool check)
        {
            switch (input)
            {
                case 1:
                    Console.Clear();
                    AddTeacher();
                    break;
                case 2:
                    Console.Clear();
                    UpdateTeacherMenu();
                    break;
                case 3:
                    Console.Clear();
                    RemoveTeacherMenu();
                    break;
                case 4:
                    Console.Clear();
                    GetCoursesByTeacher();
                    break;
                case 5:
                    Console.Clear();
                    DisplayAllTeachers();
                    break;
                case 6:
                    Console.Clear();
                    GetAllStandard();
                    break;
                case 7:
                    Console.Clear();
                    AddCourse();
                    break;
                case 8:
                    Console.Clear();
                    UpdateCourseMenu();
                    break;
                case 9:
                    Console.Clear();
                    RemoveCourseMenu();
                    break;
                case 10:
                    Console.Clear();
                    GetAllCourses();
                    break;
                case 11:
                    Console.Clear();
                    Console.WriteLine("You have exited the program");
                    check = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please try again:");
                    break;
            }
        }
        #region Teacher
        public static void AddTeacher()
        {
            Console.WriteLine("[\t\tAdd Teacher\t\t]");
            Console.WriteLine("Enter Teacher's name: ");
            string standardName = Console.ReadLine();
            Console.WriteLine("Enter Teacher's description: ");
            string standardDescription = Console.ReadLine();
            Standard newStandard = new Standard();
            Teacher newTeacher = new Teacher();
            newStandard.StandardName = standardName;
            newStandard.Description = standardDescription;
            businessLayer.AddStandard(newStandard);
            newTeacher.TeacherName = standardName;
            newTeacher.StandardId = newStandard.StandardId;
            businessLayer.AddTeacher(newTeacher);
            Console.WriteLine("{0} Added!", newTeacher.TeacherName);
        }

        public static void UpdateTeacher(Teacher t)
        {
            if (t != null)
            {
                Console.WriteLine("Change Teacher's Name to: ");
                t.TeacherName = Console.ReadLine();
                //Console.WriteLine("Change Standard's Decription to: ");
                //t.Description = Console.ReadLine();
                businessLayer.UpdateTeacher(t);
                Console.WriteLine("{0} Updated!", t.TeacherName);
            }
            else
                Console.WriteLine("Teacher does not exist");
        }

        public static void RemoveTeacherMenu()
        {
            int input;
            Teacher teacher;
            Console.WriteLine("[\t\tDelete Teacher\t\t]");
            GetAllTeachers();
            Console.WriteLine("Please enter the Teacher ID that you wish to delete: ");
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
                teacher = businessLayer.GetTeacherByID(input);
                DeleteTeacher(teacher);
            }
            catch(Exception e)
            {
                Console.WriteLine("Invalid Input");
            }
        }

        public static void DeleteTeacher(Teacher t)
        {
            if (t != null)
            {
                businessLayer.RemoveTeacher(t);
                Console.WriteLine("{0} Removed!", t.TeacherName);
            }
            else
                Console.WriteLine("Teacher does not exist.");
        }
        public static void GetAllStandard()
        {
            IEnumerable<Standard> standards = businessLayer.GetAllStandards();
            foreach (Standard s in standards)
                DisplayStandard(s);
        }

        public static void DisplayStandard(Standard s)
        {
            if (s != null)
                Console.WriteLine("Standard ID: {0}, Name: {1}, Description: {2}", s.StandardId, s.StandardName, s.Description);
            else
                Console.WriteLine("Standard does not exist.");
        }

        public static void DisplayAllTeachers()
        {
            IEnumerable<Teacher> teachers = businessLayer.GetAllTeachers();
            foreach(Teacher t in teachers)
            {
                DisplayTeacher(t);
            }
        }
        public static void DisplayTeacher(Teacher t)
        {
            if (t != null)
                Console.WriteLine("Teacher ID: {0}, Name: {1}, Standard ID: {2}", t.TeacherId, t.TeacherName, t.StandardId);
            else
                Console.WriteLine("Teacher does not exist.");
        }

        public static void UpdateTeacherMenu()
        {
            Console.WriteLine("[\t\tUpdate Teacher\t\t]");
            GetAllTeachers();
            Console.WriteLine("[1] Find by Name");
            Console.WriteLine("[2] Find by Id");
            Teacher updateTeacher;
            try
            {
                int input = Convert.ToInt32(Console.ReadLine());
                if (input == 1)
                {
                    Console.WriteLine("Enter Teacher's name: ");
                    updateTeacher = businessLayer.GetTeacherByName(Console.ReadLine());
                    //Standard updateStandard = businessLayer.GetStandardByID(standardName.StandardId);
                    UpdateTeacher(updateTeacher);
                }
                else if (input == 2)
                {
                    Console.WriteLine("Enter Teacher's id: ");
                    updateTeacher = businessLayer.GetTeacherByID(Convert.ToInt32(Console.ReadLine()));
                    UpdateTeacher(updateTeacher);
                }
                else
                    Console.WriteLine("Invalid Input.");
            }
            catch(Exception e)
            {
                Console.WriteLine("That is an invalid teacher. Please try again");
            }
            
        }

        public static void GetAllTeachers()
        {
            IEnumerable<Teacher> teachers = businessLayer.GetAllTeachers();
            foreach (Teacher t in teachers)
                DisplayTeacher(t);
        }
        public static void GetCoursesByTeacher()
        {
            int input;
            Teacher teacher;
            Console.WriteLine("[\t\tCourse by Teacher ID\t\t]");
            GetAllTeachers();
            Console.WriteLine("Please enter the Teacher ID: ");
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
                teacher = businessLayer.GetTeacherByID(input);
                if (teacher != null)
                {
                    IEnumerable<Course> courses = businessLayer.GetAllCourses();
                    foreach (Course c in courses)
                    {
                        if (c.TeacherId == input)
                            Console.WriteLine("Teacher ID: {0}, Course ID: {1}, CourseName: {2}", c.TeacherId, c.CourseId, c.CourseName);
                    }
                }
                else
                    Console.WriteLine("No such Teacher exists in the database");
            }
            catch(Exception e)
            {
                Console.WriteLine("Not a valid Teacher ID!");
            }
        }
        #endregion
        #region Course
        public static void AddCourse()
        {
            Console.WriteLine("[\t\tAdd Course\t\t]");
            Console.WriteLine("Enter Course name: ");
            string courseName = Console.ReadLine();
            GetAllTeachers();
            Console.WriteLine("Enter Course's Teacher ID: ");
            int input = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Enter Standard's description: ");
            //string standardDescription = Console.ReadLine();
            Course newCourse = new Course();
            Teacher newTeacher = businessLayer.GetTeacherByID(input);
            if (newTeacher != null)
            {
                newCourse.CourseName = courseName;
                newCourse.TeacherId = input;
                businessLayer.AddCourse(newCourse);
                Console.WriteLine("{0} Added!", newCourse.CourseName);
            }
            else
                Console.WriteLine("That teacher does not exist");
            
        }

        public static void GetAllCourses()
        {
            IEnumerable<Course> standards = businessLayer.GetAllCourses();
            foreach (Course c in standards)
                DisplayCourse(c);
        }

        public static void DisplayCourse(Course c)
        {
            if (c != null)
                Console.WriteLine("Teacher ID: {0}, Course Name: {1}, Course ID: {2}", c.TeacherId, c.CourseName, c.CourseId);
            else
                Console.WriteLine("Course does not exist.");
        }

        public static void UpdateCourseMenu()
        {
            Console.WriteLine("[\t\tUpdate Course\t\t]");
            GetAllCourses();
            Console.WriteLine("[1] Find by Name");
            Console.WriteLine("[2] Find by Id");
            Course updateCourse;
            try
            {
                int input = Convert.ToInt32(Console.ReadLine());
                if (input == 1)
                {
                    Console.WriteLine("Enter Course's name: ");
                    updateCourse = businessLayer.GetCourseByName(Console.ReadLine());
                    //Standard updateStandard = businessLayer.GetStandardByID(standardName.StandardId);
                    //UpdateCourseByName(updateCourse);
                    UpdateCourse(updateCourse);
                }
                else if (input == 2)
                {
                    Console.WriteLine("Enter Course's id: ");
                    updateCourse = businessLayer.GetCourseByID(Convert.ToInt32(Console.ReadLine()));
                    if (updateCourse != null)
                    {
                        UpdateCourse(updateCourse);
                    }
                    else
                        Console.WriteLine("That course does not exist");
                }
                else
                    Console.WriteLine("Invalid Input.");
            }
            catch (Exception e)
            {
                Console.WriteLine("That is an invalid course. Please try again");
            }

        }

        public static void UpdateCourse(Course c)
        {
            String courseName;
            Teacher teacher;
            if (c != null)
            {
                //businessLayer.RemoveCourse(c);
                Console.WriteLine("Change Course's Name: ");
                courseName = Console.ReadLine();
                GetAllTeachers();
                Console.WriteLine("\nEnter the course's Teacher ID: ");
                try
                {
                    int input = Convert.ToInt32(Console.ReadLine());
                    teacher = businessLayer.GetTeacherByID(input);
                    if (teacher != null)
                    {
                        //Console.WriteLine("INITIALIZE");
                        c.CourseName = courseName;
                        c.TeacherId = input;//teacher.TeacherId;
                        //Console.WriteLine("UPDATE");
                        businessLayer.UpdateCourse(c);
                        //Console.WriteLine("AFTER");
                        Console.WriteLine("{0} Updated!", c.CourseName);
                    }
                    else
                        Console.WriteLine("No such Teacher exists within the database");
                }
                catch(Exception e)
                {
                    Console.WriteLine("No such Teacher exists within the database EXCEPTION");
                    Console.WriteLine(e.Message);
                }
                
                
            }
            else
                Console.WriteLine("Teacher does not exist.");
        }

        public static void UpdateCourseByName(Course c)
        {
            String courseName;
            Teacher teacher;
            Teacher teacher2 = c.Teacher;
            Course find;
            if (c != null)
            {
                //businessLayer.RemoveCourse(c);
                Console.WriteLine("Change Course's Name: ");
                courseName = Console.ReadLine();
                GetAllTeachers();
                Console.WriteLine("\nEnter the course's Teacher ID: ");
                try
                {
                    int input = Convert.ToInt32(Console.ReadLine());
                    teacher = businessLayer.GetTeacherByID(input);
                    if (teacher!=null)//(teacher2.Courses.Count > 0)
                    {
                        businessLayer.RemoveCourse(c);
                        c.TeacherId = input;
                        c.CourseName = courseName;
                        businessLayer.AddCourse(c);
                        /*Console.WriteLine("CHECK");
                        for(int i =0;i< teacher.Courses.Count; i++)
                        {
                            find = teacher2.Courses.ElementAt<Course>(i);
                            Console.WriteLine(find.CourseName);
                            if (find.CourseName == c.CourseName && find.CourseId == c.CourseId && find.TeacherId == c.TeacherId)
                            {
                                teacher2.Courses.Remove(c);
                                c.CourseName = courseName;
                                c.TeacherId = input;
                                teacher.Courses.Add(c);
                            }
                        }*/
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ID SNo such Teacher exists within the database EXCEPTION");
                    Console.WriteLine(e.Message);
                }


            }
            else
                Console.WriteLine("Teacher does not exist.");
        }

        public static void RemoveCourseMenu()
        {
            int input;
            Course course;
            Console.WriteLine("[\t\tDelete Course\t\t]");
            GetAllCourses();
            Console.WriteLine("Please enter the Course ID that you wish to delete: ");
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
                course = businessLayer.GetCourseByID(input);
                DeleteCourse(course);
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input");
            }
        }

        public static void DeleteCourse(Course c)
        {
            if (c != null)
            {
                businessLayer.RemoveCourse(c);
                Console.WriteLine("{0} Removed!", c.CourseName);
            }
            else
                Console.WriteLine("Course does not exist.");
        }
        #endregion

        //Console.Clear();
        //Console.WriteLine("-----------------------------------------------------------");
        /*public static void UpdateStandardMenu()
        {
            Console.WriteLine("[\t\tUpdate Standard\t\t]");
            GetAllStandard();
            DisplaySearchOptions();
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                Console.WriteLine("Enter Standard's name: ");
                Standard standardName = businessLayer.GetStandardByName(Console.ReadLine());
                Standard updateStandard = businessLayer.GetStandardByID(standardName.StandardId);
                UpdateStandard(updateStandard);
            }
            else if (input == 2)
            {
                Console.WriteLine("Enter Standard's id: ");
                Standard updateStandard = businessLayer.GetStandardByID(Convert.ToInt32(Console.ReadLine()));
                UpdateStandard(updateStandard);
            }
            else
                Console.WriteLine("Invalid Input.");
        }

        public static void RemoveStandardMenu()
        {
            Console.WriteLine("[\t\tRemove Standard\t\t]");
            GetAllStandard();
            DisplaySearchOptions();
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                Console.WriteLine("Enter Standard's name: ");
                Standard standardName = businessLayer.GetStandardByName(Console.ReadLine());
                Standard removeStandard = businessLayer.GetStandardByID(standardName.StandardId);
                RemoveStandard(removeStandard);
            }
            else if (input == 2)
            {
                Console.WriteLine("Enter Standard's id: ");
                Standard removeStandard = businessLayer.GetStandardByID(Convert.ToInt32(Console.ReadLine()));
                RemoveStandard(removeStandard);
            }
            else
                Console.WriteLine("Invalid Input.");
        }*/
    }
}

/*
 <?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6.1" />
    </startup>
</configuration>*/
