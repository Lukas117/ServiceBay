using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Models;
using ServiceBay.Dto;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ServiceBay.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ServiceBay.Models.Auction> Auction { get; set; }
        public DbSet<ServiceBay.Models.Bid> Bid { get; set; }
        public DbSet<ServiceBay.Models.Person> Person { get; set; }
        
    }
}
