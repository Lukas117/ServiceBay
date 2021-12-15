using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Auction name")]
        public string AuctionName { get; set; }
        [Display(Name = "Auction description")]
        public string AuctionDescription { get; set; }
        [Display(Name = "Start date")]
        public DateTime StartingDate { get; set; }
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Start price")]
        public double StartingPrice { get; set; }
        public int SellerId { get; set; }
        [Display(Name = "Current bid")]
        public double? Price { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Person Seller { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
