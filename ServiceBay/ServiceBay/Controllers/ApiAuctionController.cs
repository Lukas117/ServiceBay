using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Contracts;
using ServiceBay.Dto;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepo;
        private readonly IMapper _mapper;

        public ApiAuctionController(IAuctionRepository auctionRepository, IMapper mapper)
        {
            _auctionRepo = auctionRepository;
            _mapper = mapper;
        }

        // GET: api/ApiAuction
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            return await _auctionRepo.GetAuctions();
        }

        // GET: api/ApiAuction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionForCreationDto>> GetAuction(int id)
        {
            var auction = await _auctionRepo.GetAuction(id);
            var auctionDto = _mapper.Map<AuctionForCreationDto>(auction);
            if (auction == null) { return NotFound(); }
            return auctionDto;
        }

        // PUT: api/ApiAuction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuction(int id, AuctionForUpdateDto auctionDto)
        {
            var auction = _mapper.Map<Auction>(auctionDto);
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
        public async Task<ActionResult<AuctionForCreationDto>> PostAuction(AuctionForCreationDto auctionDto)
        {
            var auction = _mapper.Map<Auction>(auctionDto);
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

        // PUT: api/ApiAuction/5
        [HttpPut("disable/{id}")]
        public async Task<IActionResult> DisableAuction(int id, AuctionForUpdateDto auctionDto)
        {
            var auction = _mapper.Map<Auction>(auctionDto);
            if (id != auction.Id)
            {
                return BadRequest();
            }

            try
            {
                await _auctionRepo.DisableAuction(id, auction);
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
    }
}
