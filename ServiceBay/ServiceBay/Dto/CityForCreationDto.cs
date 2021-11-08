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
        public string Zipcode { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
    }
}
