using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_Add_VisitApproval_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitApprovals",
                schema: "Visit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovalCasesEnum = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitApprovals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitApprovals_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitApprovals_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitApprovals_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitApprovals_VisitInfo_VisitInfoId",
                        column: x => x.VisitInfoId,
                        principalSchema: "Visit",
                        principalTable: "VisitInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitApprovals_AddedBy",
                schema: "Visit",
                table: "VisitApprovals",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitApprovals_DeletedBy",
                schema: "Visit",
                table: "VisitApprovals",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitApprovals_ModifiedBy",
                schema: "Visit",
                table: "VisitApprovals",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitApprovals_VisitInfoId",
                schema: "Visit",
                table: "VisitApprovals",
                column: "VisitInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitApprovals",
                schema: "Visit");
        }
    }
}
