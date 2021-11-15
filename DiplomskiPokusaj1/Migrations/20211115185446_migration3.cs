using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiPokusaj1.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCopy_Materials_MaterialId",
                table: "MaterialCopy");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCopyRent_MaterialCopy_MaterialCopiesId",
                table: "MaterialCopyRent");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCopyReservation_MaterialCopy_MaterialCopiesId",
                table: "MaterialCopyReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialCopy",
                table: "MaterialCopy");

            migrationBuilder.RenameTable(
                name: "MaterialCopy",
                newName: "MaterialCopies");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialCopy_MaterialId",
                table: "MaterialCopies",
                newName: "IX_MaterialCopies_MaterialId");

            migrationBuilder.AddColumn<string>(
                name: "UniqueCode",
                table: "MaterialCopies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialCopies",
                table: "MaterialCopies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCopies_Materials_MaterialId",
                table: "MaterialCopies",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCopyRent_MaterialCopies_MaterialCopiesId",
                table: "MaterialCopyRent",
                column: "MaterialCopiesId",
                principalTable: "MaterialCopies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCopyReservation_MaterialCopies_MaterialCopiesId",
                table: "MaterialCopyReservation",
                column: "MaterialCopiesId",
                principalTable: "MaterialCopies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCopies_Materials_MaterialId",
                table: "MaterialCopies");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCopyRent_MaterialCopies_MaterialCopiesId",
                table: "MaterialCopyRent");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialCopyReservation_MaterialCopies_MaterialCopiesId",
                table: "MaterialCopyReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialCopies",
                table: "MaterialCopies");

            migrationBuilder.DropColumn(
                name: "UniqueCode",
                table: "MaterialCopies");

            migrationBuilder.RenameTable(
                name: "MaterialCopies",
                newName: "MaterialCopy");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialCopies_MaterialId",
                table: "MaterialCopy",
                newName: "IX_MaterialCopy_MaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialCopy",
                table: "MaterialCopy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCopy_Materials_MaterialId",
                table: "MaterialCopy",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCopyRent_MaterialCopy_MaterialCopiesId",
                table: "MaterialCopyRent",
                column: "MaterialCopiesId",
                principalTable: "MaterialCopy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialCopyReservation_MaterialCopy_MaterialCopiesId",
                table: "MaterialCopyReservation",
                column: "MaterialCopiesId",
                principalTable: "MaterialCopy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
