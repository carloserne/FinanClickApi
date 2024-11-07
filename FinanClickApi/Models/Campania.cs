namespace FinanClickApi.Models
{
    public partial class Campania
    {
        public int IdCampania { get; set; }

        public string Nombre { get; set; }

        public string Asunto { get; set; }

        public string Contenido { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ScheduleDate { get; set; }

        public string Tipo { get; set; }

        public int Estatus { get; set; }

        public string Destinatarios { get; set; }

        public int? IdEmpresa { get; set; }
    }
}
