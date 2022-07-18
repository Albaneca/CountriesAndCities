using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CountriesAndCities.Data.Models
{
    public class City : DeletableEntity
    {
        [Required]
        public string Name { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
    }

}
