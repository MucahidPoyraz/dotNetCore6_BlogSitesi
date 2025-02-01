using BL.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;

        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllAsync();
            return View();
        }
    }
}
