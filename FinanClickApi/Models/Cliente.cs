using System;
using System.Collections.Generic;

namespace FinanClickApi.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public bool? RegimenFiscal { get; set; }

    public int? IdEmpresa { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }
}
