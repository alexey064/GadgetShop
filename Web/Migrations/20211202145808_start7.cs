using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplom.Migrations
{
    public partial class start7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChargingType",
                table: "WirelessHeadphones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ChargingTypeId",
                table: "WirelessHeadphones",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConnectionType",
                table: "WireHeadphones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ChargingTypeId",
                table: "WireHeadphones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChargingTypeId",
                table: "Smartphones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChargingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WirelessHeadphones_ChargingTypeId",
                table: "WirelessHeadphones",
                column: "ChargingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WireHeadphones_ChargingTypeId",
                table: "WireHeadphones",
                column: "ChargingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Smartphones_ChargingTypeId",
                table: "Smartphones",
                column: "ChargingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Smartphones_ChargingTypes_ChargingTypeId",
                table: "Smartphones",
                column: "ChargingTypeId",
                principalTable: "ChargingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WireHeadphones_ChargingTypes_ChargingTypeId",
                table: "WireHeadphones",
                column: "ChargingTypeId",
                principalTable: "ChargingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WirelessHeadphones_ChargingTypes_ChargingTypeId",
                table: "WirelessHeadphones",
                column: "ChargingTypeId",
                principalTable: "ChargingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Smartphones_ChargingTypes_ChargingTypeId",
                table: "Smartphones");

            migrationBuilder.DropForeignKey(
                name: "FK_WireHeadphones_ChargingTypes_ChargingTypeId",
                table: "WireHeadphones");

            migrationBuilder.DropForeignKey(
                name: "FK_WirelessHeadphones_ChargingTypes_ChargingTypeId",
                table: "WirelessHeadphones");

            migrationBuilder.DropTable(
                name: "ChargingTypes");

            migrationBuilder.DropIndex(
                name: "IX_WirelessHeadphones_ChargingTypeId",
                table: "WirelessHeadphones");

            migrationBuilder.DropIndex(
                name: "IX_WireHeadphones_ChargingTypeId",
                table: "WireHeadphones");

            migrationBuilder.DropIndex(
                name: "IX_Smartphones_ChargingTypeId",
                table: "Smartphones");

            migrationBuilder.DropColumn(
                name: "ChargingTypeId",
                table: "WirelessHeadphones");

            migrationBuilder.DropColumn(
                name: "ChargingTypeId",
                table: "WireHeadphones");

            migrationBuilder.DropColumn(
                name: "ChargingTypeId",
                table: "Smartphones");

            migrationBuilder.AlterColumn<string>(
                name: "ChargingType",
                table: "WirelessHeadphones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConnectionType",
                table: "WireHeadphones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
