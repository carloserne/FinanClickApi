using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinanClickApi.Models;

public partial class FinanclickDbContext : DbContext
{
    public FinanclickDbContext()
    {
    }

    public FinanclickDbContext(DbContextOptions<FinanclickDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VQKOVT2\\SQLEXPRESS; Initial Catalog=FinanclickDB; user id=sa; password=root;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D594664202C84A07");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).ValueGeneratedNever();
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK__Cliente__idEmpre__571DF1D5");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033EC6B28C24");

            entity.ToTable("Empresa");

            entity.Property(e => e.IdEmpresa).ValueGeneratedNever();
            entity.Property(e => e.Calle).HasMaxLength(255);
            entity.Property(e => e.Colonia).HasMaxLength(255);
            entity.Property(e => e.Cp).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FolioMercantil).HasMaxLength(50);
            entity.Property(e => e.Localidad).HasMaxLength(255);
            entity.Property(e => e.NombreNotario).HasMaxLength(255);
            entity.Property(e => e.NombreRepresentanteLegal).HasMaxLength(255);
            entity.Property(e => e.NumExterior).HasMaxLength(10);
            entity.Property(e => e.NumInterior).HasMaxLength(10);
            entity.Property(e => e.NumeroEscritura).HasMaxLength(50);
            entity.Property(e => e.NumeroEscrituraRepLeg).HasMaxLength(50);
            entity.Property(e => e.NumeroNotario).HasMaxLength(50);
            entity.Property(e => e.RazonSocial).HasMaxLength(255);
            entity.Property(e => e.Rfc).HasMaxLength(50);
            entity.Property(e => e.Teléfono).HasMaxLength(20);
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo).HasName("PK__Modulo__D9F1531565122577");

            entity.ToTable("Modulo");

            entity.Property(e => e.IdModulo).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreModulo).HasMaxLength(255);

            entity.HasMany(d => d.IdUsuarios).WithMany(p => p.IdModulos)
                .UsingEntity<Dictionary<string, object>>(
                    "DetalleModuloUsuario",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DetalleMo__IdUsu__5441852A"),
                    l => l.HasOne<Modulo>().WithMany()
                        .HasForeignKey("IdModulo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DetalleMo__IdMod__534D60F1"),
                    j =>
                    {
                        j.HasKey("IdModulo", "IdUsuario").HasName("PK__DetalleM__BC4708EC053005F0");
                        j.ToTable("DetalleModuloUsuario");
                    });
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C0157A088");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreRol).HasMaxLength(255);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF977E2FB890");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(255);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(255);
            entity.Property(e => e.Contrasenia).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(35);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(255)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK__Usuario__IdEmpre__5070F446");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
