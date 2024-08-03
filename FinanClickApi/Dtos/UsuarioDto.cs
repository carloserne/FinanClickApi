namespace FinanClickApi.Dtos
{
    public class UsuarioDto
    {
        public int? IdRol { get; set; }
        public string Contrasenia { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public int? IdEmpresa { get; set; }
        public string Usuario1 { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Imagen { get; set; }
    }
}
