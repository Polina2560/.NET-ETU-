using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents(bool includeHomeworks = false);
        Student GetStudentById(int studentId, bool includeHomeworks = false);
        List<Homework> GetStudentHomeworks(int studentId);
        void SaveStudent(Student achieve);
        void DeleteStudent(Student achieve);
    }
}
