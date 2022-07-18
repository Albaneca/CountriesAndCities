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
    public class CityService : ICityService
    {
        private readonly CountriesAndCitiesDbContext _db;
        public CityService(CountriesAndCitiesDbContext db)
        {
            _db = db;
        }

        public async Task<CityDTO> DeleteAsync(long id)
        {
            var city = await _db.Cities
                    .Include(c => c.Country)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (city is null)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.CITY_NOT_FOUND };
            }

            var cityDTO = city.GetDTO();

            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();

            return cityDTO;
        }

        public async Task<IEnumerable<CityDTO>> GetAllAsync(int page)
        {
            return await _db.Cities
                .Include(c => c.Country)
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetDTO())
                .ToListAsync();
        }

        public async Task<CityDTO> GetById(long id)
        {
            var city = await _db.Cities
                                     .Include(c => c.Country)
                                     .Where(x => x.Id == id)
                                     .Select(x => x.GetDTO())
                                     .FirstOrDefaultAsync();

            if (city is null)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.CITY_NOT_FOUND };
            }
            return city;
        }
        public async Task<IEnumerable<CityDTO>> GetCitiesInACountryAsync(long id, int page)
        {
            return await _db.Cities.Include(c => c.Country)
                    .Where(c=> c.CountryId == id)
                    .Skip(page * GlobalConstants.PageSkip)
                    .Take(10)
                    .Select(x => x.GetDTO())
                    .ToListAsync();
        }
        public async Task<int> CitiesCountAsync()
        {
            return await _db.Cities.CountAsync();
        }

        public async Task<CityDTO> PostAsync(CityDTO city)
        {
            if (city.CityName == null)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }
            if (await _db.Cities.FirstOrDefaultAsync(x => x.Name == city.CityName) != null)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.CITY_EXIST };
            }
            if (await _db.Countries.FirstOrDefaultAsync(x => x.Name == city.CityName) != null)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.COUNTRY_EXIST };
            }

            var newCity = city.GetEntity();

            await _db.Cities.AddAsync(newCity);
            await _db.SaveChangesAsync();

            var lastCityId = (long)CitiesCountAsync().Result;

            return await _db.Cities
                                  .Include(c => c.Country)
                                  .Where(x => x.Id == lastCityId)
                                  .Select(x => x.GetDTO())
                                  .FirstOrDefaultAsync();
        }

        public async Task<CityDTO> UpdateAsync(long id, CityDTO city)
        {
            var existingCity = await _db.Cities.FirstOrDefaultAsync(x => x.Id == id);
            var checkExistingCity = await _db.Cities.FirstOrDefaultAsync(x => x.Name == city.CityName);
            var checkExistingCountry = await _db.Countries.FirstOrDefaultAsync(x => x.Name == city.CityName);
            if (checkExistingCity != null
                && checkExistingCity.Id != id)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.CITY_EXIST };
            }

            if (checkExistingCountry != null)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.COUNTRY_EXIST };
            }

            if (existingCity is null)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.CITY_NOT_FOUND };
            }

            existingCity.Name = city.CityName;

            await _db.SaveChangesAsync();

            return await _db.Cities.Include(c => c.Country)
                      .Where(x => x.Id == id)
                      .Select(x => x.GetDTO())
                      .FirstOrDefaultAsync();
        }
    }
}
