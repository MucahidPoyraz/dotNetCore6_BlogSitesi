using DAL.Context;
using DAL.Repositories.Abstract;
using DAL.Repositories.Concrete;

namespace DAL.UOW
{
    public class Uow : IUow
    {
        private readonly BlogDbContext _context;

        public Uow(BlogDbContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        IRepository<T> IUow.GetRepository<T>()
        {
            return new Repository<T>(_context);
        }
    }
}
