using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RoomSharing_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RoomSharings",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Name",
                table: "RoomSharings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
