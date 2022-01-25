using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplom.Migrations
{
    public partial class start12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdMovements_PurchaseHistories_Id1Id",
                table: "ProdMovements");

            migrationBuilder.RenameColumn(
                name: "Id1Id",
                table: "ProdMovements",
                newName: "PurchaseHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdMovements_Id1Id",
                table: "ProdMovements",
                newName: "IX_ProdMovements_PurchaseHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdMovements_PurchaseHistories_PurchaseHistoryId",
                table: "ProdMovements",
                column: "PurchaseHistoryId",
                principalTable: "PurchaseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdMovements_PurchaseHistories_PurchaseHistoryId",
                table: "ProdMovements");

            migrationBuilder.RenameColumn(
                name: "PurchaseHistoryId",
                table: "ProdMovements",
                newName: "Id1Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProdMovements_PurchaseHistoryId",
                table: "ProdMovements",
                newName: "IX_ProdMovements_Id1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdMovements_PurchaseHistories_Id1Id",
                table: "ProdMovements",
                column: "Id1Id",
                principalTable: "PurchaseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
