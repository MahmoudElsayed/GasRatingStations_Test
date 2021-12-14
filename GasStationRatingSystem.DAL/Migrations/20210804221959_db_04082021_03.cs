using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_04082021_03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Setting");

            migrationBuilder.CreateTable(
                name: "ManualDistribution",
                schema: "Setting",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_ManualDistribution", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ManualDistribution_GasStations_StationId",
                        column: x => x.StationId,
                        principalSchema: "Guide",
                        principalTable: "GasStations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualDistribution_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualDistribution_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualDistribution_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManualDistribution_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManualDistribution_AddedBy",
                schema: "Setting",
                table: "ManualDistribution",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ManualDistribution_DeletedBy",
                schema: "Setting",
                table: "ManualDistribution",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ManualDistribution_ModifiedBy",
                schema: "Setting",
                table: "ManualDistribution",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ManualDistribution_StationId",
                schema: "Setting",
                table: "ManualDistribution",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualDistribution_UserId",
                schema: "Setting",
                table: "ManualDistribution",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManualDistribution",
                schema: "Setting");
        }
    }
}
