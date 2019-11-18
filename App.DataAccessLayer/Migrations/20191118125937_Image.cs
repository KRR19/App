using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DataAccessLayer.Migrations
{
    public partial class Image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoverId",
                table: "PrintingEditions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cover",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    File = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cover", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrintingEditions_CoverId",
                table: "PrintingEditions",
                column: "CoverId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrintingEditions_Cover_CoverId",
                table: "PrintingEditions",
                column: "CoverId",
                principalTable: "Cover",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintingEditions_Cover_CoverId",
                table: "PrintingEditions");

            migrationBuilder.DropTable(
                name: "Cover");

            migrationBuilder.DropIndex(
                name: "IX_PrintingEditions_CoverId",
                table: "PrintingEditions");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "PrintingEditions");
        }
    }
}
