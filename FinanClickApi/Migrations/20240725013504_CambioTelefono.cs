using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class CambioTelefono : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Teléfono",
                table: "Empresa",
                newName: "Telefono");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "Empresa",
                newName: "Teléfono");
        }
    }
}
