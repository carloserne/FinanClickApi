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
            TimeSpan periodo = GetPeriodSpan(periodicidad);

            if (subMetodoCalculo.ToLower() == "saldos globales")
            {
                decimal pagoPeriodico = CalculatePeriodicPayment(monto, tasaPeriodica, numPagos);

                for (int i = 0; i < numPagos; i++)
                {
                    AmortizacionDto payment = new AmortizacionDto();
                    payment.Fecha = fechaInicio.Add(periodo * i);
                    payment.SaldoInicial = saldoInicial;
                    payment.Interes = saldoInicial * tasaPeriodica;
                    payment.Capital = pagoPeriodico - payment.Interes;
                    payment.Iva = ivaExento ? 0 : payment.Interes * iva;
                    payment.SaldoFinal = saldoInicial - payment.Capital;
                    schedule.Add(payment);

                    saldoInicial = payment.SaldoFinal;
                }
            }
            else if (subMetodoCalculo.ToLower() == "insolutos")
            {
                decimal amortizacionCapital = monto / numPagos;

                for (int i = 0; i < numPagos; i++)
                {
                    AmortizacionDto payment = new AmortizacionDto();
                    payment.Fecha = fechaInicio.Add(periodo * i);
                    payment.SaldoInicial = saldoInicial;
                    payment.Interes = saldoInicial * tasaPeriodica;
                    payment.Capital = amortizacionCapital;
                    payment.Iva = ivaExento ? 0 : payment.Interes * iva;
                    payment.SaldoFinal = saldoInicial - payment.Capital;
                    schedule.Add(payment);

                    saldoInicial = payment.SaldoFinal;
                }
            }
            else
            {
                throw new ArgumentException("Sub método de cálculo no válido");
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

        private TimeSpan GetPeriodSpan(string periodicidad)
        {
            switch (periodicidad.ToLower())
            {
                case "mensual":
                    return TimeSpan.FromDays(30);
                case "anual":
                    return TimeSpan.FromDays(365);
                case "quincenal":
                    return TimeSpan.FromDays(15);
                default:
                    throw new ArgumentException("Periodicidad no válida");
            }
        }
    }
}
