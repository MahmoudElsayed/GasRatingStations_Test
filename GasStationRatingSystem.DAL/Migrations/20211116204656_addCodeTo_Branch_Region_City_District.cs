using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class addCodeTo_Branch_Region_City_District : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Guide",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Guide",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Guide",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Guide",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Guide",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Guide",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Guide",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Guide",
                table: "Branches");
        }
    }
}
