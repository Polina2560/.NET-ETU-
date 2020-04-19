using BL;
using PL.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PL
{
    public class ServicesManager
    {
        DataManager _dataManager;
        private StudentService _studentService;
        private TeacherService _teacherService;
        private HomeworkService _homeworkService;

        public ServicesManager(
            DataManager dataManager
            )
        {
            _dataManager = dataManager;
            _studentService = new StudentService(_dataManager);
            _teacherService = new TeacherService(_dataManager);
            _homeworkService = new HomeworkService(_dataManager);
        }
        public StudentService Students { get { return _studentService; } }
        public TeacherService Teachers { get { return _teacherService; } }
        public HomeworkService Homeworks { get { return _homeworkService; } }

    }
}
