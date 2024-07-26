using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogoDocumentos",
                columns: table => new
                {
                    IdCatalogoDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Tipo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Catalogo__661C085CC0A69FBB", x => x.IdCatalogoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpresa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FechaConstitucion = table.Column<DateOnly>(type: "date", nullable: false),
                    NumeroEscritura = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreNotario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NumeroNotario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FolioMercantil = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rfc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreRepresentanteLegal = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NumeroEscrituraRepLeg = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaInscripcion = table.Column<DateOnly>(type: "date", nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Colonia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Teléfono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Localidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NumExterior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NumInterior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empresa__5EF4033EED026D14", x => x.IdEmpresa);
                });

            migrationBuilder.CreateTable(
                name: "Modulo",
                columns: table => new
                {
                    IdModulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreModulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Modulo__D9F15315EB19A4AB", x => x.IdModulo);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    PaisNacimiento = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoNacimiento = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Genero = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    RFC = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    CURP = table.Column<string>(type: "varchar(18)", unicode: false, maxLength: 18, nullable: false),
                    ClaveElector = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Nacionalidad = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoCivil = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RegimenMatrimonial = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NombreConyuge = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Calle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NumExterior = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NumInterior = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Colonia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PaisResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CiudadResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Persona__2EC8D2AC779D8E92", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "PersonaMoral",
                columns: table => new
                {
                    IdPersonaMoral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RazonComercial = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FechaConstitucion = table.Column<DateOnly>(type: "date", nullable: false),
                    RFC = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    Nacionalidad = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PaisRegistro = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoRegistro = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CiudadRegistro = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NumEscritura = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FechaRPPC = table.Column<DateOnly>(type: "date", nullable: true),
                    NombreNotario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NumNotario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FolioMercantil = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Calle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NumExterior = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NumInterior = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Colonia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PaisResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CiudadResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PersonaM__D333C18CD1FEDAC6", x => x.IdPersonaMoral);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__2A49584C5977DB14", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegimenFiscal = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    idEmpresa = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cliente__D594664253D4D0E5", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK__Cliente__idEmpre__44FF419A",
                        column: x => x.idEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(type: "int", nullable: true),
                    Contrasenia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__5B65BF975403CE1B", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK__Usuario__IdEmpre__3E52440B",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK__Usuario__IdRol__3D5E1FD2",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "DatosClienteFisica",
                columns: table => new
                {
                    IdClienteFisica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DatosCli__0F2855A92E6905D0", x => x.IdClienteFisica);
                    table.ForeignKey(
                        name: "FK__DatosClie__IdCli__52593CB8",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK__DatosClie__IdPer__5165187F",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                });

            migrationBuilder.CreateTable(
                name: "DatosClienteMoral",
                columns: table => new
                {
                    IdClienteMoral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersonaMoral = table.Column<int>(type: "int", nullable: true),
                    NombreRepLegal = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RFCRepLegal = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DatosCli__5A0ACC3D6EF100FC", x => x.IdClienteMoral);
                    table.ForeignKey(
                        name: "FK__DatosClie__IdCli__5629CD9C",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK__DatosClie__IdPer__5535A963",
                        column: x => x.IdPersonaMoral,
                        principalTable: "PersonaMoral",
                        principalColumn: "IdPersonaMoral");
                });

            migrationBuilder.CreateTable(
                name: "DocumentosCliente",
                columns: table => new
                {
                    IdDocumentoCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentoBase64 = table.Column<string>(type: "text", nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    IdDocumento = table.Column<int>(type: "int", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__232F08451D75BCC3", x => x.IdDocumentoCliente);
                    table.ForeignKey(
                        name: "FK__Documento__IdCli__4AB81AF0",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK__Documento__IdDoc__49C3F6B7",
                        column: x => x.IdDocumento,
                        principalTable: "CatalogoDocumentos",
                        principalColumn: "IdCatalogoDocumento");
                });

            migrationBuilder.CreateTable(
                name: "DetalleModuloUsuario",
                columns: table => new
                {
                    IdModulo = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleM__BC4708EC21F1C8DD", x => new { x.IdModulo, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK__DetalleMo__IdMod__412EB0B6",
                        column: x => x.IdModulo,
                        principalTable: "Modulo",
                        principalColumn: "IdModulo");
                    table.ForeignKey(
                        name: "FK__DetalleMo__IdUsu__4222D4EF",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_idEmpresa",
                table: "Cliente",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_DatosClienteFisica_IdCliente",
                table: "DatosClienteFisica",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DatosClienteFisica_IdPersona",
                table: "DatosClienteFisica",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_DatosClienteMoral_IdCliente",
                table: "DatosClienteMoral",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DatosClienteMoral_IdPersonaMoral",
                table: "DatosClienteMoral",
                column: "IdPersonaMoral");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleModuloUsuario_IdUsuario",
                table: "DetalleModuloUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosCliente_IdCliente",
                table: "DocumentosCliente",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosCliente_IdDocumento",
                table: "DocumentosCliente",
                column: "IdDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEmpresa",
                table: "Usuario",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdRol",
                table: "Usuario",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosClienteFisica");

            migrationBuilder.DropTable(
                name: "DatosClienteMoral");

            migrationBuilder.DropTable(
                name: "DetalleModuloUsuario");

            migrationBuilder.DropTable(
                name: "DocumentosCliente");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "PersonaMoral");

            migrationBuilder.DropTable(
                name: "Modulo");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "CatalogoDocumentos");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
