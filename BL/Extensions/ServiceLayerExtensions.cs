using BL.Abstract;
using BL.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace BL.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IArticleService, ArticleService>();
            return services;
        }
    }
}
