using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerStraviaTec.Migrations
{
    public partial class rutaCarrera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ruta",
                table: "Carrera",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ruta",
                table: "Carrera");
        }
    }
}
