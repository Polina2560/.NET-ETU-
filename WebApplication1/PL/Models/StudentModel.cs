using System;
using DL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PL.Models
{
    /*public class StudentModel
    {
    }*/

    // Модель для вывода студентов
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public List<HomeworkViewModel> StudentHomeworks { get; set; }
    }

    /*public class StudentEditModel
    {
        [Required]
        public int StudentId { get; set; }

        public string StudentName { get; set; }
    }*/
}
