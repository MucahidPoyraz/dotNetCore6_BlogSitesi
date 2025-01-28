using DAL.Context;
using DAL.Repositories.Abstract;
using Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly BlogDbContext _context;

        public Repository(BlogDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table { get => _context.Set<T>(); }

        public async Task AddAsync(T entity)
        {
            try
            {
                await Table.AddAsync(entity);
            }
            catch(Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await Table.AnyAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                if (predicate is not null)
                    return await Table.CountAsync(predicate);
                return await Table.CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                await Task.Run(() => Table.Remove(entity));
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = Table;
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (includeProperties.Any())
                {
                    foreach (var include in includeProperties)
                    {
                        query = query.Include(include);
                    }                    
                }
                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByGuidAsync(object id)
        {
            try
            {
                return await Table.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                await Task.Run(() => Table.Update(entity));
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
