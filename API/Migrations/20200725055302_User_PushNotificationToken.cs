using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class User_PushNotificationToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PushNotificationToken",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PushNotificationToken",
                table: "Users");
        }
    }
}
