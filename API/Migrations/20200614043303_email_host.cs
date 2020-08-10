using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class email_host : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductCode = table.Column<string>(nullable: true),
                    CustomerCode = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Attachement = table.Column<byte[]>(nullable: true),
                    Attachement1 = table.Column<byte[]>(nullable: true),
                    Attachement2 = table.Column<byte[]>(nullable: true),
                    AttachementName = table.Column<string>(nullable: true),
                    Attachement1Name = table.Column<string>(nullable: true),
                    Attachement2Name = table.Column<string>(nullable: true),
                    IsError = table.Column<bool>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true),
                    Attempt = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailID);
                });

            migrationBuilder.CreateTable(
                name: "Hosted_Service_Urls",
                columns: table => new
                {
                    Hosted_Service_Url_ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServiceName = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    Enviorment = table.Column<string>(nullable: true),
                    IsActive = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosted_Service_Urls", x => x.Hosted_Service_Url_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Hosted_Service_Urls");
        }
    }
}
