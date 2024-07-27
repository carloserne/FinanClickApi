using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class idempresacatdocumentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idEmpresa",
                table: "CatalogoDocumentos",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoDocumentos_idEmpresa",
                table: "CatalogoDocumentos",
                column: "idEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK__Cliente__idEmpresa_C4TEMPR354",
                table: "CatalogoDocumentos",
                column: "idEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cliente__idEmpresa_C4TEMPR354",
                table: "CatalogoDocumentos");

            migrationBuilder.DropIndex(
                name: "IX_CatalogoDocumentos_idEmpresa",
                table: "CatalogoDocumentos");

            migrationBuilder.DropColumn(
                name: "idEmpresa",
                table: "CatalogoDocumentos");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "Empresa",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
