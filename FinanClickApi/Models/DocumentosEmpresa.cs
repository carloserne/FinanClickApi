using System;
using System.Collections.Generic;

namespace FinanClickApi.Models;

public partial class DocumentosEmpresa
{
    public int IdDocumentoEmpresa { get; set; }

    public int IdEmpresa { get; set; }

    public int IdDocumento { get; set; }

    public string EstadoDocumento { get; set; } = null!;

    public DateTime? FechaSubida { get; set; }

    public string? RutaArchivo { get; set; }

    public virtual Documento IdDocumentoNavigation { get; set; } = null!;

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
}
