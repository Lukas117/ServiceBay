using Microsoft.EntityFrameworkCore;
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
        public async Task<int> CreateAuction(Auction auction)
        {
            _context.Auction.Add(auction);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAuction(int id)
        {
            var auction = await _context.Auction.FindAsync(id);
            _context.Auction.Remove(auction);
            return await _context.SaveChangesAsync();
        }

        public async Task<Auction> GetAuction(int id)
        {
            var auction = await _context.Auction.FindAsync(id);
            return auction;
        }

        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            return await _context.Auction.ToListAsync();
        }

        public Task<Auction> UpdateAuction()
        {
            throw new NotImplementedException();
        }

        public void UpdatePrice(int id, double price)
        {
            
            var auction = new Auction() { Id = id, Price = price };
            using (var db = _context)
            {
                db.Auction.Attach(auction);
                db.Entry(auction).Property(x => x.Price).IsModified = true;
                db.SaveChanges();
            }
        }
    }
}
