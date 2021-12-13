using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
