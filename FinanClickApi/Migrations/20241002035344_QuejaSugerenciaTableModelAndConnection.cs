using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class QuejaSugerenciaTableModelAndConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mensaje = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Leido = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__3214EC0780E02CAF", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Notificac__IdRol__6DCC4D03",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "QuejaSugerencia",
                columns: table => new
                {
                    IdQuejaSugerencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Estado = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    FechaResolucion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Responsable = table.Column<int>(type: "int", nullable: true),
                    Prioridad = table.Column<int>(type: "int", nullable: true),
                    Comentarios = table.Column<string>(type: "text", nullable: true),
                    ArchivoAdjunto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuejaSug__8892057E15C34A65", x => x.IdQuejaSugerencia);
                    table.ForeignKey(
                        name: "FK_EmpresaQuejaSugerencia",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponsableUsuario",
                        column: x => x.Responsable,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_IdRol",
                table: "Notificacion",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_QuejaSugerencia_IdEmpresa",
                table: "QuejaSugerencia",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_QuejaSugerencia_Responsable",
                table: "QuejaSugerencia",
                column: "Responsable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "QuejaSugerencia");
        }
    }
}
