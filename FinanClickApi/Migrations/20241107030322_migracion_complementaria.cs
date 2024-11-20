using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class migracion_complementaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuejaSugerencia_Empresa_IdEmpresa",
                table: "QuejaSugerencia");

            migrationBuilder.RenameTable(
                name: "QuejaSugerencia",
                newName: "QuejaSugerencium");

            migrationBuilder.RenameIndex(
                name: "IX_QuejaSugerencia_Responsable",
                table: "QuejaSugerencium",
                newName: "IX_QuejaSugerencium_Responsable");

            migrationBuilder.RenameIndex(
                name: "IX_QuejaSugerencia_IdEmpresa",
                table: "QuejaSugerencium",
                newName: "IX_QuejaSugerencium_IdEmpresa");

            migrationBuilder.CreateTable(
                name: "ContactoPersona",
                columns: table => new
                {
                    idContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpresa = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    puesto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contacto__4B1329C75B1C17DD", x => x.idContacto);
                    table.ForeignKey(
                        name: "FK__ContactoP__idEmp__1B9317B3",
                        column: x => x.idEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            /*migrationBuilder.CreateTable(
                name: "Plan_empresa",
                columns: table => new
                {
                    IdPlan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Duracion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Plan_emp__FB8102AEC98D798A", x => x.IdPlan);
                });*/

            /*migrationBuilder.CreateTable(
                name: "VentaProspecto",
                columns: table => new
                {
                    IdVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPlan = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    fechaSolicitud = table.Column<DateOnly>(type: "date", nullable: false),
                    nombreCliente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    nombreEmpresa = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    domicilio = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ciudad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    rfc = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    numeroContacto = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VentaPro__BC1240BD2D33906A", x => x.IdVenta);
                    table.ForeignKey(
                        name: "FK_VentaProspecto_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK__VentaPros__numer__31B762FC",
                        column: x => x.IdPlan,
                        principalTable: "Plan_empresa",
                        principalColumn: "IdPlan");
                });*/

            migrationBuilder.CreateIndex(
                name: "IX_ContactoPersona_idEmpresa",
                table: "ContactoPersona",
                column: "idEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_VentaProspecto_IdPlan",
                table: "VentaProspecto",
                column: "IdPlan");

            migrationBuilder.CreateIndex(
                name: "IX_VentaProspecto_IdUsuario",
                table: "VentaProspecto",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_QuejaSugerencium_Empresa_IdEmpresa",
                table: "QuejaSugerencium",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuejaSugerencium_Empresa_IdEmpresa",
                table: "QuejaSugerencium");

            migrationBuilder.DropTable(
                name: "ContactoPersona");

            /*migrationBuilder.DropTable(
                name: "VentaProspecto");*/

            /*migrationBuilder.DropTable(
                name: "Plan_empresa");*/

            migrationBuilder.RenameTable(
                name: "QuejaSugerencium",
                newName: "QuejaSugerencia");

            migrationBuilder.RenameIndex(
                name: "IX_QuejaSugerencium_Responsable",
                table: "QuejaSugerencia",
                newName: "IX_QuejaSugerencia_Responsable");

            migrationBuilder.RenameIndex(
                name: "IX_QuejaSugerencium_IdEmpresa",
                table: "QuejaSugerencia",
                newName: "IX_QuejaSugerencia_IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_QuejaSugerencia_Empresa_IdEmpresa",
                table: "QuejaSugerencia",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }
    }
}
