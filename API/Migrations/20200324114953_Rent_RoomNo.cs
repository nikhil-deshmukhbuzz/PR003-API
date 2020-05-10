using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Rent_RoomNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomNo",
                table: "Rents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNo",
                table: "Rents");
        }
    }
}
