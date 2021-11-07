using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<T>> GetAllAsync(Func<T, bool> filter = null)
        {
            if (filter == null)
            {
                filter = x => true;
            }
            return await Task.Run(entities.Where<T>(filter).ToList);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await entities.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async void InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Add(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async void UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async void DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
