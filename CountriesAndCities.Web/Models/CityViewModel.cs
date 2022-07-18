using Microsoft.AspNetCore.Mvc.Rendering;

namespace CountriesAndCities.Web.Models
{
    public class CityViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public SelectList Countries { get; set; }
    }
}
