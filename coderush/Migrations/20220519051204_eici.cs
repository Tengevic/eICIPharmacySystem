using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class eici : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "remainder",
                table: "PrescriptionLines",
                newName: "OderId");

            migrationBuilder.RenameColumn(
                name: "ContactPerson",
                table: "Customer",
                newName: "EiciRefNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OderId",
                table: "PrescriptionLines",
                newName: "remainder");

            migrationBuilder.RenameColumn(
                name: "EiciRefNumber",
                table: "Customer",
                newName: "ContactPerson");
        }
    }
}
