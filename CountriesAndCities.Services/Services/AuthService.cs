using CountriesAndCities.Common;
using CountriesAndCities.Data;
using CountriesAndCities.Services.Contracts;
using CountriesAndCities.Services.DTOs;
using CountriesAndCities.Services.DTOs.AuthDTOs;
using CountriesAndCities.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CountriesAndCities.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly CountriesAndCitiesDbContext _db;

        public AuthService(CountriesAndCitiesDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseAuthDTO> GetByEmailAsync(string email)
        {
            var user = await _db.Users
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();

            if (user is null)
                return new ResponseAuthDTO { Message = GlobalConstants.WRONG_CREDENTIALS };

            var model = new ResponseAuthDTO();
             model.Email = user.Email;

            return model;
        }

        public async Task<bool> IsExistingAsync(string email)
        {
            return await _db.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsPasswordValidAsync(string email, string password)
        {
            if (password == null)
            {
                return false;
            }

            var userPassword = await _db.Users
                                        .Where(x => x.Email == email)
                                        .Select(x => x.Password)
                                        .FirstOrDefaultAsync();
            if (userPassword != null)
            {
                return userPassword == password;
            }
            return false;
        }
    }
}