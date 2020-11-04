﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServerStraviaTec.Clases;

namespace ServerStraviaTec.Migrations
{
    [DbContext(typeof(DatosUsuarios))]
    [Migration("20201104065819_actualizacionListas")]
    partial class actualizacionListas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ServerStraviaTec.Models.Actividad", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("completitud")
                        .HasColumnType("text");

                    b.Property<string>("duracion")
                        .HasColumnType("text");

                    b.Property<string>("fecha")
                        .HasColumnType("text");

                    b.Property<string>("hora")
                        .HasColumnType("text");

                    b.Property<string>("kilometraje")
                        .HasColumnType("text");

                    b.Property<string>("recorrido")
                        .HasColumnType("text");

                    b.Property<string>("tipo")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Actividad");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.Administrador", b =>
                {
                    b.Property<int>("Cedula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Usuario")
                        .HasColumnType("text");

                    b.HasKey("Cedula");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.Carrera", b =>
                {
                    b.Property<string>("idCarrera")
                        .HasColumnType("text");

                    b.Property<string>("categoria")
                        .HasColumnType("text");

                    b.Property<string>("costo")
                        .HasColumnType("text");

                    b.Property<string>("cuentaBancaria")
                        .HasColumnType("text");

                    b.Property<string>("fecha")
                        .HasColumnType("text");

                    b.Property<string>("listaParticipantes")
                        .HasColumnType("text");

                    b.Property<string>("listaPatrocinadores")
                        .HasColumnType("text");

                    b.Property<string>("nombre")
                        .HasColumnType("text");

                    b.Property<string>("privacidad")
                        .HasColumnType("text");

                    b.Property<string>("recorrido")
                        .HasColumnType("text");

                    b.Property<string>("reporteCarrera")
                        .HasColumnType("text");

                    b.Property<string>("solicitudAfiliaciones")
                        .HasColumnType("text");

                    b.Property<string>("tipoActividad")
                        .HasColumnType("text");

                    b.HasKey("idCarrera");

                    b.ToTable("Carrera");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.Deportista", b =>
                {
                    b.Property<string>("Usuario")
                        .HasColumnType("text");

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("Categorias")
                        .HasColumnType("text");

                    b.Property<string>("FechaNacimiento")
                        .HasColumnType("text");

                    b.Property<string>("Foto")
                        .HasColumnType("text");

                    b.Property<string>("ListaAmigos")
                        .HasColumnType("text");

                    b.Property<string>("Nacionalidad")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Usuario");

                    b.ToTable("Deportista");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.Grupo", b =>
                {
                    b.Property<string>("nombreGrupo")
                        .HasColumnType("text");

                    b.Property<string>("administrador")
                        .HasColumnType("text");

                    b.Property<string>("listaDeportistas")
                        .HasColumnType("text");

                    b.HasKey("nombreGrupo");

                    b.ToTable("Grupo");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.Patrocinador", b =>
                {
                    b.Property<string>("nombreComercio")
                        .HasColumnType("text");

                    b.Property<string>("logo")
                        .HasColumnType("text");

                    b.Property<string>("numeroTelefono")
                        .HasColumnType("text");

                    b.Property<string>("representanteLegal")
                        .HasColumnType("text");

                    b.HasKey("nombreComercio");

                    b.ToTable("Patrocinador");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.Reto", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("Privacidad")
                        .HasColumnType("text");

                    b.Property<string>("listaPatrocinadores")
                        .HasColumnType("text");

                    b.Property<string>("periodoDisponible")
                        .HasColumnType("text");

                    b.Property<string>("tipoActividad")
                        .HasColumnType("text");

                    b.Property<string>("tipoReto")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Reto");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.listaActividad", b =>
                {
                    b.Property<string>("idDeportista")
                        .HasColumnType("text");

                    b.Property<string>("ascensoActividad")
                        .HasColumnType("text");

                    b.Property<string>("completitudActividad")
                        .HasColumnType("text");

                    b.Property<string>("gpxActividad")
                        .HasColumnType("text");

                    b.Property<string>("idActividad")
                        .HasColumnType("text");

                    b.Property<string>("kilometrosActividad")
                        .HasColumnType("text");

                    b.Property<string>("tiempoActividad")
                        .HasColumnType("text");

                    b.HasKey("idDeportista");

                    b.ToTable("listaActividad");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.listaCarreras", b =>
                {
                    b.Property<string>("idDeportista")
                        .HasColumnType("text");

                    b.Property<string>("ascensoCarrera")
                        .HasColumnType("text");

                    b.Property<string>("completitudCarrera")
                        .HasColumnType("text");

                    b.Property<string>("gpxCarrera")
                        .HasColumnType("text");

                    b.Property<string>("idCarreras")
                        .HasColumnType("text");

                    b.Property<string>("kilometrosCarrera")
                        .HasColumnType("text");

                    b.Property<string>("tiempoCarrera")
                        .HasColumnType("text");

                    b.HasKey("idDeportista");

                    b.ToTable("listaCarreras");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.listaRetos", b =>
                {
                    b.Property<string>("idDeportista")
                        .HasColumnType("text");

                    b.Property<string>("ascensoReto")
                        .HasColumnType("text");

                    b.Property<string>("completitudReto")
                        .HasColumnType("text");

                    b.Property<string>("gpxReto")
                        .HasColumnType("text");

                    b.Property<string>("idReto")
                        .HasColumnType("text");

                    b.Property<string>("kilometrosReto")
                        .HasColumnType("text");

                    b.Property<string>("tiempoReto")
                        .HasColumnType("text");

                    b.Property<string>("tipoReto")
                        .HasColumnType("text");

                    b.HasKey("idDeportista");

                    b.ToTable("listaRetos");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.solicitudAfiliacion", b =>
                {
                    b.Property<string>("idAfiliacion")
                        .HasColumnType("text");

                    b.Property<string>("comprobante")
                        .HasColumnType("text");

                    b.Property<string>("deportista")
                        .HasColumnType("text");

                    b.Property<string>("idCarrera")
                        .HasColumnType("text");

                    b.HasKey("idAfiliacion");

                    b.ToTable("solicitudAfiliacion");
                });
#pragma warning restore 612, 618
        }
    }
}
