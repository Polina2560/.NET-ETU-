using BL.Interfaces;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class DataManager
    {

        private IStudentContract _studentContract;
        private IHomeworkContract _homeworkContract;
        private ITeacherContract _teacherContract;

        public DataManager(IStudentContract studentContract, IHomeworkContract homeworkContract, ITeacherContract teacherContract)
        {
            _studentContract = studentContract;
            _homeworkContract = homeworkContract;
            _teacherContract = teacherContract;
        }

        public IStudentContract Students { get { return _studentContract; } }
        public IHomeworkContract Homeworks { get { return _homeworkContract; } }
        public ITeacherContract Teachers { get { return _teacherContract; } }
    }
}
