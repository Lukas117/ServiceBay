using ServiceBay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
    public interface ICityRepository
    {
        public Task<int> CreateCity(City city);
        public Task<City> GetCity(string zipcode);
        public Task<int> UpdateCity(string zipcode, City city);
        public Task<int> DeleteCity(string zipcode);
        public Task<IEnumerable<City>> GetCities();
        public bool CityExists(string zipcode);
    }
}
