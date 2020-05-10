using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Tenant_ChckOut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MoveOutDate",
                table: "Tenants",
                newName: "CheckOutDate");

            migrationBuilder.RenameColumn(
                name: "IsMoveOut",
                table: "Tenants",
                newName: "IsCheckOut");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCheckOut",
                table: "Tenants",
                newName: "IsMoveOut");

            migrationBuilder.RenameColumn(
                name: "CheckOutDate",
                table: "Tenants",
                newName: "MoveOutDate");
        }
    }
}
