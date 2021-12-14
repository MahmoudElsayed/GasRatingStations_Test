using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_27072021_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitAnswers",
                schema: "Visit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_VisitAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitAnswers_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitAnswers_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitAnswers_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitAnswers_VisitInfo_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "Visit",
                        principalTable: "VisitInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitAnswerOptions",
                schema: "Visit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_VisitAnswerOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitAnswerOptions_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalSchema: "Guide",
                        principalTable: "Answers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitAnswerOptions_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitAnswerOptions_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitAnswerOptions_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitAnswerOptions_VisitAnswers_VisitAnswerId",
                        column: x => x.VisitAnswerId,
                        principalSchema: "Visit",
                        principalTable: "VisitAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswerOptions_AddedBy",
                schema: "Visit",
                table: "VisitAnswerOptions",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswerOptions_AnswerId",
                schema: "Visit",
                table: "VisitAnswerOptions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswerOptions_DeletedBy",
                schema: "Visit",
                table: "VisitAnswerOptions",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswerOptions_ModifiedBy",
                schema: "Visit",
                table: "VisitAnswerOptions",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswerOptions_VisitAnswerId",
                schema: "Visit",
                table: "VisitAnswerOptions",
                column: "VisitAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswers_AddedBy",
                schema: "Visit",
                table: "VisitAnswers",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswers_DeletedBy",
                schema: "Visit",
                table: "VisitAnswers",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswers_ModifiedBy",
                schema: "Visit",
                table: "VisitAnswers",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitAnswers_VisitId",
                schema: "Visit",
                table: "VisitAnswers",
                column: "VisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitAnswerOptions",
                schema: "Visit");

            migrationBuilder.DropTable(
                name: "VisitAnswers",
                schema: "Visit");
        }
    }
}
