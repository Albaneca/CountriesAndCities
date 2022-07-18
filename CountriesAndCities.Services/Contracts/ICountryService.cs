using CountriesAndCities.Data.Models;
using CountriesAndCities.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesAndCities.Services.Contracts
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetAllAsync(int page);

        Task<CountryDTO> GetById(long id);

        Task<CountryDTO> PostAsync(CountryDTO country);

        Task<CountryDTO> UpdateAsync(long id, CountryDTO country);

        Task<CountryDTO> DeleteAsync(long id);

        Task<int> CountriesCountAsync();
    }

}
