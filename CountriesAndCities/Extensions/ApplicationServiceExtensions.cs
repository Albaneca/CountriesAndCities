﻿using CountriesAndCities.Data;
using CountriesAndCities.Services.Contracts;
using CountriesAndCities.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CountriesAndCities.Extensions
{
    public static class ApplicationServiceExtensions
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
            return services;
        }
    }

}
