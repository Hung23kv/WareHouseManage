using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WareHouse.Migrations
{
    /// <inheritdoc />
    public partial class renameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DelayOrders_Orders_OrdersIdOder",
                table: "DelayOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailOrders_Orders_OrdersIdOder",
                table: "DetailOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailRequests_ItemRequests_ItemRequestsidItemRequest",
                table: "DetailRequests");

            migrationBuilder.DropColumn(
                name: "IsPresent",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StatusOrder",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "ItemRequests");

            migrationBuilder.DropColumn(
                name: "IsReceive",
                table: "ItemRequests");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "ItemRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DetailOrders");

            migrationBuilder.RenameColumn(
                name: "WorkingDate",
                table: "TimeSheets",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "IdOder",
                table: "Orders",
                newName: "IdOrder");

            migrationBuilder.RenameColumn(
                name: "idItemRequest",
                table: "ItemRequests",
                newName: "IdItemRequest");

            migrationBuilder.RenameColumn(
                name: "ItemRequestsidItemRequest",
                table: "DetailRequests",
                newName: "ItemRequestsIdItemRequest");

            migrationBuilder.RenameColumn(
                name: "ItemQuantity",
                table: "DetailRequests",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_DetailRequests_ItemRequestsidItemRequest",
                table: "DetailRequests",
                newName: "IX_DetailRequests_ItemRequestsIdItemRequest");

            migrationBuilder.RenameColumn(
                name: "OrdersIdOder",
                table: "DetailOrders",
                newName: "OrdersIdOrder");

            migrationBuilder.RenameIndex(
                name: "IX_DetailOrders_OrdersIdOder",
                table: "DetailOrders",
                newName: "IX_DetailOrders_OrdersIdOrder");

            migrationBuilder.RenameColumn(
                name: "OrdersIdOder",
                table: "DelayOrders",
                newName: "OrdersIdOrder");

            migrationBuilder.RenameIndex(
                name: "IX_DelayOrders_OrdersIdOder",
                table: "DelayOrders",
                newName: "IX_DelayOrders_OrdersIdOrder");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CheckIn",
                table: "TimeSheets",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CheckOut",
                table: "TimeSheets",
                type: "time",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ItemRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "ItemRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ItemRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "DetailOrders",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_DelayOrders_Orders_OrdersIdOrder",
                table: "DelayOrders",
                column: "OrdersIdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailOrders_Orders_OrdersIdOrder",
                table: "DetailOrders",
                column: "OrdersIdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailRequests_ItemRequests_ItemRequestsIdItemRequest",
                table: "DetailRequests",
                column: "ItemRequestsIdItemRequest",
                principalTable: "ItemRequests",
                principalColumn: "IdItemRequest",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DelayOrders_Orders_OrdersIdOrder",
                table: "DelayOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailOrders_Orders_OrdersIdOrder",
                table: "DetailOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailRequests_ItemRequests_ItemRequestsIdItemRequest",
                table: "DetailRequests");

            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ItemRequests");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "ItemRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ItemRequests");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TimeSheets",
                newName: "WorkingDate");

            migrationBuilder.RenameColumn(
                name: "IdOrder",
                table: "Orders",
                newName: "IdOder");

            migrationBuilder.RenameColumn(
                name: "IdItemRequest",
                table: "ItemRequests",
                newName: "idItemRequest");

            migrationBuilder.RenameColumn(
                name: "ItemRequestsIdItemRequest",
                table: "DetailRequests",
                newName: "ItemRequestsidItemRequest");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "DetailRequests",
                newName: "ItemQuantity");

            migrationBuilder.RenameIndex(
                name: "IX_DetailRequests_ItemRequestsIdItemRequest",
                table: "DetailRequests",
                newName: "IX_DetailRequests_ItemRequestsidItemRequest");

            migrationBuilder.RenameColumn(
                name: "OrdersIdOrder",
                table: "DetailOrders",
                newName: "OrdersIdOder");

            migrationBuilder.RenameIndex(
                name: "IX_DetailOrders_OrdersIdOrder",
                table: "DetailOrders",
                newName: "IX_DetailOrders_OrdersIdOder");

            migrationBuilder.RenameColumn(
                name: "OrdersIdOrder",
                table: "DelayOrders",
                newName: "OrdersIdOder");

            migrationBuilder.RenameIndex(
                name: "IX_DelayOrders_OrdersIdOrder",
                table: "DelayOrders",
                newName: "IX_DelayOrders_OrdersIdOder");

            migrationBuilder.AddColumn<bool>(
                name: "IsPresent",
                table: "TimeSheets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TimeSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusOrder",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "ItemRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReceive",
                table: "ItemRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "ItemRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "DetailOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DetailOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_DelayOrders_Orders_OrdersIdOder",
                table: "DelayOrders",
                column: "OrdersIdOder",
                principalTable: "Orders",
                principalColumn: "IdOder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailOrders_Orders_OrdersIdOder",
                table: "DetailOrders",
                column: "OrdersIdOder",
                principalTable: "Orders",
                principalColumn: "IdOder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailRequests_ItemRequests_ItemRequestsidItemRequest",
                table: "DetailRequests",
                column: "ItemRequestsidItemRequest",
                principalTable: "ItemRequests",
                principalColumn: "idItemRequest",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
