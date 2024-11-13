using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIdEmpresaToIngresosEgresos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__VentaPros__numer__31B762FC",
                table: "VentaProspecto");

            /*migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "Ingresos_Egresos",
                type: "int",
                nullable: true);*/

            migrationBuilder.AddForeignKey(
                name: "FK__VentaPros__IdPlan__31B762FC",
                table: "VentaProspecto",
                column: "IdPlan",
                principalTable: "Plan_empresa",
                principalColumn: "IdPlan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__VentaPros__IdPlan__31B762FC",
                table: "VentaProspecto");

            /*migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "Ingresos_Egresos");*/

            migrationBuilder.AddForeignKey(
                name: "FK__VentaPros__numer__31B762FC",
                table: "VentaProspecto",
                column: "IdPlan",
                principalTable: "Plan_empresa",
                principalColumn: "IdPlan");
        }
    }
}
