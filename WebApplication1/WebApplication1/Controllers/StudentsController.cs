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
    public class StudentsController : Controller
    {
        private DataManager _datamanager;
        private ServicesManager _servicesmanager;

        public  StudentsController(EFDbContext context, DataManager dataManager)
        {
            _datamanager = dataManager;
            _servicesmanager = new ServicesManager(_datamanager);
        }

        public IActionResult Index()
        {
            //Вывод списка студентов
            List<StudentViewModel> _st = _servicesmanager.Students.GetStudentesList();
            return View(_st);
        }


        [HttpGet]
        public IActionResult StudentEditor(int studentId)
        {
            StudentEditModel _editModel;

            if (studentId != 0)
                _editModel = _servicesmanager.Students.GetStudentEditModel(studentId);
            else
                _editModel = _servicesmanager.Students.CreateNewStudentEditModel();

            return View(_editModel);
        }

        [HttpPost]
        public IActionResult SaveStudent(StudentEditModel model)
        {
            _servicesmanager.Students.SaveStudentEditModelToDb(model);
            return RedirectToAction("Index", "Students", new { studentId = model.StudentId});
        }

        [HttpGet]
        public IActionResult StudentDelete(int studentId)
        {
            _servicesmanager.Students.DeleteStudentFromDb(studentId);
            return RedirectToAction("Index", "Students");
        }

    }
}