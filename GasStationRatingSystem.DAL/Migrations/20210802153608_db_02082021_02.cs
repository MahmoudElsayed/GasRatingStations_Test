using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_02082021_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Accreditation",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlatePicture",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accreditation",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "PlatePicture",
                schema: "Guide",
                table: "GasStations");
        }
    }
}
