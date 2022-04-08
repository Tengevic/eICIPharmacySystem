using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class expiredBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Dispose",
                table: "GoodsRecievedNoteLine",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Dispose",
                table: "ClinicalTrialsDonationLine",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceive_PaymentTypeId",
                table: "PaymentReceive",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceive_PaymentType_PaymentTypeId",
                table: "PaymentReceive",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceive_PaymentType_PaymentTypeId",
                table: "PaymentReceive");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceive_PaymentTypeId",
                table: "PaymentReceive");

            migrationBuilder.DropColumn(
                name: "Dispose",
                table: "GoodsRecievedNoteLine");

            migrationBuilder.DropColumn(
                name: "Dispose",
                table: "ClinicalTrialsDonationLine");
        }
    }
}
