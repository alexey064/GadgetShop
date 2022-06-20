using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class start3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimensional",
                table: "Smartphones");

            migrationBuilder.AlterColumn<double>(
                name: "ScreenSize",
                table: "Smartphones",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Communication",
                table: "Smartphones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NFC",
                table: "Smartphones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneSize",
                table: "Smartphones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScreenResolution",
                table: "Smartphones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "SimCount",
                table: "Smartphones",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Camera",
                table: "Notebooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Optional",
                table: "Notebooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WirelessCommunication",
                table: "Notebooks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Communication",
                table: "Smartphones");

            migrationBuilder.DropColumn(
                name: "NFC",
                table: "Smartphones");

            migrationBuilder.DropColumn(
                name: "PhoneSize",
                table: "Smartphones");

            migrationBuilder.DropColumn(
                name: "ScreenResolution",
                table: "Smartphones");

            migrationBuilder.DropColumn(
                name: "SimCount",
                table: "Smartphones");

            migrationBuilder.DropColumn(
                name: "Camera",
                table: "Notebooks");

            migrationBuilder.DropColumn(
                name: "Optional",
                table: "Notebooks");

            migrationBuilder.DropColumn(
                name: "WirelessCommunication",
                table: "Notebooks");

            migrationBuilder.AlterColumn<string>(
                name: "ScreenSize",
                table: "Smartphones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "Dimensional",
                table: "Smartphones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
