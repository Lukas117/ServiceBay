using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Dto
{
    public class AuctionForCreationDto
    {
        public int Id { get; set; }
        public string AuctionName { get; set; }
        public string AuctionDescription { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
        public double? StartingPrice { get; set; }
    }
}
