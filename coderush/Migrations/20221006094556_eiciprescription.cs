using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class eiciprescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionLines_Product_ProductId",
                table: "PrescriptionLines");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "PrescriptionLines",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_RFPRequest_RFPCustomerId",
                table: "RFPRequest",
                column: "RFPCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionLines_Product_ProductId",
                table: "PrescriptionLines",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RFPRequest_RFPCustomer_RFPCustomerId",
                table: "RFPRequest",
                column: "RFPCustomerId",
                principalTable: "RFPCustomer",
                principalColumn: "RFPCustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionLines_Product_ProductId",
                table: "PrescriptionLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RFPRequest_RFPCustomer_RFPCustomerId",
                table: "RFPRequest");

            migrationBuilder.DropIndex(
                name: "IX_RFPRequest_RFPCustomerId",
                table: "RFPRequest");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "PrescriptionLines",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionLines_Product_ProductId",
                table: "PrescriptionLines",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
