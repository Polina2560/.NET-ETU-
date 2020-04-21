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
    public class EFTeacherContract : ITeacherContract
    {
        private EFDbContext context;
        public EFTeacherContract(EFDbContext context)
        {
            this.context = context;
        }

        // Получить список всех преподавателей (включая списох дз или нет)
        public IEnumerable<Teacher> GetAllTeachers(bool includeHomeworks = false)
        {

            if (includeHomeworks)
                return context.Set<Teacher>().Include(x => x.Homeworks).AsNoTracking().ToList();
            else
                return context.Teacher.ToList();
        }

        // Получить преподавателя по ID (включая списох дз или нет)
        public Teacher GetTeacherById(int teacherId, bool includeHomeworks = false)
        {
            try
            {
                if (teacherId == 0)
                    throw new ArgumentException("Id can't be 0");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            if (includeHomeworks)
                return context.Set<Teacher>().Include(x => x.Homeworks).AsNoTracking().FirstOrDefault(x => x.TeacherId == teacherId);
            else
                return context.Teacher.FirstOrDefault(x => x.TeacherId == teacherId);
        }

        // Получить ДЗ преподавателя по его ID
        public List<Homework> GetTeacherHomeworks(int teacherId)
        {
            try
            {
                if (teacherId == 0)
                    throw new ArgumentException("Id can't be 0");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            return GetTeacherById(teacherId).Homeworks;
        }

        // Сохранить преподавателя 
        public void SaveTeacher(Teacher teacher)
        {
            try
            {
                if (teacher == null)
                    throw new ArgumentException("Teacher null");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            if (teacher.TeacherId == 0)
                context.Teacher.Add(teacher);
            else
                context.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        // Удалить преподавателя 
        public void DeleteTeacher(Teacher teacher)
        {
            try
            {
                if (teacher == null)
                    throw new ArgumentException("Teacher null");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            context.Teacher.Remove(teacher);
            context.SaveChanges();
        }
    }
}
