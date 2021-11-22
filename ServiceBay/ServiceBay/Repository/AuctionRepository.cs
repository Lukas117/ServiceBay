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
           return await _context.Auction.FindAsync(id);
        }

        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            return await _context.Auction.ToListAsync();
        }

        public async Task<int> UpdateAuction(int id, Auction auction)
        {
            try {
                //_context.Entry(auction).State = EntityState.Modified;
                _context.Entry(auction).Property(x => x.AuctionName).IsModified = true;
                _context.Entry(auction).Property(x => x.AuctionDescription).IsModified = true;
                _context.Entry(auction).Property(x => x.EndDate).IsModified = true;
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating auction: '{ex.Message}'.", ex);
            }
        }
        public async Task<int> DisableAuction(int id, Auction auction)
        {
            try
            {
                if (auction != null && auction.EndDate >= DateTime.Now)
                {
                    auction.EndDate = DateTime.Now;
                    //_context.Auction.Attach(auction);
                    _context.Entry(auction).Property(x => x.EndDate).IsModified = true;
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating auction: '{ex.Message}'.", ex);
            }
        }

        public bool AuctionExists(int id)
        {
            return _context.Auction.Any(e => e.Id == id);
        }

        //public void UpdatePrice(int id, double price)
        //{
        //    var auction = new Auction() { Id = id, Price = price };
        //    _context.Auction.Attach(auction);
        //    _context.Entry(auction).Property(x => x.Price).IsModified = true;
        //    _context.SaveChangesAsync();
        //}
    }
}
