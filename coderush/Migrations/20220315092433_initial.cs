using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "SalesTypeId",
                table: "SalesOrder");

            migrationBuilder.AddColumn<int>(
                name: "GoodsReceivedNoteLineId",
                table: "GoodsReceivedNote",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GoodsRecievedNoteLine",
                columns: table => new
                {
                    GoodsRecievedNoteLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BatchID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    GoodsReceivedNoteId = table.Column<int>(nullable: false),
                    ManufareDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsRecievedNoteLine", x => x.GoodsRecievedNoteLineId);
                    table.ForeignKey(
                        name: "FK_GoodsRecievedNoteLine_GoodsReceivedNote_GoodsReceivedNoteId",
                        column: x => x.GoodsReceivedNoteId,
                        principalTable: "GoodsReceivedNote",
                        principalColumn: "GoodsReceivedNoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsRecievedNoteLine_GoodsReceivedNoteId",
                table: "GoodsRecievedNoteLine",
                column: "GoodsReceivedNoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsRecievedNoteLine");

            migrationBuilder.DropColumn(
                name: "GoodsReceivedNoteLineId",
                table: "GoodsReceivedNote");

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
        }
    }
}
