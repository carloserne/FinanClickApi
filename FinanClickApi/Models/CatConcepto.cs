using FinanClickApi.Modelss;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class CatConcepto
{
    public int IdConcepto { get; set; }

    public string? NombreConcepto { get; set; }

    public decimal? Valor { get; set; }

    public string? TipoValor { get; set; }

    public decimal? Iva { get; set; }

    public int? Estatus { get; set; }

    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleProducto> DetalleProductos { get; set; } = new List<DetalleProducto>();
}
