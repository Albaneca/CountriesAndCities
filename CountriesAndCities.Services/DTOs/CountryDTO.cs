using CountriesAndCities.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesAndCities.Services.DTOs
{
    public class CountryDTO : IErrorMessage
    {
        public long Id { get; set; }
        public string CountryName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
