using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Contracts;
using ServiceBay.Dto;
using ServiceBay.Middleware;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiAddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepo;
        private readonly IMapper _mapper;

        public ApiAddressController(IAddressRepository addressRepo, IMapper mapper)
        {
            _addressRepo = addressRepo;
            _mapper = mapper;
        }

        // GET: api/ApiAddress
        [HttpGet]
        public async Task<IEnumerable<Address>> GetAddress()
        {
            return await _addressRepo.GetAddresses();
        }

        // GET: api/ApiAddress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _addressRepo.GetAddress(id);

            if (address == null) { return NotFound(); }
            return address;
        }

        // PUT: api/ApiAddress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, AddressForCreationDto addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);

            if (id != address.Id) { return BadRequest(); }

            try
            {
                await _addressRepo.UpdateAddress(id, address);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_addressRepo.AddressExists(id))
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

        // POST: api/ApiAddress
        [HttpPost]
        public async Task<ActionResult<AddressForCreationDto>> PostAddress(AddressForCreationDto addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);
            await _addressRepo.CreateAddress(address);
            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        // DELETE: api/ApiAddress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _addressRepo.DeleteAddress(id);
            if (address == 0) { return NotFound(); }
            return NoContent();
        }
    }
}
