using BL;
using DL.Entities;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PL.Services
{
    public class StudentService
    {
        private DataManager _dataManager;
        private HomeworkService _homeworkService;
        public StudentService(DataManager dataManager)
        {
            this._dataManager = dataManager;
            _homeworkService = new HomeworkService(dataManager);
        }


        //Просмотр списка студентов
        public List<StudentViewModel> GetStudentesList()
        {
            var _dirs = _dataManager.Students.GetAllStudents();
            List<StudentViewModel> _modelsList = new List<StudentViewModel>();
            foreach (var item in _dirs)
            {
                _modelsList.Add(StudentDBToViewModelById(item.StudentId));
            }
            return _modelsList;
        }

        //Получение модели студента по ID
        public StudentViewModel StudentDBToViewModelById(int studentId)
        {
            var _student = _dataManager.Students.GetStudentById(studentId, true);

            List<HomeworkViewModel> _homeworksViewModelList = new List<HomeworkViewModel>(); //--
            foreach (var item in _student.Homeworks)
            {
                _homeworksViewModelList.Add(_homeworkService.HomeworkDBModelToView(item.HomeworkId));
            }
            return new StudentViewModel() { Student = _student, StudentHomeworks = _homeworksViewModelList };
        }

        /*public StudentEditModel GetStudentEditModel(int studentid = 0)
        {
            if (studentid != 0)
            {
                var _dirDB = _dataManager.Students.GetStudentById(studentid);
                var _dirEditModel = new StudentEditModel()
                {
                    StudentId = _dirDB.StudentId,
                    StudentName = _dirDB.StudentName
                };
                return _dirEditModel;
            }
            else { return new StudentEditModel() { }; }
        }

        public StudentViewModel SaveStudentEditModelToDb(StudentEditModel studentEditModel)
        {
            Student _studentDbModel;
            if (studentEditModel.StudentId != 0)
            {
                _studentDbModel = _dataManager.Students.GetStudentById(studentEditModel.StudentId);
            }
            else
            {
                _studentDbModel = new Student();
            }
            _studentDbModel.StudentName = studentEditModel.StudentName;

            _dataManager.Students.SaveStudent(_studentDbModel);

            return StudentDBToViewModelById(_studentDbModel.StudentId);
        }

        public StudentEditModel CreateNewStudentEditModel()
        {
            return new StudentEditModel() { };
        }*/
    }
}
