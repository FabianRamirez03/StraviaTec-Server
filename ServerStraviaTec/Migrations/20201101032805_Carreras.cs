using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ServerStraviaTec.Migrations
{
    public partial class Carreras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(nullable: true),
                    privacidad = table.Column<string>(nullable: true),
                    fecha = table.Column<string>(nullable: true),
                    categoria = table.Column<string>(nullable: true),
                    cuentaBancarias = table.Column<string>(nullable: true),
                    listaPatrocinadores = table.Column<string>(nullable: true),
                    costo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carrera");
        }
    }
}
