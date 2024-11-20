using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class IngresosEgresos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropPrimaryKey(
                name: "PK__Plan_emp__FB8102AEC98D798A",
                table: "Plan_empresa");
            */

            migrationBuilder.AddColumn<int>(
                name: "numero_meses",
                table: "Plan_empresa",
                type: "int",
                nullable: true);

            /*
            migrationBuilder.AddPrimaryKey(
                name: "PK_Plan_emp_FB8102AE15FD51B9",
                table: "Plan_empresa",
                column: "IdPlan");
            */

            migrationBuilder.CreateTable(
                name: "Ingresos_Egresos",
                columns: table => new
                {
                    Id_Ingresos_Egresos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    TipoTransaccion = table.Column<int>(type: "int", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Categoria = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ingresos__28C0110C36D631C4", x => x.Id_Ingresos_Egresos);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingresos_Egresos");

            /*
            migrationBuilder.DropPrimaryKey(
                name: "PK_Plan_emp_FB8102AE15FD51B9",
                table: "Plan_empresa");
            */

            migrationBuilder.DropColumn(
                name: "numero_meses",
                table: "Plan_empresa");

            /*
            migrationBuilder.AddPrimaryKey(
                name: "PK__Plan_emp__FB8102AEC98D798A",
                table: "Plan_empresa",
                column: "IdPlan");
            */
        }
    }
}
