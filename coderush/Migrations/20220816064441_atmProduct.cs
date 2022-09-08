using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class atmProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAtM",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_GoodsRecievedNoteLineId",
                table: "SalesOrderLine",
                column: "GoodsRecievedNoteLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId",
                principalTable: "CustomerType",
                principalColumn: "CustomerTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_GoodsRecievedNoteLine_GoodsRecievedNoteLineId",
                table: "SalesOrderLine",
                column: "GoodsRecievedNoteLineId",
                principalTable: "GoodsRecievedNoteLine",
                principalColumn: "GoodsRecievedNoteLineId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_GoodsRecievedNoteLine_GoodsRecievedNoteLineId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_GoodsRecievedNoteLineId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CustomerTypeId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsAtM",
                table: "Product");
        }
    }
}
