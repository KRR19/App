using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DataAccessLayer.Migrations
{
    public partial class Image_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrintingEditions_Cover_CoverId",
                table: "PrintingEditions");

            migrationBuilder.DropIndex(
                name: "IX_PrintingEditions_CoverId",
                table: "PrintingEditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cover",
                table: "Cover");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "PrintingEditions");

            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Cover");

            migrationBuilder.RenameTable(
                name: "Cover",
                newName: "Covers");

            migrationBuilder.AddColumn<string>(
                name: "Base64Image",
                table: "Covers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrintingEditionId",
                table: "Covers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Covers",
                table: "Covers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Covers_PrintingEditionId",
                table: "Covers",
                column: "PrintingEditionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Covers_PrintingEditions_PrintingEditionId",
                table: "Covers",
                column: "PrintingEditionId",
                principalTable: "PrintingEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Covers_PrintingEditions_PrintingEditionId",
                table: "Covers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Covers",
                table: "Covers");

            migrationBuilder.DropIndex(
                name: "IX_Covers_PrintingEditionId",
                table: "Covers");

            migrationBuilder.DropColumn(
                name: "Base64Image",
                table: "Covers");

            migrationBuilder.DropColumn(
                name: "PrintingEditionId",
                table: "Covers");

            migrationBuilder.RenameTable(
                name: "Covers",
                newName: "Cover");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverId",
                table: "PrintingEditions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "Cover",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cover",
                table: "Cover",
                column: "Id");

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
    }
}
