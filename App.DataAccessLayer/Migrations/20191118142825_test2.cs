using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DataAccessLayer.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Covers_PrintingEditions_PrintingEditionId",
                table: "Covers");

            migrationBuilder.DropIndex(
                name: "IX_Covers_PrintingEditionId",
                table: "Covers");

            migrationBuilder.AlterColumn<Guid>(
                name: "PrintingEditionId",
                table: "Covers",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Covers_PrintingEditionId",
                table: "Covers",
                column: "PrintingEditionId",
                unique: true,
                filter: "[PrintingEditionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Covers_PrintingEditions_PrintingEditionId",
                table: "Covers",
                column: "PrintingEditionId",
                principalTable: "PrintingEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Covers_PrintingEditions_PrintingEditionId",
                table: "Covers");

            migrationBuilder.DropIndex(
                name: "IX_Covers_PrintingEditionId",
                table: "Covers");

            migrationBuilder.AlterColumn<Guid>(
                name: "PrintingEditionId",
                table: "Covers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

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
    }
}
