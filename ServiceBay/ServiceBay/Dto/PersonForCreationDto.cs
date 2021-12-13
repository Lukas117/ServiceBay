using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Dto
{
    public class PersonForCreationDto
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Phoneno { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int UserRole { get; set; }
        public int AddressId { get; set; }
        public class AddressForCreationDto
        {
            public int Id { get; set; }
            public string StreetName { get; set; }
            public string StreetNumber { get; set; }
            public string CityZipcode { get; set; }
        }
        public class CityForCreationDto
        {
            [Key]
            public string Zipcode { get; set; }
            public string CityName { get; set; }
            public string Country { get; set; }
        }
    }
}
