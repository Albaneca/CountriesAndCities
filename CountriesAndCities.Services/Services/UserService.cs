using CountriesAndCities.Common;
using CountriesAndCities.Data;
using CountriesAndCities.Data.Models;
using CountriesAndCities.Services.Contracts;
using CountriesAndCities.Services.DTOs;
using CountriesAndCities.Services.DTOs.UserDTOs;
using CountriesAndCities.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CountriesAndCities.Services.Services
{
    public class UserService : IUserService
    {
        private readonly CountriesAndCitiesDbContext _db;
        public UserService(CountriesAndCitiesDbContext db)
        {
            _db = db;
        }

        public async Task<UserDTO> GetUserByEmailOrIdAsync(string emailOrId)
        {
            var user = await _db.Users
                         .Where(x => x.Email == emailOrId || x.Id.ToString() == emailOrId)
                         .Select(x => x.GetDTO())
                         .FirstOrDefaultAsync();

            if (user is null)
            {
                return new UserDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            return user;
        }

        public async Task<UserDTO> PostAsync(RegisterUserDTO obj)
        {
            var errorMessage = await CheckUserData(obj);
            if (errorMessage != null)
            {
                return new UserDTO { ErrorMessage = errorMessage };
            }

            var newUser = obj.GetEntity();

            newUser.Password = newUser.Password;
            newUser.CreatedOn = DateTime.Now;

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return await _db.Users
                                  .Where(x => x.Username == newUser.Username)
                                  .Select(x => x.GetDTO())
                                  .FirstOrDefaultAsync();

        }

        public async Task<int> UsersCountAsync()
        {
            return await _db.Users.CountAsync();
        }

        private async Task<string> CheckUserData(RegisterUserDTO obj)
        {
            if (obj.Username == null ||
                obj.Password == null ||
                obj.Email == null ||
                obj.FirstName == null ||
                obj.LastName == null)
            {
                return GlobalConstants.INCORRECT_DATA;
            }
            if (!IsValidUser(obj.Username, obj.Email, obj.Password))
            {
                return GlobalConstants.INCORRECT_DATA;
            }

            if (await _db.Users.AnyAsync(x => x.Username == obj.Username))
            {
                return GlobalConstants.USERNAME_EXIST;
            }

            if (await _db.Users.AnyAsync(x => x.Email == obj.Email))
            {
                return GlobalConstants.USER_EXISTS;
            }

            return null;
        }

        private bool IsValidUser(string username, string email, string password)
        {
            var validUsername = username.Length >= 2 && username.Length <= 20;
            var validEmail = Regex.IsMatch(email ?? "", @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+");
            var validPassword = Regex.IsMatch(password ?? "", GlobalConstants.PassRegex);
            return validUsername && validEmail && validPassword;
        }
    }
}
