using System;

namespace ServiceBay.Dto
{
    public class AuctionForUpdateDto
    {
        public int Id { get; set; }
        public string AuctionName { get; set; }
        public string AuctionDescription { get; set; }
        public DateTime EndDate { get; set; }
    }
}
