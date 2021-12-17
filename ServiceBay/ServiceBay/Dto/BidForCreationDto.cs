using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Dto
{
    public class BidForCreationDto
    {
        //public int Id { get; set; }
        public double Price { get; set; }
        public int BuyerId { get; set; }
        public int AuctionId { get; set; }
    }
}
