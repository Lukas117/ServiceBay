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

namespace ServiceBay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepo;

        public ApiPersonController(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        // POST: api/ApiPerson
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
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
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
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
