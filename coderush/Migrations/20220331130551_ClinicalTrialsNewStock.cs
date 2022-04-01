using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class ClinicalTrialsNewStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicalTrialsStock");

            migrationBuilder.AddColumn<int>(
                name: "ClinicalTrialsDonationLineId",
                table: "ClinicalTrialsSalesLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Deficit",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Expired",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InStock",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalRecieved",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalSales",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Expired",
                table: "ClinicalTrialsDonationLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InStock",
                table: "ClinicalTrialsDonationLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sold",
                table: "ClinicalTrialsDonationLine",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicalTrialsDonationLineId",
                table: "ClinicalTrialsSalesLine");

            migrationBuilder.DropColumn(
                name: "Deficit",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropColumn(
                name: "Expired",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropColumn(
                name: "TotalRecieved",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropColumn(
                name: "TotalSales",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropColumn(
                name: "Expired",
                table: "ClinicalTrialsDonationLine");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "ClinicalTrialsDonationLine");

            migrationBuilder.DropColumn(
                name: "Sold",
                table: "ClinicalTrialsDonationLine");

            migrationBuilder.CreateTable(
                name: "ClinicalTrialsStock",
                columns: table => new
                {
                    ClinicalTrialsStockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClinicalTrialsProductsId = table.Column<int>(nullable: false),
                    Deficit = table.Column<double>(nullable: false),
                    InStock = table.Column<double>(nullable: false),
                    TotalRecieved = table.Column<double>(nullable: false),
                    TotalSales = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsStock", x => x.ClinicalTrialsStockId);
                    table.ForeignKey(
                        name: "FK_ClinicalTrialsStock_ClinicalTrialsProducts_ClinicalTrialsProductsId",
                        column: x => x.ClinicalTrialsProductsId,
                        principalTable: "ClinicalTrialsProducts",
                        principalColumn: "ClinicalTrialsProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsStock_ClinicalTrialsProductsId",
                table: "ClinicalTrialsStock",
                column: "ClinicalTrialsProductsId",
                unique: true);
        }
    }
}
