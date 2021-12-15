using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Contracts;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBidController : ControllerBase
    {
        private readonly IBidRepository _bidRepo;

        public ApiBidController(IBidRepository bidRepo)
        {
            _bidRepo = bidRepo;
        }

        // GET: api/ApiBid
        [HttpGet]
        public async Task<IEnumerable<Bid>> GetBids()
        {
            return await _bidRepo.GetBids();
        }

        // GET: api/ApiBid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBid(int id)
        {
            var bid = await _bidRepo.GetBid(id);
            if (bid == null) { return NotFound(); }
            return bid;
        }

        // PUT: api/ApiBid/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBid(int id, Bid bid)
        {
            if (id != bid.Id) { return BadRequest(); }

            try
            {
                await _bidRepo.UpdateBid(id, bid);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_bidRepo.BidExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        // POST: api/ApiBid
        [HttpPost]
        public async Task<ActionResult<Bid>> PostBid(Bid bid)
        {
            await _bidRepo.CreateBid(bid);
            //return CreatedAtAction("GetBid", new { id = bid.Id }, bid);
            return Ok();
        }

        // DELETE: api/ApiBid/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var bid = await _bidRepo.DeleteBid(id);
            if (bid == 0) { return NotFound(); }
            return NoContent();
        }

        //
        [HttpGet("User")]
        public async Task<IEnumerable<Bid>> GetUsersBids()
        {
            int id = StaticVar.currentUser.Id;
            return await _bidRepo.GetMyBids(id);
        }
    }
}
