using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public bool Estatus { get; set; }

    public string Descripcion { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
