using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DistributedCacheDemo.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DistributedCacheDemo.Controllers
{
    public class HomeController : Controller
    {
        IDistributedCache cache;

        public HomeController(IDistributedCache cache)
        {
            this.cache = cache;
        }
        public IActionResult Index()
        {
            //var data = cache.GetString("data");
            //if(!string.IsNullOrEmpty(data))
            //{
            //    ViewBag.Message = data;
            //}
            //else
            //{
            //    ViewBag.Message = "Nothing in cache";
            //    cache.SetString("data", "This is cached data");
            //}

            /* For creating cache table in SQL Server*/

            //  dotnet sql-cache create "Data Source=172.25.163.79;User ID=sa1;Password=Password123; Initial Catalog=CacheDB;" dbo CacheTable


            var data = cache.Get("data");
            if(data != null)
            {
                var message = Encoding.UTF8.GetString(data);
                ViewBag.Message = message;
            }
            else
            {
                ViewBag.Message = "Nothing in cache";
                var text = "This is sample text data";
                var value = Encoding.UTF8.GetBytes(text);
                cache.Set("data", value);
            }
            //var msg = Encoding.UTF8.GetBytes("This is Core Session data");
           // HttpContext.Session.Set("message", msg);
            HttpContext.Session.SetInt32("Count", 100);
            HttpContext.Session.SetString("Name", "Hexaware Technlogoies");
            ViewBag.Flag = HttpContext.Items["Flag"];
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Flag = HttpContext.Items["Flag"];

            //var msg = HttpContext.Session.GetString("message");
            //ViewBag.Message = msg;

            // var msg = HttpContext.Session.Get("message");
            ViewBag.Message = "rajesh";// Encoding.UTF8.GetString(msg);
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Count = HttpContext.Session.GetInt32("Count");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
