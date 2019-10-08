using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DataAccessLayer.Migrations
{
    public partial class AddAuthorDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateBirth",
                table: "Authors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeath",
                table: "Authors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBirth",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DateDeath",
                table: "Authors");
        }
    }
}
