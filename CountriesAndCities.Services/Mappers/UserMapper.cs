using CountriesAndCities.Common;
using CountriesAndCities.Data.Models;
using CountriesAndCities.Services.DTOs;
using CountriesAndCities.Services.DTOs.UserDTOs;
using System.Text.RegularExpressions;

namespace CountriesAndCities.Services.Mappers
{
    public static class UserMapper
    {
        public static UserDTO GetDTO(this User user)
        {
            if (user is null || user.Username is null || user.FirstName is null
                || user.LastName is null || user.Email is null
                || !Regex.IsMatch(user.Password ?? "", GlobalConstants.PassRegex))
            {
                return new UserDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
            };
        }
        public static User GetEntity(this RegisterUserDTO user)
        {
            return new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}
