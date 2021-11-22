using Microsoft.EntityFrameworkCore;
using ServiceBay.Contracts;
using ServiceBay.Data;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePerson(Person person)
        {
            _context.Person.Add(person);
            return await _context.SaveChangesAsync();
        }

        public async Task<Person> GetPerson(string id)
        {
            return await _context.Person.FindAsync(id);
        }

        public async Task<int> UpdatePerson(string id, Person person)
        {
            try
            {
                _context.Entry(person).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating person: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> DeletePerson(string id)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await _context.Person.ToListAsync();
        }

        public bool PersonExists(string id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
