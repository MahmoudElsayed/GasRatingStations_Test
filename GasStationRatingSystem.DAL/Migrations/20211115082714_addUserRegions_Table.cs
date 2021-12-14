using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class addUserRegions_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                schema: "People",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CityId",
                schema: "People",
                table: "Users",
                newName: "CityID");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CityId",
                schema: "People",
                table: "Users",
                newName: "IX_Users_CityID");

            migrationBuilder.CreateTable(
                name: "UserRegions",
                schema: "People",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_UserRegions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRegions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "Guide",
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRegions_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRegions_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRegions_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRegions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRegions_AddedBy",
                schema: "People",
                table: "UserRegions",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegions_DeletedBy",
                schema: "People",
                table: "UserRegions",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegions_ModifiedBy",
                schema: "People",
                table: "UserRegions",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegions_RegionId",
                schema: "People",
                table: "UserRegions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegions_UserId",
                schema: "People",
                table: "UserRegions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityID",
                schema: "People",
                table: "Users",
                column: "CityID",
                principalSchema: "Guide",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityID",
                schema: "People",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserRegions",
                schema: "People");

            migrationBuilder.RenameColumn(
                name: "CityID",
                schema: "People",
                table: "Users",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CityID",
                schema: "People",
                table: "Users",
                newName: "IX_Users_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                schema: "People",
                table: "Users",
                column: "CityId",
                principalSchema: "Guide",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
