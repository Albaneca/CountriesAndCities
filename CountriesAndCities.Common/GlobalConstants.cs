using System;

namespace CountriesAndCities.Common
{
    public static class GlobalConstants
    {
        public const string PassRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$";

        public const string PASSWORD_ERROR_MESSAGE = "Password must be at least 8 symbols and should contain capital letter, digit and special symbol (+, -, *, &, ^, …)";
        public const string USER_EXISTS = "User with this email already exist. Try another!";
        public static string USERNAME_EXIST = "User with this username already exist. Try another!";
        public static string INCORRECT_DATA = "Incorrect user data. Try again!";
        public static string USER_NOT_FOUND = "User was not found. Try again!";
        public static string CITY_NOT_FOUND = "City was not found. Try again!";
        public static string COUNTRY_NOT_FOUND = "Country was not found. Try again!";
        public static int PageSkip = 10;
        public static string COUNTRY_EXIST = "Country with this name already exist. Try another!";
        public static string CITY_EXIST = "City with this name already exist. Try another!";
        public const string WRONG_CREDENTIALS = "Wrong credentials! Try again";
        public static string LOGGED = "Succesfully logged";
        public const string Secret = "SomeLongerJWTSecretKeyCuzItWasBroken";
    }
}
