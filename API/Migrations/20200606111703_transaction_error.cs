using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class transaction_error : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PGID",
                table: "TransactionErrors",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantID",
                table: "TransactionErrors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionErrors_PGID",
                table: "TransactionErrors",
                column: "PGID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionErrors_TenantID",
                table: "TransactionErrors",
                column: "TenantID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionErrors_PGs_PGID",
                table: "TransactionErrors",
                column: "PGID",
                principalTable: "PGs",
                principalColumn: "PGID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionErrors_Tenants_TenantID",
                table: "TransactionErrors",
                column: "TenantID",
                principalTable: "Tenants",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionErrors_PGs_PGID",
                table: "TransactionErrors");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionErrors_Tenants_TenantID",
                table: "TransactionErrors");

            migrationBuilder.DropIndex(
                name: "IX_TransactionErrors_PGID",
                table: "TransactionErrors");

            migrationBuilder.DropIndex(
                name: "IX_TransactionErrors_TenantID",
                table: "TransactionErrors");

            migrationBuilder.DropColumn(
                name: "PGID",
                table: "TransactionErrors");

            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "TransactionErrors");
        }
    }
}
