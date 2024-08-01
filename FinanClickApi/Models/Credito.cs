using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class Credito
{
    public int IdCredito { get; set; }

    public int IdProducto { get; set; }

    public decimal Monto { get; set; }

    public int Estatus { get; set; }

    public decimal Iva { get; set; }

    public string Periodicidad { get; set; } = null!;

    public DateOnly? FechaFirma { get; set; }

    public DateOnly? FechaActivacion { get; set; }

    public int NumPagos { get; set; }

    public decimal InteresOrdinario { get; set; }

    public int? IdPromotor { get; set; }

    public int? IdCliente { get; set; }

    public decimal InteresMoratorio { get; set; }

    public virtual ICollection<Aval> Avals { get; set; } = new List<Aval>();

    public virtual ICollection<Obligado> Obligados { get; set; } = new List<Obligado>();

    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }




}
