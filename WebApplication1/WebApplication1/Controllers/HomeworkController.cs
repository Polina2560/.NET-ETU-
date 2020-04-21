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
    public class HomeworkController : Controller
    {

        private DataManager _datamanager;
        private ServicesManager _servicesmanager;

        public IActionResult Index()
        {
            return View();
        }

        public HomeworkController(EFDbContext context, DataManager dataManager)
        {
            _datamanager = dataManager;
            _servicesmanager = new ServicesManager(_datamanager);
        }

       /* public IActionResult Index()
        {
            //Вывод списка студентов
            List<HomeworkViewModel> _st = _servicesmanager.Homeworks.GetHomeworkesList();
            return View(_st);
        }*/


        [HttpGet]
        public IActionResult HomeworkEditor(int homeworkId)
        {
            HomeworkEditModel _editModel;

            if (homeworkId != 0)
                    _editModel = _servicesmanager.Homeworks.GetHomeworkEditModel(homeworkId);
                else
                    _editModel = _servicesmanager.Homeworks.CreateNewHomeworkEditModel(homeworkId);


            return View(_editModel);
        }

        [HttpPost]
        public IActionResult SaveHomework(HomeworkEditModel model)
        {
            _servicesmanager.Homeworks.SaveHomeworkEditModelToDb(model);
            return RedirectToAction("Index", "Home", new { homeworkId = model.HomeworkId });
        }

        [HttpGet]
        public IActionResult HomeworkDelete(int homeworkId)
        {
            _servicesmanager.Homeworks.DeleteHomeworkFromDb(homeworkId);
            return RedirectToAction("Index", "Homeworks");
        }
    }
}