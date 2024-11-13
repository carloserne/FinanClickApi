using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoIdIngresoEgresoAVentaProspecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdIngresoEgreso",
                table: "VentaProspecto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VentaProspecto_IdIngresoEgreso",
                table: "VentaProspecto",
                column: "IdIngresoEgreso");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaProspecto_Ingresos_Egresos_IdIngresoEgreso",
                table: "VentaProspecto",
                column: "IdIngresoEgreso",
                principalTable: "Ingresos_Egresos",
                principalColumn: "Id_Ingresos_Egresos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaProspecto_Ingresos_Egresos_IdIngresoEgreso",
                table: "VentaProspecto");

            migrationBuilder.DropIndex(
                name: "IX_VentaProspecto_IdIngresoEgreso",
                table: "VentaProspecto");

            migrationBuilder.DropColumn(
                name: "IdIngresoEgreso",
                table: "VentaProspecto");
        }
    }
}
