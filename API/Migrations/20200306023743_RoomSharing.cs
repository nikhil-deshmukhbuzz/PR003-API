using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RoomSharing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomSharings",
                columns: table => new
                {
                    RoomSharingID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<long>(nullable: false),
                    NoOfBed = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PGID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSharings", x => x.RoomSharingID);
                    table.ForeignKey(
                        name: "FK_RoomSharings_PGs_PGID",
                        column: x => x.PGID,
                        principalTable: "PGs",
                        principalColumn: "PGID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomSharings_PGID",
                table: "RoomSharings",
                column: "PGID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomSharings");
        }
    }
}
