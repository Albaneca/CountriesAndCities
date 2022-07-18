using CountriesAndCities.Services.DTOs;

namespace CountriesAndCities.Web.Models.Mapper
{
    public static class CountryMapper
    {
        public static CountryDTO GetDTO(this CountryViewModel model)
        {
            var entity = new CountryDTO
            {
                Id = model.Id,
                CountryName = model.Name
            };
            return entity;
        }

        public static CountryViewModel GetModel(this CountryDTO country)
        {
            var entity = new CountryViewModel
            {
                Name = country.CountryName,
                Id = country.Id
            };
            return entity;
        }

    }
}
