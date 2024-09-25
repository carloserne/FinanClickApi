using System;
using System.Collections.Generic;

namespace FinanClickApi.Models;

public partial class Notificacion
{
    public int Id { get; set; }

    public string Mensaje { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public bool? Leido { get; set; }

    public int IdRol { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
