using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbelloLLC.DataAccess.Migrations
{
    public partial class CapacityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Capacity",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Drivers");
        }
    }
}
