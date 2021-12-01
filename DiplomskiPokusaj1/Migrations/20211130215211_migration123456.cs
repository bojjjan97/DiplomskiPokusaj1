using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiPokusaj1.Migrations
{
    public partial class migration123456 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Authors_AuthorId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Libraries_LibraryId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Materials_MaterialId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Publishers_PublisherId",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_AuthorId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_LibraryId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_MaterialId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_PublisherId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "AuhtorId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Publishers",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Materials",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Libraries",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Authors",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Images",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Images",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_ImageId",
                table: "Publishers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ImageId",
                table: "Materials",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_ImageId",
                table: "Libraries",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ImageId",
                table: "Authors",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ImageId",
                table: "AspNetUsers",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_ImageId",
                table: "AspNetUsers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Images_ImageId",
                table: "Authors",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Images_ImageId",
                table: "Libraries",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Images_ImageId",
                table: "Materials",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Images_ImageId",
                table: "Publishers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_ImageId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Images_ImageId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Images_ImageId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Images_ImageId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Images_ImageId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_ImageId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Materials_ImageId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Libraries_ImageId",
                table: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Authors_ImageId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ImageId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Image",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Image",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AuhtorId",
                table: "Image",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Image",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LibraryId",
                table: "Image",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MaterialId",
                table: "Image",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PublisherId",
                table: "Image",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Image_AuthorId",
                table: "Image",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_LibraryId",
                table: "Image",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_MaterialId",
                table: "Image",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_PublisherId",
                table: "Image",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Authors_AuthorId",
                table: "Image",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Libraries_LibraryId",
                table: "Image",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Materials_MaterialId",
                table: "Image",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Publishers_PublisherId",
                table: "Image",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
