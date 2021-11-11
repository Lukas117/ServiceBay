using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Data;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuctionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiAuctionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiAuction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuction()
        {
            return await _context.Auction.ToListAsync();
        }

        // GET: api/ApiAuction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auction>> GetAuction(int id)
        {
            var auction = await _context.Auction.FindAsync(id);

            if (auction == null)
            {
                return NotFound();
            }

            return auction;
        }

        // PUT: api/ApiAuction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuction(int id, Auction auction)
        {
            if (id != auction.Id)
            {
                return BadRequest();
            }

            _context.Entry(auction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionExists(id))
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

        // POST: api/ApiAuction
        [HttpPost]
        public async Task<ActionResult<Auction>> PostAuction(Auction auction)
        {
            _context.Auction.Add(auction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuction", new { id = auction.Id }, auction);
        }

        // DELETE: api/ApiAuction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            var auction = await _context.Auction.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            _context.Auction.Remove(auction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuctionExists(int id)
        {
            return _context.Auction.Any(e => e.Id == id);
        }
    }
}
