using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class prescriptionQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "PrescriptionLines",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_RFPSaleorder_RFPCustomerId",
                table: "RFPSaleorder",
                column: "RFPCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RFPSaleorder_RFPCustomer_RFPCustomerId",
                table: "RFPSaleorder",
                column: "RFPCustomerId",
                principalTable: "RFPCustomer",
                principalColumn: "RFPCustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RFPSaleorder_RFPCustomer_RFPCustomerId",
                table: "RFPSaleorder");

            migrationBuilder.DropIndex(
                name: "IX_RFPSaleorder_RFPCustomerId",
                table: "RFPSaleorder");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "PrescriptionLines",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
