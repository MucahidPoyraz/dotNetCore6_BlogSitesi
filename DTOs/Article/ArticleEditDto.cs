using DTOs.Category;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;

namespace DTOs.Article
{
    public class ArticleEditDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }

        public Image Image { get; set; }
        public IFormFile? Photo { get; set; }

        public IList<CategoryDto> Categories { get; set; }
    }
}
