using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class Ip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultBuyingPrice",
                table: "ClinicalTrialsProducts");

            migrationBuilder.DropColumn(
                name: "DefaultSellingPrice",
                table: "ClinicalTrialsProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DefaultBuyingPrice",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DefaultSellingPrice",
                table: "ClinicalTrialsProducts",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
