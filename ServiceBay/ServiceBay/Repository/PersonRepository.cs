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
            try
            {
                //encryption
                _context.Person.Add(person);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating person: '{ex.Message}'.", ex);
            }
        }

        public async Task<Person> GetPerson(int id)
        {
            try
            {
                return await _context.Person.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting person: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            try
            {
                return await _context.Person.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting people: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> UpdatePerson(int id, Person person)
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

        public async Task<int> DeletePerson(int id)
        {
            try
            {
                var person = await _context.Person.FindAsync(id);
                _context.Person.Remove(person);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting person: '{ex.Message}'.", ex);
            }
        }

        public bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }

        //TO-Do implemnt into api and mvc
        public async Task<Person> GetPersonByEmail(string email)
        {
            return await _context.Person.FirstOrDefaultAsync(x => x.Email.Equals(email));
            
        }
    }
}
