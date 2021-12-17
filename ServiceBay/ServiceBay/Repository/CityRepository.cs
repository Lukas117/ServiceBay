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
                throw new Exception($"Error creating city: '{ex.Message}'.", ex);
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
                throw new Exception($"Error getting city: '{ex.Message}'.", ex);
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
                throw new Exception($"Error getting cities: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> UpdateCity(string zipcode, City city)
        {
            try
            {
                if (!CityExists(city.Zipcode))
                {
                    await CreateCity(city);
                }
                _context.Entry(city).Property(x => x.CityName).IsModified = true;
                _context.Entry(city).Property(x => x.Country).IsModified = true;
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating city: '{ex.Message}'.", ex);
            }
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
                throw new Exception($"Error deleting city: '{ex.Message}'.", ex);
            }
        }

        public bool CityExists(string zipcode)
        {
            return _context.City.Any(e => e.Zipcode.Equals(zipcode));
        }
    }
}
