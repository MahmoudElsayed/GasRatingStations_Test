using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_MainInfluence_MainInfluenceQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainInfluences",
                schema: "Guide",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MainInfluences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MainInfluences_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainInfluences_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainInfluences_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainInfluenceQuestions",
                schema: "Guide",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MainInfluenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_MainInfluenceQuestions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MainInfluenceQuestions_MainInfluences_MainInfluenceId",
                        column: x => x.MainInfluenceId,
                        principalSchema: "Guide",
                        principalTable: "MainInfluences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MainInfluenceQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Guide",
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MainInfluenceQuestions_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainInfluenceQuestions_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainInfluenceQuestions_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainInfluenceQuestions_AddedBy",
                schema: "Guide",
                table: "MainInfluenceQuestions",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MainInfluenceQuestions_DeletedBy",
                schema: "Guide",
                table: "MainInfluenceQuestions",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MainInfluenceQuestions_MainInfluenceId",
                schema: "Guide",
                table: "MainInfluenceQuestions",
                column: "MainInfluenceId");

            migrationBuilder.CreateIndex(
                name: "IX_MainInfluenceQuestions_ModifiedBy",
                schema: "Guide",
                table: "MainInfluenceQuestions",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MainInfluenceQuestions_QuestionId",
                schema: "Guide",
                table: "MainInfluenceQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MainInfluences_AddedBy",
                schema: "Guide",
                table: "MainInfluences",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MainInfluences_DeletedBy",
                schema: "Guide",
                table: "MainInfluences",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MainInfluences_ModifiedBy",
                schema: "Guide",
                table: "MainInfluences",
                column: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainInfluenceQuestions",
                schema: "Guide");

            migrationBuilder.DropTable(
                name: "MainInfluences",
                schema: "Guide");
        }
    }
}
