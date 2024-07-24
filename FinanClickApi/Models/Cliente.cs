using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? RegimenFiscal { get; set; }

    public int? IdEmpresa { get; set; }

    public int Estatus { get; set; }

    public virtual ICollection<DatosClienteFisica> DatosClienteFisicas { get; set; } = new List<DatosClienteFisica>();

    public virtual ICollection<DatosClienteMoral> DatosClienteMorals { get; set; } = new List<DatosClienteMoral>();

    [JsonIgnore]
    public virtual ICollection<DocumentosCliente> DocumentosClientes { get; set; } = new List<DocumentosCliente>();

    [JsonIgnore]
    public virtual Empresa? IdEmpresaNavigation { get; set; }
}
