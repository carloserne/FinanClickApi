using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class creditoavalescolumnaProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PagoAnticipado",
                table: "Producto",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Credito",
                columns: table => new
                {
                    IdCredito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    Iva = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Periodicidad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FechaFirma = table.Column<DateOnly>(type: "date", nullable: true),
                    FechaActivacion = table.Column<DateOnly>(type: "date", nullable: true),
                    NumPagos = table.Column<int>(type: "int", nullable: false),
                    InteresOrdinario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdPromotor = table.Column<int>(type: "int", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: true),
                    InteresMoratorio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Credito__EF6108CB209BE43B", x => x.IdCredito);
                    table.ForeignKey(
                        name: "FK__Credito__IdProdu__2DE6D218",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "Aval",
                columns: table => new
                {
                    idAval = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCredito = table.Column<int>(type: "int", nullable: true),
                    idPersona = table.Column<int>(type: "int", nullable: true),
                    idPersonaMoral = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Aval__D8A6A80225CFEA8D", x => x.idAval);
                    table.ForeignKey(
                        name: "FK_Aval_Persona",
                        column: x => x.idPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                    table.ForeignKey(
                        name: "FK_Aval_PersonaMoral",
                        column: x => x.idPersonaMoral,
                        principalTable: "PersonaMoral",
                        principalColumn: "IdPersonaMoral");
                    table.ForeignKey(
                        name: "FK__Aval__idCredito__32AB8735",
                        column: x => x.idCredito,
                        principalTable: "Credito",
                        principalColumn: "IdCredito");
                });

            migrationBuilder.CreateTable(
                name: "Obligado",
                columns: table => new
                {
                    idObligado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCredito = table.Column<int>(type: "int", nullable: true),
                    idPersona = table.Column<int>(type: "int", nullable: true),
                    idPersonaMoral = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Obligado__E088162F78AC3B83", x => x.idObligado);
                    table.ForeignKey(
                        name: "FK_Obligado_Persona",
                        column: x => x.idPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                    table.ForeignKey(
                        name: "FK_Obligado_PersonaMoral",
                        column: x => x.idPersonaMoral,
                        principalTable: "PersonaMoral",
                        principalColumn: "IdPersonaMoral");
                    table.ForeignKey(
                        name: "FK__Obligado__idCred__37703C52",
                        column: x => x.idCredito,
                        principalTable: "Credito",
                        principalColumn: "IdCredito");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aval_idCredito",
                table: "Aval",
                column: "idCredito");

            migrationBuilder.CreateIndex(
                name: "IX_Aval_idPersona",
                table: "Aval",
                column: "idPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Aval_idPersonaMoral",
                table: "Aval",
                column: "idPersonaMoral");

            migrationBuilder.CreateIndex(
                name: "IX_Credito_IdProducto",
                table: "Credito",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Obligado_idCredito",
                table: "Obligado",
                column: "idCredito");

            migrationBuilder.CreateIndex(
                name: "IX_Obligado_idPersona",
                table: "Obligado",
                column: "idPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Obligado_idPersonaMoral",
                table: "Obligado",
                column: "idPersonaMoral");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aval");

            migrationBuilder.DropTable(
                name: "Obligado");

            migrationBuilder.DropTable(
                name: "Credito");

            migrationBuilder.AlterColumn<bool>(
                name: "PagoAnticipado",
                table: "Producto",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
