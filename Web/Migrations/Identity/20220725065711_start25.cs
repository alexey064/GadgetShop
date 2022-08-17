using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations.Identity
{
    public partial class start25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "RefreshToken",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: true);

            migrationBuilder.AddColumn<int>(
            name: "RefreshTokenExpiryTime",
            table: "AspNetUsers",
            type: "datetime2",
            nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "RefreshToken",
            table: "AspNetUsers");

            migrationBuilder.DropColumn(
            name: "RefreshTokenExpiryTime",
            table: "AspNetUsers");
        }
    }
}