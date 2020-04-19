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

    }
}