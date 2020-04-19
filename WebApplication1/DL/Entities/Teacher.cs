using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public List<Homework> Homeworks { get; set; }       // У преподавателя мб несколько ДЗ

        public Teacher()
        {
            Homeworks = new List<Homework>();
        }
    }
}
