﻿using Entity.Abstract;
using System.Linq.Expressions;

namespace DAL.Repositories.Abstract
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task AddAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByGuidAsync(object id);
        Task<T> GetByGuidAsync(object id, params Expression<Func<T, object>>[] includeProperties);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}
