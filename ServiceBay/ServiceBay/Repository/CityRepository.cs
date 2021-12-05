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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCity(City city)
        {
            try
            {
                if (CityExists(city.Zipcode))
                {
                    return 0;
                }
                else
                {
                    _context.City.Add(city);
                    return await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating person: '{ex.Message}'.", ex);
            }
        }

        public async Task<City> GetCity(string zipcode)
        {
            try
            {
                return await _context.City.FindAsync(zipcode);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting person: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            try
            {
                return await _context.City.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting people: '{ex.Message}'.", ex);
            }
        }

        public Task<int> UpdateCity(string zipcode, City city)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteCity(string zipcode)
        {
            try
            {
                var city = await _context.City.FindAsync(zipcode);
                _context.City.Remove(city);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting person: '{ex.Message}'.", ex);
            }
        }

        public bool CityExists(string zipcode)
        {
            return _context.City.Any(e => e.Zipcode.Equals(zipcode));
        }
    }
}
