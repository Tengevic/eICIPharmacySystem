using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class clinicaltrialreturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "ClinicalTrialsSales");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "SalesOrder",
                newName: "SaleDate");

            migrationBuilder.RenameColumn(
                name: "CustomerRefNumber",
                table: "SalesOrder",
                newName: "PatientRefNumber");

            migrationBuilder.RenameColumn(
                name: "ShipmentId",
                table: "Invoice",
                newName: "SalesOrderId");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "ClinicalTrialsSales",
                newName: "UseDate");

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

            migrationBuilder.AddColumn<bool>(
                name: "RequireUpload",
                table: "PaymentType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Returned",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Returned",
                table: "ClinicalTrialsDonationLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ClinicalTrialsReturned",
                columns: table => new
                {
                    ClinicalTrialsReturnedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClinicalTrialsReturnedName = table.Column<string>(nullable: true),
                    ReturnedDate = table.Column<DateTimeOffset>(nullable: false),
                    VendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsReturned", x => x.ClinicalTrialsReturnedId);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalTrialsReturnedLine",
                columns: table => new
                {
                    ClinicalTrialsReturnedLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClinicalTrialsDonationLineId = table.Column<int>(nullable: false),
                    ClinicalTrialsProductsId = table.Column<int>(nullable: false),
                    ClinicalTrialsReturnedId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsReturnedLine", x => x.ClinicalTrialsReturnedLineId);
                    table.ForeignKey(
                        name: "FK_ClinicalTrialsReturnedLine_ClinicalTrialsReturned_ClinicalTrialsReturnedId",
                        column: x => x.ClinicalTrialsReturnedId,
                        principalTable: "ClinicalTrialsReturned",
                        principalColumn: "ClinicalTrialsReturnedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsDonation_VendorId",
                table: "ClinicalTrialsDonation",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsReturnedLine_ClinicalTrialsReturnedId",
                table: "ClinicalTrialsReturnedLine",
                column: "ClinicalTrialsReturnedId");

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

            migrationBuilder.DropTable(
                name: "ClinicalTrialsReturnedLine");

            migrationBuilder.DropTable(
                name: "ClinicalTrialsReturned");

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
                name: "RequireUpload",
                table: "PaymentType");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropColumn(
                name: "Returned",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropColumn(
                name: "Returned",
                table: "ClinicalTrialsDonationLine");

            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "SalesOrder",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "PatientRefNumber",
                table: "SalesOrder",
                newName: "CustomerRefNumber");

            migrationBuilder.RenameColumn(
                name: "SalesOrderId",
                table: "Invoice",
                newName: "ShipmentId");

            migrationBuilder.RenameColumn(
                name: "UseDate",
                table: "ClinicalTrialsSales",
                newName: "OrderDate");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeliveryDate",
                table: "SalesOrder",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeliveryDate",
                table: "ClinicalTrialsSales",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
