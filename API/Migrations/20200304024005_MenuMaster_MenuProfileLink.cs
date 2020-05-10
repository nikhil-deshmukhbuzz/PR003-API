using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class MenuMaster_MenuProfileLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuMasters",
                columns: table => new
                {
                    MenuID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuName = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    IsMobile = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SequenceNo = table.Column<int>(nullable: false),
                    ParentMenuID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuMasters", x => x.MenuID);
                });

            migrationBuilder.CreateTable(
                name: "MenuProfileLinks",
                columns: table => new
                {
                    MenuProfileLinkID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfileID = table.Column<long>(nullable: false),
                    MenuID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuProfileLinks", x => x.MenuProfileLinkID);
                    table.ForeignKey(
                        name: "FK_MenuProfileLinks_MenuMasters_MenuID",
                        column: x => x.MenuID,
                        principalTable: "MenuMasters",
                        principalColumn: "MenuID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuProfileLinks_ProfileMasters_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "ProfileMasters",
                        principalColumn: "ProfileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuProfileLinks_MenuID",
                table: "MenuProfileLinks",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuProfileLinks_ProfileID",
                table: "MenuProfileLinks",
                column: "ProfileID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuProfileLinks");

            migrationBuilder.DropTable(
                name: "MenuMasters");
        }
    }
}
