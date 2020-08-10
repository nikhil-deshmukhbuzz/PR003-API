using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class ValidityInDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidityInMonth",
                table: "Suscriptions",
                newName: "ValidityInDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidityInDays",
                table: "Suscriptions",
                newName: "ValidityInMonth");
        }
    }
}
