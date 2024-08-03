using System;
using System.Text.Json.Serialization;
namespace FinanClickApi.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public int IdCredito { get; set; }

    public DateOnly FechaPago { get; set; }

    public decimal MontoPago { get; set; }

    public DateOnly FechaAplicacion { get; set; }

    public int Estatus { get; set; }

    [JsonIgnore]
    public virtual Credito IdCreditoNavigation { get; set; } = null!;
}
