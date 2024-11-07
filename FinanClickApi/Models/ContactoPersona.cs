using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class ContactoPersona
{
    public int IdContacto { get; set; }

    public int IdEmpresa { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Puesto { get; set; }

    [JsonIgnore]
    public virtual Empresa? IdEmpresaNavigation { get; set; } = null!;
}
