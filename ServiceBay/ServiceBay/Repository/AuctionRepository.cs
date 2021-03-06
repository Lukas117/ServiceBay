using Microsoft.EntityFrameworkCore;
using ServiceBay.Contracts;
using ServiceBay.Data;
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
            try
            {
                StaticVar.prevPrices.Add(auction.StartingPrice);
                auction.Price = auction.StartingPrice;
                _context.Auction.Add(auction);
                return await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException($"Error creating auction: '{ex.Message}'.", ex);
            }
        }

        public async Task<Auction> GetAuction(int id)
        {
            try
            {
                return await _context.Auction.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting auction: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            try
            {
                return await _context.Auction.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting auctions: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<Auction>> GetSellerAuctions(int sellerId)
        {
            try
            {
                return await _context.Auction.Where(a => a.SellerId == sellerId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting auctions: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> UpdateAuction(int id, Auction auction)
        {
            try
            {

                _context.Entry(auction).Property(x => x.AuctionName).IsModified = true;
                _context.Entry(auction).Property(x => x.AuctionDescription).IsModified = true;
                _context.Entry(auction).Property(x => x.EndDate).IsModified = true;
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException($"Error updating auction: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> DeleteAuction(int id)
        {
            try
            {
                var auction = await _context.Auction.FindAsync(id);
                if (StaticVar.currentUser.Id != auction.SellerId && StaticVar.currentUser.UserRole == 0) return 0;
                else
                {
                    _context.Bid.RemoveRange
                    (_context.Bid.Where(b => auction.Id == b.AuctionId));
                    _context.Auction.Remove(auction);
                    return await _context.SaveChangesAsync();
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting auction: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> DisableAuction(int id, Auction auction)
        {
            try
            {
                if (auction != null && auction.EndDate >= DateTime.Now)
                {
                    auction.EndDate = DateTime.Now;
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
    }
}
