using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using DL;
using Microsoft.AspNetCore.Mvc;
using PL;
using PL.Models;

namespace WebApplication1.Controllers
{
    public class TeachersController : Controller
    {
        private DataManager _datamanager;
        private ServicesManager _servicesmanager;

        public TeachersController(EFDbContext context, DataManager dataManager)
        {
            _datamanager = dataManager;
            _servicesmanager = new ServicesManager(_datamanager);
        }

        public IActionResult Index()
        {
            //Вывод списка преподавателей
            List<TeacherViewModel> _tch = _servicesmanager.Teachers.GetTeacheresList();
            return View(_tch);
        }

        [HttpGet]
        public IActionResult TeacherEditor(int teacherId)
        {
            TeacherEditModel _editModel;

            if (teacherId != 0)
                _editModel = _servicesmanager.Teachers.GetTeacherEditModel(teacherId);
            else
                _editModel = _servicesmanager.Teachers.CreateNewTeacherEditModel();

            return View(_editModel);
        }

        [HttpPost]
        public IActionResult SaveTeacher(TeacherEditModel model)
        {
            _servicesmanager.Teachers.SaveTeacherEditModelToDb(model);
            return RedirectToAction("Index", "Teachers", new { teacherId = model.TeacherId });
        }

        [HttpGet]
        public IActionResult TeacherDelete(int teacherId)
        {
            _servicesmanager.Teachers.DeleteTeacherFromDb(teacherId);
            return RedirectToAction("Index", "Teachers");
        }

    }
}