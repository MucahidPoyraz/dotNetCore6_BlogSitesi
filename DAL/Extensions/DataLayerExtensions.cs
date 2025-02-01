using DAL.Context;
using DAL.Repositories.Abstract;
using DAL.Repositories.Concrete;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Extensions
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUow, Uow>();
            services.AddDbContext<BlogDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Local")));

            return services;
        }
    }
}
