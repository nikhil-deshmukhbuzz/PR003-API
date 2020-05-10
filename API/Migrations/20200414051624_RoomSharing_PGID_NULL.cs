using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RoomSharing_PGID_NULL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomSharings_PGs_PGID",
                table: "RoomSharings");

            migrationBuilder.AlterColumn<long>(
                name: "PGID",
                table: "RoomSharings",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSharings_PGs_PGID",
                table: "RoomSharings",
                column: "PGID",
                principalTable: "PGs",
                principalColumn: "PGID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomSharings_PGs_PGID",
                table: "RoomSharings");

            migrationBuilder.AlterColumn<long>(
                name: "PGID",
                table: "RoomSharings",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSharings_PGs_PGID",
                table: "RoomSharings",
                column: "PGID",
                principalTable: "PGs",
                principalColumn: "PGID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
