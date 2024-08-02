using FinanClickApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanClickApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class SimulacionController : ControllerBase
    {
        [HttpPost]
        public ActionResult<List<AmortizacionDto>> CalculateAmortization([FromBody] SimulacionDto parameters)
        {
            var schedule = CalculateAmortizacionDto(
                parameters.MetodoCalculo,
                parameters.SubMetodoCalculo,
                parameters.Periodicidad,
                parameters.NumPagos,
                parameters.InteresAnual,
                parameters.Iva,
                parameters.IvaExento,
                parameters.FechaInicio,
                parameters.Monto
            );

            return Ok(schedule);
        }

        private List<AmortizacionDto> CalculateAmortizacionDto(
            string metodoCalculo,
            string subMetodoCalculo,
            string periodicidad,
            int numPagos,
            decimal interesAnual,
            decimal iva,
            bool ivaExento,
            DateTime fechaInicio,
            decimal monto)
        {
            List<AmortizacionDto> schedule = new List<AmortizacionDto>();
            decimal saldoInicial = monto;
            decimal tasaPeriodica = GetPeriodicInterestRate(interesAnual, periodicidad);
            decimal tasaDeflactada = tasaPeriodica * 1.16m;
            decimal pagoFijo = CalculatePeriodicPayment(monto, tasaDeflactada, numPagos);
            DateTime fechaPago = fechaInicio;

            for (int i = 0; i < numPagos; i++)
            {
                DateTime fechaPagoFin = GetNextPeriodDate(fechaPago, periodicidad);

                AmortizacionDto payment = new AmortizacionDto
                {
                    NumPago = i + 1,
                    SaldoInsoluto = saldoInicial,
                    FechaInicio = fechaPago,
                    FechaFin = fechaPagoFin,
                };

                if (subMetodoCalculo.ToLower() == "insolutos")
                {
                    payment.Interes = payment.SaldoInsoluto * tasaPeriodica;
                    payment.IvaSobreInteres = ivaExento ? 0 : payment.Interes * (iva/100);
                    payment.InteresMasIva = payment.Interes + payment.IvaSobreInteres;
                    payment.Capital = pagoFijo - payment.InteresMasIva;
                    payment.PagoFijo = pagoFijo;
                }
                else if (subMetodoCalculo.ToLower() == "globales")
                {
                    payment.Capital = monto / numPagos;
                    payment.Interes = monto * tasaPeriodica;
                    payment.IvaSobreInteres = ivaExento ? 0 : payment.Interes * (iva/100);
                    payment.InteresMasIva = payment.Interes + payment.IvaSobreInteres;
                    payment.PagoFijo = payment.Capital + payment.InteresMasIva;
                }
                else
                {
                    throw new ArgumentException("Sub método de cálculo no válido");
                }

                schedule.Add(payment);
                saldoInicial -= payment.Capital;
                fechaPago = fechaPagoFin;

            }

            return schedule;
        }

        private decimal GetPeriodicInterestRate(decimal interesAnual, string periodicidad)
        {
            switch (periodicidad.ToLower())
            {
                case "mensual":
                    return interesAnual / 12 / 100;
                case "anual":
                    return interesAnual / 1 / 100;
                case "quincenal":
                    return interesAnual / 24 / 100;
                default:
                    throw new ArgumentException("Periodicidad no válida");
            }
        }

        private decimal CalculatePeriodicPayment(decimal monto, decimal tasaPeriodica, int numPagos)
        {
            if (tasaPeriodica == 0)
                return monto / numPagos;

            return monto * (tasaPeriodica * (decimal)Math.Pow(1 + (double)tasaPeriodica, numPagos)) / (decimal)(Math.Pow(1 + (double)tasaPeriodica, numPagos) - 1);
        }

        private DateTime GetNextPeriodDate(DateTime current, string periodicidad)
        {
            switch (periodicidad.ToLower())
            {
                case "mensual":
                    return current.AddMonths(1);
                case "anual":
                    return current.AddYears(1);
                case "quincenal":
                    return current.AddDays(15);
                default:
                    throw new ArgumentException("Periodicidad no válida");
            }
        }
    }
}
