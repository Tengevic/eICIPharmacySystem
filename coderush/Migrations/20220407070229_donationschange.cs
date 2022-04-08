using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class donationschange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VendorInvoiceNumber",
                table: "ClinicalTrialsDonation");

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "ClinicalTrialsDonation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "ClinicalTrialsDonation");

            migrationBuilder.AddColumn<string>(
                name: "VendorInvoiceNumber",
                table: "ClinicalTrialsDonation",
                nullable: true);
        }
    }
}
