using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class logintime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogoutDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RFPDrugRecieve_RFPpaymentRecievedId",
                table: "RFPDrugRecieve",
                column: "RFPpaymentRecievedId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_VendorId",
                table: "PurchaseOrder",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Vendor_VendorId",
                table: "PurchaseOrder",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Vendor_VendorId",
                table: "PurchaseOrder");

            migrationBuilder.DropIndex(
                name: "IX_RFPDrugRecieve_RFPpaymentRecievedId",
                table: "RFPDrugRecieve");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrder_VendorId",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastLogoutDate",
                table: "AspNetUsers");

         
        }
    }
}
