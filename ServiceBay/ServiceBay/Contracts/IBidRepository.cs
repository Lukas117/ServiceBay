using ServiceBay.Dto;
using ServiceBay.Models;
using ServiceBay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
    public interface IBidRepository
    {
        public Task<int> CreateBid(Bid bid);
        public Task<Bid> GetBid(int id);
        public Task<int> UpdateBid(int id, Bid bid);
        public Task<int> DeleteBid(int id);
        public Task<IEnumerable<Bid>> GetBids();
        public bool BidExists(int id);
        public Task<IEnumerable<Bid>> GetMyBids(int buyerId);
    }
}
