using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class ProfileMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProfileMasterID",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ProfileMasters",
                columns: table => new
                {
                    ProfileID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileMasters", x => x.ProfileID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileMasterID",
                table: "Users",
                column: "ProfileMasterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfileMasters_ProfileMasterID",
                table: "Users",
                column: "ProfileMasterID",
                principalTable: "ProfileMasters",
                principalColumn: "ProfileID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfileMasters_ProfileMasterID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ProfileMasters");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileMasterID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileMasterID",
                table: "Users");
        }
    }
}
