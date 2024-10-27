using FinanClickApi.Temp_Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class PlanEmpresa
{
    public int IdPlan { get; set; }

    public double Precio { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Duracion { get; set; } = null!;

    public int Estatus { get; set; }

    [JsonIgnore]
    public virtual ICollection<VentaProspecto> VentaProspectos { get; set; } = new List<VentaProspecto>();
}
