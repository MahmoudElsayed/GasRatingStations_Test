using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_21072021_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitInfo_Users_UserId",
                schema: "Visit",
                table: "VisitInfo");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "Visit",
                table: "VisitInfo",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitInfo_Users_UserId",
                schema: "Visit",
                table: "VisitInfo",
                column: "UserId",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitInfo_Users_UserId",
                schema: "Visit",
                table: "VisitInfo");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "Visit",
                table: "VisitInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitInfo_Users_UserId",
                schema: "Visit",
                table: "VisitInfo",
                column: "UserId",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
