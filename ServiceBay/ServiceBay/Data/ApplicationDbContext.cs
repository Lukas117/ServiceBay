using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Models;

namespace ServiceBay.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ServiceBay.Models.Auction> Auction { get; set; }
        
    }
}
