using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanClickApi.Controllers;
using System;
using FinanClickApi.Dtos;
using System.Security.Claims;

namespace FinanClickApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class PagosController : Controller
    {

        private readonly FinanclickDbContext _baseDatos;

        public PagosController(FinanclickDbContext context)
        {
            _baseDatos = context;
        }


        [HttpGet("PagosCredito/{id}")]
        public async Task<IActionResult> GetAllPagos(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            var pagos = await _baseDatos.Pagos
                 .Where(c => c.Estatus != 0 && c.IdCredito == id && c.IdCreditoNavigation.IdProductoNavigation.IdEmpresa ==user.IdEmpresa)
                .ToListAsync();
            return Ok(pagos);
        }


        // Eliminación lógica de un pago
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _baseDatos.Pagos.FindAsync(id);
            if (pago == null || pago.Estatus == 0)
            {
                return NotFound();
            }

            pago.Estatus = 0; // Estatus eliminado lógicamente
            _baseDatos.Entry(pago).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("registrarPago")]
        public async Task<IActionResult> RegistrarPago([FromBody] RegistrarPagoDto pagoDto)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            var credito = await _baseDatos.Creditos
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(c => c.IdCredito == pagoDto.IdCredito && c.Estatus != 0 && c.IdProductoNavigation.IdEmpresa == user.IdEmpresa);

            if (credito == null)
            {
                return NotFound("El crédito no existe o no está activo.");
            }

            var pago = new Pago
            {
                IdCredito = pagoDto.IdCredito,
                FechaPago = DateOnly.FromDateTime(DateTime.Now),
                MontoPago = pagoDto.MontoPago,
                FechaAplicacion = DateOnly.FromDateTime(DateTime.Now),
                Estatus = 1
            };

            _baseDatos.Pagos.Add(pago);
            await _baseDatos.SaveChangesAsync();

            return Ok("Pago registrado correctamente.");
        }



        [HttpPost("aplicarPago/{id}")]
        public async Task<IActionResult> AplicarPago(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            var pago = await _baseDatos.Pagos
                .Include(c => c.IdCreditoNavigation)
                .FirstOrDefaultAsync(c => c.IdPago == id && c.Estatus == 1 && c.IdCreditoNavigation.IdProductoNavigation.IdEmpresa == user.IdEmpresa);

            if (pago == null)
            {
                return NotFound("El pago no se encuentra.");
            }

            var credito = await _baseDatos.Creditos
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(c => c.IdCredito == pago.IdCredito && c.Estatus != 0);

            if (credito == null)
            {
                return NotFound("El crédito no existe o no está activo.");
            }

            var producto = credito.IdProductoNavigation;
            var amortizaciones = await _baseDatos.Amortizacions
                .Where(a => a.IdCredito == credito.IdCredito && (a.Estatus == 2 || a.Estatus == 3))
                .OrderBy(a => a.FechaFin)
                .ToListAsync();

            decimal montoPagoRestante = pago.MontoPago;

            if (producto.AplicacionDePagos == "ordenPrelacion")
            {
                foreach (var amortizacion in amortizaciones)
                {
                    if (montoPagoRestante <= 0) break;

                    if (amortizacion.InteresMoratorio > 0)
                    {
                        var pagoInteresMoratorio = Math.Min(amortizacion.InteresMoratorio, montoPagoRestante);
                        amortizacion.InteresMoratorio -= pagoInteresMoratorio;
                        montoPagoRestante -= pagoInteresMoratorio;
                    }
                }

                foreach (var amortizacion in amortizaciones)
                {
                    if (montoPagoRestante <= 0) break;

                    if (amortizacion.InteresMasIva > 0)
                    {
                        var pagoInteresMasIva = Math.Min(amortizacion.InteresMasIva, montoPagoRestante);
                        amortizacion.InteresMasIva -= pagoInteresMasIva;
                        montoPagoRestante -= pagoInteresMasIva;
                    }
                }

                foreach (var amortizacion in amortizaciones)
                {
                    if (montoPagoRestante <= 0) break;

                    if (amortizacion.Capital > 0)
                    {
                        var pagoCapital = Math.Min(amortizacion.Capital, montoPagoRestante);
                        amortizacion.Capital -= pagoCapital;
                        montoPagoRestante -= pagoCapital;
                    }
                }
            }
            else if (producto.AplicacionDePagos == "vencimientoVigencias")
            {
                foreach (var amortizacion in amortizaciones)
                {
                    if (montoPagoRestante <= 0) break;

                    if (amortizacion.InteresMoratorio > 0)
                    {
                        var pagoInteresMoratorio = Math.Min(amortizacion.InteresMoratorio, montoPagoRestante);
                        amortizacion.InteresMoratorio -= pagoInteresMoratorio;
                        montoPagoRestante -= pagoInteresMoratorio;
                    }

                    if (montoPagoRestante <= 0) break;

                    if (amortizacion.InteresMasIva > 0)
                    {
                        var pagoInteresMasIva = Math.Min(amortizacion.InteresMasIva, montoPagoRestante);
                        amortizacion.InteresMasIva -= pagoInteresMasIva;
                        montoPagoRestante -= pagoInteresMasIva;
                    }

                    if (montoPagoRestante <= 0) break;

                    if (amortizacion.Capital > 0)
                    {
                        var pagoCapital = Math.Min(amortizacion.Capital, montoPagoRestante);
                        amortizacion.Capital -= pagoCapital;
                        montoPagoRestante -= pagoCapital;
                    }


                }
            }

            foreach (var amortizacion in amortizaciones)
            {
                if (amortizacion.Capital == 0 && amortizacion.InteresMasIva == 0 && amortizacion.InteresMoratorio == 0)
                {



                    amortizacion.Estatus = 4;
                }
            }

            if (montoPagoRestante > 0)
            {
                await ProcesarPagoAnticipado(credito, montoPagoRestante);
            }

            pago.Estatus = 2;

            await _baseDatos.SaveChangesAsync();

            return Ok("Pago aplicado correctamente.");
        }

        private async Task ProcesarPagoAnticipado(Credito credito, decimal montoPagoRestante)
        {
            var subMetodoCalculo = credito.IdProductoNavigation.SubMetodo;
            var pagoAnticipado = credito.IdProductoNavigation.PagoAnticipado;

            decimal tasaPeriodica = GetPeriodicInterestRate(credito.InteresOrdinario, credito.Periodicidad);
            decimal tasaDeflactada = tasaPeriodica * 1.16m;

            var primeraAmortizacion = await _baseDatos.Amortizacions
    .Where(a => a.IdCredito == credito.IdCredito)
    .FirstOrDefaultAsync();

            var amortizaciones = await _baseDatos.Amortizacions
                 .Where(a => a.IdCredito == credito.IdCredito && (a.Estatus == 1))
                  .OrderByDescending(a => a.FechaFin)
                 .ToListAsync();

            int cantidadAmortizaciones = await _baseDatos.Amortizacions
               .Where(a => a.IdCredito == credito.IdCredito && (a.Estatus == 1))
               .CountAsync();

            if (subMetodoCalculo == "insolutos")
            {
                if (pagoAnticipado == "reduccionPlazo")
                {
                    foreach(var amor in amortizaciones)
                    {
                        if (montoPagoRestante <= 0) break;

                        if (montoPagoRestante > 0) {
                            amor.SaldoInsoluto = amor.Capital;
                            var pagoAmortizacion = Math.Min(amor.SaldoInsoluto, montoPagoRestante);
                            amor.SaldoInsoluto -= pagoAmortizacion;
                            amor.Capital -= pagoAmortizacion;
                            montoPagoRestante -= pagoAmortizacion;

                            amor.InteresOrdinario = amor.SaldoInsoluto * tasaPeriodica;
                            amor.Iva = amor.InteresOrdinario * (credito.Iva / 100);
                            amor.InteresMasIva = amor.InteresOrdinario + amor.Iva;

                        }

                        if(amor.Capital == 0)
                        {
                            amor.Estatus = 4;
                        }

                    }
                }
                if (pagoAnticipado == "reduccionPago")
                {

                    var pagoAmortizacion = montoPagoRestante / amortizaciones.Count;

                    foreach (var amor in amortizaciones)
                    {
                        if (montoPagoRestante <= 0) break;
                        if (montoPagoRestante > 0)
                        {
                            amor.SaldoInsoluto -= pagoAmortizacion;
                            amor.Capital -= pagoAmortizacion;
                            montoPagoRestante -= pagoAmortizacion;

                            amor.InteresOrdinario = amor.SaldoInsoluto * tasaPeriodica;
                            amor.Iva = amor.InteresOrdinario * (credito.Iva / 100);
                            amor.InteresMasIva = amor.InteresOrdinario + amor.Iva;

                        }
                        if (amor.Capital == 0)
                        {
                            amor.Estatus = 4;
                        }

                    }
                }

            }
            else
            {


                if (pagoAnticipado == "reduccionPlazo")
                {
                    foreach (var amor in amortizaciones)
                    {
                        if (montoPagoRestante <= 0) break;

                        if (montoPagoRestante > 0)
                        {
                            amor.SaldoInsoluto = amor.Capital;
                            var pagoAmortizacion = Math.Min(amor.SaldoInsoluto, montoPagoRestante);
                            amor.SaldoInsoluto -= pagoAmortizacion;
                            primeraAmortizacion.SaldoInsoluto -= pagoAmortizacion;
                            amor.Capital -= pagoAmortizacion;
                            montoPagoRestante -= pagoAmortizacion;

                            amor.InteresOrdinario = primeraAmortizacion.SaldoInsoluto * tasaPeriodica;
                            amor.Iva = amor.InteresOrdinario * (credito.Iva / 100);
                            amor.InteresMasIva = amor.InteresOrdinario + amor.Iva;

                        }

                        if (amor.Capital == 0)
                        {
                            amor.Estatus = 4;
                        }

                    }
                }
                if (pagoAnticipado == "reduccionPago")
                {

                    var pagoAmortizacion = montoPagoRestante / amortizaciones.Count;

                    foreach (var amor in amortizaciones)
                    {
                        if (montoPagoRestante <= 0) break;
                        if (montoPagoRestante > 0)
                        {
                            amor.SaldoInsoluto -= pagoAmortizacion;
                            amor.Capital -= pagoAmortizacion;
                            primeraAmortizacion.SaldoInsoluto -= pagoAmortizacion;
                            montoPagoRestante -= pagoAmortizacion;

                            amor.InteresOrdinario = primeraAmortizacion.SaldoInsoluto * tasaPeriodica;
                            amor.Iva = amor.InteresOrdinario * (credito.Iva / 100);
                            amor.InteresMasIva = amor.InteresOrdinario + amor.Iva;

                        }
                        if (amor.Capital == 0)
                        {
                            amor.Estatus = 4;
                        }

                    }
                }

            }
            await _baseDatos.SaveChangesAsync();


        }

        [HttpGet("PagosRecibidosUltimoMes/{empresaId}")]
        public async Task<IActionResult> GetPagosRecibidosUltimoMes(int empresaId)
        {
            var fechaInicio = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
            var fechaActual = DateOnly.FromDateTime(DateTime.Now);

            var totalMontoPagos = await _baseDatos.Pagos
                .Where(p => p.FechaPago >= fechaInicio && p.FechaPago <= fechaActual)
                .Join(_baseDatos.Creditos,
                    pago => pago.IdCredito,
                    credito => credito.IdCredito,
                    (pago, credito) => new { pago, credito })
                .Join(_baseDatos.Productos,
                    pc => pc.credito.IdProducto,
                    producto => producto.IdProducto,
                    (pc, producto) => new { pc.pago, pc.credito, producto })
                .Where(pcp => pcp.producto.IdEmpresa == empresaId)
                .SumAsync(pcp => pcp.pago.MontoPago);

            return Ok(totalMontoPagos);
        }


        private static decimal GetPeriodicInterestRate(decimal interesAnual, string periodicidad)
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

    }
}
