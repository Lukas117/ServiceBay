using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Contracts;
using ServiceBay.Data;
using ServiceBay.Dto;

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

        private readonly IAuctionRepository _auctionRepo;

        public ApiAuctionController(IAuctionRepository auctionRepo)
        {

            _auctionRepo = auctionRepo;

        }

        // GET: api/ApiAuction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionForCreationDto>>> GetAuctionForCreationDto()
        {
            return await _context.AuctionForCreationDto.ToListAsync();
        }

        // GET: api/ApiAuction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionForCreationDto>> GetAuctionForCreationDto(int id)
        {
            var auctionForCreationDto = await _context.AuctionForCreationDto.FindAsync(id);

            if (auctionForCreationDto == null)
            {
                return NotFound();
            }

            return auctionForCreationDto;
        }

        // PUT: api/ApiAuction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuctionForCreationDto(int id, AuctionForCreationDto auctionForCreationDto)
        {
            if (id != auctionForCreationDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(auctionForCreationDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionForCreationDtoExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuctionForCreationDto>> PostAuctionForCreationDto(AuctionForCreationDto auctionForCreationDto)
        {
            await _auctionRepo.CreateAuction(auctionForCreationDto);

            return CreatedAtAction("GetAuctionForCreationDto", new { id = auctionForCreationDto.Id }, auctionForCreationDto);
        }

        // DELETE: api/ApiAuction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuctionForCreationDto(int id)
        {
            var auctionForCreationDto = await _context.AuctionForCreationDto.FindAsync(id);
            if (auctionForCreationDto == null)
            {
                return NotFound();
            }

            _context.AuctionForCreationDto.Remove(auctionForCreationDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuctionForCreationDtoExists(int id)
        {
            return _context.AuctionForCreationDto.Any(e => e.Id == id);
        }
    }
}
