using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using persistence.Repositories;
using Microsoft.Extensions.Configuration;

using persistence;
using Application.Services.IServices;
using persistence.Services;
using Application.Helpers;
using Application.Services;
using Application.Infrastructure;

namespace persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                   configuration.GetConnectionString("ConnectionString")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISuggestionRepository, SuggestionRepository>();
            services.AddScoped<ICarRpository,CarRepository>();
            services.AddScoped<IOffreRepository, OffreRepository>();  
            services.AddScoped<IRentRepository, RentRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IRentServices, RentServices>();
            services.AddScoped<IOffreServices, OffreServices>();
            services.AddScoped<ICarServices, CarServices>();
            services.AddScoped<ISuggestionServices,SuggestionServices>();

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.AddScoped<IAuthService, AuthServices>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}