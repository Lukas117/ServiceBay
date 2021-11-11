using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceBay.Models
{
    public partial class Person
    {
        public Person()
        {
            Auctions = new HashSet<Auction>();
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Phoneno { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int UserRole { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
