namespace FinanClickApi.Dtos
{
    public class AmortizacionDto
    {
        public int NumPago { get; set; }
        public decimal SaldoInsoluto { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal IvaSobreInteres { get; set; }
        public decimal InteresMasIva { get; set; }
        public decimal PagoFijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
