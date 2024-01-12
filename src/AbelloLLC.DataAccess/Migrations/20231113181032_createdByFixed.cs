using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbelloLLC.DataAccess.Migrations
{
    public partial class createdByFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateBy",
                table: "VehicleTypes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                table: "Drivers",
                newName: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "VehicleTypes",
                newName: "CreateBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Drivers",
                newName: "CreateBy");
        }
    }
}
