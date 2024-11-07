using System;
using System.Collections.Generic;

namespace FinanClickApi.Models;

public partial class IngresosEgreso
{
    public int IdIngresosEgresos { get; set; }

    public DateOnly Fecha { get; set; }

    public int? TipoTransaccion { get; set; }

    public decimal Monto { get; set; }

    public string? Descripcion { get; set; }

    public string? Categoria { get; set; }

    public int? Estatus { get; set; }
}
