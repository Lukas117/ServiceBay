﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Dto
{
    public class AddressForCreationDto
    {
        public int Id { get; set; }
        [Display(Name = "Street name")]
        public string StreetName { get; set; }
        [Display(Name = "Street number")]
        public string StreetNumber { get; set; }
        public string CityZipcode { get; set; }
    }
}
