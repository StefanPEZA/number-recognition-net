using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IRepository<T> where T:BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(int limit = 10, Func<T, bool> filter = null);
        Task<T> GetByIdAsync(Guid id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
