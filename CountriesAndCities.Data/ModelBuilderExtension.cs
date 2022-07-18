using CountriesAndCities.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace CountriesAndCities.Data
{
    public static class ModelBuilderExtension
    {
        public static IEnumerable<Country> Countries { get; }

        public static IEnumerable<City> Cities { get; }

        public static IEnumerable<User> Users { get; }

        static ModelBuilderExtension()
        {
            Cities = new HashSet<City>
            {
            new City { Id = 1, CountryId = 1, Name = "Sofia" },
            new City { Id = 2, CountryId = 2, Name = "Oslo" },
            new City { Id = 3, CountryId = 3, Name = "Helsinki" },
            new City { Id = 4, CountryId = 4, Name = "Bern" },
            new City { Id = 5, Name = "Plovdiv", CountryId = 1 },
            new City { Id = 6, Name = "Bucharest", CountryId = 5 },
            new City { Id = 7, Name = "Belgrad", CountryId = 6 },
            new City { Id = 8, Name = "Kozloduy", CountryId = 1 }
            };

            Countries = new HashSet<Country>
            {
            new Country { Id = 1, Name = "Bulgaria", },
            new Country { Id = 2, Name = "Norway", },
            new Country { Id = 3, Name = "Finland", },
            new Country { Id = 4, Name = "Switzerland", },
            new Country { Id = 5, Name = "Romania", },
            new Country { Id = 6, Name = "Serbia", }
            };

            Users = new HashSet<User>
            {
                new User { Id = 1, FirstName= "Anjelo", LastName ="Jotov", Email="anjelo9898@gmail.com", Password="Admin123$", Username="Albaneca"},

            };

        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(Users);
            modelBuilder.Entity<Country>().HasData(Countries);
            modelBuilder.Entity<City>().HasData(Cities);
        }
    }
}
