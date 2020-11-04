using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ServerStraviaTec.Migrations
{
    public partial class Actualizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Deportista",
                table: "Deportista");

            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "Deportista");

            migrationBuilder.RenameColumn(
                name: "nacionalidad",
                table: "Deportista",
                newName: "Nacionalidad");

            migrationBuilder.RenameColumn(
                name: "foto",
                table: "Deportista",
                newName: "Foto");

            migrationBuilder.RenameColumn(
                name: "fechaNacimiento",
                table: "Deportista",
                newName: "FechaNacimiento");

            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Deportista",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Categorias",
                table: "Deportista",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListaAmigos",
                table: "Deportista",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deportista",
                table: "Deportista",
                column: "Usuario");

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    nombreGrupo = table.Column<string>(nullable: false),
                    listaDeportistas = table.Column<string>(nullable: true),
                    administrador = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.nombreGrupo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deportista",
                table: "Deportista");

            migrationBuilder.DropColumn(
                name: "Categorias",
                table: "Deportista");

            migrationBuilder.DropColumn(
                name: "ListaAmigos",
                table: "Deportista");

            migrationBuilder.RenameColumn(
                name: "Nacionalidad",
                table: "Deportista",
                newName: "nacionalidad");

            migrationBuilder.RenameColumn(
                name: "Foto",
                table: "Deportista",
                newName: "foto");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "Deportista",
                newName: "fechaNacimiento");

            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Deportista",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Cedula",
                table: "Deportista",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deportista",
                table: "Deportista",
                column: "Cedula");
        }
    }
}
