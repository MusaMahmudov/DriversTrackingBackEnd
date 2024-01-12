using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbelloLLC.DataAccess.Migrations
{
    public partial class reservedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReservedAt",
                table: "Drivers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isReserved",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedAt",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "isReserved",
                table: "Drivers");
        }
    }
}
