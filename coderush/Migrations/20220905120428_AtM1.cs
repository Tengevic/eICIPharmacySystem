using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class AtM1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientGuid",
                table: "RFPCustomer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RFPRequest",
                columns: table => new
                {
                    RFPRequestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FundingApplicationGuid = table.Column<string>(nullable: true),
                    InstitutionId = table.Column<int>(nullable: false),
                    InstitutionName = table.Column<string>(nullable: true),
                    RFPCustomerId = table.Column<int>(nullable: false),
                    RFPRquestName = table.Column<string>(nullable: true),
                    RequestDate = table.Column<DateTimeOffset>(nullable: false),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFPRequest", x => x.RFPRequestId);
                });

            migrationBuilder.CreateTable(
                name: "RFPRequestLine",
                columns: table => new
                {
                    RFPRequestLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Prescription = table.Column<string>(nullable: true),
                    RFPRequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFPRequestLine", x => x.RFPRequestLineId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RFPRequest");

            migrationBuilder.DropTable(
                name: "RFPRequestLine");

            migrationBuilder.DropColumn(
                name: "PatientGuid",
                table: "RFPCustomer");
        }
    }
}
