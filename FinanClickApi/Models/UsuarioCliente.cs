using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class UsuarioCliente
{
    public int IdUsuarioCliente { get; set; }

    public int? IdCliente { get; set; }

    public string? Usuario { get; set; }

    public string? Contrasenia { get; set; }

    public int? Estatus { get; set; }
    [JsonIgnore]
    public virtual Cliente? IdClienteNavigation { get; set; }
}
