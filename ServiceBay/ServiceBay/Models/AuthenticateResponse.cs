using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBay.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Person user, string token)
        {
            Id = user.Id;
            FirstName = user.Fname;
            LastName = user.Lname;
            Token = token;
        }
    }
}
