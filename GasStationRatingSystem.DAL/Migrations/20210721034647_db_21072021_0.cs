using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_21072021_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StationId",
                schema: "Visit",
                table: "VisitInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VisitInfo_StationId",
                schema: "Visit",
                table: "VisitInfo",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitInfo_GasStations_StationId",
                schema: "Visit",
                table: "VisitInfo",
                column: "StationId",
                principalSchema: "Guide",
                principalTable: "GasStations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitInfo_GasStations_StationId",
                schema: "Visit",
                table: "VisitInfo");

            migrationBuilder.DropIndex(
                name: "IX_VisitInfo_StationId",
                schema: "Visit",
                table: "VisitInfo");

            migrationBuilder.DropColumn(
                name: "StationId",
                schema: "Visit",
                table: "VisitInfo");
        }
    }
}
