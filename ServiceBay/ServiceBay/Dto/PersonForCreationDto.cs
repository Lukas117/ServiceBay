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
        [Display(Name = "First name")]
        [Required]
        public string Fname { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public string Lname { get; set; }
        [Display(Name = "Phone number")]
        [Required]
        public string Phoneno { get; set; }
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public int UserRole { get; set; }
        public int AddressId { get; set; }
    }
}
