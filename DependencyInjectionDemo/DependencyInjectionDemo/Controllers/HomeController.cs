using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Services;

namespace DependencyInjectionDemo.Controllers
{
    public class HomeController : Controller
    {
        IDataManager dm;
        public HomeController(IDataManager manager)
        {
            this.dm = manager;
        }
        public IActionResult Index()
        {
            ViewBag.message = dm.GetMessage();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
