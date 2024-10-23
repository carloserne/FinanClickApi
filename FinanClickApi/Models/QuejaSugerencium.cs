using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class QuejaSugerencium
{
    public int IdQuejaSugerencia { get; set; }

    public int? IdEmpresa { get; set; }

    public string Tipo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int Estatus { get; set; }

    public DateTime? FechaResolucion { get; set; }

    public int? Responsable { get; set; }

    public int? Prioridad { get; set; }

    public string? Comentarios { get; set; }

    public string? ArchivoAdjunto { get; set; }

    [JsonIgnore]
    public virtual Empresa? IdEmpresaNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Usuario? ResponsableNavigation { get; set; }
}
