using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface ITeacherContract
    {
        IEnumerable<Teacher> GetAllTeachers(bool includeHomeworks = false);
        Teacher GetTeacherById(int teacherId, bool includeHomeworks = false);
        List<Homework> GetTeacherHomeworks(int teacherId);
        void SaveTeacher(Teacher achieve);
        void DeleteTeacher(Teacher achieve);
    }
}
