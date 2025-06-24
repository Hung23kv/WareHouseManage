using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WareHouse.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSupier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account",
                table: "Suppliers");
        }
    }
}
