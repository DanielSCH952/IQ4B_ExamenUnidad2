﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExU2.Models;

public partial class SanjuanProjectDbContext : DbContext
{
    public SanjuanProjectDbContext()
    {
    }

    public SanjuanProjectDbContext(DbContextOptions<SanjuanProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coordenadum> Coordenada { get; set; }

    public virtual DbSet<Dispositivo> Dispositivos { get; set; }

    public virtual DbSet<HistorialDispositivo> HistorialDispositivos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-9J15T2T; database=sanjuanProjectDB; Integrated security= true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coordenadum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coordena__3213E83FC7AE9335");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdZona).HasColumnName("idZona");
            entity.Property(e => e.Latitud)
                .HasColumnType("decimal(8, 6)")
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasColumnType("decimal(8, 6)")
                .HasColumnName("longitud");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Coordenada)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("fk_Coordenada_IdZona");
        });

        modelBuilder.Entity<Dispositivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Disposit__3213E83F45C31185");

            entity.ToTable("Dispositivo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(264)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.DireccionMac)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("direccionMac");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdZona).HasColumnName("idZona");
            entity.Property(e => e.Latitud)
                .HasColumnType("decimal(8, 6)")
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasColumnType("decimal(8, 6)")
                .HasColumnName("longitud");
            entity.Property(e => e.Prioridad).HasColumnName("prioridad");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Dispositivos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_Dispositivo_IdUsuario");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Dispositivos)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("fk_Dispositivo_IdZona");
        });

        modelBuilder.Entity<HistorialDispositivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3213E83F0600F245");

            entity.ToTable("HistorialDispositivo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadFinal)
                .HasColumnType("decimal(8, 6)")
                .HasColumnName("cantidadFinal");
            entity.Property(e => e.CantidadInicial)
                .HasColumnType("decimal(8, 6)")
                .HasColumnName("cantidadInicial");
            entity.Property(e => e.Fin)
                .HasColumnType("datetime")
                .HasColumnName("fin");
            entity.Property(e => e.IdDispositivo).HasColumnName("idDispositivo");
            entity.Property(e => e.Inicio)
                .HasColumnType("datetime")
                .HasColumnName("inicio");

            entity.HasOne(d => d.IdDispositivoNavigation).WithMany(p => p.HistorialDispositivos)
                .HasForeignKey(d => d.IdDispositivo)
                .HasConstraintName("fk_HistorialDisp_Dispositivo");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3213E83F7DF770B0");

            entity.ToTable("Rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombreRol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83FBEC1DAEE");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellidoMaterno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellidoPaterno");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sexo");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_Usuario_IdRol");
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Zona__3213E83FDD4BCCCD");

            entity.ToTable("Zona");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
