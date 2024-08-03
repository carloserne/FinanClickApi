using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class pagosYamortizaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aval_Persona",
                table: "Aval");

            migrationBuilder.DropForeignKey(
                name: "FK_Aval_PersonaMoral",
                table: "Aval");

            migrationBuilder.CreateTable(
                name: "Amortizacion",
                columns: table => new
                {
                    IdAmortizacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCredito = table.Column<int>(type: "INT", nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "DATE", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Estatus = table.Column<int>(type: "INT", nullable: false),
                    SaldoInsoluto = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Capital = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    InteresOrdinario = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    IVA = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    InteresMasIva = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PagoFijo = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    InteresMoratorio = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Amortiza__8D928C9884C966EE", x => x.IdAmortizacion);
                    table.ForeignKey(
                        name: "FK_Credito_Amortizacion_72C60C4A",
                        column: x => x.IdCredito,
                        principalTable: "Credito",
                        principalColumn: "IdCredito",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    IdPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCredito = table.Column<int>(type: "INT", nullable: false),
                    FechaPago = table.Column<DateOnly>(type: "DATE", nullable: false),
                    MontoPago = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    FechaAplicacion = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Estatus = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pago__FC851A3A66103805", x => x.IdPago);
                    table.ForeignKey(
                        name: "FK_Credito_Pago_72C60C4A",
                        column: x => x.IdCredito,
                        principalTable: "Credito",
                        principalColumn: "IdCredito",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amortizacion_IdCredito",
                table: "Amortizacion",
                column: "IdCredito");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdCredito",
                table: "Pago",
                column: "IdCredito");

            migrationBuilder.AddForeignKey(
                name: "FK_Obligado_Persona_obp3r",
                table: "Obligado",
                column: "idPersona",
                principalTable: "Persona",
                principalColumn: "IdPersona");

            migrationBuilder.AddForeignKey(
                name: "FK_Obligado_PersonaMoral_obep3rm",
                table: "Obligado",
                column: "idPersonaMoral",
                principalTable: "PersonaMoral",
                principalColumn: "IdPersonaMoral");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Obligado_Persona",
                table: "Obligado");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligado_PersonaMoral",
                table: "Obligado");

            migrationBuilder.DropTable(
                name: "Amortizacion");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.AddForeignKey(
                name: "c",
                table: "Aval",
                column: "idPersona",
                principalTable: "Persona",
                principalColumn: "IdPersona");

            migrationBuilder.AddForeignKey(
                name: "c",
                table: "Aval",
                column: "idPersonaMoral",
                principalTable: "PersonaMoral",
                principalColumn: "IdPersonaMoral");
        }
    }
}
