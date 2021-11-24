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
        public Task<Person> GetPerson(int id);
        public Task<int> UpdatePerson(int id, Person person);
        public Task<int> DeletePerson(int id);
        public Task<IEnumerable<Person>> GetPersons();
        public bool PersonExists(int id);
        public Task<Person> GetPersonByEmail(string email);
    }
}
