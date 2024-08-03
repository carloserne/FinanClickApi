using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class Amortizacion
{
    public int IdAmortizacion { get; set; }

    public int IdCredito { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public int Estatus { get; set; }

    public decimal SaldoInsoluto { get; set; }

    public decimal Capital { get; set; }

    public decimal InteresOrdinario { get; set; }

    public decimal Iva { get; set; }

    public decimal InteresMasIva {  get; set; }

    public decimal PagoFijo { get; set; }

    public decimal InteresMoratorio { get; set; }

    [JsonIgnore]
    public virtual Credito IdCreditoNavigation { get; set; } = null!;
}
