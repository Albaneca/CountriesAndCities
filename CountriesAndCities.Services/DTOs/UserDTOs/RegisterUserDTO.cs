using CountriesAndCities.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesAndCities.Services.DTOs.UserDTOs
{
    public class RegisterUserDTO : IErrorMessage
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}
