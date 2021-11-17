﻿using System;
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        //// POST: api/ApiBid
        [HttpPost]
        public async Task<ActionResult<Bid>> PostBid(Bid bid)
         {
            await _bidRepo.CreateBid(bid);
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
    }
}
