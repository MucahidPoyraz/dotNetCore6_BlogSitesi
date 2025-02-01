using BL.Abstract;
using DAL.UOW;
using Entity.Concrete;

namespace BL.Concrete
{
    public class ArticleService : GenericService<Article>, IArticleService
    {
        public ArticleService(IUow uow) : base(uow)
        {
        }
    }
}
