using AutoMapper;
using BL.Abstract;
using BL.Concrete;
using DTOs.Article;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace UI.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IGenericService<Article> _articleService;
        private readonly IGenericService<Category> _categoryService;
        private readonly IMapper _mapper;

        public ArticleController(IGenericService<Article> articleService, IGenericService<Category> categoryService, IMapper mapper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            
            List<ArticleDto> articles = _mapper.Map<List<ArticleDto>>(await _articleService.GetAllAsync(x => !x.IsDeleted, x => x.Category));
            
            return View(articles);
        }
    }
}
