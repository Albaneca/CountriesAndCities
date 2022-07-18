using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesAndCities.Services.DTOs.AuthDTOs
{
    public class ResponseAuthDTO
    {
        public string Email { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

    }
}
