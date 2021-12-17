using Microsoft.EntityFrameworkCore;

namespace ServiceBay.Dto
{
    [Keyless]
    public class PersonViewModel
    {
        public CityForCreationDto cityDto { get; set; }
        public AddressForCreationDto addressDto { get; set; }
        public PersonForCreationDto personDto { get; set; }
    }
}
