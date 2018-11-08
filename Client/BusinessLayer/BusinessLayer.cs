using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class BusinessLayer : IBuisnessLayer
    {
        private readonly IStandardRepository _standardRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;

        public BusinessLayer()
        {
            _standardRepository = new StandardRepository();
            _courseRepository = new CourseRepository();
            _teacherRepository = new TeacherRepository();
        }

        public void AddStandard(Standard standard)
        {
            _standardRepository.Insert(standard);
        }

        public void AddCourse(Course student)
        {
            _courseRepository.Insert(student);
        }

        public IEnumerable<Standard> GetAllStandards()
        {
            return _standardRepository.GetAll();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }

        public Standard GetStandardByID(int id)
        {
            return _standardRepository.GetById(id);
        }

        public Standard GetStandardByName(string name)
        {
            return _standardRepository.GetSingle(
                s => s.StandardName.Equals(name),
                s => s.Students);
        }

        public Course GetCourseByID(int id)
        {
            return _courseRepository.GetById(id);
        }

        public Course GetCourseByName(string course)
        {
            return _courseRepository.GetSingle(
                s => s.CourseName.Equals(course),
                s => s.Teacher);
        }

        public void RemoveStandard(Standard standard)
        {
            _standardRepository.Delete(standard);
        }

        public void RemoveCourse(Course student)
        {
            _courseRepository.Delete(student);
        }

        public void UpdateStandard(Standard standard)
        {
            _standardRepository.Update(standard);
        }

        public void UpdateCourse(Course student)
        {
            _courseRepository.Update(student);
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAll();
        }

        public Teacher GetTeacherByID(int id)
        {
            return _teacherRepository.GetById(id);
        }

        public Teacher GetTeacherByName(string teacher)
        {
            return _teacherRepository.GetSingle(
                s => s.TeacherName.Equals(teacher),
                s => s.Standard);
        }

        public void AddTeacher(Teacher teacher)
        {
            _teacherRepository.Insert(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            _teacherRepository.Update(teacher);
        }

        public void RemoveTeacher(Teacher teacher)
        {
            _teacherRepository.Delete(teacher);
        }
    }
}
