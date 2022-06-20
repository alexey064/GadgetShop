using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class start10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ChargingType",
                table: "WirelessHeadphones");

            migrationBuilder.DropColumn(
                name: "ConnectionType",
                table: "WireHeadphones");

            migrationBuilder.AlterColumn<int>(
                name: "ChargingTypeId",
                table: "WirelessHeadphones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChargingTypeId",
                table: "WireHeadphones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChargingTypeId",
                table: "Smartphones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Smartphones_ChargingTypes_ChargingTypeId",
                table: "Smartphones",
                column: "ChargingTypeId",
                principalTable: "ChargingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WireHeadphones_ChargingTypes_ChargingTypeId",
                table: "WireHeadphones",
                column: "ChargingTypeId",
                principalTable: "ChargingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WirelessHeadphones_ChargingTypes_ChargingTypeId",
                table: "WirelessHeadphones",
                column: "ChargingTypeId",
                principalTable: "ChargingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "ChargingTypeId",
                table: "WirelessHeadphones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ChargingType",
                table: "WirelessHeadphones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChargingTypeId",
                table: "WireHeadphones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionType",
                table: "WireHeadphones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChargingTypeId",
                table: "Smartphones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
