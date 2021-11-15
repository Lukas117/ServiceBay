using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Contracts;
using ServiceBay.Data;
using ServiceBay.Models;
using ServiceBay.Repository;

namespace ServiceBay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBidController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuctionRepository _auctionRepo;
        private readonly IBidRepository _bidRepo;

        public ApiBidController(ApplicationDbContext context)
        {
            _context = context;
            _auctionRepo = new AuctionRepository(_context);
            _bidRepo = new BidRepository(_context);
        }

        //private readonly IBidRepository _bidRepo;

        //public ApiBidController(IBidRepository bidRepo)
        //{

        //    _bidRepo = bidRepo;

        //}

        // GET: api/ApiBid
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBid()
        {
            return await _context.Bid.ToListAsync();
        }

        // GET: api/ApiBid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBid(int id)
        {
            var bid = await _context.Bid.FindAsync(id);

            if (bid == null)
            {
                return NotFound();
            }

            return bid;
        }

        // PUT: api/ApiBid/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBid(int id, Bid bid)
        {
            if (id != bid.Id)
            {
                return BadRequest();
            }

            _context.Entry(bid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //// POST: api/ApiBid
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bid>> PostBid(Bid bid)
         {
            await _bidRepo.CreateBid(bid);
            //var auction = await _auctionRepo.GetAuction(bid.AuctionId);
            return Ok();

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

            //return NotFound();

        }

        // DELETE: api/ApiBid/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var bid = await _context.Bid.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            _context.Bid.Remove(bid);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidExists(int id)
        {
            return _context.Bid.Any(e => e.Id == id);
        }
    }
}
