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
    public class StudentPersonalController : Controller
    {
        private DataManager _datamanager;
        private ServicesManager _servicesmanager;

        public StudentPersonalController(EFDbContext context, DataManager dataManager)
        {
            _datamanager = dataManager;
            _servicesmanager = new ServicesManager(_datamanager);
        }

        public IActionResult Index(int studentId)
        {
            //Вывод инфо по одному студенту
            StudentViewModel _st = _servicesmanager.Students.StudentDBToViewModelById(studentId);
            return View(_st);
        }

    }
}