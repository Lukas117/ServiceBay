using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Dto
{
    public class AuctionForCreationDto
    {
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
    }
}
