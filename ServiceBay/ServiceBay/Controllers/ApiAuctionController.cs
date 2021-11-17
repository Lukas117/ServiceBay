using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ApiAuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepo;

        public ApiAuctionController(IAuctionRepository auctionRepository)
        {
            _auctionRepo = auctionRepository;
        }

        // GET: api/ApiAuction
        [HttpGet]
        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            return await _auctionRepo.GetAuctions();
        }

        // GET: api/ApiAuction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auction>> GetAuction(int id)
        {
            var auction = await _auctionRepo.GetAuction(id);
            if (auction == null) { return NotFound(); }
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

            try
            {
                await _auctionRepo.UpdateAuction(id, auction);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_auctionRepo.AuctionExists(id))
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
            await _auctionRepo.CreateAuction(auction);
            return CreatedAtAction("GetAuction", new { id = auction.Id }, auction);
        }

        // DELETE: api/ApiAuction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            var auction = await _auctionRepo.DeleteAuction(id);
            if (auction == 0) { return NotFound(); }
            return NoContent();
        } 
    }
}
