using CountriesAndCities.Data;
using CountriesAndCities.Services.Contracts;
using CountriesAndCities.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CountriesAndCities.Web.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //DB
            var connectionStrings = config.GetConnectionString("Anjelo");  /* Write connection string here */
            services.AddDbContext<CountriesAndCitiesDbContext>(options => options.UseSqlServer(connectionStrings));

            //Services
            // services.AddScoped<I, >();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<GetObjectsService>();
            return services;
        }
    }
}
