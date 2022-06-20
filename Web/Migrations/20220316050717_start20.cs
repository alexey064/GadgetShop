using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class start20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "PurchaseHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPurchaseAgree",
                table: "PurchaseHistories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "IsPurchaseAgree",
                table: "PurchaseHistories");
        }
    }
}
