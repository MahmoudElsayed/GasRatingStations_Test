using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_2021_07_31_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainItemId",
                schema: "Visit",
                table: "VisitAnswerOptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswerOptions_MainItemId",
                schema: "Visit",
                table: "VisitAnswerOptions",
                column: "MainItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitAnswerOptions_Questions_MainItemId",
                schema: "Visit",
                table: "VisitAnswerOptions",
                column: "MainItemId",
                principalSchema: "Guide",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitAnswerOptions_Questions_MainItemId",
                schema: "Visit",
                table: "VisitAnswerOptions");

            migrationBuilder.DropIndex(
                name: "IX_VisitAnswerOptions_MainItemId",
                schema: "Visit",
                table: "VisitAnswerOptions");

            migrationBuilder.DropColumn(
                name: "MainItemId",
                schema: "Visit",
                table: "VisitAnswerOptions");
        }
    }
}
