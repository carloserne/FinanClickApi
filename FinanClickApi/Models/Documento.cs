using System;
using System.Collections.Generic;

namespace FinanClickApi.Models;

public partial class Documento
{
    public int IdDocumento { get; set; }

    public string NombreDocumento { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool EsObligatorio { get; set; }

    public virtual ICollection<DocumentosEmpresa> DocumentosEmpresas { get; set; } = new List<DocumentosEmpresa>();
}
