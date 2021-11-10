using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceBay.Models
{
    public partial class Bid
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int BuyerId { get; set; }
        public int AuctionId { get; set; }

        public virtual Auction Auction { get; set; }
        public virtual Person Buyer { get; set; }
    }
}
