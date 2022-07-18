using CountriesAndCities.Common;
using CountriesAndCities.Data;
using CountriesAndCities.Data.Models;
using CountriesAndCities.Services.Contracts;
using CountriesAndCities.Services.DTOs;
using CountriesAndCities.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesAndCities.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly CountriesAndCitiesDbContext _db;
        public CountryService(CountriesAndCitiesDbContext db)
        {
            _db = db;
        }

        public async Task<int> CountriesCountAsync()
        {
            return await _db.Countries.CountAsync();
        }

        public async Task<CountryDTO> DeleteAsync(long id)
        {
            var country = await _db.Countries
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (country is null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_NOT_FOUND };
            }

            var countryDTO = country.GetDTO();

            _db.Countries.Remove(country);
            await _db.SaveChangesAsync();

            return countryDTO;
        }

        public async Task<IEnumerable<CountryDTO>> GetAllAsync(int page)
        {
            return await _db.Countries
                 .Include(c => c.Cities)
                 .Skip(page * GlobalConstants.PageSkip)
                 .Take(10)
                 .Select(x => x.GetDTO())
                 .ToListAsync();
        }

        public async Task<CountryDTO> GetById(long id)
        {
            var country = await _db.Countries
                                     .Include(c => c.Cities)
                                     .Where(x =>x.Id == id)
                                     .Select(x => x.GetDTO())
                                     .FirstOrDefaultAsync();

            if (country is null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_NOT_FOUND };
            }
            return country;
        }

        public async Task<CountryDTO> PostAsync(CountryDTO country)
        {
            if (country.CountryName == null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }
            if (await _db.Countries.FirstOrDefaultAsync(x => x.Name == country.CountryName) != null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_EXIST };
            }

            if (await _db.Cities.FirstOrDefaultAsync(x => x.Name == country.CountryName) != null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.CITY_EXIST };
            }

            var newCountry = country.GetEntity();

            await _db.Countries.AddAsync(newCountry);
            await _db.SaveChangesAsync();

            var lastCountryId = (long)CountriesCountAsync().Result;

            return await _db.Countries.Where(x => x.Id == lastCountryId)
                                  .Include(x => x.Cities)
                                  .Select(x => x.GetDTO())
                                  .FirstOrDefaultAsync();

        }

        public async Task<CountryDTO> UpdateAsync(long id, CountryDTO country)
        {
            var existingCountry = await _db.Countries.FirstOrDefaultAsync(x => x.Id == id);
            var checkExistingCountry = await _db.Countries.FirstOrDefaultAsync(x => x.Name == country.CountryName);
            var checkExistingCity = await _db.Countries.FirstOrDefaultAsync(x => x.Name ==country.CountryName);
            if (checkExistingCountry != null
                && checkExistingCountry.Id != id)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_EXIST };
            }
            if (checkExistingCity != null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.CITY_EXIST };
            }

            if (existingCountry is null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.COUNTRY_NOT_FOUND };
            }

            existingCountry.Name = country.CountryName;

            await _db.SaveChangesAsync();

            return await _db.Countries.Where(x => x.Id == id)
                      .Include(x => x.Cities)
                      .Select(x => x.GetDTO())
                      .FirstOrDefaultAsync();

        }
    }
}
