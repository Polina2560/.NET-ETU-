using BL.Interfaces;
using DL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Implementations
{
   public class EFStudentContract : IStudentContract
    {
        private EFDbContext context;

        public EFStudentContract(EFDbContext context)
        {
            this.context = context;
        }

        // Получить список всех студентов (включая списох дз или нет)
        public IEnumerable<Student> GetAllStudents(bool includeHomeworks = false)
        {
            if (includeHomeworks)
                return context.Set<Student>().Include(x => x.Homeworks).AsNoTracking().ToList();
            else
                return context.Student.ToList();
        }

        // Получить студента по ID (включая списох дз или нет)
        public Student GetStudentById(int studentId, bool includeHomeworks = false)
        {
            try
            {
                if (studentId == 0)
                    throw new ArgumentException("Id can't be 0");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            if (includeHomeworks)
                return context.Set<Student>().Include(x => x.Homeworks).AsNoTracking().FirstOrDefault(x => x.StudentId == studentId);
            else
                return context.Student.FirstOrDefault(x => x.StudentId == studentId);
        }

        // Получить ДЗ студента по его ID
        public List<Homework> GetStudentHomeworks(int studentId)
        {
            try
            {
                if (studentId == 0)
                    throw new ArgumentException("Id can't be 0");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            return GetStudentById(studentId).Homeworks;
        }
        
        // Сохранить студента 
        public void SaveStudent(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentException("Student null");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            if (student.StudentId == 0)
                context.Student.Add(student);
            else
                context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        // Удалить студента 
        public void DeleteStudent(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentException("Student null");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            context.Student.Remove(student);
            context.SaveChanges();
        }
    }
}
