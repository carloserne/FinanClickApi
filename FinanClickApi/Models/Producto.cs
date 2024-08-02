using FinanClickApi.Modelss;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal? Reca { get; set; }

    public string? MetodoCalculo { get; set; }

    public string? SubMetodo { get; set; }

    public decimal? Monto { get; set; }

    public string? Periodicidad { get; set; }

    public int? NumPagos { get; set; }

    public decimal? InteresAnual { get; set; }

    public decimal? Iva { get; set; }

    public decimal? InteresMoratorio { get; set; }

    public string? PagoAnticipado { get; set; }

    public string? AplicacionDePagos { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdDetalleProductos { get; set; }

    public int? Estatus { get; set; }

    public virtual ICollection<DetalleProducto> DetalleProductos { get; set; } = new List<DetalleProducto>();

    [JsonIgnore]
    public virtual ICollection<Credito> Creditos { get; set; } = new List<Credito>();

}
