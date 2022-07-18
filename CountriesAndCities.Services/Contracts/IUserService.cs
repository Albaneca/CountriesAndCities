using CountriesAndCities.Services.DTOs;
using CountriesAndCities.Services.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesAndCities.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByEmailOrIdAsync(string emailOrId);

        Task<UserDTO> PostAsync(RegisterUserDTO obj);

        Task<int> UsersCountAsync();
    }
}
