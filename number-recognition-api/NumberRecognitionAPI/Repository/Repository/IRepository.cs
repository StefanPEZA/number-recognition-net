using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IRepository<T> where T:BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(Func<T, bool> filter = null);
        Task<T> GetByIdAsync(Guid id);
        void InsertAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
