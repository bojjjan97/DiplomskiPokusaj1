using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiPokusaj1.Migrations
{
    public partial class migration23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rents_RentId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RentId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RentId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "ReservationId",
                table: "Rents",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_ReservationId",
                table: "Rents",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Reservations_ReservationId",
                table: "Rents",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Reservations_ReservationId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_ReservationId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Rents");

            migrationBuilder.AddColumn<string>(
                name: "RentId",
                table: "Reservations",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RentId",
                table: "Reservations",
                column: "RentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rents_RentId",
                table: "Reservations",
                column: "RentId",
                principalTable: "Rents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
