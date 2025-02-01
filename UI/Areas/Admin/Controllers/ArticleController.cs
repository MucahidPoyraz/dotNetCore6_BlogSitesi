using AutoMapper;
using BL.Abstract;
using DTOs.Article;
using DTOs.Category;
using DTOs.Extensions;
using Entity.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using UI.ResultMessages;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IGenericService<Article> _articleService;
        private readonly IGenericService<Category> _categoryService;
        private readonly IValidator<Article> _validator;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;

        public ArticleController(
            IGenericService<Article> articleService,
            IGenericService<Category> categoryService,
            IMapper mapper,
            IValidator<Article> validator,
            IToastNotification toast)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
            _validator = validator;
            _toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = _mapper.Map<List<ArticleDto>>(await _articleService.GetAllAsync(
                x => !x.IsDeleted,
                x => x.Category));

            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetAllAsync(
                x => !x.IsDeleted));
            return View(new ArticleAddDto { Categories = categories });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
        {
            if (!ModelState.IsValid)
            {
                var categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetAllAsync(
                    x => x.IsDeleted == false));
                articleAddDto.Categories = categories;
                return View(articleAddDto);
            }

            var map = _mapper.Map<Article>(articleAddDto);
            var result = await _validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await _articleService.AddAsync(map);
                _toast.AddSuccessToastMessage(Messages.Article.Add(articleAddDto.Title),
                    new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction(nameof(Index));
            }
            else
            {
                result.AddToModelState(ModelState);
                var categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetAllAsync(
                    x => x.IsDeleted == false));
                articleAddDto.Categories = categories;
                return View(articleAddDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid articleId)
        {
            var article = await _articleService.GetByGuidAsync(articleId, x => x.Category);
            if (article == null)
            {
                return NotFound();
            }

            var categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetAllAsync(
                x => x.IsDeleted == false));
            var articleUpdateDto = _mapper.Map<ArticleEditDto>(article);
            articleUpdateDto.Categories = categories;

            return View(articleUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ArticleEditDto articleEditDto)
        {
            if (!ModelState.IsValid)
            {
                var categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetAllAsync(
                    x => x.IsDeleted == false));
                articleEditDto.Categories = categories;
                return View(articleEditDto);
            }

            var map = _mapper.Map<Article>(articleEditDto);
            var result = await _validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await _articleService.UpdateAsync(map);
                _toast.AddSuccessToastMessage(Messages.Article.Update(articleEditDto.Title),
                    new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction(nameof(Index));
            }
            else
            {
                result.AddToModelState(ModelState);
                var categories = _mapper.Map<List<CategoryDto>>(await _categoryService.GetAllAsync(
                    x => x.IsDeleted == false));
                articleEditDto.Categories = categories;
                return View(articleEditDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid articleId)
        {
            var article = await _articleService.GetByGuidAsync(articleId);
            if (article == null)
            {
                return NotFound();
            }

            await _articleService.DeleteAsync(article);
            _toast.AddSuccessToastMessage(Messages.Article.Delete(article.Title),
                new ToastrOptions { Title = "İşlem Başarılı" });

            return RedirectToAction(nameof(Index));
        }
    }
}
