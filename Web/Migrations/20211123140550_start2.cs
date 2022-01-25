using Microsoft.EntityFrameworkCore.Migrations;

namespace Diplom.Migrations
{
    public partial class start2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideocardID",
                table: "Notebooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_VideocardID",
                table: "Notebooks",
                column: "VideocardID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notebooks_Videocards_VideocardID",
                table: "Notebooks",
                column: "VideocardID",
                principalTable: "Videocards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notebooks_Videocards_VideocardID",
                table: "Notebooks");

            migrationBuilder.DropIndex(
                name: "IX_Notebooks_VideocardID",
                table: "Notebooks");

            migrationBuilder.DropColumn(
                name: "VideocardID",
                table: "Notebooks");
        }
    }
}
