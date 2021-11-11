using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceBay.Migrations
{
    public partial class rowversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Auction");

            migrationBuilder.AddColumn<System.Data.DataRowVersion>(
                name: "RowVersion",
                table: "Auction",
                type: "rowversion",
                nullable: false
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Address_AddressId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "BidForCreationDto");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "AuctionForCreationDto");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "AuctionForCreationDto");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Auction");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Person",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "StartingPrice",
                table: "AuctionForCreationDto",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<DateTime>(
                name: "Version",
                table: "Auction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Address_AddressId",
                table: "Person",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
