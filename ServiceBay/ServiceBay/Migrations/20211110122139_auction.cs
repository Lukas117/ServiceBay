using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceBay.Migrations
{
    public partial class auction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
               name: "AuctionName",
               table: "Auction",
               type: "nvarchar(50)",
               nullable: false,
               
               oldType: "nvarchar(50)",
               oldNullable: true);

            migrationBuilder.AlterColumn<string>(
               name: "AuctionDescription",
               table: "Auction",
               type: "nvarchar(255)",
               nullable: false,

               oldType: "nvarchar(50)",
               oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
