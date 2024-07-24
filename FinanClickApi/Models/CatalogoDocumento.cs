using System;
using System.Collections.Generic;

namespace FinanClickApi.Models;

public partial class CatalogoDocumento
{
    public int IdCatalogoDocumento { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int Estatus { get; set; }

    public virtual ICollection<DocumentosCliente> DocumentosClientes { get; set; } = new List<DocumentosCliente>();
}
