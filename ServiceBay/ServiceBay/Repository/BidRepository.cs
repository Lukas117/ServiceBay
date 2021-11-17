﻿using ServiceBay.Contracts;
using ServiceBay.Data;
using ServiceBay.Dto;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Services;
using System.Collections;

namespace ServiceBay.Repository
{
    public class BidRepository : IBidRepository
    {
        public List<IBidObserver> observers = new List<IBidObserver>();
        private readonly ApplicationDbContext _context;
        private readonly IAuctionRepository _auctionRepo;

        public BidRepository(ApplicationDbContext context)
        {
            _context = context;
            _auctionRepo = new AuctionRepository(_context);
        }

        public void Attach(IBidObserver observer)
        {
            observers.Add(observer);
        }

        public async Task<int> CreateBid(Bid bid)
        {
            var auction = await _auctionRepo.GetAuction(bid.AuctionId);
            var version = _context.Entry(auction).CurrentValues["RowVersion"];
            try
            {
                if (auction.SellerId != bid.BuyerId && auction.Price < bid.Price && auction.StartingPrice < bid.Price && auction.EndDate >= DateTime.Now)
                {
                    _context.Add(bid);
                    auction.Price = bid.Price;
                    _context.Auction.Attach(auction);
                    _context.Entry(auction).Property(x => x.Price).IsModified = true;
                    var rowVersion = _context.Entry(auction).CurrentValues["RowVersion"];
                    if (StructuralComparisons.StructuralEqualityComparer.Equals(version, rowVersion))
                    {
                        return await _context.SaveChangesAsync();
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("The record you attempted to edit "
                + "was modified by another user after you got the original value. The"
                + "edit operation was canceled and the current values in the database "
                + "have been displayed. If you still want to edit this record, click "
                + "the Save button again. Otherwise click the Back to List hyperlink.");
                return 0;
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
