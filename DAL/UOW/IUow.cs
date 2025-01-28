using DAL.Repositories.Abstract;
using Entity.Abstract;

namespace DAL.UOW
{
    public interface IUow : IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IEntity, new();
        Task<int> SaveAsync();
        int Save();
    }
}
