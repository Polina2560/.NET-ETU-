using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class Homework
    {

        public int HomeworkId { get; set; } 
        public int HomeworkMark { get; set; }       // оценка
        public DateTime HomeworkDate { get; set; }  // дата

        public Teacher HomeworkTeacher { get; set; }    // Дз проверяет один преподаватель
        public Student HomeworkStudent { get; set; }    // Дз выполняет один студент
    }
}
