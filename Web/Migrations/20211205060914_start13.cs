using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class start13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdMovements_Providers_MoveId",
                table: "ProdMovements");

            migrationBuilder.RenameColumn(
                name: "MoveId",
                table: "ProdMovements",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdMovements_MoveId",
                table: "ProdMovements",
                newName: "IX_ProdMovements_ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdMovements_Providers_ProviderId",
                table: "ProdMovements",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdMovements_Providers_ProviderId",
                table: "ProdMovements");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "ProdMovements",
                newName: "MoveId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdMovements_ProviderId",
                table: "ProdMovements",
                newName: "IX_ProdMovements_MoveId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdMovements_Providers_MoveId",
                table: "ProdMovements",
                column: "MoveId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
