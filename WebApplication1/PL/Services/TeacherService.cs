using BL;
using DL.Entities;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PL.Services
{
    public class TeacherService
    {
        private DataManager _dataManager;
        private HomeworkService _homeworkService;
        public TeacherService(DataManager dataManager)
        {
            this._dataManager = dataManager;
            _homeworkService = new HomeworkService(dataManager);
        }

        //Просмотр списка преподавателей
        public List<TeacherViewModel> GetTeacheresList()
        {
            var _dirs = _dataManager.Teachers.GetAllTeachers();
            List<TeacherViewModel> _modelsList = new List<TeacherViewModel>();
            foreach (var item in _dirs)
            {
                _modelsList.Add(TeacherDBToViewModelById(item.TeacherId));
            }
            return _modelsList;
        }

        //Получение модели преподавателя по ID
        public TeacherViewModel TeacherDBToViewModelById(int teacherId)
        {
            var _teacher = _dataManager.Teachers.GetTeacherById(teacherId, true);

            List<HomeworkViewModel> _homeworksViewModelList = new List<HomeworkViewModel>();
            foreach (var item in _teacher.Homeworks)
            {
                _homeworksViewModelList.Add(_homeworkService.HomeworkDBModelToView(item.HomeworkId));
            }
            return new TeacherViewModel() { Teacher = _teacher, TeacherHomeworks = _homeworksViewModelList };
        }

        public TeacherEditModel GetTeacherEditModel(int teacherid = 0)
        {
            if (teacherid != 0)
            {
                var _dirDB = _dataManager.Teachers.GetTeacherById(teacherid);
                var _dirEditModel = new TeacherEditModel()
                {
                    TeacherId = _dirDB.TeacherId,
                    TeacherName = _dirDB.TeacherName
                };
                return _dirEditModel;
            }
            else { return new TeacherEditModel() { }; }
        }

        public TeacherViewModel SaveTeacherEditModelToDb(TeacherEditModel teacherEditModel)
        {
            Teacher _teacherDbModel;
            if (teacherEditModel.TeacherId != 0)
            {
                _teacherDbModel = _dataManager.Teachers.GetTeacherById(teacherEditModel.TeacherId);
            }
            else
            {
                _teacherDbModel = new Teacher();
            }
            _teacherDbModel.TeacherName = teacherEditModel.TeacherName;

            _dataManager.Teachers.SaveTeacher(_teacherDbModel);

            return TeacherDBToViewModelById(_teacherDbModel.TeacherId);
        }

        public void DeleteTeacherFromDb(int studentId)
        {
            Teacher _studentDbModel;
            _studentDbModel = _dataManager.Teachers.GetTeacherById(studentId);
            _dataManager.Teachers.DeleteTeacher(_studentDbModel);

        }

        public TeacherEditModel CreateNewTeacherEditModel()
        {
            return new TeacherEditModel() { };
        }

    }
}
