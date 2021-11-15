using ServiceBay.Contracts;
using ServiceBay.Data;
using ServiceBay.Dto;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Services;

namespace ServiceBay.Repository
{
    public class BidRepository : IBidRepository
    {
        public List<IBidObserver> observers = new List<IBidObserver>();
        private readonly ApplicationDbContext _context;

        public BidRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Attach(IBidObserver observer)
        {
            observers.Add(observer);
        }

        public async Task<int> CreateBid(Bid bid)
        {
            AuctionRepository _auctionRepo = new AuctionRepository(_context);
            var auction = await _auctionRepo.GetAuction(bid.AuctionId);
            if (auction.SellerId != bid.BuyerId && auction.Price < bid.Price && auction.StartingPrice < bid.Price && auction.EndDate >= DateTime.Now)
            {
                _context.Add(bid);
                _auctionRepo.UpdatePrice(bid.AuctionId, bid.Price);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public Task<Bid> DeleteBid()
        {
            throw new NotImplementedException();
        }

        public void Detach(IBidObserver observer)
        {
            observers.Remove(observer);
        }

        public Task<Bid> GetBid()
        {
            throw new NotImplementedException();
        }

        public void Notify(Bid bid)
        {
            foreach (var observer in observers)
            {
                observer.updateBid(bid);
            }
        }

        public Task<Bid> UpdateBid()
        {
            throw new NotImplementedException();
        }
    }
}
