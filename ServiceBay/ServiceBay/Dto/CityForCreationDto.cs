using System.ComponentModel.DataAnnotations;

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
