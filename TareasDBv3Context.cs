using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tareasv2
{
    public partial class TareasDBv3Context : DbContext
    {
        public TareasDBv3Context()
        {
        }

        public TareasDBv3Context(DbContextOptions<TareasDBv3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<EstadoTarea> EstadoTareas { get; set; } = null!;
        public virtual DbSet<Grupo> Grupos { get; set; } = null!;
        public virtual DbSet<Notificacion> Notificacions { get; set; } = null!;
        public virtual DbSet<Prioridad> Prioridads { get; set; } = null!;
        public virtual DbSet<Proyecto> Proyectos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Tarea> Tareas { get; set; } = null!;
        public virtual DbSet<TareaGrupo> TareaGrupos { get; set; } = null!;
        public virtual DbSet<TareaUsuario> TareaUsuarios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuarioGrupo> UsuarioGrupos { get; set; } = null!;
        public virtual DbSet<UsuarioRol> UsuarioRols { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=TUFPC;Initial Catalog=TareasDBv3;User ID=sa;Password=admin123;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstadoTarea>(entity =>
            {
                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.EstadoTareas)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("estado_tarea_idestado_foreign");

                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.EstadoTareas)
                    .HasForeignKey(d => d.IdTarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("estado_tarea_idtarea_foreign");
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.HasOne(d => d.Proyecto)
                    .WithMany(p => p.Notificacions)
                    .HasForeignKey(d => d.ProyectoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notificacion_proyectoid_foreign");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Notificacions)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notificacion_usuarioid_foreign");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proyecto_idusuario_foreign");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tarea_idproyecto_foreign");

                entity.HasOne(d => d.PrioridadNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.Prioridad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tarea_prioridad_foreign");
            });

            modelBuilder.Entity<TareaGrupo>(entity =>
            {
                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.TareaGrupos)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tarea_grupo_idgrupo_foreign");

                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.TareaGrupos)
                    .HasForeignKey(d => d.IdTarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tarea_grupo_idtarea_foreign");
            });

            modelBuilder.Entity<TareaUsuario>(entity =>
            {
                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.TareaUsuarios)
                    .HasForeignKey(d => d.IdTarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tarea_usuario_idtarea_foreign");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TareaUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tarea_usuario_idusuario_foreign");
            });

            modelBuilder.Entity<UsuarioGrupo>(entity =>
            {
                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.UsuarioGrupos)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuario_grupo_idgrupo_foreign");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioGrupos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuario_grupo_idusuario_foreign");
            });

            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.UsuarioRols)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuario_rol_idrol_foreign");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioRols)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuario_rol_idusuario_foreign");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
