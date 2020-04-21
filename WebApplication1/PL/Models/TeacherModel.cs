using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PL.Models
{
   /* public class TeacherModel
    {
    }*/

        // Модель для вывода преподавателей
        public class TeacherViewModel
        {
            public Teacher Teacher { get; set; }
            public List<HomeworkViewModel> TeacherHomeworks { get; set; }
    }

        public class TeacherEditModel
        {
            [Required]
            public int TeacherId { get; set; }

            public string TeacherName { get; set; }
        }
}
