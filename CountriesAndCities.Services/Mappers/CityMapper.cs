using CountriesAndCities.Common;
using CountriesAndCities.Data.Models;
using CountriesAndCities.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesAndCities.Services.Mappers
{
    public static class CityMapper
    {
        public static CityDTO GetDTO(this City city)
        {
            if (city is null)
            {
                return new CityDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new CityDTO
            {
                Id = city.Id,
                CityName = city.Name,
                CountryName = city.Country.Name
            };
        }
        public static City GetEntity(this CityDTO city)
        {
            return new City
            {
                Name = city.CityName
            };
        }
    }
}
