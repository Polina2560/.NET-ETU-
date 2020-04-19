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
    public class EFTeacherRepository : ITeacherRepository
    {
        private EFDbContext context;
        public EFTeacherRepository(EFDbContext context)
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
            if (includeHomeworks)
                return context.Set<Teacher>().Include(x => x.Homeworks).AsNoTracking().FirstOrDefault(x => x.TeacherId == teacherId);
            else
                return context.Teacher.FirstOrDefault(x => x.TeacherId == teacherId);
        }

        // Получить ДЗ преподавателя по его ID
        public List<Homework> GetTeacherHomeworks(int teacherId)
        {
            return GetTeacherById(teacherId).Homeworks;
        }

        // Сохранить преподавателя 
        public void SaveTeacher(Teacher teacher)
        {
            if (teacher.TeacherId == 0)
                context.Teacher.Add(teacher);
            else
                context.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        // Удалить преподавателя 
        public void DeleteTeacher(Teacher teacher)
        {
            context.Teacher.Remove(teacher);
            context.SaveChanges();
        }
    }
}
