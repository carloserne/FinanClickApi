using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class ClientesModificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cliente__IdEmpre__4AB81AF0",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK__Documento__IdCli__5070F446",
                table: "DocumentosCliente");

            migrationBuilder.DropForeignKey(
                name: "FK__Documento__IdDoc__4F7CD00D",
                table: "DocumentosCliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Document__232F0845AB0DD62B",
                table: "DocumentosCliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Cliente__D594664233EE2C82",
                table: "Cliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Catalogo__661C085CFFF601D2",
                table: "CatalogoDocumentos");

            migrationBuilder.RenameColumn(
                name: "IdEmpresa",
                table: "Cliente",
                newName: "idEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_IdEmpresa",
                table: "Cliente",
                newName: "IX_Cliente_idEmpresa");

            migrationBuilder.AlterColumn<int>(
                name: "IdDocumentoCliente",
                table: "DocumentosCliente",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "RegimenFiscal",
                table: "Cliente",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "IdCliente",
                table: "Cliente",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "Cliente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdCatalogoDocumento",
                table: "CatalogoDocumentos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Document__232F0845D31D05C3",
                table: "DocumentosCliente",
                column: "IdDocumentoCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Cliente__D594664231FDEEB9",
                table: "Cliente",
                column: "IdCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Catalogo__661C085CB02BF800",
                table: "CatalogoDocumentos",
                column: "IdCatalogoDocumento");

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    PaisNacimiento = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoNacimiento = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Genero = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    RFC = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    CURP = table.Column<string>(type: "varchar(18)", unicode: false, maxLength: 18, nullable: false),
                    ClaveElector = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Nacionalidad = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoCivil = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RegimenMatrimonial = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NombreConyuge = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Calle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NumExterior = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NumInterior = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Colonia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PaisResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CiudadResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Persona__2EC8D2AC8A73447C", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "PersonaMoral",
                columns: table => new
                {
                    IdPersonaMoral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RazonComercial = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FechaConstitucion = table.Column<DateOnly>(type: "date", nullable: false),
                    RFC = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    Nacionalidad = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PaisRegistro = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoRegistro = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CiudadRegistro = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NumEscritura = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FechaRPPC = table.Column<DateOnly>(type: "date", nullable: true),
                    NombreNotario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NumNotario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FolioMercantil = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Calle = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NumExterior = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NumInterior = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Colonia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PaisResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EstadoResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CiudadResidencia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PersonaM__D333C18CA44A9D18", x => x.IdPersonaMoral);
                });

            migrationBuilder.CreateTable(
                name: "DatosClienteFisica",
                columns: table => new
                {
                    IdClienteFisica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DatosCli__0F2855A9BD3241AF", x => x.IdClienteFisica);
                    table.ForeignKey(
                        name: "FK__DatosClie__IdCli__778AC167",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK__DatosClie__IdPer__76969D2E",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                });

            migrationBuilder.CreateTable(
                name: "DatosClienteMoral",
                columns: table => new
                {
                    IdClienteMoral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersonaMoral = table.Column<int>(type: "int", nullable: true),
                    NombreRepLegal = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RFCRepLegal = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DatosCli__5A0ACC3D5431E273", x => x.IdClienteMoral);
                    table.ForeignKey(
                        name: "FK__DatosClie__IdCli__02084FDA",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK__DatosClie__IdPer__01142BA1",
                        column: x => x.IdPersonaMoral,
                        principalTable: "PersonaMoral",
                        principalColumn: "IdPersonaMoral");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatosClienteFisica_IdCliente",
                table: "DatosClienteFisica",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DatosClienteFisica_IdPersona",
                table: "DatosClienteFisica",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_DatosClienteMoral_IdCliente",
                table: "DatosClienteMoral",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DatosClienteMoral_IdPersonaMoral",
                table: "DatosClienteMoral",
                column: "IdPersonaMoral");

            migrationBuilder.AddForeignKey(
                name: "FK__Cliente__idEmpre__6C190EBB",
                table: "Cliente",
                column: "idEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK__Documento__IdCli__71D1E811",
                table: "DocumentosCliente",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK__Documento__IdDoc__70DDC3D8",
                table: "DocumentosCliente",
                column: "IdDocumento",
                principalTable: "CatalogoDocumentos",
                principalColumn: "IdCatalogoDocumento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cliente__idEmpre__6C190EBB",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK__Documento__IdCli__71D1E811",
                table: "DocumentosCliente");

            migrationBuilder.DropForeignKey(
                name: "FK__Documento__IdDoc__70DDC3D8",
                table: "DocumentosCliente");

            migrationBuilder.DropTable(
                name: "DatosClienteFisica");

            migrationBuilder.DropTable(
                name: "DatosClienteMoral");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "PersonaMoral");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Document__232F0845D31D05C3",
                table: "DocumentosCliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Cliente__D594664231FDEEB9",
                table: "Cliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Catalogo__661C085CB02BF800",
                table: "CatalogoDocumentos");

            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "idEmpresa",
                table: "Cliente",
                newName: "IdEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_idEmpresa",
                table: "Cliente",
                newName: "IX_Cliente_IdEmpresa");

            migrationBuilder.AlterColumn<int>(
                name: "IdDocumentoCliente",
                table: "DocumentosCliente",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "RegimenFiscal",
                table: "Cliente",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdCliente",
                table: "Cliente",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "IdCatalogoDocumento",
                table: "CatalogoDocumentos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Document__232F0845AB0DD62B",
                table: "DocumentosCliente",
                column: "IdDocumentoCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Cliente__D594664233EE2C82",
                table: "Cliente",
                column: "IdCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Catalogo__661C085CFFF601D2",
                table: "CatalogoDocumentos",
                column: "IdCatalogoDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK__Cliente__IdEmpre__4AB81AF0",
                table: "Cliente",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK__Documento__IdCli__5070F446",
                table: "DocumentosCliente",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK__Documento__IdDoc__4F7CD00D",
                table: "DocumentosCliente",
                column: "IdDocumento",
                principalTable: "CatalogoDocumentos",
                principalColumn: "IdCatalogoDocumento");
        }
    }
}
