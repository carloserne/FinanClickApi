using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cliente__idEmpre__4D94879B",
                table: "Cliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Cliente__D5946642DF235F53",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "idEmpresa",
                table: "Cliente",
                newName: "IdEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_idEmpresa",
                table: "Cliente",
                newName: "IX_Cliente_IdEmpresa");

            migrationBuilder.AlterColumn<string>(
                name: "RegimenFiscal",
                table: "Cliente",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Cliente__D594664233EE2C82",
                table: "Cliente",
                column: "IdCliente");

            migrationBuilder.CreateTable(
                name: "CatalogoDocumentos",
                columns: table => new
                {
                    IdCatalogoDocumento = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Tipo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Catalogo__661C085CFFF601D2", x => x.IdCatalogoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosCliente",
                columns: table => new
                {
                    IdDocumentoCliente = table.Column<int>(type: "int", nullable: false),
                    DocumentoBase64 = table.Column<string>(type: "text", nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    IdDocumento = table.Column<int>(type: "int", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__232F0845AB0DD62B", x => x.IdDocumentoCliente);
                    table.ForeignKey(
                        name: "FK__Documento__IdCli__5070F446",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK__Documento__IdDoc__4F7CD00D",
                        column: x => x.IdDocumento,
                        principalTable: "CatalogoDocumentos",
                        principalColumn: "IdCatalogoDocumento");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosCliente_IdCliente",
                table: "DocumentosCliente",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosCliente_IdDocumento",
                table: "DocumentosCliente",
                column: "IdDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK__Cliente__IdEmpre__4AB81AF0",
                table: "Cliente",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cliente__IdEmpre__4AB81AF0",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "DocumentosCliente");

            migrationBuilder.DropTable(
                name: "CatalogoDocumentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Cliente__D594664233EE2C82",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "IdEmpresa",
                table: "Cliente",
                newName: "idEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_IdEmpresa",
                table: "Cliente",
                newName: "IX_Cliente_idEmpresa");

            migrationBuilder.AlterColumn<bool>(
                name: "RegimenFiscal",
                table: "Cliente",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Cliente__D5946642DF235F53",
                table: "Cliente",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK__Cliente__idEmpre__4D94879B",
                table: "Cliente",
                column: "idEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }
    }
}
