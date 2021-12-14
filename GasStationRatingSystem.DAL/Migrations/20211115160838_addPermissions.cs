using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStationRatingSystem.DAL.Migrations
{
    public partial class addPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                schema: "People",
                table: "UserPermissions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "People",
                table: "UserPermissions",
                newName: "UserTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_UserId",
                schema: "People",
                table: "UserPermissions",
                newName: "IX_UserPermissions_UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_UserTypes_UserTypeId",
                schema: "People",
                table: "UserPermissions",
                column: "UserTypeId",
                principalSchema: "Guide",
                principalTable: "UserTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_UserTypes_UserTypeId",
                schema: "People",
                table: "UserPermissions");

            migrationBuilder.RenameColumn(
                name: "UserTypeId",
                schema: "People",
                table: "UserPermissions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_UserTypeId",
                schema: "People",
                table: "UserPermissions",
                newName: "IX_UserPermissions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                schema: "People",
                table: "UserPermissions",
                column: "UserId",
                principalSchema: "People",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
