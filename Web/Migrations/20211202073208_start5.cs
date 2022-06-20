using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class start5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Radius",
                table: "WirelessHeadphones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AddInfo",
                table: "Processors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Radius",
                table: "WirelessHeadphones");

            migrationBuilder.DropColumn(
                name: "AddInfo",
                table: "Processors");
        }
    }
}
