using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_VisitNoteImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitNotes",
                schema: "Visit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_VisitNotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitNotes_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitNotes_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitNotes_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitNotes_VisitInfo_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "Visit",
                        principalTable: "VisitInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitQuestionImages",
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
                    table.PrimaryKey("PK_VisitQuestionImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitQuestionImages_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitQuestionImages_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitQuestionImages_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitQuestionImages_VisitInfo_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "Visit",
                        principalTable: "VisitInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitSigntures",
                schema: "Visit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SigntureNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SigntureImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_VisitSigntures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitSigntures_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitSigntures_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitSigntures_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitSigntures_VisitInfo_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "Visit",
                        principalTable: "VisitInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitNoteImages",
                schema: "Visit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_VisitNoteImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitNoteImages_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitNoteImages_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitNoteImages_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitNoteImages_VisitNotes_VisitNoteId",
                        column: x => x.VisitNoteId,
                        principalSchema: "Visit",
                        principalTable: "VisitNotes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitQuestionImageDetails",
                schema: "Visit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitQuestionImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_VisitQuestionImageDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitQuestionImageDetails_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Guide",
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitQuestionImageDetails_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitQuestionImageDetails_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitQuestionImageDetails_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitQuestionImageDetails_VisitQuestionImages_VisitQuestionImageId",
                        column: x => x.VisitQuestionImageId,
                        principalSchema: "Visit",
                        principalTable: "VisitQuestionImages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitSigntureDataImages",
                schema: "Visit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitSigntureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_VisitSigntureDataImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitSigntureDataImages_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitSigntureDataImages_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitSigntureDataImages_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitSigntureDataImages_VisitSigntures_VisitSigntureId",
                        column: x => x.VisitSigntureId,
                        principalSchema: "Visit",
                        principalTable: "VisitSigntures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitNoteImages_AddedBy",
                schema: "Visit",
                table: "VisitNoteImages",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNoteImages_DeletedBy",
                schema: "Visit",
                table: "VisitNoteImages",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNoteImages_ModifiedBy",
                schema: "Visit",
                table: "VisitNoteImages",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNoteImages_VisitNoteId",
                schema: "Visit",
                table: "VisitNoteImages",
                column: "VisitNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNotes_AddedBy",
                schema: "Visit",
                table: "VisitNotes",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNotes_DeletedBy",
                schema: "Visit",
                table: "VisitNotes",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNotes_ModifiedBy",
                schema: "Visit",
                table: "VisitNotes",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNotes_VisitId",
                schema: "Visit",
                table: "VisitNotes",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImageDetails_AddedBy",
                schema: "Visit",
                table: "VisitQuestionImageDetails",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImageDetails_DeletedBy",
                schema: "Visit",
                table: "VisitQuestionImageDetails",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImageDetails_ModifiedBy",
                schema: "Visit",
                table: "VisitQuestionImageDetails",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImageDetails_QuestionId",
                schema: "Visit",
                table: "VisitQuestionImageDetails",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImageDetails_VisitQuestionImageId",
                schema: "Visit",
                table: "VisitQuestionImageDetails",
                column: "VisitQuestionImageId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImages_AddedBy",
                schema: "Visit",
                table: "VisitQuestionImages",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImages_DeletedBy",
                schema: "Visit",
                table: "VisitQuestionImages",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImages_ModifiedBy",
                schema: "Visit",
                table: "VisitQuestionImages",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitQuestionImages_VisitId",
                schema: "Visit",
                table: "VisitQuestionImages",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntureDataImages_AddedBy",
                schema: "Visit",
                table: "VisitSigntureDataImages",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntureDataImages_DeletedBy",
                schema: "Visit",
                table: "VisitSigntureDataImages",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntureDataImages_ModifiedBy",
                schema: "Visit",
                table: "VisitSigntureDataImages",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntureDataImages_VisitSigntureId",
                schema: "Visit",
                table: "VisitSigntureDataImages",
                column: "VisitSigntureId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntures_AddedBy",
                schema: "Visit",
                table: "VisitSigntures",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntures_DeletedBy",
                schema: "Visit",
                table: "VisitSigntures",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntures_ModifiedBy",
                schema: "Visit",
                table: "VisitSigntures",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VisitSigntures_VisitId",
                schema: "Visit",
                table: "VisitSigntures",
                column: "VisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitNoteImages",
                schema: "Visit");

            migrationBuilder.DropTable(
                name: "VisitQuestionImageDetails",
                schema: "Visit");

            migrationBuilder.DropTable(
                name: "VisitSigntureDataImages",
                schema: "Visit");

            migrationBuilder.DropTable(
                name: "VisitNotes",
                schema: "Visit");

            migrationBuilder.DropTable(
                name: "VisitQuestionImages",
                schema: "Visit");

            migrationBuilder.DropTable(
                name: "VisitSigntures",
                schema: "Visit");
        }
    }
}
