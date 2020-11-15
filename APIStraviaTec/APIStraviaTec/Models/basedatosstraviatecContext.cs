using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIStraviaTec.Models
{
    public partial class basedatosstraviatecContext : DbContext
    {
        public basedatosstraviatecContext()
        {
        }

        public basedatosstraviatecContext(DbContextOptions<basedatosstraviatecContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<Actividaddeportista> Actividaddeportista { get; set; }
        public virtual DbSet<Amigosusuario> Amigosusuario { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<Carrerasgrupo> Carrerasgrupo { get; set; }
        public virtual DbSet<Categoriacarrera> Categoriacarrera { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Patrocinador> Patrocinador { get; set; }
        public virtual DbSet<Patrocinadorescarrera> Patrocinadorescarrera { get; set; }
        public virtual DbSet<Patrocinadoresreto> Patrocinadoresreto { get; set; }
        public virtual DbSet<Reto> Reto { get; set; }
        public virtual DbSet<Retosgrupo> Retosgrupo { get; set; }
        public virtual DbSet<Solicitudescarrera> Solicitudescarrera { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Usuarioscarrera> Usuarioscarrera { get; set; }
        public virtual DbSet<Usuariosporgrupo> Usuariosporgrupo { get; set; }
        public virtual DbSet<Usuariosreto> Usuariosreto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=basedatosstraviatec;Username=postgres;Password=sarcu1209");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.Idactividad)
                    .HasName("actividad_pkey");

                entity.ToTable("actividad");

                entity.Property(e => e.Idactividad).HasColumnName("idactividad");

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.Nombreactividad)
                    .IsRequired()
                    .HasColumnName("nombreactividad");

                entity.Property(e => e.Tipoactividad)
                    .IsRequired()
                    .HasColumnName("tipoactividad");
            });

            modelBuilder.Entity<Actividaddeportista>(entity =>
            {
                entity.HasKey(e => new { e.Idactividad, e.Iddeportista })
                    .HasName("actividaddeportista_pkey");

                entity.ToTable("actividaddeportista");

                entity.Property(e => e.Idactividad).HasColumnName("idactividad");

                entity.Property(e => e.Iddeportista).HasColumnName("iddeportista");

                entity.Property(e => e.Altura).HasColumnName("altura");

                entity.Property(e => e.Duracion).HasColumnName("duracion");

                entity.Property(e => e.Kilometraje).HasColumnName("kilometraje");

                entity.Property(e => e.Recorrido)
                    .HasColumnName("recorrido")
                    .HasColumnType("xml");
            });

            modelBuilder.Entity<Amigosusuario>(entity =>
            {
                entity.HasKey(e => new { e.Iddeportista, e.Idamigo })
                    .HasName("amigosusuario_pkey");

                entity.ToTable("amigosusuario");

                entity.Property(e => e.Iddeportista).HasColumnName("iddeportista");

                entity.Property(e => e.Idamigo).HasColumnName("idamigo");
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => e.Idcarrera)
                    .HasName("carrera_pkey");

                entity.ToTable("carrera");

                entity.Property(e => e.Idcarrera).HasColumnName("idcarrera");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.Cuentabancaria)
                    .IsRequired()
                    .HasColumnName("cuentabancaria");

                entity.Property(e => e.Fechacarrera).HasColumnName("fechacarrera");

                entity.Property(e => e.Idorganizador).HasColumnName("idorganizador");

                entity.Property(e => e.Nombrecarrera)
                    .IsRequired()
                    .HasColumnName("nombrecarrera");

                entity.Property(e => e.Privada)
                    .HasColumnName("privada")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Recorrido)
                    .HasColumnName("recorrido")
                    .HasColumnType("xml");

                entity.Property(e => e.Tipoactividad)
                    .IsRequired()
                    .HasColumnName("tipoactividad");

                entity.HasOne(d => d.IdorganizadorNavigation)
                    .WithMany(p => p.Carrera)
                    .HasForeignKey(d => d.Idorganizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_organizadorcarrera");
            });

            modelBuilder.Entity<Carrerasgrupo>(entity =>
            {
                entity.HasKey(e => new { e.Nombregrupo, e.Idcarrera })
                    .HasName("carrerasgrupo_pkey");

                entity.ToTable("carrerasgrupo");

                entity.Property(e => e.Nombregrupo).HasColumnName("nombregrupo");

                entity.Property(e => e.Idcarrera).HasColumnName("idcarrera");
            });

            modelBuilder.Entity<Categoriacarrera>(entity =>
            {
                entity.HasKey(e => new { e.Idcarrera, e.Categoria })
                    .HasName("categoriacarrera_pkey");

                entity.ToTable("categoriacarrera");

                entity.Property(e => e.Idcarrera).HasColumnName("idcarrera");

                entity.Property(e => e.Categoria).HasColumnName("categoria");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("grupo_pkey");

                entity.ToTable("grupo");

                entity.Property(e => e.Nombre).HasColumnName("nombre");

                entity.Property(e => e.Idadministrador).HasColumnName("idadministrador");

                entity.HasOne(d => d.IdadministradorNavigation)
                    .WithMany(p => p.Grupo)
                    .HasForeignKey(d => d.Idadministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_administrador");
            });

            modelBuilder.Entity<Patrocinador>(entity =>
            {
                entity.HasKey(e => e.Nombrecomercial)
                    .HasName("patrocinador_pkey");

                entity.ToTable("patrocinador");

                entity.Property(e => e.Nombrecomercial).HasColumnName("nombrecomercial");

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.Numerotelefono)
                    .IsRequired()
                    .HasColumnName("numerotelefono");

                entity.Property(e => e.Representante)
                    .IsRequired()
                    .HasColumnName("representante");
            });

            modelBuilder.Entity<Patrocinadorescarrera>(entity =>
            {
                entity.HasKey(e => new { e.Idcarrera, e.Nombrecomercial })
                    .HasName("patrocinadorescarrera_pkey");

                entity.ToTable("patrocinadorescarrera");

                entity.Property(e => e.Idcarrera).HasColumnName("idcarrera");

                entity.Property(e => e.Nombrecomercial).HasColumnName("nombrecomercial");
            });

            modelBuilder.Entity<Patrocinadoresreto>(entity =>
            {
                entity.HasKey(e => new { e.Idreto, e.Nombrecomercial })
                    .HasName("patrocinadoresreto_pkey");

                entity.ToTable("patrocinadoresreto");

                entity.Property(e => e.Idreto).HasColumnName("idreto");

                entity.Property(e => e.Nombrecomercial).HasColumnName("nombrecomercial");
            });

            modelBuilder.Entity<Reto>(entity =>
            {
                entity.HasKey(e => e.Idreto)
                    .HasName("reto_pkey");

                entity.ToTable("reto");

                entity.Property(e => e.Idreto).HasColumnName("idreto");

                entity.Property(e => e.Fechafinaliza).HasColumnName("fechafinaliza");

                entity.Property(e => e.Fechainicio).HasColumnName("fechainicio");

                entity.Property(e => e.Idorganizador).HasColumnName("idorganizador");

                entity.Property(e => e.Nombrereto)
                    .IsRequired()
                    .HasColumnName("nombrereto");

                entity.Property(e => e.Objetivoreto)
                    .IsRequired()
                    .HasColumnName("objetivoreto");

                entity.Property(e => e.Privada)
                    .HasColumnName("privada")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Tipoactividad)
                    .IsRequired()
                    .HasColumnName("tipoactividad");

                entity.Property(e => e.Tiporeto)
                    .IsRequired()
                    .HasColumnName("tiporeto");

                entity.HasOne(d => d.IdorganizadorNavigation)
                    .WithMany(p => p.Reto)
                    .HasForeignKey(d => d.Idorganizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_organizadorreto");
            });

            modelBuilder.Entity<Retosgrupo>(entity =>
            {
                entity.HasKey(e => new { e.Nombregrupo, e.Idreto })
                    .HasName("retosgrupo_pkey");

                entity.ToTable("retosgrupo");

                entity.Property(e => e.Nombregrupo).HasColumnName("nombregrupo");

                entity.Property(e => e.Idreto).HasColumnName("idreto");
            });

            modelBuilder.Entity<Solicitudescarrera>(entity =>
            {
                entity.HasKey(e => new { e.Idcarrera, e.Idusuario })
                    .HasName("solicitudescarrera_pkey");

                entity.ToTable("solicitudescarrera");

                entity.Property(e => e.Idcarrera).HasColumnName("idcarrera");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.Recibo).HasColumnName("recibo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("usuario_pkey");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.Nombreusuario)
                    .HasName("uq_nombreusuario")
                    .IsUnique();

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnName("contrasena");

                entity.Property(e => e.Fechanacimiento)
                    .HasColumnName("fechanacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Foto).HasColumnName("foto");

                entity.Property(e => e.Nacionalidad)
                    .HasColumnName("nacionalidad")
                    .HasDefaultValueSql("'No indica'::text");

                entity.Property(e => e.Nombreusuario)
                    .IsRequired()
                    .HasColumnName("nombreusuario");

                entity.Property(e => e.Primernombre)
                    .IsRequired()
                    .HasColumnName("primernombre");
            });

            modelBuilder.Entity<Usuarioscarrera>(entity =>
            {
                entity.HasKey(e => new { e.Iddeportista, e.Idcarrera })
                    .HasName("usuarioscarrera_pkey");

                entity.ToTable("usuarioscarrera");

                entity.Property(e => e.Iddeportista).HasColumnName("iddeportista");

                entity.Property(e => e.Idcarrera).HasColumnName("idcarrera");

                entity.Property(e => e.Altura).HasColumnName("altura");

                entity.Property(e => e.Completitud)
                    .HasColumnName("completitud")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Kilometraje).HasColumnName("kilometraje");

                entity.Property(e => e.Recorrido)
                    .HasColumnName("recorrido")
                    .HasColumnType("xml");

                entity.Property(e => e.Tiemporegistrado).HasColumnName("tiemporegistrado");
            });

            modelBuilder.Entity<Usuariosporgrupo>(entity =>
            {
                entity.HasKey(e => new { e.Idusuario, e.Nombregrupo })
                    .HasName("usuariosporgrupo_pkey");

                entity.ToTable("usuariosporgrupo");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.Nombregrupo).HasColumnName("nombregrupo");
            });

            modelBuilder.Entity<Usuariosreto>(entity =>
            {
                entity.HasKey(e => new { e.Iddeportista, e.Idreto })
                    .HasName("usuariosreto_pkey");

                entity.ToTable("usuariosreto");

                entity.Property(e => e.Iddeportista).HasColumnName("iddeportista");

                entity.Property(e => e.Idreto).HasColumnName("idreto");

                entity.Property(e => e.Altura).HasColumnName("altura");

                entity.Property(e => e.Completitud)
                    .HasColumnName("completitud")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Duracion).HasColumnName("duracion");

                entity.Property(e => e.Kilometraje).HasColumnName("kilometraje");

                entity.Property(e => e.Recorrido)
                    .HasColumnName("recorrido")
                    .HasColumnType("xml");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
