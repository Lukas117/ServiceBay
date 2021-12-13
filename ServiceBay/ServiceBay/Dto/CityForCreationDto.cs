using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Dto
{
    public class CityForCreationDto
    {
        [Key]
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }
        [Display(Name = "City")]
        public string CityName { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
    }
}
