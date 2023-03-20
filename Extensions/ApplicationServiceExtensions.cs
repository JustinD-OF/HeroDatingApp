using HeroDatingApp.Data;
using HeroDatingApp.Helpers;
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

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddScoped<LogUserActivity>();

            services.AddScoped<ILikesRepository, LikesRepository>();

            services.AddScoped<IMessageRepository, MessageRepository>();

            return services;

        }
    }
}