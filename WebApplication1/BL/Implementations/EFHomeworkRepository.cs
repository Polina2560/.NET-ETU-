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
   public class EFHomeworkRepository : IHomeworkRepository
    {
        private EFDbContext context;
        public EFHomeworkRepository(EFDbContext context)
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
            return context.Homework.FirstOrDefault(x => x.HomeworkId == homeworkId).HomeworkStudent;
        }

        // Получить преподавателя по ID работы
        public Teacher GetTeacherFromHomework(int homeworkId)
        {
            return context.Homework.FirstOrDefault(x => x.HomeworkId == homeworkId).HomeworkTeacher;
        }

        // Созранить работу
        public void SaveHomework(Homework homework)
        {
            if (homework.HomeworkId == 0)
                context.Homework.Add(homework);
            else
                context.Entry(homework).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        //Удалить работу
        public void DeleteHomework(Homework homework)
        {
            context.Homework.Remove(homework);
            context.SaveChanges();
        }
    }
}
