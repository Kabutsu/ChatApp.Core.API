using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Core.API.Database.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> GetAll();
        Task<T> Get(string id);
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
