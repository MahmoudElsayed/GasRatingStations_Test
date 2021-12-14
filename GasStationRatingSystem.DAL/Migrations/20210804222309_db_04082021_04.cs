using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_04082021_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                schema: "Guide",
                table: "GasStations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GasStations_DistrictId",
                schema: "Guide",
                table: "GasStations",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_GasStations_Districts_DistrictId",
                schema: "Guide",
                table: "GasStations",
                column: "DistrictId",
                principalSchema: "Guide",
                principalTable: "Districts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GasStations_Districts_DistrictId",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropIndex(
                name: "IX_GasStations_DistrictId",
                schema: "Guide",
                table: "GasStations");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                schema: "Guide",
                table: "GasStations");
        }
    }
}
