using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class userId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RFPSaleorder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RFPpaymentRecieved",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RFPinvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RFPDrugRecieve",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PurchaseOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Prescription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GoodsReceivedNote",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "stockNumber",
                columns: table => new
                {
                    stockNumberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Add = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GoodsRecievedNoteLineId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    subtract = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockNumber", x => x.stockNumberId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stockNumber");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RFPSaleorder");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RFPpaymentRecieved");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RFPinvoice");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RFPDrugRecieve");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GoodsReceivedNote");
        }
    }
}
