using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class Aval
{
    [JsonIgnore]
    public int IdAval { get; set; }
    [JsonIgnore]
    public int? IdCredito { get; set; }
    [JsonIgnore]
    public int? IdPersona { get; set; }
    [JsonIgnore]
    public int? IdPersonaMoral { get; set; }

    [JsonIgnore]
    public virtual Credito? IdCreditoNavigation { get; set; }

    public virtual Persona? IdPersonaNavigation { get; set; }

    public virtual PersonaMoral? IdPersonaMoralNavigation { get; set; }

}
