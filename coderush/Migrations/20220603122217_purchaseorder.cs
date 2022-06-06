using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class purchaseorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedNote_PurchaseOrderId",
                table: "GoodsReceivedNote");

            migrationBuilder.AddColumn<bool>(
                name: "fullyPaid",
                table: "PurchaseOrder",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNote_PurchaseOrderId",
                table: "GoodsReceivedNote",
                column: "PurchaseOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedNote_PurchaseOrderId",
                table: "GoodsReceivedNote");

            migrationBuilder.DropColumn(
                name: "fullyPaid",
                table: "PurchaseOrder");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNote_PurchaseOrderId",
                table: "GoodsReceivedNote",
                column: "PurchaseOrderId",
                unique: true);
        }
    }
}
