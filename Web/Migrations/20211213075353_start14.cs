using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class start14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdMovementId",
                table: "PurchaseHistories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdMovementId",
                table: "PurchaseHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
