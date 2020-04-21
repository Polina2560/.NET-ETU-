using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PL.Models
{

    // Модель для вывода ДЗ
    public class HomeworkViewModel 
    {
        public Homework Homework { get; set; }
        public Homework NextHomework { get; set; }
    }

    public class HomeworkEditModel 
    {
        [Required]
        public int HomeworkId { get; set; }

        public int HomeworkTeacherId { get; set; }
        public int HomeworkStudentId { get; set; }

        public int HomeworkMark { get; set; }
        public DateTime HomeworkDate { get; set; }
    }
}
