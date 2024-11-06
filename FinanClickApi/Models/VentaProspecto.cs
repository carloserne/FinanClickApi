using FinanClickApi.Models;
using FinanClickApi.Temp_Models;
using System.Text.Json.Serialization;

public partial class VentaProspecto
{
    public int IdVenta { get; set; }
    public int IdPlan { get; set; }
    public int? IdUsuario { get; set; }
    public DateOnly FechaSolicitud { get; set; }
    public string NombreCliente { get; set; } = null!;
    public string NombreEmpresa { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string Domicilio { get; set; } = null!;
    public string Ciudad { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string Rfc { get; set; } = null!;
    public string NumeroContacto { get; set; } = null!;

    [JsonIgnore]
    public virtual PlanEmpresa IdPlanNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
