using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class start11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WireHeadphones_ChargingTypes_ChargingTypeId",
                table: "WireHeadphones");

            migrationBuilder.RenameColumn(
                name: "ChargingTypeId",
                table: "WireHeadphones",
                newName: "ConnectionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WireHeadphones_ChargingTypeId",
                table: "WireHeadphones",
                newName: "IX_WireHeadphones_ConnectionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WireHeadphones_ChargingTypes_ConnectionTypeId",
                table: "WireHeadphones",
                column: "ConnectionTypeId",
                principalTable: "ChargingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WireHeadphones_ChargingTypes_ConnectionTypeId",
                table: "WireHeadphones");

            migrationBuilder.RenameColumn(
                name: "ConnectionTypeId",
                table: "WireHeadphones",
                newName: "ChargingTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WireHeadphones_ConnectionTypeId",
                table: "WireHeadphones",
                newName: "IX_WireHeadphones_ChargingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WireHeadphones_ChargingTypes_ChargingTypeId",
                table: "WireHeadphones",
                column: "ChargingTypeId",
                principalTable: "ChargingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
