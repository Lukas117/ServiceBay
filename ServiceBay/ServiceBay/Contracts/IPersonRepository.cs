using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Contracts
{
    public interface IPersonRepository
    {
        public Task<Person> GetPerson(int id);
    }
}
