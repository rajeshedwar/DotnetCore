using Microsoft.EntityFrameworkCore;
using MVCApp.Infrastructure;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Repositary
{
    public class EventRepository<T> : IEventRepository<T> where T : Baseentity
    {
        private EventDbContext dbContext;
        private DbSet<T> entities;

        public EventRepository(EventDbContext db)
        {
            this.dbContext = db;
            entities = dbContext.Set<T>();
        }

        public async Task<T> Add(T item)
        {
            await entities.AddAsync(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(int id)
        {
            var item = await entities.FindAsync(id);
            if (item == null)
                throw new Exception("Item not found for deletion");
            entities.Remove(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public T Get(int id)
        {
            var item = entities.Find(id);
            return item;
        }
        public IEnumerable<T> GetAll()
        {
                return entities.ToList();
        }

        public async Task<T> Update(int id, T item)
        {
            entities.Update(item).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return item;
        }
    }
}
