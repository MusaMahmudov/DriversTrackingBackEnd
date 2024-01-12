using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbelloLLC.DataAccess.Migrations
{
    public partial class LatAndLongAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "LocationDriver",
                table: "Drivers");

            migrationBuilder.DropCheckConstraint(
                name: "NotesDriver",
                table: "Drivers");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentLocation",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Drivers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Drivers",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Drivers");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Drivers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentLocation",
                table: "Drivers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "LocationDriver",
                table: "Drivers",
                sql: "Len(CurrentLocation) >= 3");

            migrationBuilder.AddCheckConstraint(
                name: "NotesDriver",
                table: "Drivers",
                sql: "Len(Notes) >= 3");
        }
    }
}
