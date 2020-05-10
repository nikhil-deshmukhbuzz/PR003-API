using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Rent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    RentID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    RentAmount = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    PGID = table.Column<long>(nullable: false),
                    TenantID = table.Column<long>(nullable: false),
                    PaymentStatusID = table.Column<long>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    MonthID = table.Column<long>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.RentID);
                    table.ForeignKey(
                        name: "FK_Rents_Months_MonthID",
                        column: x => x.MonthID,
                        principalTable: "Months",
                        principalColumn: "MonthID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_PGs_PGID",
                        column: x => x.PGID,
                        principalTable: "PGs",
                        principalColumn: "PGID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_PaymentStatuss_PaymentStatusID",
                        column: x => x.PaymentStatusID,
                        principalTable: "PaymentStatuss",
                        principalColumn: "PaymentStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_MonthID",
                table: "Rents",
                column: "MonthID");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_PGID",
                table: "Rents",
                column: "PGID");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_PaymentStatusID",
                table: "Rents",
                column: "PaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_TenantID",
                table: "Rents",
                column: "TenantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rents");
        }
    }
}
