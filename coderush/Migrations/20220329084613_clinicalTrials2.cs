using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class clinicalTrials2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClinicalTrialsSales",
                columns: table => new
                {
                    ClinicalTrialsSalesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClinicalTrialsSalesName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(nullable: false),
                    PatientRefNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsSales", x => x.ClinicalTrialsSalesId);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalTrialsSalesLine",
                columns: table => new
                {
                    ClinicalTrialsSalesLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    ClinicalTrialsProductsId = table.Column<int>(nullable: false),
                    ClinicalTrialsSalesId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DiscountAmount = table.Column<double>(nullable: false),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsSalesLine", x => x.ClinicalTrialsSalesLineId);
                    table.ForeignKey(
                        name: "FK_ClinicalTrialsSalesLine_ClinicalTrialsSales_ClinicalTrialsSalesId",
                        column: x => x.ClinicalTrialsSalesId,
                        principalTable: "ClinicalTrialsSales",
                        principalColumn: "ClinicalTrialsSalesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsSalesLine_ClinicalTrialsSalesId",
                table: "ClinicalTrialsSalesLine",
                column: "ClinicalTrialsSalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicalTrialsSalesLine");

            migrationBuilder.DropTable(
                name: "ClinicalTrialsSales");
        }
    }
}
