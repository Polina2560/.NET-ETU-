using BL;
using DL;
using Microsoft.AspNetCore.Mvc;
using PL;
using PL.Models;

namespace WebApplication1.Controllers
{
    public class TeacherPersonalController : Controller
    {
        private DataManager _datamanager;
        private ServicesManager _servicesmanager;

        public TeacherPersonalController(EFDbContext context, DataManager dataManager)
        {
            _datamanager = dataManager;
            _servicesmanager = new ServicesManager(_datamanager);
        }

        public IActionResult Index(int teacherId)
        {
            //Вывод инфо по одному преподавателю
            TeacherViewModel _tch = _servicesmanager.Teachers.TeacherDBToViewModelById(teacherId);
            return View(_tch);
        }

    }
}