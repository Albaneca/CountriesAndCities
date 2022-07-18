using CountriesAndCities.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesAndCities.Services.DTOs
{
    public class CityDTO : IErrorMessage
    {
        public long Id  { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public long CountryId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
