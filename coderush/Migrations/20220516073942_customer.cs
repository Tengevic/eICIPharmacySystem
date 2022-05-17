using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Vendor",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Customer",
                newName: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_PrescriptionId",
                table: "SalesOrder",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_ProductId",
                table: "PurchaseOrderLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsProducts_ProductTypeId",
                table: "ClinicalTrialsProducts",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsProducts_UnitOfMeasureId",
                table: "ClinicalTrialsProducts",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalTrialsProducts_ProductType_ProductTypeId",
                table: "ClinicalTrialsProducts",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "ProductTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalTrialsProducts_UnitOfMeasure_UnitOfMeasureId",
                table: "ClinicalTrialsProducts",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "ProductTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderLine_Product_ProductId",
                table: "PurchaseOrderLine",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_Prescription_PrescriptionId",
                table: "SalesOrder",
                column: "PrescriptionId",
                principalTable: "Prescription",
                principalColumn: "PrescriptionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalTrialsProducts_ProductType_ProductTypeId",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalTrialsProducts_UnitOfMeasure_UnitOfMeasureId",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderLine_Product_ProductId",
                table: "PurchaseOrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_Prescription_PrescriptionId",
                table: "SalesOrder");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_PrescriptionId",
                table: "SalesOrder");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderLine_ProductId",
                table: "PurchaseOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_ClinicalTrialsProducts_ProductTypeId",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropIndex(
                name: "IX_ClinicalTrialsProducts_UnitOfMeasureId",
                table: "ClinicalTrialsProducts");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Vendor",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Customer",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Customer",
                nullable: true);
        }
    }
}
