namespace FinanClickApi.Dtos
{
    public class SimulacionDto
    {
        public string MetodoCalculo { get; set; }
        public string SubMetodoCalculo { get; set; }
        public string Periodicidad { get; set; }
        public int NumPagos { get; set; }
        public decimal InteresAnual { get; set; }
        public decimal Iva { get; set; }
        public bool IvaExento { get; set; }
        public DateTime FechaInicio { get; set; }
        public decimal Monto { get; set; }
    }
}
