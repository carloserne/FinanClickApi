using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class DatosClienteMoral
{
    public int IdClienteMoral { get; set; }

    public int? IdPersonaMoral { get; set; }

    public string NombreRepLegal { get; set; } = null!;

    public string RfcrepLegal { get; set; } = null!;

    public int? IdCliente { get; set; }

    [JsonIgnore]
    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual PersonaMoral? IdPersonaMoralNavigation { get; set; }
}
