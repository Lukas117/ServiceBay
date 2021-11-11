using ServiceBay.Contracts;
using ServiceBay.Data;
using ServiceBay.Dto;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiceBay.Repository
{
    public class BidRepository : IBidRepository
    {
        private readonly ApplicationDbContext _context;

        public BidRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBid(BidForCreationDto bidDto)
        {
            _context.Add(bidDto);
            return await _context.SaveChangesAsync();
        }

        public Task<Bid> DeleteBid()
        {
            throw new NotImplementedException();
        }

        public Task<Bid> GetBid()
        {
            throw new NotImplementedException();
        }

        public Task<Bid> UpdateBid()
        {
            throw new NotImplementedException();
        }
    }
}
