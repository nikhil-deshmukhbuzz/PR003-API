using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class pushnotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config_PushNotifications");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.AddColumn<bool>(
                name: "Push",
                table: "PushNotifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "PushNotifications",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Push",
                table: "PushNotifications");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "PushNotifications");

            migrationBuilder.CreateTable(
                name: "Config_PushNotifications",
                columns: table => new
                {
                    Config_PushNotificationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enviorment = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    SenderID = table.Column<string>(nullable: true),
                    ServerKey = table.Column<string>(nullable: true),
                    WebAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config_PushNotifications", x => x.Config_PushNotificationID);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DashboardVisibility = table.Column<bool>(nullable: false),
                    DeviceID = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: true),
                    IsPushNotification = table.Column<bool>(nullable: false),
                    MobileNo = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PGID = table.Column<long>(nullable: true),
                    TenantID = table.Column<long>(nullable: true),
                    Titile = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
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
    }
}
