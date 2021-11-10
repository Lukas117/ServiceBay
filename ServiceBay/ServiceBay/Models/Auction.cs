﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceBay.Models
{
    public partial class Auction
    {
        public Auction()
        {
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public string AuctionName { get; set; }
        public string AuctionDescription { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
        public double StartingPrice { get; set; }
        public int SellerId { get; set; }
        public double? Price { get; set; }
        public DateTime Version { get; set; }

        public virtual Person Seller { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
