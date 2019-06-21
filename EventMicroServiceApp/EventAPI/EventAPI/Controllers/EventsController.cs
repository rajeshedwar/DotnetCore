using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventAPI.Models;
using EventAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    [Authorize]
    [FormatFilter]
    //[Produces("application/json")]
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEventRepository<EventData> eventrepo;

        public EventsController(IEventRepository<EventData> eventRepo)
        {
            this.eventrepo = eventRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EventData>> GetEvents()
        {
            var events = eventrepo.GetAllAsync();
            return events.ToList();
        }

        //Get /api/events
        //[HttpGet("{id}")]
        [HttpGet("{id}.{format?}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<EventData> GetById([FromRoute] int id)
        {
            var item = eventrepo.GetAsync(id);
            //if(item == null)
            //{
            //    return NotFound(); //404
            //}
            return item;  //200
        }

        //Post /api/events
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventData>> AddAsync([FromBody] EventData ev)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await eventrepo.AddAsync(ev);
            // return Created($"/api/events/{result.Id}",result);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

    }
}