using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Application.Interfaces;
using System.Linq.Expressions;
using ProductApi.Domain.Entities;

namespace ProductApi.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        internal readonly ApplicationDbContext context;
        internal DbSet<T> dbset { get; set; }
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            dbset = context.Set<T>();
        }
        public virtual async Task<int> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await dbset.AddAsync(entity);
            return entity.Id;
        }
        //public virtual Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        //{
        //    return dbset.Where(expression);
        //}
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await dbset.ToListAsync();
            return result;
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbset.FindAsync(keyValues: id);
        }
        public virtual async Task DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Task.Run(() =>
            {
                dbset.Remove(entity);
            });
        }

        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Task.Run(() =>
            {
                dbset.Update(entity);
            });
        }
    }
}