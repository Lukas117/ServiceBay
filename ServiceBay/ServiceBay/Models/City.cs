using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ServiceBay.Models
{
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        [Key]
        public string Zipcode { get; set; }

        public string CityName { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
