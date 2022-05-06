using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class prescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Approved = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    PrescriptionName = table.Column<string>(nullable: true),
                    presciptionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescription_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionLines",
                columns: table => new
                {
                    PrescriptionLinesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrescriptionId = table.Column<int>(nullable: false),
                    PrescriptionLinesName = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    remainder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionLines", x => x.PrescriptionLinesId);
                    table.ForeignKey(
                        name: "FK_PrescriptionLines_Prescription_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescription",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionLines_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_CustomerId",
                table: "Prescription",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionLines_PrescriptionId",
                table: "PrescriptionLines",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionLines_ProductId",
                table: "PrescriptionLines",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionLines");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "SalesOrder");
        }
    }
}
