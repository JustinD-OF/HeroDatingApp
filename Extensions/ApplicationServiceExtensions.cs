using HeroDatingApp.Data;
using HeroDatingApp.Interfaces;
using HeroDatingApp.Services;
using Microsoft.EntityFrameworkCore;

namespace HeroDatingApp.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();

            return services;

        }
    }
}