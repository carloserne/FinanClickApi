using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
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
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empresa__5EF4033EF3A3FD3B", x => x.IdEmpresa);
                });

            migrationBuilder.CreateTable(
                name: "Modulo",
                columns: table => new
                {
                    IdModulo = table.Column<int>(type: "int", nullable: false),
                    NombreModulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Modulo__D9F153151E446F3E", x => x.IdModulo);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    NombreRol = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__2A49584C90796678", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    RegimenFiscal = table.Column<bool>(type: "bit", nullable: true),
                    idEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cliente__D5946642DF235F53", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK__Cliente__idEmpre__4D94879B",
                        column: x => x.idEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK__Usuario__5B65BF9749BA4FAE", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK__Usuario__IdEmpre__440B1D61",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK__Usuario__IdRol__4316F928",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol");
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
                    table.PrimaryKey("PK__DetalleM__BC4708ECC48CFC34", x => new { x.IdModulo, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK__DetalleMo__IdMod__46E78A0C",
                        column: x => x.IdModulo,
                        principalTable: "Modulo",
                        principalColumn: "IdModulo");
                    table.ForeignKey(
                        name: "FK__DetalleMo__IdUsu__47DBAE45",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_idEmpresa",
                table: "Cliente",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleModuloUsuario_IdUsuario",
                table: "DetalleModuloUsuario",
                column: "IdUsuario");

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
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "DetalleModuloUsuario");

            migrationBuilder.DropTable(
                name: "Modulo");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
