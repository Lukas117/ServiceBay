using ServiceBay.Contracts;
using ServiceBay.Dto;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Repository
{
    public class AuctionRepository : IAuctionRepository
    {
        public Task<AuctionForCreationDto> CreateAuction(AuctionForCreationDto auctionDto)
        {
            throw new NotImplementedException();
        }

        public Task<Auction> DeleteAuction()
        {
            throw new NotImplementedException();
        }

        public Task<Auction> GetAuction()
        {
            throw new NotImplementedException();
        }

        public Task<Auction> UpdateAuction()
        {
            throw new NotImplementedException();
        }
    }
}
