using BL;
using DL.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL.Services
{
    public class HomeworkService
    {
        private DataManager dataManager;
        public HomeworkService(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public HomeworkViewModel HomeworkDBModelToView(int homeworkId)
        {
            var _model = new HomeworkViewModel()
            {
                Homework = dataManager.Homeworks.GetHomeworkById(homeworkId),
            };

            var _hmw = dataManager.Homeworks.GetAllHomeworks(true,true);
           if (_hmw.IndexOf(_hmw.FirstOrDefault(x => x.HomeworkId == _model.Homework.HomeworkId)) != _hmw.Count() - 1)
            {
                _model.NextHomework = _hmw.ElementAt(_hmw.IndexOf(_hmw.FirstOrDefault(x => x.HomeworkId == _model.Homework.HomeworkId)) + 1);
            }
            return _model;
        }

        public HomeworkEditModel GetHomeworkEditModel(int homeworkId)
        {
            var _dbModel = dataManager.Homeworks.GetHomeworkById(homeworkId);
            var _editModel = new HomeworkEditModel()
            {
                HomeworkId = _dbModel.HomeworkId = _dbModel.HomeworkId,
                HomeworkMark = _dbModel.HomeworkMark,
                HomeworkDate = _dbModel.HomeworkDate,
                HomeworkTeacherId = dataManager.Homeworks.GetTeacherFromHomework(homeworkId).TeacherId,
                HomeworkStudentId = dataManager.Homeworks.GetTeacherFromHomework(homeworkId).TeacherId
            };
            return _editModel;
        }

        public HomeworkViewModel SaveHomeworkEditModelToDb(HomeworkEditModel editModel)
        {
            Homework homework;
            if (editModel.HomeworkId != 0)
            {
                homework = dataManager.Homeworks.GetHomeworkById(editModel.HomeworkId);
            }
            else
            {
                homework = new Homework();
            }
            homework.HomeworkMark = editModel.HomeworkMark;
            homework.HomeworkDate = editModel.HomeworkDate;
            homework.HomeworkTeacher = dataManager.Homeworks.GetTeacherFromHomework(editModel.HomeworkTeacherId);
            homework.HomeworkStudent = dataManager.Homeworks.GetStudentFromHomework(editModel.HomeworkStudentId);
            dataManager.Homeworks.SaveHomework(homework);
            return HomeworkDBModelToView(homework.HomeworkId);
        }

        public void DeleteHomeworkFromDb(int homeworkId)
        {
            Homework _homeworkDbModel;
            _homeworkDbModel = dataManager.Homeworks.GetHomeworkById(homeworkId);
            dataManager.Homeworks.DeleteHomework(_homeworkDbModel);

        }


        public HomeworkEditModel CreateNewHomeworkEditModel(int homeworkId)
        {
            return new HomeworkEditModel() { HomeworkId = homeworkId };
        }
    }
}
