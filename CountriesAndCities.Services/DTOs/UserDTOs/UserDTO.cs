using CountriesAndCities.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesAndCities.Services.DTOs
{
    public class UserDTO : IErrorMessage
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ErrorMessage { get; set; }
    }
}
