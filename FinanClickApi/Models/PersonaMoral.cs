using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class PersonaMoral
{
    public int IdPersonaMoral { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string? RazonComercial { get; set; }

    public DateOnly FechaConstitucion { get; set; }

    public string Rfc { get; set; } = null!;

    public string Nacionalidad { get; set; } = null!;

    public string PaisRegistro { get; set; } = null!;

    public string EstadoRegistro { get; set; } = null!;

    public string CiudadRegistro { get; set; } = null!;

    public string NumEscritura { get; set; } = null!;

    public DateOnly? FechaRppc { get; set; }

    public string? NombreNotario { get; set; }

    public string? NumNotario { get; set; }

    public string? FolioMercantil { get; set; }

    public string Calle { get; set; } = null!;

    public string NumExterior { get; set; } = null!;

    public string? NumInterior { get; set; }

    public string Colonia { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public string PaisResidencia { get; set; } = null!;

    public string EstadoResidencia { get; set; } = null!;

    public string CiudadResidencia { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<DatosClienteMoral> DatosClienteMorals { get; set; } = new List<DatosClienteMoral>();

    [JsonIgnore]
    public virtual ICollection<Aval> Avals { get; set; } = new List<Aval>();

    [JsonIgnore]
    public virtual ICollection<Obligado> Obligados { get; set; } = new List<Obligado>();
}
