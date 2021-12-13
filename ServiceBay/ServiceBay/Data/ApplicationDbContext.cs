using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Models;
using ServiceBay.Dto;

namespace ServiceBay.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>()
                .Property(u => u.RowVersion)
                .IsRowVersion();
        }

        public DbSet<ServiceBay.Models.Auction> Auction { get; set; }
        public DbSet<ServiceBay.Models.Bid> Bid { get; set; }
        public DbSet<ServiceBay.Models.Person> Person { get; set; }
        public DbSet<ServiceBay.Models.Address> Address { get; set; }
        public DbSet<ServiceBay.Models.City> City { get; set; }
        public DbSet<ServiceBay.Dto.AuctionForUpdateDto> AuctionForUpdateDto { get; set; }
        public DbSet<ServiceBay.Dto.AuctionForCreationDto> AuctionForCreationDto { get; set; }
        public DbSet<ServiceBay.Models.Login> Login { get; set; }
        public DbSet<ServiceBay.Dto.PersonForCreationDto> PersonForCreationDto { get; set; }
        
    }
}
