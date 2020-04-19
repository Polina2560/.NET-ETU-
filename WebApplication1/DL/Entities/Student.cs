using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public List<Homework> Homeworks { get; set; }       // У студента мб несколько ДЗ

         public Student()
         {
            Homeworks = new List<Homework>();
         }
    }
}