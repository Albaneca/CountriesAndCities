using CountriesAndCities.Services.DTOs.UserDTOs;

namespace CountriesAndCities.Web.Models.Mapper
{
    public static class UserMapper
    {
        public static RegisterUserDTO GetDTO(this RegisterViewModel user)
        {
            return new RegisterUserDTO
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
