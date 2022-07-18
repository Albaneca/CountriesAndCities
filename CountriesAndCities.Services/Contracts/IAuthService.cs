using CountriesAndCities.Services.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesAndCities.Services.Contracts
{
    public interface IAuthService
    {

        Task<bool> IsExistingAsync(string email);

        Task<ResponseAuthDTO> GetByEmailAsync(string email);

        Task<bool> IsPasswordValidAsync(string email, string password);
    }
}
