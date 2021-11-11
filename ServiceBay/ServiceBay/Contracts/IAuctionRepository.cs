using ServiceBay.Dto;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
    public interface IAuctionRepository
    {
        public Task<int> CreateAuction(Auction auction);
        public Task<Auction> GetAuction(int id);
        public Task<Auction> UpdateAuction();
        public Task<bool> DeleteAuction(int id);
        public Task<IEnumerable<Auction>> GetAuctions();
    }
}
