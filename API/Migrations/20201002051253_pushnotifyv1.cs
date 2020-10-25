using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class pushnotifyv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "PushNotifications");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "PushNotifications");

            migrationBuilder.AddColumn<long>(
                name: "PGID",
                table: "PushNotifications",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantID",
                table: "PushNotifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PushNotifications_PGID",
                table: "PushNotifications",
                column: "PGID");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotifications_TenantID",
                table: "PushNotifications",
                column: "TenantID");

            migrationBuilder.AddForeignKey(
                name: "FK_PushNotifications_PGs_PGID",
                table: "PushNotifications",
                column: "PGID",
                principalTable: "PGs",
                principalColumn: "PGID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PushNotifications_Tenants_TenantID",
                table: "PushNotifications",
                column: "TenantID",
                principalTable: "Tenants",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushNotifications_PGs_PGID",
                table: "PushNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PushNotifications_Tenants_TenantID",
                table: "PushNotifications");

            migrationBuilder.DropIndex(
                name: "IX_PushNotifications_PGID",
                table: "PushNotifications");

            migrationBuilder.DropIndex(
                name: "IX_PushNotifications_TenantID",
                table: "PushNotifications");

            migrationBuilder.DropColumn(
                name: "PGID",
                table: "PushNotifications");

            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "PushNotifications");

            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "PushNotifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "PushNotifications",
                nullable: true);
        }
    }
}
