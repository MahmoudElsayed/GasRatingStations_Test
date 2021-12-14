using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class _addFromVisitApprovalInManualDistributionDTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FromVisitApproval",
                schema: "Setting",
                table: "ManualDistribution",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromVisitApproval",
                schema: "Setting",
                table: "ManualDistribution");
        }
    }
}
