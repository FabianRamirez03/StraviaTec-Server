using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerStraviaTec.Migrations
{
    public partial class cuentaBancaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cuentaBancarias",
                table: "Carrera");

            migrationBuilder.AddColumn<string>(
                name: "cuentaBancaria",
                table: "Carrera",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cuentaBancaria",
                table: "Carrera");

            migrationBuilder.AddColumn<string>(
                name: "cuentaBancarias",
                table: "Carrera",
                type: "text",
                nullable: true);
        }
    }
}
