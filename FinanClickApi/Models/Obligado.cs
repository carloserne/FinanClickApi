using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class Obligado
{
    public int IdObligado { get; set; }

    public int? IdCredito { get; set; }

    public int? IdPersona { get; set; }

    public int? IdPersonaMoral { get; set; }

    [JsonIgnore]
    public virtual Credito? IdCreditoNavigation { get; set; }

    public virtual Persona? IdPersonaNavigation { get; set; }

    public virtual PersonaMoral? IdPersonaMoralNavigation { get; set; }
}
