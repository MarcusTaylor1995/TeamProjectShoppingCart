using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Data.Migrations
{
    public partial class OrderEntityFixed10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "username",
                table: "Orders");
        }
    }
}
