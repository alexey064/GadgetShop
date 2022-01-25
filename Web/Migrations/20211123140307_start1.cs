using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplom.Migrations
{
    public partial class start1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "MovementTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OS",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OS", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Processors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScreenTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videocards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videocards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Types_PostId",
                        column: x => x.PostId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    DiscountDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<int>(type: "int", nullable: false),
                    ProdMovementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseHistories_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseHistories_Clients_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseHistories_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseHistories_Types_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accessories_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notebooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessorId = table.Column<int>(type: "int", nullable: false),
                    OSId = table.Column<int>(type: "int", nullable: false),
                    RAMCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    HDDSize = table.Column<int>(type: "int", nullable: false),
                    SSDSize = table.Column<int>(type: "int", nullable: false),
                    ScreenSize = table.Column<double>(type: "float", nullable: false),
                    ScreenResolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenTypeId = table.Column<int>(type: "int", nullable: true),
                    BatteryCapacity = table.Column<short>(type: "smallint", nullable: true),
                    Outputs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notebooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notebooks_OS_OSId",
                        column: x => x.OSId,
                        principalTable: "OS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notebooks_Processors_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "Processors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notebooks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notebooks_ScreenTypes_ScreenTypeId",
                        column: x => x.ScreenTypeId,
                        principalTable: "ScreenTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Smartphones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenTypeId = table.Column<int>(type: "int", nullable: false),
                    OSId = table.Column<int>(type: "int", nullable: false),
                    RAMCount = table.Column<byte>(type: "tinyint", nullable: false),
                    BatteryCapacity = table.Column<int>(type: "int", nullable: false),
                    Camera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimensional = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    ScreenSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessorId = table.Column<int>(type: "int", nullable: false),
                    Optional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smartphones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Smartphones_OS_OSId",
                        column: x => x.OSId,
                        principalTable: "OS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Smartphones_Processors_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "Processors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Smartphones_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Smartphones_ScreenTypes_ScreenTypeId",
                        column: x => x.ScreenTypeId,
                        principalTable: "ScreenTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WireHeadphones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WireLenght = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WireHeadphones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WireHeadphones_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WirelessHeadphones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BluetoothVersion = table.Column<double>(type: "float", nullable: false),
                    Battery = table.Column<short>(type: "smallint", nullable: true),
                    CaseBattery = table.Column<short>(type: "smallint", nullable: true),
                    ChargingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WirelessHeadphones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WirelessHeadphones_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    MovementTypeId = table.Column<int>(type: "int", nullable: false),
                    MoveId = table.Column<int>(type: "int", nullable: true),
                    Id1Id = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdMovements_MovementTypes_MovementTypeId",
                        column: x => x.MovementTypeId,
                        principalTable: "MovementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdMovements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdMovements_Providers_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdMovements_PurchaseHistories_Id1Id",
                        column: x => x.Id1Id,
                        principalTable: "PurchaseHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdMovements_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_ProductID",
                table: "Accessories",
                column: "ProductID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DepartmentId",
                table: "Clients",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PostId",
                table: "Clients",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_OSId",
                table: "Notebooks",
                column: "OSId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_ProcessorId",
                table: "Notebooks",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_ProductId",
                table: "Notebooks",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_ScreenTypeId",
                table: "Notebooks",
                column: "ScreenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdMovements_Id1Id",
                table: "ProdMovements",
                column: "Id1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProdMovements_MoveId",
                table: "ProdMovements",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdMovements_MovementTypeId",
                table: "ProdMovements",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdMovements_ProductId",
                table: "ProdMovements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdMovements_TypeId",
                table: "ProdMovements",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ColorId",
                table: "Products",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DepartmentId",
                table: "Products",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_DepartmentId",
                table: "Providers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_ClientId",
                table: "PurchaseHistories",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_DepartmentId",
                table: "PurchaseHistories",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_SellerId",
                table: "PurchaseHistories",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_StatusId",
                table: "PurchaseHistories",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Smartphones_OSId",
                table: "Smartphones",
                column: "OSId");

            migrationBuilder.CreateIndex(
                name: "IX_Smartphones_ProcessorId",
                table: "Smartphones",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Smartphones_ProductId",
                table: "Smartphones",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Smartphones_ScreenTypeId",
                table: "Smartphones",
                column: "ScreenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WireHeadphones_ProductId",
                table: "WireHeadphones",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WirelessHeadphones_ProductId",
                table: "WirelessHeadphones",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropTable(
                name: "Notebooks");

            migrationBuilder.DropTable(
                name: "ProdMovements");

            migrationBuilder.DropTable(
                name: "Smartphones");

            migrationBuilder.DropTable(
                name: "Videocards");

            migrationBuilder.DropTable(
                name: "WireHeadphones");

            migrationBuilder.DropTable(
                name: "WirelessHeadphones");

            migrationBuilder.DropTable(
                name: "MovementTypes");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "PurchaseHistories");

            migrationBuilder.DropTable(
                name: "OS");

            migrationBuilder.DropTable(
                name: "Processors");

            migrationBuilder.DropTable(
                name: "ScreenTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
