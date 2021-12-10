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
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<int> CreateAddress(Address address)
        {
            try
            {
                _context.Address.Add(address);
                return await _context.SaveChangesAsync();
                int id = address.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating person: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> DeleteAddress(int id)
        {
            try
            {
                var address = await _context.Address.FindAsync(id);
                _context.Address.Remove(address);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting person: '{ex.Message}'.", ex);
            }
        }

        public async Task<Address> GetAddress(int id)
        {
            try
            {
                return await _context.Address.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting person: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<Address>> GetAddresses()
        {
            try
            {
                return await _context.Address.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting people: '{ex.Message}'.", ex);
            }
        }

        public Task<int> UpdateAddress(int id, Address address)
        {
            throw new NotImplementedException();
        }

        public bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
    }
}
