using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Repositary
{
    public interface IEventRepository<T> where T:Baseentity
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        Task<T> Add(T item);

        Task<T> Update(int id, T item);

        Task<T> Delete(int id);
    }
}
