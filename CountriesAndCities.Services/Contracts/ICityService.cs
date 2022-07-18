using CountriesAndCities.Data.Models;
using CountriesAndCities.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountriesAndCities.Services.Contracts
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetAllAsync(int page);

        Task<IEnumerable<CityDTO>> GetCitiesInACountryAsync(long id, int page);

        Task<CityDTO> PostAsync(CityDTO city);

        Task<CityDTO> UpdateAsync(long id, CityDTO city);

        Task<CityDTO> DeleteAsync(long id);

        Task<int> CitiesCountAsync();

        Task<CityDTO> GetById(long id);
    }
}
