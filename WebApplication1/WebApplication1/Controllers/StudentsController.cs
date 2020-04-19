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

    }
}