using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
    public interface IAddressRepository
    {
        public Task<int> CreateAddress(Address address);
        public Task<Address> GetAddress(int id);
        public Task<int> UpdateAddress(int id, Address address);
        public Task<int> DeleteAddress(int id);
        public Task<IEnumerable<Address>> GetAddresses();
        public bool AddressExists(int id);
    }
}
