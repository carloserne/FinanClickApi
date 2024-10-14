using System;
using System.Collections.Generic;

namespace FinanClickApi.Models;

public partial class QuejaSugerencium
{
    public int IdQuejaSugerencia { get; set; }

    public int IdEmpresa { get; set; }

    public string Tipo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int Estado { get; set; }

    public DateTime? FechaResolucion { get; set; }

    public int? Responsable { get; set; }

    public int? Prioridad { get; set; }

    public string? Comentarios { get; set; }

    public string? ArchivoAdjunto { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Usuario? ResponsableNavigation { get; set; }
}
