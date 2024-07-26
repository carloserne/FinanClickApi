﻿// <auto-generated />
using System;
using FinanClickApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinanClickApi.Migrations
{
    [DbContext(typeof(FinanclickDbContext))]
    partial class FinanclickDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DetalleModuloUsuario", b =>
                {
                    b.Property<int>("IdModulo")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("IdModulo", "IdUsuario")
                        .HasName("PK__DetalleM__BC4708EC21F1C8DD");

                    b.HasIndex("IdUsuario");

                    b.ToTable("DetalleModuloUsuario", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.CatalogoDocumento", b =>
                {
                    b.Property<int>("IdCatalogoDocumento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCatalogoDocumento"));

                    b.Property<int>("Estatus")
                        .HasColumnType("int");

                    b.Property<int?>("IdEmpresa")
                        .HasColumnType("int")
                        .HasColumnName("idEmpresa");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdCatalogoDocumento")
                        .HasName("PK__Catalogo__661C085CC0A69FBB");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("CatalogoDocumentos");
                });

            modelBuilder.Entity("FinanClickApi.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<int>("Estatus")
                        .HasColumnType("int");

                    b.Property<int?>("IdEmpresa")
                        .HasColumnType("int")
                        .HasColumnName("idEmpresa");

                    b.Property<string>("RegimenFiscal")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("IdCliente")
                        .HasName("PK__Cliente__D594664253D4D0E5");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.DatosClienteFisica", b =>
                {
                    b.Property<int>("IdClienteFisica")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClienteFisica"));

                    b.Property<int?>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int?>("IdPersona")
                        .HasColumnType("int");

                    b.HasKey("IdClienteFisica")
                        .HasName("PK__DatosCli__0F2855A92E6905D0");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdPersona");

                    b.ToTable("DatosClienteFisica", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.DatosClienteMoral", b =>
                {
                    b.Property<int>("IdClienteMoral")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClienteMoral"));

                    b.Property<int?>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int?>("IdPersonaMoral")
                        .HasColumnType("int");

                    b.Property<string>("NombreRepLegal")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RfcrepLegal")
                        .IsRequired()
                        .HasMaxLength(13)
                        .IsUnicode(false)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("RFCRepLegal");

                    b.HasKey("IdClienteMoral")
                        .HasName("PK__DatosCli__5A0ACC3D6EF100FC");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdPersonaMoral");

                    b.ToTable("DatosClienteMoral", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.DocumentosCliente", b =>
                {
                    b.Property<int>("IdDocumentoCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDocumentoCliente"));

                    b.Property<string>("DocumentoBase64")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Estatus")
                        .HasColumnType("int");

                    b.Property<int?>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int?>("IdDocumento")
                        .HasColumnType("int");

                    b.HasKey("IdDocumentoCliente")
                        .HasName("PK__Document__232F08451D75BCC3");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdDocumento");

                    b.ToTable("DocumentosCliente", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.Empresa", b =>
                {
                    b.Property<int>("IdEmpresa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmpresa"));

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Colonia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Cp")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Estatus")
                        .HasColumnType("int");

                    b.Property<DateOnly>("FechaConstitucion")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FechaInscripcion")
                        .HasColumnType("date");

                    b.Property<string>("FolioMercantil")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Localidad")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreEmpresa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NombreNotario")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NombreRepresentanteLegal")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NumExterior")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("NumInterior")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("NumeroEscritura")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NumeroEscrituraRepLeg")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NumeroNotario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Rfc")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("IdEmpresa")
                        .HasName("PK__Empresa__5EF4033EED026D14");

                    b.ToTable("Empresa", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.Modulo", b =>
                {
                    b.Property<int>("IdModulo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdModulo"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Estatus")
                        .HasColumnType("bit");

                    b.Property<string>("NombreModulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdModulo")
                        .HasName("PK__Modulo__D9F15315EB19A4AB");

                    b.ToTable("Modulo", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.Persona", b =>
                {
                    b.Property<int>("IdPersona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPersona"));

                    b.Property<string>("ApellidoMaterno")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CiudadResidencia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ClaveElector")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CodigoPostal")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Colonia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Curp")
                        .IsRequired()
                        .HasMaxLength(18)
                        .IsUnicode(false)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("CURP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EstadoCivil")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("EstadoNacimiento")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EstadoResidencia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateOnly>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nacionalidad")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NombreConyuge")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NumExterior")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NumInterior")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PaisNacimiento")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PaisResidencia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RegimenMatrimonial")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Rfc")
                        .IsRequired()
                        .HasMaxLength(13)
                        .IsUnicode(false)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("RFC");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdPersona")
                        .HasName("PK__Persona__2EC8D2AC779D8E92");

                    b.ToTable("Persona", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.PersonaMoral", b =>
                {
                    b.Property<int>("IdPersonaMoral")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPersonaMoral"));

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CiudadRegistro")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CiudadResidencia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CodigoPostal")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Colonia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EstadoRegistro")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EstadoResidencia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateOnly>("FechaConstitucion")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("FechaRppc")
                        .HasColumnType("date")
                        .HasColumnName("FechaRPPC");

                    b.Property<string>("FolioMercantil")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nacionalidad")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NombreNotario")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NumEscritura")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NumExterior")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NumInterior")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NumNotario")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PaisRegistro")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PaisResidencia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RazonComercial")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Rfc")
                        .IsRequired()
                        .HasMaxLength(13)
                        .IsUnicode(false)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("RFC");

                    b.HasKey("IdPersonaMoral")
                        .HasName("PK__PersonaM__D333C18CD1FEDAC6");

                    b.ToTable("PersonaMoral", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.Rol", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRol"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Estatus")
                        .HasColumnType("bit");

                    b.Property<string>("NombreRol")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdRol")
                        .HasName("PK__Rol__2A49584C5977DB14");

                    b.ToTable("Rol", (string)null);
                });

            modelBuilder.Entity("FinanClickApi.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("ApellidoMaterno")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("IdEmpresa")
                        .HasColumnType("int");

                    b.Property<int?>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("Imagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("Usuario1")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Usuario");

                    b.HasKey("IdUsuario")
                        .HasName("PK__Usuario__5B65BF975403CE1B");

                    b.HasIndex("IdEmpresa");

                    b.HasIndex("IdRol");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("DetalleModuloUsuario", b =>
                {
                    b.HasOne("FinanClickApi.Models.Modulo", null)
                        .WithMany()
                        .HasForeignKey("IdModulo")
                        .IsRequired()
                        .HasConstraintName("FK__DetalleMo__IdMod__412EB0B6");

                    b.HasOne("FinanClickApi.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK__DetalleMo__IdUsu__4222D4EF");
                });

            modelBuilder.Entity("FinanClickApi.Models.CatalogoDocumento", b =>
                {
                    b.HasOne("FinanClickApi.Models.Empresa", "IdEmpresaNavigation")
                        .WithMany("CatalogoDocumentos")
                        .HasForeignKey("IdEmpresa")
                        .HasConstraintName("FK__Cliente__idEmpresa_C4TEMPR354");

                    b.Navigation("IdEmpresaNavigation");
                });

            modelBuilder.Entity("FinanClickApi.Models.Cliente", b =>
                {
                    b.HasOne("FinanClickApi.Models.Empresa", "IdEmpresaNavigation")
                        .WithMany("Clientes")
                        .HasForeignKey("IdEmpresa")
                        .HasConstraintName("FK__Cliente__idEmpre__44FF419A");

                    b.Navigation("IdEmpresaNavigation");
                });

            modelBuilder.Entity("FinanClickApi.Models.DatosClienteFisica", b =>
                {
                    b.HasOne("FinanClickApi.Models.Cliente", "IdClienteNavigation")
                        .WithMany("DatosClienteFisicas")
                        .HasForeignKey("IdCliente")
                        .HasConstraintName("FK__DatosClie__IdCli__52593CB8");

                    b.HasOne("FinanClickApi.Models.Persona", "IdPersonaNavigation")
                        .WithMany("DatosClienteFisicas")
                        .HasForeignKey("IdPersona")
                        .HasConstraintName("FK__DatosClie__IdPer__5165187F");

                    b.Navigation("IdClienteNavigation");

                    b.Navigation("IdPersonaNavigation");
                });

            modelBuilder.Entity("FinanClickApi.Models.DatosClienteMoral", b =>
                {
                    b.HasOne("FinanClickApi.Models.Cliente", "IdClienteNavigation")
                        .WithMany("DatosClienteMorals")
                        .HasForeignKey("IdCliente")
                        .HasConstraintName("FK__DatosClie__IdCli__5629CD9C");

                    b.HasOne("FinanClickApi.Models.PersonaMoral", "IdPersonaMoralNavigation")
                        .WithMany("DatosClienteMorals")
                        .HasForeignKey("IdPersonaMoral")
                        .HasConstraintName("FK__DatosClie__IdPer__5535A963");

                    b.Navigation("IdClienteNavigation");

                    b.Navigation("IdPersonaMoralNavigation");
                });

            modelBuilder.Entity("FinanClickApi.Models.DocumentosCliente", b =>
                {
                    b.HasOne("FinanClickApi.Models.Cliente", "IdClienteNavigation")
                        .WithMany("DocumentosClientes")
                        .HasForeignKey("IdCliente")
                        .HasConstraintName("FK__Documento__IdCli__4AB81AF0");

                    b.HasOne("FinanClickApi.Models.CatalogoDocumento", "IdDocumentoNavigation")
                        .WithMany("DocumentosClientes")
                        .HasForeignKey("IdDocumento")
                        .HasConstraintName("FK__Documento__IdDoc__49C3F6B7");

                    b.Navigation("IdClienteNavigation");

                    b.Navigation("IdDocumentoNavigation");
                });

            modelBuilder.Entity("FinanClickApi.Models.Usuario", b =>
                {
                    b.HasOne("FinanClickApi.Models.Empresa", "IdEmpresaNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdEmpresa")
                        .HasConstraintName("FK__Usuario__IdEmpre__3E52440B");

                    b.HasOne("FinanClickApi.Models.Rol", "IdRolNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__Usuario__IdRol__3D5E1FD2");

                    b.Navigation("IdEmpresaNavigation");

                    b.Navigation("IdRolNavigation");
                });

            modelBuilder.Entity("FinanClickApi.Models.CatalogoDocumento", b =>
                {
                    b.Navigation("DocumentosClientes");
                });

            modelBuilder.Entity("FinanClickApi.Models.Cliente", b =>
                {
                    b.Navigation("DatosClienteFisicas");

                    b.Navigation("DatosClienteMorals");

                    b.Navigation("DocumentosClientes");
                });

            modelBuilder.Entity("FinanClickApi.Models.Empresa", b =>
                {
                    b.Navigation("CatalogoDocumentos");

                    b.Navigation("Clientes");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("FinanClickApi.Models.Persona", b =>
                {
                    b.Navigation("DatosClienteFisicas");
                });

            modelBuilder.Entity("FinanClickApi.Models.PersonaMoral", b =>
                {
                    b.Navigation("DatosClienteMorals");
                });

            modelBuilder.Entity("FinanClickApi.Models.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
