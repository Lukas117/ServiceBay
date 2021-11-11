using ServiceBay.Dto;
using ServiceBay.Models;
using ServiceBay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
    interface IBidRepository : IBidNotifier
    {
        public Task<int> CreateBid(BidForCreationDto bidDto);
        public Task<Bid> GetBid();
        public Task<Bid> UpdateBid();
        public Task<Bid> DeleteBid();
    }
}
