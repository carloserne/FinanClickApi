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

    public virtual DbSet<CatalogoDocumento> CatalogoDocumentos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DatosClienteFisica> DatosClienteFisicas { get; set; }

    public virtual DbSet<DatosClienteMoral> DatosClienteMorals { get; set; }

    public virtual DbSet<DocumentosCliente> DocumentosClientes { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<PersonaMoral> PersonaMorals { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdCatalogoDocumento).HasName("PK__Catalogo__661C085CC0A69FBB");

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D594664253D4D0E5");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.RegimenFiscal)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK__Cliente__idEmpre__44FF419A");
        });

        modelBuilder.Entity<DatosClienteFisica>(entity =>
        {
            entity.HasKey(e => e.IdClienteFisica).HasName("PK__DatosCli__0F2855A92E6905D0");

            entity.ToTable("DatosClienteFisica");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DatosClienteFisicas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__DatosClie__IdCli__52593CB8");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.DatosClienteFisicas)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK__DatosClie__IdPer__5165187F");
        });

        modelBuilder.Entity<DatosClienteMoral>(entity =>
        {
            entity.HasKey(e => e.IdClienteMoral).HasName("PK__DatosCli__5A0ACC3D6EF100FC");

            entity.ToTable("DatosClienteMoral");

            entity.Property(e => e.NombreRepLegal)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RfcrepLegal)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RFCRepLegal");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DatosClienteMorals)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__DatosClie__IdCli__5629CD9C");

            entity.HasOne(d => d.IdPersonaMoralNavigation).WithMany(p => p.DatosClienteMorals)
                .HasForeignKey(d => d.IdPersonaMoral)
                .HasConstraintName("FK__DatosClie__IdPer__5535A963");
        });

        modelBuilder.Entity<DocumentosCliente>(entity =>
        {
            entity.HasKey(e => e.IdDocumentoCliente).HasName("PK__Document__232F08451D75BCC3");

            entity.ToTable("DocumentosCliente");

            entity.Property(e => e.DocumentoBase64).HasColumnType("text");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DocumentosClientes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Documento__IdCli__4AB81AF0");

            entity.HasOne(d => d.IdDocumentoNavigation).WithMany(p => p.DocumentosClientes)
                .HasForeignKey(d => d.IdDocumento)
                .HasConstraintName("FK__Documento__IdDoc__49C3F6B7");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033EED026D14");

            entity.ToTable("Empresa");

            entity.Property(e => e.Calle).HasMaxLength(255);
            entity.Property(e => e.Colonia).HasMaxLength(255);
            entity.Property(e => e.Cp).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FolioMercantil).HasMaxLength(50);
            entity.Property(e => e.Localidad).HasMaxLength(255);
            entity.Property(e => e.NombreEmpresa).HasMaxLength(255);
            entity.Property(e => e.NombreNotario).HasMaxLength(255);
            entity.Property(e => e.NombreRepresentanteLegal).HasMaxLength(255);
            entity.Property(e => e.NumExterior).HasMaxLength(10);
            entity.Property(e => e.NumInterior).HasMaxLength(10);
            entity.Property(e => e.NumeroEscritura).HasMaxLength(50);
            entity.Property(e => e.NumeroEscrituraRepLeg).HasMaxLength(50);
            entity.Property(e => e.NumeroNotario).HasMaxLength(50);
            entity.Property(e => e.RazonSocial).HasMaxLength(255);
            entity.Property(e => e.Rfc).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo).HasName("PK__Modulo__D9F15315EB19A4AB");

            entity.ToTable("Modulo");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreModulo).HasMaxLength(255);

            entity.HasMany(d => d.IdUsuarios).WithMany(p => p.IdModulos)
                .UsingEntity<Dictionary<string, object>>(
                    "DetalleModuloUsuario",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DetalleMo__IdUsu__4222D4EF"),
                    l => l.HasOne<Modulo>().WithMany()
                        .HasForeignKey("IdModulo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DetalleMo__IdMod__412EB0B6"),
                    j =>
                    {
                        j.HasKey("IdModulo", "IdUsuario").HasName("PK__DetalleM__BC4708EC21F1C8DD");
                        j.ToTable("DetalleModuloUsuario");
                    });
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__2EC8D2AC779D8E92");

            entity.ToTable("Persona");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Calle)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CiudadResidencia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ClaveElector)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Colonia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoNacimiento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EstadoResidencia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombreConyuge)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NumExterior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumInterior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaisNacimiento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaisResidencia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RegimenMatrimonial)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PersonaMoral>(entity =>
        {
            entity.HasKey(e => e.IdPersonaMoral).HasName("PK__PersonaM__D333C18CD1FEDAC6");

            entity.ToTable("PersonaMoral");

            entity.Property(e => e.Calle)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CiudadRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CiudadResidencia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Colonia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EstadoResidencia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaRppc).HasColumnName("FechaRPPC");
            entity.Property(e => e.FolioMercantil)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombreNotario)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NumEscritura)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NumExterior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumInterior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumNotario)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaisRegistro)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaisResidencia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RazonComercial)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RFC");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C5977DB14");

            entity.ToTable("Rol");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreRol).HasMaxLength(255);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF975403CE1B");

            entity.ToTable("Usuario");

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(255);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(255);
            entity.Property(e => e.Contrasenia).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(35);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(255)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK__Usuario__IdEmpre__3E52440B");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
