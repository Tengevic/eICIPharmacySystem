using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class newStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.AddColumn<int>(
                name: "GoodsRecievedNoteLineId",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Deficit",
                table: "Product",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ExpiredStock",
                table: "Product",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InStock",
                table: "Product",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalRecieved",
                table: "Product",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalSales",
                table: "Product",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Expired",
                table: "GoodsRecievedNoteLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InStock",
                table: "GoodsRecievedNoteLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sold",
                table: "GoodsRecievedNoteLine",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodsRecievedNoteLineId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "Deficit",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ExpiredStock",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TotalRecieved",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TotalSales",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Expired",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.DropColumn(
                name: "Sold",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Deficit = table.Column<double>(nullable: false),
                    InStock = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    TotalRecieved = table.Column<double>(nullable: false),
                    TotalSales = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stock_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductId",
                table: "Stock",
                column: "ProductId",
                unique: true);
        }
    }
}
