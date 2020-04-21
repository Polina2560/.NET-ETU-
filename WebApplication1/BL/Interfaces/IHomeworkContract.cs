using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface IHomeworkContract
    {
        IEnumerable<Homework> GetAllHomeworks(bool includeStudents = false, bool includeTeachers = false);
        Homework GetHomeworkById(int homeworkId, bool includeStudents = false, bool includeTeachers = false);
        void SaveHomework(Homework achieve);
        void DeleteHomework(Homework achieve);
        Student GetStudentFromHomework(int homeworkId);
        Teacher GetTeacherFromHomework(int homeworkId);
    }
}
