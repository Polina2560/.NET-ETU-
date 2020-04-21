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
   public class EFHomeworkContract : IHomeworkContract
    {
        private EFDbContext context;
        public EFHomeworkContract(EFDbContext context)
        {
            this.context = context;
        }

        // Получить список всех домашних работ (включая студента и/или преподователя или нет)
        public IEnumerable<Homework> GetAllHomeworks(bool includeStudents = false, bool includeTeachers = false)
        {
            if (includeStudents)
            {
                if (includeTeachers)
                    return context.Set<Homework>().Include(x => x.HomeworkStudent).Include(x => x.HomeworkTeacher).AsNoTracking().ToList();
                else
                    return context.Set<Homework>().Include(x => x.HomeworkStudent).AsNoTracking().ToList();
            }
            else
            {
                if (includeTeachers)
                    return context.Set<Homework>().Include(x => x.HomeworkTeacher).AsNoTracking().ToList();
                else
                    return context.Homework.ToList();
            }
               
        }


        // Получить работу по её ID (включая студента и/или преподователя или нет)
        public Homework GetHomeworkById(int homeworkId, bool includeStudents = false, bool includeTeachers = false)
        {
            try
            {
                if (homeworkId == 0)
                    throw new ArgumentNullException("Id can't be 0");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
                if (includeStudents)
            {
                if (includeTeachers)
                    return context.Set<Homework>().Include(x => x.HomeworkStudent).Include(x => x.HomeworkTeacher).AsNoTracking().FirstOrDefault(x => x.HomeworkId == homeworkId);
                else
                    return context.Set<Homework>().Include(x => x.HomeworkStudent).AsNoTracking().FirstOrDefault(x => x.HomeworkId == homeworkId);
            }
            else
            {
                if (includeTeachers)
                    return context.Set<Homework>().Include(x => x.HomeworkTeacher).AsNoTracking().FirstOrDefault(x => x.HomeworkId == homeworkId);
                else
                    return context.Homework.FirstOrDefault(x => x.HomeworkId == homeworkId);
            }
            //return context.Homework.FirstOrDefault(x => x.HomeworkId == homeworkId);
        }

        // Получить студента по ID работы
        public Student GetStudentFromHomework(int homeworkId)
        {
            try
            {
                if (homeworkId == 0)
                    throw new ArgumentNullException("Id can't be 0");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            return context.Homework.FirstOrDefault(x => x.HomeworkId == homeworkId).HomeworkStudent;
        }

        // Получить преподавателя по ID работы
        public Teacher GetTeacherFromHomework(int homeworkId)
        {
            try
            {
                if (homeworkId == 0)
                    throw new ArgumentNullException("Id can't be 0");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            return context.Homework.FirstOrDefault(x => x.HomeworkId == homeworkId).HomeworkTeacher;
        }

        // Созранить работу
        public void SaveHomework(Homework homework)
        {
            try
            {
                if (homework == null)
                    throw new ArgumentNullException("Homework null");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Exeption was thown: {ex}");
                throw ex;
            }
            if (homework.HomeworkId == 0)
                context.Homework.Add(homework);
            else
                context.Entry(homework).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        //Удалить работу
        public void DeleteHomework(Homework homework)
        {
            try
            {
                if (homework == null)
                    throw new ArgumentException("Homework null");

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            context.Homework.Remove(homework);
            context.SaveChanges();

        }
    }
}
