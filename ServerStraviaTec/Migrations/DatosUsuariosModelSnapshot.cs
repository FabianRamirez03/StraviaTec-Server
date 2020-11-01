﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServerStraviaTec.Clases;

namespace ServerStraviaTec.Migrations
{
    [DbContext(typeof(DatosUsuarios))]
    partial class DatosUsuariosModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

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
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("categoria")
                        .HasColumnType("text");

                    b.Property<int>("costo")
                        .HasColumnType("integer");

                    b.Property<string>("cuentaBancarias")
                        .HasColumnType("text");

                    b.Property<string>("fecha")
                        .HasColumnType("text");

                    b.Property<string>("listaPatrocinadores")
                        .HasColumnType("text");

                    b.Property<string>("nombre")
                        .HasColumnType("text");

                    b.Property<string>("privacidad")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Carrera");
                });

            modelBuilder.Entity("ServerStraviaTec.Models.Deportista", b =>
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

                    b.ToTable("Deportista");
                });
#pragma warning restore 612, 618
        }
    }
}
