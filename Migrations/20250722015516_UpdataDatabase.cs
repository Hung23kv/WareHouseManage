using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WareHouse.Migrations
{
    /// <inheritdoc />
    public partial class UpdataDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RolesIdRole",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RolesIdRole",
                table: "Users",
                newName: "RolesidRole");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RolesIdRole",
                table: "Users",
                newName: "IX_Users_RolesidRole");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "ItemRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RolesidRole",
                table: "Users",
                column: "RolesidRole",
                principalTable: "Roles",
                principalColumn: "IdRole",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RolesidRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "ItemRequests");

            migrationBuilder.RenameColumn(
                name: "RolesidRole",
                table: "Users",
                newName: "RolesIdRole");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RolesidRole",
                table: "Users",
                newName: "IX_Users_RolesIdRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RolesIdRole",
                table: "Users",
                column: "RolesIdRole",
                principalTable: "Roles",
                principalColumn: "IdRole",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
