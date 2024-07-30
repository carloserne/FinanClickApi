namespace FinanClickApi.Dtos
{
    public class AmortizacionDto
    {
        public DateTime Fecha { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Iva { get; set; }
        public decimal SaldoFinal { get; set; }


    }
}
