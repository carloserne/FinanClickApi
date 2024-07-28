using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanClickApi.Migrations
{
    /// <inheritdoc />
    public partial class productos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatConceptos",
                columns: table => new
                {
                    IdConcepto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreConcepto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TipoValor = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CatConce__367401534DDC30ED", x => x.IdConcepto);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Reca = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MetodoCalculo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SubMetodo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Periodicidad = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NumPagos = table.Column<int>(type: "int", nullable: true),
                    InteresAnual = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InteresMoratorio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PagoAnticipado = table.Column<bool>(type: "bit", nullable: true),
                    AplicacionDePagos = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    IdDetalleProductos = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__0988921069CC884B", x => x.IdProducto);
                });

            migrationBuilder.CreateTable(
                name: "DetalleProductos",
                columns: table => new
                {
                    IdDetalleProductos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TipoValor = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IdConcepto = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleP__C1FCE8FD435910E6", x => x.IdDetalleProductos);
                    table.ForeignKey(
                        name: "FK__DetallePr__IdCon__18EBB532",
                        column: x => x.IdConcepto,
                        principalTable: "CatConceptos",
                        principalColumn: "IdConcepto");
                    table.ForeignKey(
                        name: "FK__DetallePr__IdPro__17F790F9",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleProductos_IdConcepto",
                table: "DetalleProductos",
                column: "IdConcepto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleProductos_IdProducto",
                table: "DetalleProductos",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleProductos");

            migrationBuilder.DropTable(
                name: "CatConceptos");

            migrationBuilder.DropTable(
                name: "Producto");
        }
    }
}
