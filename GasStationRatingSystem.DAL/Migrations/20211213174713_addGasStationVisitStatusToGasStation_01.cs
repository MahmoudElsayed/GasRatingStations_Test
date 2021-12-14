using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class addGasStationVisitStatusToGasStation_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GasStationVisitStatusNo",
                schema: "Guide",
                table: "GasStations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Station",
                schema: "Guide",
                table: "GasStations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GasStationVisitStatusNo",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "Station",
                schema: "Guide",
                table: "GasStations");
        }
    }
}
