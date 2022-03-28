using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class clinicalTrials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClinicalTrialsDonation",
                columns: table => new
                {
                    ClinicalTrialsDonationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CTDDate = table.Column<DateTimeOffset>(nullable: false),
                    ClinicalTrialsDonationName = table.Column<string>(nullable: true),
                    VendorDONumber = table.Column<string>(nullable: true),
                    VendorInvoiceNumber = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsDonation", x => x.ClinicalTrialsDonationId);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalTrialsProducts",
                columns: table => new
                {
                    ClinicalTrialsProductsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    DefaultBuyingPrice = table.Column<double>(nullable: false),
                    DefaultSellingPrice = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductImageUrl = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: false),
                    UnitOfMeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsProducts", x => x.ClinicalTrialsProductsId);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalTrialsSales",
                columns: table => new
                {
                    ClinicalTrialsSalesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    ClinicalTrialsSalesName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(nullable: false),
                    PatientRefNumber = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SubTotal = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsSales", x => x.ClinicalTrialsSalesId);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalTrialsDonationLine",
                columns: table => new
                {
                    ClinicalTrialsDonationLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BatchID = table.Column<string>(nullable: true),
                    ClinicalTrialsDonationId = table.Column<int>(nullable: false),
                    ClinicalTrialsProductsId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    ManufareDate = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsDonationLine", x => x.ClinicalTrialsDonationLineId);
                    table.ForeignKey(
                        name: "FK_ClinicalTrialsDonationLine_ClinicalTrialsDonation_ClinicalTrialsDonationId",
                        column: x => x.ClinicalTrialsDonationId,
                        principalTable: "ClinicalTrialsDonation",
                        principalColumn: "ClinicalTrialsDonationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicalTrialsDonationLine_ClinicalTrialsProducts_ClinicalTrialsProductsId",
                        column: x => x.ClinicalTrialsProductsId,
                        principalTable: "ClinicalTrialsProducts",
                        principalColumn: "ClinicalTrialsProductsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalTrialsStock",
                columns: table => new
                {
                    ClinicalTrialsStockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClinicalTrialsProductsId = table.Column<int>(nullable: false),
                    Deficit = table.Column<double>(nullable: false),
                    InStock = table.Column<double>(nullable: false),
                    TotalRecieved = table.Column<double>(nullable: false),
                    TotalSales = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsStock", x => x.ClinicalTrialsStockId);
                    table.ForeignKey(
                        name: "FK_ClinicalTrialsStock_ClinicalTrialsProducts_ClinicalTrialsProductsId",
                        column: x => x.ClinicalTrialsProductsId,
                        principalTable: "ClinicalTrialsProducts",
                        principalColumn: "ClinicalTrialsProductsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalTrialsSalesLine",
                columns: table => new
                {
                    ClinicalTrialsSalesLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    ClinicalTrialsProductsId = table.Column<int>(nullable: false),
                    ClinicalTrialsSalesId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DiscountAmount = table.Column<double>(nullable: false),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrialsSalesLine", x => x.ClinicalTrialsSalesLineId);
                    table.ForeignKey(
                        name: "FK_ClinicalTrialsSalesLine_ClinicalTrialsSales_ClinicalTrialsSalesId",
                        column: x => x.ClinicalTrialsSalesId,
                        principalTable: "ClinicalTrialsSales",
                        principalColumn: "ClinicalTrialsSalesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductId",
                table: "Stock",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsRecievedNoteLine_ProductId",
                table: "GoodsRecievedNoteLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsDonationLine_ClinicalTrialsDonationId",
                table: "ClinicalTrialsDonationLine",
                column: "ClinicalTrialsDonationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsDonationLine_ClinicalTrialsProductsId",
                table: "ClinicalTrialsDonationLine",
                column: "ClinicalTrialsProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsSalesLine_ClinicalTrialsSalesId",
                table: "ClinicalTrialsSalesLine",
                column: "ClinicalTrialsSalesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrialsStock_ClinicalTrialsProductsId",
                table: "ClinicalTrialsStock",
                column: "ClinicalTrialsProductsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsRecievedNoteLine_Product_ProductId",
                table: "GoodsRecievedNoteLine",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Product_ProductId",
                table: "Stock",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsRecievedNoteLine_Product_ProductId",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Product_ProductId",
                table: "Stock");

            migrationBuilder.DropTable(
                name: "ClinicalTrialsDonationLine");

            migrationBuilder.DropTable(
                name: "ClinicalTrialsSalesLine");

            migrationBuilder.DropTable(
                name: "ClinicalTrialsStock");

            migrationBuilder.DropTable(
                name: "ClinicalTrialsDonation");

            migrationBuilder.DropTable(
                name: "ClinicalTrialsSales");

            migrationBuilder.DropTable(
                name: "ClinicalTrialsProducts");

            migrationBuilder.DropIndex(
                name: "IX_Stock_ProductId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_GoodsRecievedNoteLine_ProductId",
                table: "GoodsRecievedNoteLine");
        }
    }
}
