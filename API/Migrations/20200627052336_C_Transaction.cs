using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class C_Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "C_Transactions",
                columns: table => new
                {
                    C_TransactionID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductCode = table.Column<string>(nullable: true),
                    CustomerCode = table.Column<string>(nullable: true),
                    PaymentID = table.Column<string>(nullable: true),
                    OrderID = table.Column<string>(nullable: true),
                    Signature = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    PayeeName = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<string>(nullable: true),
                    PaymentType = table.Column<string>(nullable: true),
                    TransactionStep = table.Column<string>(nullable: true),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    ValidityInMonth = table.Column<int>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Transactions", x => x.C_TransactionID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "C_Transactions");
        }
    }
}
