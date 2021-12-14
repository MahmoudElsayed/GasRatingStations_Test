using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class db_21072021_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserType_UserTypeId",
                schema: "People",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserType_Users_AddedBy",
                table: "UserType");

            migrationBuilder.DropForeignKey(
                name: "FK_UserType_Users_DeletedBy",
                table: "UserType");

            migrationBuilder.DropForeignKey(
                name: "FK_UserType_Users_ModifiedBy",
                table: "UserType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserType",
                table: "UserType");

            migrationBuilder.EnsureSchema(
                name: "Guide");

            migrationBuilder.RenameTable(
                name: "UserType",
                newName: "UserTypes");

            migrationBuilder.RenameIndex(
                name: "IX_UserType_ModifiedBy",
                table: "UserTypes",
                newName: "IX_UserTypes_ModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_UserType_DeletedBy",
                table: "UserTypes",
                newName: "IX_UserTypes_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_UserType_AddedBy",
                table: "UserTypes",
                newName: "IX_UserTypes_AddedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTypes",
                table: "UserTypes",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "GasStations",
                schema: "Guide",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LauncherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_GasStations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GasStations_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GasStations_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GasStations_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GasStationContacts",
                schema: "Guide",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GasStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_GasStationContacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GasStationContacts_GasStations_GasStationId",
                        column: x => x.GasStationId,
                        principalSchema: "Guide",
                        principalTable: "GasStations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GasStationContacts_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GasStationContacts_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GasStationContacts_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "People",
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GasStationContacts_AddedBy",
                schema: "Guide",
                table: "GasStationContacts",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GasStationContacts_DeletedBy",
                schema: "Guide",
                table: "GasStationContacts",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GasStationContacts_GasStationId",
                schema: "Guide",
                table: "GasStationContacts",
                column: "GasStationId");

            migrationBuilder.CreateIndex(
                name: "IX_GasStationContacts_ModifiedBy",
                schema: "Guide",
                table: "GasStationContacts",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GasStations_AddedBy",
                schema: "Guide",
                table: "GasStations",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GasStations_DeletedBy",
                schema: "Guide",
                table: "GasStations",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GasStations_ModifiedBy",
                schema: "Guide",
                table: "GasStations",
                column: "ModifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                schema: "People",
                table: "Users",
                column: "UserTypeId",
                principalTable: "UserTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTypes_Users_AddedBy",
                table: "UserTypes",
                column: "AddedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTypes_Users_DeletedBy",
                table: "UserTypes",
                column: "DeletedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTypes_Users_ModifiedBy",
                table: "UserTypes",
                column: "ModifiedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                schema: "People",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTypes_Users_AddedBy",
                table: "UserTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTypes_Users_DeletedBy",
                table: "UserTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTypes_Users_ModifiedBy",
                table: "UserTypes");

            migrationBuilder.DropTable(
                name: "GasStationContacts",
                schema: "Guide");

            migrationBuilder.DropTable(
                name: "GasStations",
                schema: "Guide");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTypes",
                table: "UserTypes");

            migrationBuilder.RenameTable(
                name: "UserTypes",
                newName: "UserType");

            migrationBuilder.RenameIndex(
                name: "IX_UserTypes_ModifiedBy",
                table: "UserType",
                newName: "IX_UserType_ModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_UserTypes_DeletedBy",
                table: "UserType",
                newName: "IX_UserType_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_UserTypes_AddedBy",
                table: "UserType",
                newName: "IX_UserType_AddedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserType",
                table: "UserType",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserType_UserTypeId",
                schema: "People",
                table: "Users",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserType_Users_AddedBy",
                table: "UserType",
                column: "AddedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserType_Users_DeletedBy",
                table: "UserType",
                column: "DeletedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserType_Users_ModifiedBy",
                table: "UserType",
                column: "ModifiedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
