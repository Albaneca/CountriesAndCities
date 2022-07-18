using CountriesAndCities.Services.DTOs;

namespace CountriesAndCities.Web.Models.Mapper
{
    public static class CityMapper
    {
        public static CityDTO GetDTO(this CityViewModel model)
        {
            var entity = new CityDTO
            {
                Id = model.Id,
                CityName = model.Name,
                CountryName = model.CountryName,
                CountryId = model.CountryId
            };
            return entity;
        }

        public static CityViewModel GetModel(this CityDTO city)
        {
            var entity = new CityViewModel
            {
                CountryId = city.CountryId,
                CountryName = city.CountryName,
                Name = city.CityName,
                Id = city.Id
            };
            return entity;
        }
    }
}
