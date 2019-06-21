using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCApp.Infrastructure;
using MVCApp.Models;

namespace MVCApp.Services
{
    public class EventManager : IEventManager
    {
        private static List<EventData> events = new List<EventData>()
        {
            new EventData
            {
                Id=1,Title="Developing cloud Apps using .Net core",
                Speaker="Sonu", Location="chennai",Url="http://learningskills.com/register/EV102-core",
                StartDate=DateTime.Now.AddDays(2),EndDate=DateTime.Now.AddDays(5)
            },
            new EventData
            {
                Id=2,Title="Cloud Architeatching using Azure",
                Speaker="Gopinath", Location="Covai",Url="http://learningskills.com/register/EV105-AZ300",
                StartDate=DateTime.Now.AddDays(6),EndDate=DateTime.Now.AddDays(15)
            }
    };
        private EventDbContext db;
        public EventManager(EventDbContext db)
        {
            this.db = db;
        }
        public EventData Add(EventData data)
        {
           // data.Id = events.Max(e => e.Id) + 1;
            db.Events.Add(data);
            db.SaveChanges();
            return data;
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public EventData GetEvent(int id)
        {
            return db.Events.Find(id);
        }

        public IEnumerable<EventData> GetEvents()
        {
            return db.Events.ToList();
        }

        public EventData Update(int id, EventData data)
        {
            throw new NotImplementedException();
        }
    }
}
