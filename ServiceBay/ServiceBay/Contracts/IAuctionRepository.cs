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
        public Task<IEnumerable<Auction>> GetSellerAuctions(int SellerId);
        public Task<int> UpdateAuction(int id, Auction auction);
        public Task<int> DeleteAuction(int id);
        public Task<IEnumerable<Auction>> GetAuctions();
        public Task<int> DisableAuction(int id, Auction auction);
        public bool AuctionExists(int id);
        //public void UpdatePrice(int id, double price);
    }
}
