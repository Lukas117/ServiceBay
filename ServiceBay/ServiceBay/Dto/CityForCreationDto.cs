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
        [Required]
        public string Zipcode { get; set; }
        [Display(Name = "City")]
        [Required]
        public string CityName { get; set; }
        [Display(Name = "Country")]
        [Required]
        public string Country { get; set; }
    }
}
