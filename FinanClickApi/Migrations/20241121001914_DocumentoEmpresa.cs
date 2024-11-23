using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class DocumentoEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    IdActividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Pendiente"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Activida__5EAF86A4864D3F7C", x => x.IdActividad);
                    table.ForeignKey(
                        name: "FK__Actividad__IdUsu__2CF2ADDF",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    IdDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDocumento = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsObligatorio = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__E520734746846EF9", x => x.IdDocumento);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosEmpresa",
                columns: table => new
                {
                    IdDocumentoEmpresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    IdDocumento = table.Column<int>(type: "int", nullable: false),
                    EstadoDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime", nullable: true),
                    RutaArchivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__D8E796008CB5341B", x => x.IdDocumentoEmpresa);
                    table.ForeignKey(
                        name: "FK__Documento__IdDoc__40F9A68C",
                        column: x => x.IdDocumento,
                        principalTable: "Documentos",
                        principalColumn: "IdDocumento");
                    table.ForeignKey(
                        name: "FK__Documento__IdEmp__40058253",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_IdUsuario",
                table: "Actividad",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosEmpresa_IdDocumento",
                table: "DocumentosEmpresa",
                column: "IdDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosEmpresa_IdEmpresa",
                table: "DocumentosEmpresa",
                column: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "DocumentosEmpresa");

            migrationBuilder.DropTable(
                name: "Documentos");
        }
    }
}
