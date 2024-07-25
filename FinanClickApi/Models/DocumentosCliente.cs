using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class DocumentosCliente
{
    public int IdDocumentoCliente { get; set; }

    public string DocumentoBase64 { get; set; } = null!;

    public int Estatus { get; set; }

    public int? IdDocumento { get; set; }

    public int? IdCliente { get; set; }


    [JsonIgnore]
    public virtual Cliente? IdClienteNavigation { get; set; }
    [JsonIgnore]
    public virtual CatalogoDocumento? IdDocumentoNavigation { get; set; }
}
