using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_VisitNoteImage_03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VisitSigntures_VisitId",
                schema: "Visit",
                table: "VisitSigntures");

            migrationBuilder.DropIndex(
                name: "IX_VisitQuestionImages_VisitId",
                schema: "Visit",
                table: "VisitQuestionImages");

            migrationBuilder.DropIndex(
                name: "IX_VisitNotes_VisitId",
                schema: "Visit",
                table: "VisitNotes");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntures_VisitId",
                schema: "Visit",
                table: "VisitSigntures",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImages_VisitId",
                schema: "Visit",
                table: "VisitQuestionImages",
                column: "VisitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitNotes_VisitId",
                schema: "Visit",
                table: "VisitNotes",
                column: "VisitId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VisitSigntures_VisitId",
                schema: "Visit",
                table: "VisitSigntures");

            migrationBuilder.DropIndex(
                name: "IX_VisitQuestionImages_VisitId",
                schema: "Visit",
                table: "VisitQuestionImages");

            migrationBuilder.DropIndex(
                name: "IX_VisitNotes_VisitId",
                schema: "Visit",
                table: "VisitNotes");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntures_VisitId",
                schema: "Visit",
                table: "VisitSigntures",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImages_VisitId",
                schema: "Visit",
                table: "VisitQuestionImages",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNotes_VisitId",
                schema: "Visit",
                table: "VisitNotes",
                column: "VisitId");
        }
    }
}
