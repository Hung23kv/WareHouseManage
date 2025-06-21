using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WareHouse.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCategory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    IdSupplier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.IdSupplier);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RolesIdRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RolesIdRole",
                        column: x => x.RolesIdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    IdOder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusOrder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuppliersIdSupplier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.IdOder);
                    table.ForeignKey(
                        name: "FK_Orders_Suppliers_SuppliersIdSupplier",
                        column: x => x.SuppliersIdSupplier,
                        principalTable: "Suppliers",
                        principalColumn: "IdSupplier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    IdProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameProduct = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    remainingQuantity = table.Column<int>(type: "int", nullable: false),
                    CategorysIdCategory = table.Column<int>(type: "int", nullable: false),
                    SuppliersIdSupplier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.IdProduct);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategorysIdCategory",
                        column: x => x.CategorysIdCategory,
                        principalTable: "Categories",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SuppliersIdSupplier",
                        column: x => x.SuppliersIdSupplier,
                        principalTable: "Suppliers",
                        principalColumn: "IdSupplier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemRequests",
                columns: table => new
                {
                    idItemRequest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    UsersIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemRequests", x => x.idItemRequest);
                    table.ForeignKey(
                        name: "FK_ItemRequests_Users_UsersIdUser",
                        column: x => x.UsersIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    IdTimeSheet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    WorkingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsersIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.IdTimeSheet);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Users_UsersIdUser",
                        column: x => x.UsersIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DelayOrders",
                columns: table => new
                {
                    IdDelayOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DelayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrdersIdOder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelayOrders", x => x.IdDelayOrder);
                    table.ForeignKey(
                        name: "FK_DelayOrders_Orders_OrdersIdOder",
                        column: x => x.OrdersIdOder,
                        principalTable: "Orders",
                        principalColumn: "IdOder",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailOrders",
                columns: table => new
                {
                    IdDetailOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrdersIdOder = table.Column<int>(type: "int", nullable: false),
                    ProductsIdProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailOrders", x => x.IdDetailOrder);
                    table.ForeignKey(
                        name: "FK_DetailOrders_Orders_OrdersIdOder",
                        column: x => x.OrdersIdOder,
                        principalTable: "Orders",
                        principalColumn: "IdOder",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailOrders_Products_ProductsIdProduct",
                        column: x => x.ProductsIdProduct,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OutHistories",
                columns: table => new
                {
                    IdOutHistory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutQuantity = table.Column<int>(type: "int", nullable: false),
                    OutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductsIdProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutHistories", x => x.IdOutHistory);
                    table.ForeignKey(
                        name: "FK_OutHistories_Products_ProductsIdProduct",
                        column: x => x.ProductsIdProduct,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailRequests",
                columns: table => new
                {
                    IdDetailRequest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemQuantity = table.Column<int>(type: "int", nullable: false),
                    ItemRequestsidItemRequest = table.Column<int>(type: "int", nullable: false),
                    ProductsIdProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailRequests", x => x.IdDetailRequest);
                    table.ForeignKey(
                        name: "FK_DetailRequests_ItemRequests_ItemRequestsidItemRequest",
                        column: x => x.ItemRequestsidItemRequest,
                        principalTable: "ItemRequests",
                        principalColumn: "idItemRequest",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailRequests_Products_ProductsIdProduct",
                        column: x => x.ProductsIdProduct,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DelayOrders_OrdersIdOder",
                table: "DelayOrders",
                column: "OrdersIdOder");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrders_OrdersIdOder",
                table: "DetailOrders",
                column: "OrdersIdOder");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrders_ProductsIdProduct",
                table: "DetailOrders",
                column: "ProductsIdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_DetailRequests_ItemRequestsidItemRequest",
                table: "DetailRequests",
                column: "ItemRequestsidItemRequest");

            migrationBuilder.CreateIndex(
                name: "IX_DetailRequests_ProductsIdProduct",
                table: "DetailRequests",
                column: "ProductsIdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_ItemRequests_UsersIdUser",
                table: "ItemRequests",
                column: "UsersIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SuppliersIdSupplier",
                table: "Orders",
                column: "SuppliersIdSupplier");

            migrationBuilder.CreateIndex(
                name: "IX_OutHistories_ProductsIdProduct",
                table: "OutHistories",
                column: "ProductsIdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategorysIdCategory",
                table: "Products",
                column: "CategorysIdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SuppliersIdSupplier",
                table: "Products",
                column: "SuppliersIdSupplier");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_UsersIdUser",
                table: "TimeSheets",
                column: "UsersIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RolesIdRole",
                table: "Users",
                column: "RolesIdRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelayOrders");

            migrationBuilder.DropTable(
                name: "DetailOrders");

            migrationBuilder.DropTable(
                name: "DetailRequests");

            migrationBuilder.DropTable(
                name: "OutHistories");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ItemRequests");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
