using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Services;
using MVCApp.Models;
using MVCApp.Repositary;

namespace MVCApp.Controllers
{
    [Route("events")]
    public class EventInfoController : Controller
    {
        private IEventRepository<EventData> repo;
        private IEventRepository<EventUser> userrepo;
        public EventInfoController(IEventRepository<EventData> repository, IEventRepository<EventUser> user)
        {
            this.repo = repository;
            this.userrepo = user;
        }
        [HttpGet("",Name ="EventList")]
        public IActionResult Index()
        {
            var events = repo.GetAll();
            return View(events);
        }

        [HttpGet("new",Name ="NewEvent")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("register", Name = "RegisterEvent")]
        public IActionResult EventRegister([FromQuery(Name ="id")] int id)
        {
            ViewBag.id = id;
            var item = repo.Get(id);
            ViewBag.EventName = item.Title;
            return View();
        }



        [HttpPost("register", Name = "RegisterEvent")]
        public async Task<IActionResult> EventRegister(EventUser eventmodel)
        {
            if (ModelState.IsValid)
            {
                await userrepo.Add(eventmodel);
                return RedirectToRoute("EventList");
            }
            return View();

        }

        [HttpPost("new",Name ="NewEvent")]
        public async Task<IActionResult> Create(EventData model)
        {
            if (ModelState.IsValid)
            {
                await repo.Add(model);
                return RedirectToRoute("EventList");
            }
            return View();
            
        }


    }
}