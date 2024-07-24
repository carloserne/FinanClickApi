using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class DatosClienteFisica
{
    public int IdClienteFisica { get; set; }

    public int? IdPersona { get; set; }

    public int? IdCliente { get; set; }

    [JsonIgnore]
    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Persona? IdPersonaNavigation { get; set; }
}
