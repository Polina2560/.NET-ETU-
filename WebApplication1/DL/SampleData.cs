using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DL
{
    public class SampleData
    {

        // Если бд пустая, заполнить ее примерами
        public static void InitData(EFDbContext context)
        {
            if (!context.Student.Any())
            {
                context.Student.Add(new Entities.Student() { StudentName = "Ivam Ivanov" });
                context.Student.Add(new Entities.Student() { StudentName = "Petr Petriv" });
                context.Student.Add(new Entities.Student() { StudentName = "Evkakii Epapovich" });

                //context.SaveChanges();
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    var sqlException = e.GetBaseException() as SqlException;
                    
                }

            }


            if (!context.Teacher.Any())
            {
                context.Teacher.Add(new Entities.Teacher() { TeacherName = "Fedor Vasiliev" });
                context.Teacher.Add(new Entities.Teacher() { TeacherName = "Larisa Urievich" });
                //context.SaveChanges();
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    var sqlException = e.GetBaseException() as SqlException;

                }
            }

            if (!context.Homework.Any())
            {
                context.Homework.Add(new Entities.Homework() {HomeworkMark = 4, HomeworkDate = new DateTime(2019, 04, 30),HomeworkTeacher = context.Teacher.First(), HomeworkStudent = context.Student.First()});
                context.Homework.Add(new Entities.Homework() { HomeworkMark = 5, HomeworkDate = new DateTime(2019, 04, 28), HomeworkTeacher = context.Teacher.First(), HomeworkStudent = context.Student.Find(context.Student.First().StudentId + 1) });
                context.Homework.Add(new Entities.Homework() { HomeworkMark = 3, HomeworkDate = new DateTime(2019, 04, 17), HomeworkTeacher = context.Teacher.Last(), HomeworkStudent = context.Student.Last() });

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    var sqlException = e.GetBaseException() as SqlException;

                }
            }
        }
    }
}
