using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class bill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "fullPaid",
                table: "Bill",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucher_BillId",
                table: "PaymentVoucher",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVoucher_PaymentTypeId",
                table: "PaymentVoucher",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_GoodsReceivedNoteId",
                table: "Bill",
                column: "GoodsReceivedNoteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_GoodsReceivedNote_GoodsReceivedNoteId",
                table: "Bill",
                column: "GoodsReceivedNoteId",
                principalTable: "GoodsReceivedNote",
                principalColumn: "GoodsReceivedNoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentVoucher_Bill_BillId",
                table: "PaymentVoucher",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentVoucher_PaymentType_PaymentTypeId",
                table: "PaymentVoucher",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_GoodsReceivedNote_GoodsReceivedNoteId",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentVoucher_Bill_BillId",
                table: "PaymentVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentVoucher_PaymentType_PaymentTypeId",
                table: "PaymentVoucher");

            migrationBuilder.DropIndex(
                name: "IX_PaymentVoucher_BillId",
                table: "PaymentVoucher");

            migrationBuilder.DropIndex(
                name: "IX_PaymentVoucher_PaymentTypeId",
                table: "PaymentVoucher");

            migrationBuilder.DropIndex(
                name: "IX_Bill_GoodsReceivedNoteId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "fullPaid",
                table: "Bill");
        }
    }
}
