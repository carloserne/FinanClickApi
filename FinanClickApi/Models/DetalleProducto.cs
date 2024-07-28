using FinanClickApi.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Modelss;

public partial class DetalleProducto
{
    public int IdDetalleProductos { get; set; }

    public int? IdProducto { get; set; }

    public decimal? Valor { get; set; }

    public string? TipoValor { get; set; }

    public decimal? Iva { get; set; }

    public int? IdConcepto { get; set; }

    public int? Estatus { get; set; }


    public virtual CatConcepto? IdConceptoNavigation { get; set; }

    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
}
