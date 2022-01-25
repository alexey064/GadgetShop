using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplom.Migrations
{
    public partial class start4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemoryCount",
                table: "Smartphones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemoryCount",
                table: "Smartphones");
        }
    }
}
