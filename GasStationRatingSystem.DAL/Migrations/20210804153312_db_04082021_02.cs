using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_04082021_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                schema: "People",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                schema: "People",
                table: "Users",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                schema: "People",
                table: "Users",
                column: "CityId",
                principalSchema: "Guide",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                schema: "People",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                schema: "People",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "People",
                table: "Users");
        }
    }
}
