using FluentValidation;

namespace DTOs.FluentValidations
{
    public class ArticleValidator : AbstractValidator<Entity.Concrete.Article>
    {
        public ArticleValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithName("Başlık");

            RuleFor(x => x.Content)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithName("İçerik");
        }
    }
}
