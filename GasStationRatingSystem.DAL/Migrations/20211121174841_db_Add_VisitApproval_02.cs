using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_Add_VisitApproval_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApprovalCasesEnum",
                schema: "Visit",
                table: "VisitApprovals",
                newName: "ApprovalCaseNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApprovalCaseNo",
                schema: "Visit",
                table: "VisitApprovals",
                newName: "ApprovalCasesEnum");
        }
    }
}
