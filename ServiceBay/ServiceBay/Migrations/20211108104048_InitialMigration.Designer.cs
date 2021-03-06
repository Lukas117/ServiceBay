// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceBay.Data;

namespace ServiceBay.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211108104048_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServiceBay.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityZipcode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityZipcode");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("ServiceBay.Models.Auction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuctionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuctionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartingDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("StartingPrice")
                        .HasColumnType("float");

                    b.Property<byte[]>("Version")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Auction");
                });

            modelBuilder.Entity("ServiceBay.Models.Bid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuctionId")
                        .HasColumnType("int");

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.HasIndex("BuyerId");

                    b.ToTable("Bid");
                });

            modelBuilder.Entity("ServiceBay.Models.City", b =>
                {
                    b.Property<string>("Zipcode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Zipcode");

                    b.ToTable("City");
                });

            modelBuilder.Entity("ServiceBay.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phoneno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("ServiceBay.Models.Address", b =>
                {
                    b.HasOne("ServiceBay.Models.City", "CityZipcodeNavigation")
                        .WithMany("Addresses")
                        .HasForeignKey("CityZipcode");

                    b.Navigation("CityZipcodeNavigation");
                });

            modelBuilder.Entity("ServiceBay.Models.Auction", b =>
                {
                    b.HasOne("ServiceBay.Models.Person", "Seller")
                        .WithMany("Auctions")
                        .HasForeignKey("SellerId");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("ServiceBay.Models.Bid", b =>
                {
                    b.HasOne("ServiceBay.Models.Auction", "Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionId");

                    b.HasOne("ServiceBay.Models.Person", "Buyer")
                        .WithMany("Bids")
                        .HasForeignKey("BuyerId");

                    b.Navigation("Auction");

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("ServiceBay.Models.Person", b =>
                {
                    b.HasOne("ServiceBay.Models.Address", "Address")
                        .WithMany("People")
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ServiceBay.Models.Address", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("ServiceBay.Models.Auction", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("ServiceBay.Models.City", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("ServiceBay.Models.Person", b =>
                {
                    b.Navigation("Auctions");

                    b.Navigation("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}
