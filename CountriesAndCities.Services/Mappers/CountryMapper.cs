using CountriesAndCities.Common;
using CountriesAndCities.Data.Models;
using CountriesAndCities.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesAndCities.Services.Mappers
{
    public static class CountryMapper
    {
        public static CountryDTO GetDTO(this Country country)
        {
            if (country is null)
            {
                return new CountryDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new CountryDTO
            {
                Id = country.Id,
                CountryName = country.Name
            };
        }
        public static Country GetEntity(this CountryDTO country)
        {
            return new Country
            {
                Name = country.CountryName
            };
        }
    }
}
