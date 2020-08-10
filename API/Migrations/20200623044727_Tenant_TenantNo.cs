using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Tenant_TenantNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantNo",
                table: "Tenants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantNo",
                table: "Tenants");
        }
    }
}
