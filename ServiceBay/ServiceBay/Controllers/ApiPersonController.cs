using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Contracts;
using ServiceBay.Dto;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepo;
        private readonly IMapper _mapper;

        public ApiPersonController(IPersonRepository personRepo, IMapper mapper)
        {
            _personRepo = personRepo;
            _mapper = mapper;
        }

        // POST: api/ApiPerson
        [HttpPost]
        public async Task<ActionResult<PersonForCreationDto>> CreatePerson(PersonForCreationDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            await _personRepo.CreatePerson(person);
            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // GET: api/ApiPerson
        [HttpGet]
         public async Task<IEnumerable<Person>> GetPersons()
        {
            return await _personRepo.GetPersons();
        }

        // GET: api/ApiPerson/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _personRepo.GetPerson(id);

            if (person == null) { return NotFound(); }
            return person;
        }

        // PUT: api/ApiPerson/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, PersonForCreationDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);

            if (id != person.Id) { return BadRequest(); }

            try
            {
                await _personRepo.UpdatePerson(id, person);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_personRepo.PersonExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        // DELETE: api/ApiPerson/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _personRepo.DeletePerson(id);
            if (person == 0) { return NotFound(); }
            return NoContent();
        }
    }
}
