using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    DeviceID = table.Column<string>(nullable: true),
                    Titile = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    DashboardVisibility = table.Column<bool>(nullable: false),
                    IsPushNotification = table.Column<bool>(nullable: false),
                    PGID = table.Column<long>(nullable: true),
                    TenantID = table.Column<long>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_PGs_PGID",
                        column: x => x.PGID,
                        principalTable: "PGs",
                        principalColumn: "PGID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PGID",
                table: "Notifications",
                column: "PGID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TenantID",
                table: "Notifications",
                column: "TenantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
