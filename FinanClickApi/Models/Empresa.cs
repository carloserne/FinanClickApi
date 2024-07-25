using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string? NombreEmpresa { get; set; }

    public string RazonSocial { get; set; } = null!;

    public DateOnly FechaConstitucion { get; set; }

    public string NumeroEscritura { get; set; } = null!;

    public string NombreNotario { get; set; } = null!;

    public string NumeroNotario { get; set; } = null!;

    public string FolioMercantil { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public string NombreRepresentanteLegal { get; set; } = null!;

    public string NumeroEscrituraRepLeg { get; set; } = null!;

    public DateOnly FechaInscripcion { get; set; }

    public string Calle { get; set; } = null!;

    public string Colonia { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public string Teléfono { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Localidad { get; set; } = null!;

    public string NumExterior { get; set; } = null!;

    public string NumInterior { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Estatus { get; set; }

    public byte[]? Logo { get; set; }
    [JsonIgnore]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [JsonIgnore]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
