 using ServiceBay.Contracts;
using ServiceBay.Data;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Services;
using ServiceBay.Security;

namespace ServiceBay.Repository
{
    public class BidRepository : IBidRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuctionRepository _auctionRepo;
        private readonly EmailService emailService;
        private readonly IPersonRepository personRepository;

        public BidRepository(ApplicationDbContext context)
        {
            _context = context;
            _auctionRepo = new AuctionRepository(_context);
            emailService = new EmailService();
            personRepository = new PersonRepository(_context);
        }

        public async Task<int> CreateBid(Bid bid)
        {
            var auction = await _auctionRepo.GetAuction(bid.AuctionId);
            try
            {
                if (auction.SellerId != bid.BuyerId && auction.Price < bid.Price && auction.StartingPrice < bid.Price && auction.EndDate >= DateTime.Now)
                {
                    auction.Price = bid.Price;
                    _context.Add(bid);
                    _context.Auction.Attach(auction);
                    _context.Entry(auction).Property(x => x.Price).IsModified = true;

                    var lastBid = bid.Auction.Bids.OrderByDescending(x => x.Price).First();
                    var lastBidder = await personRepository.GetPerson(lastBid.BuyerId);
                    if (lastBid != null && lastBidder != null) { NotifyBuyer(bid, lastBidder.Email); }
                    var seller = await personRepository.GetPerson(auction.SellerId);
                    NotifySeller(bid, seller.Email);
                    lastBid = bid;

                    return await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException($"Error creating bid: '{ex.Message}'.", ex);
            }
            return 0;
        }

        public async Task<Bid> GetBid(int id)
        {
            try
            {
                return await _context.Bid.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting bid: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<Bid>> GetBids()
        {
            try
            {
                return await _context.Bid.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting bids: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<Bid>> GetMyBids(int buyerId)
        {
            try
            {
                return await _context.Bid.Where(a => a.BuyerId == buyerId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting auctions: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> UpdateBid(int id, Bid bid)
        {
            try
            {
                _context.Entry(bid).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException($"Error updating bid: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> DeleteBid(int id)
        {
            var bid = await _context.Bid.FindAsync(id);
            try
            {
                _context.Bid.Remove(bid);
                return await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new Exception($"Error getting bids: '{ex.Message}'.", ex);
            }
        }

        public void NotifyBuyer(Bid bid, String email)
        {
            emailService.SendUpdateEmail(bid, email);
        }

        public void NotifySeller(Bid bid, String email)
        {
            emailService.SendSellerEmail(bid, email);
        }    

        public bool BidExists(int id)
        {
            return _context.Bid.Any(e => e.Id == id);
        }
    }
}
