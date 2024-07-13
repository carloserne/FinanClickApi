using System;
using System.Collections.Generic;

namespace FinanClickApi.Models;

public partial class Modulo
{
    public int IdModulo { get; set; }

    public string NombreModulo { get; set; } = null!;

    public bool Estatus { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Usuario> IdUsuarios { get; set; } = new List<Usuario>();
}
