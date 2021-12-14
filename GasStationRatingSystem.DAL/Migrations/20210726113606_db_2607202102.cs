using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_2607202102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasMultiAnswer",
                schema: "Guide",
                table: "Questions",
                newName: "HasMultiCategoryAnswer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasMultiCategoryAnswer",
                schema: "Guide",
                table: "Questions",
                newName: "HasMultiAnswer");
        }
    }
}
