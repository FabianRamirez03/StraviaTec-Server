using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerStraviaTec.Migrations
{
    public partial class Prueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fechaNacimiento",
                table: "Deportista",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "foto",
                table: "Deportista",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nacionalidad",
                table: "Deportista",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fechaNacimiento",
                table: "Deportista");

            migrationBuilder.DropColumn(
                name: "foto",
                table: "Deportista");

            migrationBuilder.DropColumn(
                name: "nacionalidad",
                table: "Deportista");
        }
    }
}
