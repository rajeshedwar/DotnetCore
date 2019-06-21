using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCApp.Models;

namespace MVCApp.Infrastructure
{
    public class EventDbContext:DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options):base(options)
        {

        }
        public DbSet<EventData> Events { get; set; }
        public DbSet<EventUser> EventUsers { get; set; }
    }
}
