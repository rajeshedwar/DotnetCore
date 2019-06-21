using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;

namespace MVCApp.Controllers
{
   // [Route("home")]
    public class HomeController : Controller
    {
        //Get   /hom/
       // [HttpGet("",Name ="Index")]
        public IActionResult Index()
        {
            return View();
        }
        //Get   /hom/privacy
       // [HttpGet("privacy",Name ="Privacy")]
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
