using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreConfigurationDemo.Models;
using Microsoft.Extensions.Configuration;

namespace CoreConfigurationDemo.Controllers
{
    public class HomeController : Controller
    {
        // IConfiguration configuration;

        // public HomeController(IConfiguration config)
        // {
        //     this.configuration=config;
        // }

        //public IActionResult Index([FromServices] IConfiguration configuration)
        public IActionResult Index()
        {
            var configuration=HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            ViewBag.Name=configuration.GetValue<string>("Developer:Name");
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
