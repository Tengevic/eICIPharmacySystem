using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class changestock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "quantity",
                table: "stockNumber",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "changestock",
                table: "GoodsRecievedNoteLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CustomerId",
                table: "SalesOrder",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_Customer_CustomerId",
                table: "SalesOrder",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_Customer_CustomerId",
                table: "SalesOrder");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_CustomerId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "stockNumber");

            migrationBuilder.DropColumn(
                name: "changestock",
                table: "GoodsRecievedNoteLine");
        }
    }
}
