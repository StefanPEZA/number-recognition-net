using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<T> entities;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int limit = 10, Func<T, bool> filter = null)
        {
            return await Task.Run(entities.Where<T>(filter).Take<T>(limit).ToList);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await entities.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(T entity)
        {
            CheckForNull(entity);
            entities.Add(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            CheckForNull(entity);
            entities.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            CheckForNull(entity);
            entities.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        private static void CheckForNull(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
    }
}
