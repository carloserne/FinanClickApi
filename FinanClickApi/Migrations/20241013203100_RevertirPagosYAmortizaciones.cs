using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class RevertirPagosYAmortizaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Verificar si las claves existen antes de eliminarlas (esto es solo conceptual).
            migrationBuilder.DropForeignKey(
                name: "FK_Obligado_Persona",
                table: "Obligado");

            migrationBuilder.DropForeignKey(
                name: "FK_Obligado_PersonaMoral",
                table: "Obligado");

            // Agregar las claves externas nuevamente, solo si es necesario.
            migrationBuilder.AddForeignKey(
                name: "FK_Aval_PersonaMoral_avp3rm",
                table: "Aval",
                column: "idPersonaMoral",
                principalTable: "PersonaMoral",
                principalColumn: "IdPersonaMoral");

            migrationBuilder.AddForeignKey(
                name: "FK_Aval_Persona_avp3r",
                table: "Aval",
                column: "idPersona",
                principalTable: "Persona",
                principalColumn: "IdPersona");

            // Solo agrega si no existe en la base de datos.
            migrationBuilder.AddForeignKey(
                name: "FK_Obligado_PersonaMoral_obep3rm",
                table: "Obligado",
                column: "idPersonaMoral",
                principalTable: "PersonaMoral",
                principalColumn: "IdPersonaMoral",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Obligado_Persona_obp3r",
                table: "Obligado",
                column: "idPersona",
                principalTable: "Persona",
                principalColumn: "IdPersona");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aval_PersonaMoral_avp3rm",
                table: "Aval");

            migrationBuilder.DropForeignKey(
                name: "FK_Aval_Persona_avp3r",
                table: "Aval");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "QuejaSugerencia");

            migrationBuilder.AddForeignKey(
                name: "FK_Aval_Persona",
                table: "Aval",
                column: "idPersona",
                principalTable: "Persona",
                principalColumn: "IdPersona");

            migrationBuilder.AddForeignKey(
                name: "FK_Aval_PersonaMoral",
                table: "Aval",
                column: "idPersonaMoral",
                principalTable: "PersonaMoral",
                principalColumn: "IdPersonaMoral");

            migrationBuilder.AddForeignKey(
                name: "FK_Obligado_Persona",
                table: "Obligado",
                column: "idPersona",
                principalTable: "Persona",
                principalColumn: "IdPersona");

            migrationBuilder.AddForeignKey(
                name: "FK_Obligado_PersonaMoral",
                table: "Obligado",
                column: "idPersonaMoral",
                principalTable: "PersonaMoral",
                principalColumn: "IdPersonaMoral");
        }
    }
}
