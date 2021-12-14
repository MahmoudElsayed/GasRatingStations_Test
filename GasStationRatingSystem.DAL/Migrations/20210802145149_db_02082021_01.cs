using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_02082021_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                schema: "Guide",
                table: "GasStations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AreaName",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorityBranch",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingNo",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CountOfDailyWorkingHours",
                schema: "Guide",
                table: "GasStations",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CountOfVentilationTubes",
                schema: "Guide",
                table: "GasStations",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictName",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElectricityNumber",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnvDescriptionEast",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnvDescriptionEastNotes",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnvDescriptionNorth",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnvDescriptionNorthNotes",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnvDescriptionSouth",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnvDescriptionSouthNotes",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnvDescriptionWest",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnvDescriptionWestNotes",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "HeightOfVentilationPipe",
                schema: "Guide",
                table: "GasStations",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InspectorName",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lng",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameInLicense",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionName",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StationClass",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusText",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                schema: "Guide",
                table: "GasStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerCount",
                schema: "Guide",
                table: "GasStations",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "AreaName",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "AuthorityBranch",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "BuildingNo",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "CityName",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "CountOfDailyWorkingHours",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "CountOfVentilationTubes",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "DistrictName",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "ElectricityNumber",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "EnvDescriptionEast",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "EnvDescriptionEastNotes",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "EnvDescriptionNorth",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "EnvDescriptionNorthNotes",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "EnvDescriptionSouth",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "EnvDescriptionSouthNotes",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "EnvDescriptionWest",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "EnvDescriptionWestNotes",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "HeightOfVentilationPipe",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "InspectorName",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "Lat",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "Lng",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "NameInLicense",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "PostCode",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "RegionName",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "StationClass",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "StatusText",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "StreetName",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "WorkerCount",
                schema: "Guide",
                table: "GasStations");
        }
    }
}
