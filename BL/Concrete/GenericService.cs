using BL.Abstract;
using DAL.UOW;
using Entity.Abstract;
using System;
using System.Linq.Expressions;

namespace BL.Concrete
{
    public class GenericService<T> : IGenericService<T> where T : class, IEntity, new()
    {
        private readonly IUow _uow;

        public GenericService(IUow uow)
        {
            _uow = uow;
        }

        public async Task AddAsync(T entity)
        {
            await _uow.GetRepository<T>().AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
           return await _uow.GetRepository<T>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await _uow.GetRepository<T>().CountAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            await _uow.GetRepository<T>().DeleteAsync(entity);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            return await _uow.GetRepository<T>().GetAllAsync(predicate, includeProperties);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await _uow.GetRepository<T>().GetAsync(predicate, includeProperties);
        }

        public async Task<T> GetByGuidAsync(object id)
        {
            return await _uow.GetRepository<T>().GetByGuidAsync(id);
        }

        public async Task<T> GetByGuidAsync(object id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await _uow.GetRepository<T>().GetByGuidAsync(id, includeProperties);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await _uow.GetRepository<T>().UpdateAsync(entity);
        }
    }
}
