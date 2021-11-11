using ServiceBay.Contracts;
using ServiceBay.Data;
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
        private readonly ApplicationDbContext _context;

        public AuctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAuction(AuctionForCreationDto auctionDto)
        {

            _context.AuctionForCreationDto.Add(auctionDto);
            return await _context.SaveChangesAsync();
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
