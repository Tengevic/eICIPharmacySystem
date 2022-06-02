using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class RFP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsRecievedNoteLine_GoodsReceivedNote_GoodsReceivedNoteId",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                table: "SalesOrderLine");

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderId",
                table: "SalesOrderLine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RFPSaleorderId",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GoodsReceivedNoteId",
                table: "GoodsRecievedNoteLine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RFPDrugRecieveId",
                table: "GoodsRecievedNoteLine",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RFPCustomer",
                columns: table => new
                {
                    RFPCustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CustomerTypeId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    RFPCustomerName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFPCustomer", x => x.RFPCustomerId);
                });

            migrationBuilder.CreateTable(
                name: "RFPDrugRecieve",
                columns: table => new
                {
                    RFPDrugRecieveId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GRNDate = table.Column<DateTimeOffset>(nullable: false),
                    IsFullReceive = table.Column<bool>(nullable: false),
                    RFPDrugRecieveName = table.Column<string>(nullable: true),
                    RFPpaymentRecievedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFPDrugRecieve", x => x.RFPDrugRecieveId);
                });

            migrationBuilder.CreateTable(
                name: "RFPSaleorder",
                columns: table => new
                {
                    RFPSaleorderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    RFPCustomerId = table.Column<int>(nullable: false),
                    RFPSaleorderName = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SaleDate = table.Column<DateTimeOffset>(nullable: false),
                    SalesTypeId = table.Column<int>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFPSaleorder", x => x.RFPSaleorderId);
                });

            migrationBuilder.CreateTable(
                name: "RFPinvoice",
                columns: table => new
                {
                    RFPinvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceDate = table.Column<DateTimeOffset>(nullable: false),
                    InvoiceDueDate = table.Column<DateTimeOffset>(nullable: false),
                    InvoiceTypeId = table.Column<int>(nullable: false),
                    RFPSaleorderId = table.Column<int>(nullable: false),
                    RFPinvoiceName = table.Column<string>(nullable: true),
                    fullyPaid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFPinvoice", x => x.RFPinvoiceId);
                    table.ForeignKey(
                        name: "FK_RFPinvoice_RFPSaleorder_RFPSaleorderId",
                        column: x => x.RFPSaleorderId,
                        principalTable: "RFPSaleorder",
                        principalColumn: "RFPSaleorderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RFPpaymentRecieved",
                columns: table => new
                {
                    RFPpaymentRecievedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsFullPayment = table.Column<bool>(nullable: false),
                    PaymentAmount = table.Column<double>(nullable: false),
                    PaymentDate = table.Column<DateTimeOffset>(nullable: false),
                    PaymentTypeId = table.Column<int>(nullable: false),
                    RFPinvoiceId = table.Column<int>(nullable: false),
                    RFPpaymentRecievedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFPpaymentRecieved", x => x.RFPpaymentRecievedId);
                    table.ForeignKey(
                        name: "FK_RFPpaymentRecieved_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RFPpaymentRecieved_RFPinvoice_RFPinvoiceId",
                        column: x => x.RFPinvoiceId,
                        principalTable: "RFPinvoice",
                        principalColumn: "RFPinvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_RFPSaleorderId",
                table: "SalesOrderLine",
                column: "RFPSaleorderId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsRecievedNoteLine_RFPDrugRecieveId",
                table: "GoodsRecievedNoteLine",
                column: "RFPDrugRecieveId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNote_PurchaseOrderId",
                table: "GoodsReceivedNote",
                column: "PurchaseOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RFPinvoice_RFPSaleorderId",
                table: "RFPinvoice",
                column: "RFPSaleorderId");

            migrationBuilder.CreateIndex(
                name: "IX_RFPpaymentRecieved_PaymentTypeId",
                table: "RFPpaymentRecieved",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RFPpaymentRecieved_RFPinvoiceId",
                table: "RFPpaymentRecieved",
                column: "RFPinvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedNote_PurchaseOrder_PurchaseOrderId",
                table: "GoodsReceivedNote",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrder",
                principalColumn: "PurchaseOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsRecievedNoteLine_GoodsReceivedNote_GoodsReceivedNoteId",
                table: "GoodsRecievedNoteLine",
                column: "GoodsReceivedNoteId",
                principalTable: "GoodsReceivedNote",
                principalColumn: "GoodsReceivedNoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsRecievedNoteLine_RFPDrugRecieve_RFPDrugRecieveId",
                table: "GoodsRecievedNoteLine",
                column: "RFPDrugRecieveId",
                principalTable: "RFPDrugRecieve",
                principalColumn: "RFPDrugRecieveId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_RFPSaleorder_RFPSaleorderId",
                table: "SalesOrderLine",
                column: "RFPSaleorderId",
                principalTable: "RFPSaleorder",
                principalColumn: "RFPSaleorderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                table: "SalesOrderLine",
                column: "SalesOrderId",
                principalTable: "SalesOrder",
                principalColumn: "SalesOrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedNote_PurchaseOrder_PurchaseOrderId",
                table: "GoodsReceivedNote");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsRecievedNoteLine_GoodsReceivedNote_GoodsReceivedNoteId",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsRecievedNoteLine_RFPDrugRecieve_RFPDrugRecieveId",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_RFPSaleorder_RFPSaleorderId",
                table: "SalesOrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                table: "SalesOrderLine");

            migrationBuilder.DropTable(
                name: "RFPCustomer");

            migrationBuilder.DropTable(
                name: "RFPDrugRecieve");

            migrationBuilder.DropTable(
                name: "RFPpaymentRecieved");

            migrationBuilder.DropTable(
                name: "RFPinvoice");

            migrationBuilder.DropTable(
                name: "RFPSaleorder");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_RFPSaleorderId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_GoodsRecievedNoteLine_RFPDrugRecieveId",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedNote_PurchaseOrderId",
                table: "GoodsReceivedNote");

            migrationBuilder.DropColumn(
                name: "RFPSaleorderId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "RFPDrugRecieveId",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderId",
                table: "SalesOrderLine",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GoodsReceivedNoteId",
                table: "GoodsRecievedNoteLine",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsRecievedNoteLine_GoodsReceivedNote_GoodsReceivedNoteId",
                table: "GoodsRecievedNoteLine",
                column: "GoodsReceivedNoteId",
                principalTable: "GoodsReceivedNote",
                principalColumn: "GoodsReceivedNoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                table: "SalesOrderLine",
                column: "SalesOrderId",
                principalTable: "SalesOrder",
                principalColumn: "SalesOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
