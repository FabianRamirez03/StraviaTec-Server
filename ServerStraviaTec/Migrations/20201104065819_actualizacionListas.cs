using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ServerStraviaTec.Migrations
{
    public partial class actualizacionListas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrera",
                table: "Carrera");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Carrera");

            migrationBuilder.DropColumn(
                name: "ruta",
                table: "Carrera");

            migrationBuilder.AlterColumn<string>(
                name: "costo",
                table: "Carrera",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "idCarrera",
                table: "Carrera",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "listaParticipantes",
                table: "Carrera",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "recorrido",
                table: "Carrera",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reporteCarrera",
                table: "Carrera",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "solicitudAfiliaciones",
                table: "Carrera",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tipoActividad",
                table: "Carrera",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrera",
                table: "Carrera",
                column: "idCarrera");

            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    fecha = table.Column<string>(nullable: true),
                    hora = table.Column<string>(nullable: true),
                    duracion = table.Column<string>(nullable: true),
                    tipo = table.Column<string>(nullable: true),
                    kilometraje = table.Column<string>(nullable: true),
                    recorrido = table.Column<string>(nullable: true),
                    completitud = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "listaActividad",
                columns: table => new
                {
                    idDeportista = table.Column<string>(nullable: false),
                    idActividad = table.Column<string>(nullable: true),
                    kilometrosActividad = table.Column<string>(nullable: true),
                    ascensoActividad = table.Column<string>(nullable: true),
                    completitudActividad = table.Column<string>(nullable: true),
                    gpxActividad = table.Column<string>(nullable: true),
                    tiempoActividad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listaActividad", x => x.idDeportista);
                });

            migrationBuilder.CreateTable(
                name: "listaCarreras",
                columns: table => new
                {
                    idDeportista = table.Column<string>(nullable: false),
                    idCarreras = table.Column<string>(nullable: true),
                    gpxCarrera = table.Column<string>(nullable: true),
                    tiempoCarrera = table.Column<string>(nullable: true),
                    kilometrosCarrera = table.Column<string>(nullable: true),
                    ascensoCarrera = table.Column<string>(nullable: true),
                    completitudCarrera = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listaCarreras", x => x.idDeportista);
                });

            migrationBuilder.CreateTable(
                name: "listaRetos",
                columns: table => new
                {
                    idDeportista = table.Column<string>(nullable: false),
                    idReto = table.Column<string>(nullable: true),
                    tipoReto = table.Column<string>(nullable: true),
                    gpxReto = table.Column<string>(nullable: true),
                    tiempoReto = table.Column<string>(nullable: true),
                    kilometrosReto = table.Column<string>(nullable: true),
                    ascensoReto = table.Column<string>(nullable: true),
                    completitudReto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listaRetos", x => x.idDeportista);
                });

            migrationBuilder.CreateTable(
                name: "Patrocinador",
                columns: table => new
                {
                    nombreComercio = table.Column<string>(nullable: false),
                    representanteLegal = table.Column<string>(nullable: true),
                    numeroTelefono = table.Column<string>(nullable: true),
                    logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrocinador", x => x.nombreComercio);
                });

            migrationBuilder.CreateTable(
                name: "Reto",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    periodoDisponible = table.Column<string>(nullable: true),
                    listaPatrocinadores = table.Column<string>(nullable: true),
                    tipoReto = table.Column<string>(nullable: true),
                    tipoActividad = table.Column<string>(nullable: true),
                    Privacidad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reto", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "solicitudAfiliacion",
                columns: table => new
                {
                    idAfiliacion = table.Column<string>(nullable: false),
                    deportista = table.Column<string>(nullable: true),
                    comprobante = table.Column<string>(nullable: true),
                    idCarrera = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitudAfiliacion", x => x.idAfiliacion);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "listaActividad");

            migrationBuilder.DropTable(
                name: "listaCarreras");

            migrationBuilder.DropTable(
                name: "listaRetos");

            migrationBuilder.DropTable(
                name: "Patrocinador");

            migrationBuilder.DropTable(
                name: "Reto");

            migrationBuilder.DropTable(
                name: "solicitudAfiliacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrera",
                table: "Carrera");

            migrationBuilder.DropColumn(
                name: "idCarrera",
                table: "Carrera");

            migrationBuilder.DropColumn(
                name: "listaParticipantes",
                table: "Carrera");

            migrationBuilder.DropColumn(
                name: "recorrido",
                table: "Carrera");

            migrationBuilder.DropColumn(
                name: "reporteCarrera",
                table: "Carrera");

            migrationBuilder.DropColumn(
                name: "solicitudAfiliaciones",
                table: "Carrera");

            migrationBuilder.DropColumn(
                name: "tipoActividad",
                table: "Carrera");

            migrationBuilder.AlterColumn<int>(
                name: "costo",
                table: "Carrera",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Carrera",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "ruta",
                table: "Carrera",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrera",
                table: "Carrera",
                column: "id");
        }
    }
}
