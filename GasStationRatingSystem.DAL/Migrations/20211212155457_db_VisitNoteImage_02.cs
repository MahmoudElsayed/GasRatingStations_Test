using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_VisitNoteImage_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitNotes_VisitSigntures_VisitSigntureID",
                schema: "Visit",
                table: "VisitNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitSigntures_VisitSigntures_VisitSigntureID",
                schema: "Visit",
                table: "VisitSigntures");

            migrationBuilder.DropIndex(
                name: "IX_VisitSigntures_VisitSigntureID",
                schema: "Visit",
                table: "VisitSigntures");

            migrationBuilder.DropIndex(
                name: "IX_VisitNotes_VisitSigntureID",
                schema: "Visit",
                table: "VisitNotes");

            migrationBuilder.DropColumn(
                name: "VisitSigntureID",
                schema: "Visit",
                table: "VisitSigntures");

            migrationBuilder.DropColumn(
                name: "VisitSigntureID",
                schema: "Visit",
                table: "VisitNotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VisitSigntureID",
                schema: "Visit",
                table: "VisitSigntures",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitSigntureID",
                schema: "Visit",
                table: "VisitNotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntures_VisitSigntureID",
                schema: "Visit",
                table: "VisitSigntures",
                column: "VisitSigntureID");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNotes_VisitSigntureID",
                schema: "Visit",
                table: "VisitNotes",
                column: "VisitSigntureID");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitNotes_VisitSigntures_VisitSigntureID",
                schema: "Visit",
                table: "VisitNotes",
                column: "VisitSigntureID",
                principalSchema: "Visit",
                principalTable: "VisitSigntures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitSigntures_VisitSigntures_VisitSigntureID",
                schema: "Visit",
                table: "VisitSigntures",
                column: "VisitSigntureID",
                principalSchema: "Visit",
                principalTable: "VisitSigntures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
