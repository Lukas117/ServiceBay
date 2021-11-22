using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
    public interface IPersonRepository
    {
        public Task<int> CreatePerson(Person person);
        public Task<Person> GetPerson(string id);
        public Task<int> UpdatePerson(string id, Person person);
        public Task<int> DeletePerson(string id);
        public Task<IEnumerable<Person>> GetPersons();
        public bool PersonExists(string id);
    }
}
