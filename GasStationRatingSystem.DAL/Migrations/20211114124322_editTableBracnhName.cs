using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class editTableBracnhName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_Users_AddedBy",
                schema: "Guide",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_Users_DeletedBy",
                schema: "Guide",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_Users_ModifiedBy",
                schema: "Guide",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Branchs_BranchId",
                schema: "Guide",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branchs",
                schema: "Guide",
                table: "Branchs");

            migrationBuilder.RenameTable(
                name: "Branchs",
                schema: "Guide",
                newName: "Branches",
                newSchema: "Guide");

            migrationBuilder.RenameIndex(
                name: "IX_Branchs_ModifiedBy",
                schema: "Guide",
                table: "Branches",
                newName: "IX_Branches_ModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Branchs_DeletedBy",
                schema: "Guide",
                table: "Branches",
                newName: "IX_Branches_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Branchs_AddedBy",
                schema: "Guide",
                table: "Branches",
                newName: "IX_Branches_AddedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                schema: "Guide",
                table: "Branches",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Users_AddedBy",
                schema: "Guide",
                table: "Branches",
                column: "AddedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Users_DeletedBy",
                schema: "Guide",
                table: "Branches",
                column: "DeletedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Users_ModifiedBy",
                schema: "Guide",
                table: "Branches",
                column: "ModifiedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Branches_BranchId",
                schema: "Guide",
                table: "Regions",
                column: "BranchId",
                principalSchema: "Guide",
                principalTable: "Branches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Users_AddedBy",
                schema: "Guide",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Users_DeletedBy",
                schema: "Guide",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Users_ModifiedBy",
                schema: "Guide",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Branches_BranchId",
                schema: "Guide",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                schema: "Guide",
                table: "Branches");

            migrationBuilder.RenameTable(
                name: "Branches",
                schema: "Guide",
                newName: "Branchs",
                newSchema: "Guide");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_ModifiedBy",
                schema: "Guide",
                table: "Branchs",
                newName: "IX_Branchs_ModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_DeletedBy",
                schema: "Guide",
                table: "Branchs",
                newName: "IX_Branchs_DeletedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_AddedBy",
                schema: "Guide",
                table: "Branchs",
                newName: "IX_Branchs_AddedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branchs",
                schema: "Guide",
                table: "Branchs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Users_AddedBy",
                schema: "Guide",
                table: "Branchs",
                column: "AddedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Users_DeletedBy",
                schema: "Guide",
                table: "Branchs",
                column: "DeletedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Users_ModifiedBy",
                schema: "Guide",
                table: "Branchs",
                column: "ModifiedBy",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Branchs_BranchId",
                schema: "Guide",
                table: "Regions",
                column: "BranchId",
                principalSchema: "Guide",
                principalTable: "Branchs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
