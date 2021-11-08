using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceBay.Models
{
    public partial class Address
    {
        public Address()
        {
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string CityZipcode { get; set; }

        public virtual City CityZipcodeNavigation { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
