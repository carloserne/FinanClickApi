using System;
using System.Collections.Generic;

namespace FinanClickApi.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdRol { get; set; }

    public string Contrasenia { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public int? IdEmpresa { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Imagen { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Modulo> IdModulos { get; set; } = new List<Modulo>();
}
