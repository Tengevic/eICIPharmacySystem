using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class generictype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesTypeId",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsDonation_VendorId",
                table: "ClinicalTrialsDonation",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalTrialsDonation_Vendor_VendorId",
                table: "ClinicalTrialsDonation",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalTrialsDonation_Vendor_VendorId",
                table: "ClinicalTrialsDonation");

            migrationBuilder.DropIndex(
                name: "IX_ClinicalTrialsDonation_VendorId",
                table: "ClinicalTrialsDonation");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "SalesTypeId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "ClinicalTrialsProducts");
        }
    }
}
