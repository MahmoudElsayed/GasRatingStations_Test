using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class addIconAndOrderInPagesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconName",
                schema: "Page",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                schema: "Page",
                table: "Pages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconName",
                schema: "Page",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                schema: "Page",
                table: "Areas",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconName",
                schema: "Page",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "OrderNo",
                schema: "Page",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "IconName",
                schema: "Page",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "OrderNo",
                schema: "Page",
                table: "Areas");
        }
    }
}
