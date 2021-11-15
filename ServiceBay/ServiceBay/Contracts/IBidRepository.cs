using ServiceBay.Dto;
using ServiceBay.Models;
using ServiceBay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
    public interface IBidRepository : IBidNotifier
    {
        public Task<int> CreateBid(Bid bid);
        public Task<Bid> GetBid();
        public Task<Bid> UpdateBid();
        public Task<Bid> DeleteBid();
        //public Task<int> UpdatePrice(int id, double price);

    }
}
