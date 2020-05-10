using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Room : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomNo = table.Column<string>(nullable: true),
                    RentAmount = table.Column<decimal>(nullable: false),
                    DepositAmount = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RoomSharingID = table.Column<long>(nullable: false),
                    PGID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_PGs_PGID",
                        column: x => x.PGID,
                        principalTable: "PGs",
                        principalColumn: "PGID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomSharings_RoomSharingID",
                        column: x => x.RoomSharingID,
                        principalTable: "RoomSharings",
                        principalColumn: "RoomSharingID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PGID",
                table: "Rooms",
                column: "PGID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomSharingID",
                table: "Rooms",
                column: "RoomSharingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
