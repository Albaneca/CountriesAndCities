using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountriesAndCities.Data.Models
{
    public class Country : DeletableEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; } = new List<City>();
    }

}
