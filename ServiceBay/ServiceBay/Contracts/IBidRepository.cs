using ServiceBay.Dto;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
<<<<<<< HEAD
    public interface IBidRepository : IBidNotifier
=======
    interface IBidRepository
>>>>>>> parent of 1f9bfbb (Merge branch 'main' of https://github.com/Lukas117/ServiceBay)
    {
        public Task<int> CreateBid(BidForCreationDto bidDto);
        public Task<Bid> GetBid();
        public Task<Bid> UpdateBid();
        public Task<Bid> DeleteBid();
    }
}
