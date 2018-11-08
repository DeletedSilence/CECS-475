using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBuisnessLayer
    {
        #region Standard
        IEnumerable<Standard> GetAllStandards();

        Standard GetStandardByID(int id);

        Standard GetStandardByName(string name);

        void AddStandard(Standard standard);

        void UpdateStandard(Standard standard);

        void RemoveStandard(Standard standard);
        #endregion

        #region Course
        IEnumerable<Course> GetAllCourses();

        Course GetCourseByID(int id);

        Course GetCourseByName(string student);

        void AddCourse(Course student);

        void UpdateCourse(Course student);

        void RemoveCourse(Course student);
        #endregion

        #region Teacher
        IEnumerable<Teacher> GetAllTeachers();

        Teacher GetTeacherByID(int id);

        Teacher GetTeacherByName(string teacher);

        void AddTeacher(Teacher teacher);

        void UpdateTeacher(Teacher teacher);

        void RemoveTeacher(Teacher teacher);
        #endregion
    }
}
