using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string PaisNacimiento { get; set; } = null!;

    public string EstadoNacimiento { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public string Curp { get; set; } = null!;

    public string? ClaveElector { get; set; }

    public string Nacionalidad { get; set; } = null!;

    public string? EstadoCivil { get; set; }

    public string? RegimenMatrimonial { get; set; }

    public string? NombreConyuge { get; set; }

    public string Calle { get; set; } = null!;

    public string NumExterior { get; set; } = null!;

    public string? NumInterior { get; set; }

    public string Colonia { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public string PaisResidencia { get; set; } = null!;

    public string EstadoResidencia { get; set; } = null!;

    public string CiudadResidencia { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    [JsonIgnore]
    public virtual ICollection<DatosClienteFisica> DatosClienteFisicas { get; set; } = new List<DatosClienteFisica>();

    [JsonIgnore]
    public virtual ICollection<Aval> Avals { get; set; } = new List<Aval>();

    [JsonIgnore]
    public virtual ICollection<Obligado> Obligados { get; set; } = new List<Obligado>();
}
