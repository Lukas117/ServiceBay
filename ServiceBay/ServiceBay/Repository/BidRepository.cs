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
            //AuctionRepository _auctionRepo = new AuctionRepository(_context);
            var auction = await _auctionRepo.GetAuction(bid.AuctionId);
            var version = _context.Entry(auction).OriginalValues["RowVersion"];
            try
            {
                if (auction.SellerId != bid.BuyerId && auction.Price < bid.Price && auction.StartingPrice < bid.Price && auction.EndDate >= DateTime.Now)
                {
                    _context.Add(bid);
                    //_auctionRepo.UpdatePrice(bid.AuctionId, bid.Price);
                    auction.Price = bid.Price;
                    _context.Auction.Attach(auction);
                    _context.Entry(auction).Property(x => x.Price).IsModified = true;
                    if (version == _context.Entry(auction).OriginalValues["RowVersion"])
                    {
                        return await _context.SaveChangesAsync();
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                
                if (auction.RowVersion.Equals(version)) Console.WriteLine("The record you attempted to edit "
        + "was modified by another user after you got the original value. The"
        + "edit operation was canceled and the current values in the database "
        + "have been displayed. If you still want to edit this record, click "
        + "the Save button again. Otherwise click the Back to List hyperlink.");
                return 0;
            }
            return 0;
        }
        //if (auction != null)
        //{

        //    try
        //    {
        //        if (auction.SellerId != bid.BuyerId && auction.Price < bid.Price && auction.StartingPrice < bid.Price && auction.EndDate >= DateTime.Now)
        //        {
        //            _context.Bid.Add(bid);
        //            await _context.SaveChangesAsync();
        //            //ApiAuctionController apiAuction = new ApiAuctionController(_context);
        //            _auctionRepo.UpdatePrice(bid.AuctionId, bid.Price);
        //            return CreatedAtAction("GetBid", new { id = bid.Id }, bid);
        //        }
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        var version = _context.Entry(auction).OriginalValues["RowVersion"];
        //        if (auction.RowVersion.Equals(version)) ModelState.AddModelError(string.Empty, "The record you attempted to edit "
        //+ "was modified by another user after you got the original value. The"
        //+ "edit operation was canceled and the current values in the database "
        //+ "have been displayed. If you still want to edit this record, click "
        //+ "the Save button again. Otherwise click the Back to List hyperlink.");
        //        return BadRequest();

        //    }

        //}

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
