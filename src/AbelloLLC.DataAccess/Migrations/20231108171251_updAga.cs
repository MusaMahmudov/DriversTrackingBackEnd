using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbelloLLC.DataAccess.Migrations
{
    public partial class updAga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleType",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Drivers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "Drivers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Drivers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Drivers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "Drivers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentLocation",
                table: "Drivers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "VehicleTypeName",
                table: "VehicleType",
                sql: "Len(Name) >= 3");

            migrationBuilder.AddCheckConstraint(
                name: "DimensionDriver",
                table: "Drivers",
                sql: "Len(Dimensions) >= 3");

            migrationBuilder.AddCheckConstraint(
                name: "DriverName",
                table: "Drivers",
                sql: "Len(Name) >= 3");

            migrationBuilder.AddCheckConstraint(
                name: "LocationDriver",
                table: "Drivers",
                sql: "Len(CurrentLocation) >= 3");

            migrationBuilder.AddCheckConstraint(
                name: "NotesDriver",
                table: "Drivers",
                sql: "Len(Notes) >= 3");

            migrationBuilder.AddCheckConstraint(
                name: "OwnerDriver",
                table: "Drivers",
                sql: "Len(Owner) >= 3");

            migrationBuilder.AddCheckConstraint(
                name: "PhoneDriver",
                table: "Drivers",
                sql: "Len(Phone) >= 3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "VehicleTypeName",
                table: "VehicleType");

            migrationBuilder.DropCheckConstraint(
                name: "DimensionDriver",
                table: "Drivers");

            migrationBuilder.DropCheckConstraint(
                name: "DriverName",
                table: "Drivers");

            migrationBuilder.DropCheckConstraint(
                name: "LocationDriver",
                table: "Drivers");

            migrationBuilder.DropCheckConstraint(
                name: "NotesDriver",
                table: "Drivers");

            migrationBuilder.DropCheckConstraint(
                name: "OwnerDriver",
                table: "Drivers");

            migrationBuilder.DropCheckConstraint(
                name: "PhoneDriver",
                table: "Drivers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
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
                oldMaxLength: 128);
        }
    }
}
