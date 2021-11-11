using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Data;
using ServiceBay.Dto;

namespace ServiceBay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBidController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiBidController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiBid
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidForCreationDto>>> GetBidForCreationDto()
        {
            return await _context.BidForCreationDto.ToListAsync();
        }

        // GET: api/ApiBid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BidForCreationDto>> GetBidForCreationDto(int id)
        {
            var bidForCreationDto = await _context.BidForCreationDto.FindAsync(id);

            if (bidForCreationDto == null)
            {
                return NotFound();
            }

            return bidForCreationDto;
        }

        // PUT: api/ApiBid/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBidForCreationDto(int id, BidForCreationDto bidForCreationDto)
        {
            if (id != bidForCreationDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(bidForCreationDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidForCreationDtoExists(id))
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

        // POST: api/ApiBid
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BidForCreationDto>> PostBidForCreationDto(BidForCreationDto bidForCreationDto)
        {
            _context.BidForCreationDto.Add(bidForCreationDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBidForCreationDto", new { id = bidForCreationDto.Id }, bidForCreationDto);
        }

        // DELETE: api/ApiBid/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBidForCreationDto(int id)
        {
            var bidForCreationDto = await _context.BidForCreationDto.FindAsync(id);
            if (bidForCreationDto == null)
            {
                return NotFound();
            }

            _context.BidForCreationDto.Remove(bidForCreationDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidForCreationDtoExists(int id)
        {
            return _context.BidForCreationDto.Any(e => e.Id == id);
        }
    }
}
