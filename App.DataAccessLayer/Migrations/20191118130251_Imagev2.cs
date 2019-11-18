using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DataAccessLayer.Migrations
{
    public partial class Imagev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Cover");

            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "Cover",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Cover");

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Cover",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
