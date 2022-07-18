using CountriesAndCities.Data;
using CountriesAndCities.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesAndCities.Services.Services
{
    public class GetObjectsService
    {
        private readonly CountriesAndCitiesDbContext _db;
        public GetObjectsService(CountriesAndCitiesDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Country> GetCountries()
        {
            return this._db.Countries;
        }

    }
}
