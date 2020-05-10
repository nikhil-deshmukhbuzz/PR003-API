using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Registration_MobileNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobileNo",
                table: "Registrations",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MobileNo",
                table: "Registrations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
