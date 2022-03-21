using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodsReceivedNoteLineId",
                table: "GoodsReceivedNote");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoodsReceivedNoteLineId",
                table: "GoodsReceivedNote",
                nullable: false,
                defaultValue: 0);
        }
    }
}
