using BL.Interfaces;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class DataManager
    {

        private IStudentRepository _studentRepository;
        private IHomeworkRepository _homeworkRepository;
        private ITeacherRepository _teacherRepository;

        public DataManager(IStudentRepository studentRepository, IHomeworkRepository homeworkRepository, ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _homeworkRepository = homeworkRepository;
            _teacherRepository = teacherRepository;
        }

        public IStudentRepository Students { get { return _studentRepository; } }
        public IHomeworkRepository Homeworks { get { return _homeworkRepository; } }
        public ITeacherRepository Teachers { get { return _teacherRepository; } }
    }
}
