using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanClickApi.Controllers;
using System;
using FinanClickApi.Dtos;

namespace FinanClickApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CreditoController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;

        public CreditoController(FinanclickDbContext context)
        {
            _baseDatos = context;
        }

        // Obtener todos los créditos con estatus 1
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var creditos = await _baseDatos.Creditos
                 .Where(c => c.Estatus != 0)
                .Include(c => c.Avals)
                .ThenInclude(a => a.IdPersonaNavigation)
                .Include(c => c.Avals)
                .ThenInclude(a => a.IdPersonaMoralNavigation)
                .Include(c => c.Obligados)
                .ThenInclude(o => o.IdPersonaNavigation)
                .Include(c => c.Obligados)
                .ThenInclude(o => o.IdPersonaMoralNavigation)
                .ToListAsync();
            return Ok(creditos);
        }

        [HttpGet("amortizaciones/{id}")]
        public async Task<IActionResult> GetAllAmortizaciones(int id)
        {
            var amortizaciones = await _baseDatos.Amortizacions
                .Where(c => c.Estatus != 0 && c.IdCredito == id)
                .ToListAsync();
            return Ok(amortizaciones);
        }

        [HttpGet("pagos/{id}")]
        public async Task<IActionResult> GetAllPagos(int id)
        {
            var pagos = await _baseDatos.Pagos
                 .Where(c => c.Estatus != 0 && c.IdCredito == id)
                .ToListAsync();
            return Ok(pagos);
        }

        // Obtener un crédito por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var credito = await _baseDatos.Creditos
                .Include(c => c.Avals)
                .ThenInclude(a => a.IdPersonaNavigation)
                .Include(c => c.Avals)
                .ThenInclude(a => a.IdPersonaMoralNavigation)
                .Include(c => c.Obligados)
                .ThenInclude(o => o.IdPersonaNavigation)
                .Include(c => c.Obligados)
                .ThenInclude(o => o.IdPersonaMoralNavigation)
                .FirstOrDefaultAsync(c => c.IdCredito == id && c.Estatus != 0);

            if (credito == null)
            {
                return NotFound();
            }

            return Ok(credito);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Credito credito)
        {
            credito.Estatus = 1; // Estatus activo

            // Agregar personas y personas morales relacionadas con los avales
            foreach (var aval in credito.Avals)
            {
                if (aval.IdPersonaNavigation != null)
                {
                    _baseDatos.Personas.Add(aval.IdPersonaNavigation);
                    await _baseDatos.SaveChangesAsync();
                    aval.IdPersona = aval.IdPersonaNavigation.IdPersona;
                    aval.IdPersonaNavigation = null; // Clear the navigation property to avoid recursive save issues
                }
                if (aval.IdPersonaMoralNavigation != null)
                {
                    _baseDatos.PersonaMorals.Add(aval.IdPersonaMoralNavigation);
                    await _baseDatos.SaveChangesAsync();
                    aval.IdPersonaMoral = aval.IdPersonaMoralNavigation.IdPersonaMoral;
                    aval.IdPersonaMoralNavigation = null; // Clear the navigation property to avoid recursive save issues
                }
            }

            // Agregar personas y personas morales relacionadas con los obligados
            foreach (var obligado in credito.Obligados)
            {
                if (obligado.IdPersonaNavigation != null)
                {
                    _baseDatos.Personas.Add(obligado.IdPersonaNavigation);
                    await _baseDatos.SaveChangesAsync();
                    obligado.IdPersona = obligado.IdPersonaNavigation.IdPersona;
                    obligado.IdPersonaNavigation = null; // Clear the navigation property to avoid recursive save issues
                }
                if (obligado.IdPersonaMoralNavigation != null)
                {
                    _baseDatos.PersonaMorals.Add(obligado.IdPersonaMoralNavigation);
                    await _baseDatos.SaveChangesAsync();
                    obligado.IdPersonaMoral = obligado.IdPersonaMoralNavigation.IdPersonaMoral;
                    obligado.IdPersonaMoralNavigation = null; // Clear the navigation property to avoid recursive save issues
                }
            }

            _baseDatos.Creditos.Add(credito);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = credito.IdCredito }, credito);
        }


        // Actualizar un crédito existente con avales y obligados
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Credito credito)
        {
            credito.IdCredito = id;

            var existingCredito = await _baseDatos.Creditos
                 .Include(c => c.Avals)
                .ThenInclude(a => a.IdPersonaNavigation)
                .Include(c => c.Avals)
                .ThenInclude(a => a.IdPersonaMoralNavigation)
                .Include(c => c.Obligados)
                .ThenInclude(o => o.IdPersonaNavigation)
                .Include(c => c.Obligados)
                .ThenInclude(o => o.IdPersonaMoralNavigation)
                .FirstOrDefaultAsync(c => c.IdCredito == id);

            if (existingCredito == null || existingCredito.Estatus == 0)
            {
                return NotFound();
            }

            existingCredito.IdProducto = credito.IdProducto;
            existingCredito.Monto = credito.Monto;
            existingCredito.Iva = credito.Iva;
            existingCredito.Periodicidad = credito.Periodicidad;
            existingCredito.FechaFirma = credito.FechaFirma;
            existingCredito.FechaActivacion = credito.FechaActivacion;
            existingCredito.NumPagos = credito.NumPagos;
            existingCredito.InteresOrdinario = credito.InteresOrdinario;
            existingCredito.IdPromotor = credito.IdPromotor;
            existingCredito.IdCliente = credito.IdCliente;
            existingCredito.InteresMoratorio = credito.InteresMoratorio;

            // Actualizar avales y obligados
            _baseDatos.Avals.RemoveRange(existingCredito.Avals);
            existingCredito.Avals = credito.Avals;

            foreach (var aval in credito.Avals)
            {
                if (aval.IdPersonaNavigation != null)
                {
                    _baseDatos.Personas.Add(aval.IdPersonaNavigation);
                }
                if (aval.IdPersonaMoralNavigation != null)
                {
                    _baseDatos.PersonaMorals.Add(aval.IdPersonaMoralNavigation);
                }
            }

            _baseDatos.Obligados.RemoveRange(existingCredito.Obligados);
            existingCredito.Obligados = credito.Obligados;

            foreach (var obligado in credito.Obligados)
            {
                if (obligado.IdPersonaNavigation != null)
                {
                    _baseDatos.Personas.Add(obligado.IdPersonaNavigation);
                }
                if (obligado.IdPersonaMoralNavigation != null)
                {
                    _baseDatos.PersonaMorals.Add(obligado.IdPersonaMoralNavigation);
                }
            }

            if (credito.FechaActivacion != null)
            {
                await ActivarCredito(id);
            }

            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        // Eliminación lógica de un crédito
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var credito = await _baseDatos.Creditos.FindAsync(id);
            if (credito == null || credito.Estatus == 0)
            {
                return NotFound();
            }

            credito.Estatus = 0; // Estatus eliminado lógicamente
            _baseDatos.Entry(credito).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        public async Task<IActionResult> ActivarCredito(int id)
        {
            var credito = await _baseDatos.Creditos
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(c => c.IdCredito == id && c.Estatus != 0);

            if (credito == null)
            {
                return NotFound();
            }

            var producto = await _baseDatos.Productos.FirstOrDefaultAsync(p => p.IdProducto == credito.IdProducto);

            if (producto == null)
            {
                return BadRequest("El producto asociado al crédito no existe.");
            }

            DateOnly defaultFechaActivacion = DateOnly.FromDateTime(DateTime.Now);

            DateOnly FechaActivacionValidate = credito.FechaActivacion ?? defaultFechaActivacion;

            bool ivaExento = false;

            List<AmortizacionDto> amortizaciones = SimulacionController.CalculateAmortizacionDto(
                producto.MetodoCalculo,
                producto.SubMetodo,
                credito.Periodicidad,
                credito.NumPagos,
                credito.InteresOrdinario,
                credito.Iva,
                ivaExento,
                FechaActivacionValidate,
                credito.Monto
            );

            foreach (var amortizacion in amortizaciones)
            {
                var newAmortizacion = new Amortizacion
                {
                    IdCredito = credito.IdCredito,
                    FechaInicio = amortizacion.FechaInicio,
                    FechaFin = amortizacion.FechaFin,
                    Estatus = amortizacion.FechaInicio > DateOnly.FromDateTime(DateTime.Now) ? 1 : 2, // 1 = Pendiente, 2 = Vencida
                    SaldoInsoluto = amortizacion.SaldoInsoluto,
                    Capital = amortizacion.Capital,
                    InteresOrdinario = amortizacion.Interes,
                    InteresMasIva = amortizacion.InteresMasIva,
                    Iva = amortizacion.IvaSobreInteres,
                    PagoFijo = amortizacion.PagoFijo,
                    InteresMoratorio = 0 // Inicialmente sin interés moratorio
                };

                _baseDatos.Amortizacions.Add(newAmortizacion);
            }

            await _baseDatos.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("actualizarInteres/{id}")]
        public async Task<IActionResult> ActualizarInteresMoratorio(int id)
        {
            var credito = await _baseDatos.Creditos
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(c => c.IdCredito == id && c.Estatus != 0 && c.Estatus != 4);

            if (credito == null)
            {
                return NotFound();
            }

            var producto = await _baseDatos.Productos.FirstOrDefaultAsync(p => p.IdProducto == credito.IdProducto);

            if (producto == null)
            {
                return BadRequest("El producto asociado al crédito no existe.");
            }

            var amortizacionesVencidas = await _baseDatos.Amortizacions
                .Where(a => a.IdCredito == id && a.Estatus == 2 && a.FechaFin < DateOnly.FromDateTime(DateTime.Now))
                .ToListAsync();

            bool tieneMoratorios = false;

            foreach (var amortizacion in amortizacionesVencidas)
            {
                DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);

                DateTime fechaFinDateTime = amortizacion.FechaFin.ToDateTime(TimeOnly.MinValue);
                DateTime fechaActualDateTime = fechaActual.ToDateTime(TimeOnly.MinValue);

                int diasVencidos = (fechaActualDateTime - fechaFinDateTime).Days;
                decimal interesMoratorioDiario = (amortizacion.SaldoInsoluto * producto.InteresMoratorio.Value / 100) / 360;
                decimal interesMoratorioAcumulado = interesMoratorioDiario * diasVencidos;

                amortizacion.InteresMoratorio += interesMoratorioAcumulado;

                if (interesMoratorioAcumulado > 0)
                {
                    amortizacion.Estatus = 3;
                }

            }

            await _baseDatos.SaveChangesAsync();

            return Ok("Intereses moratorios actualizados correctamente.");
        }


        [HttpPost("registrarPago")]
        public async Task<IActionResult> RegistrarPago([FromBody] RegistrarPagoDto pagoDto)
        {
            var credito = await _baseDatos.Creditos
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(c => c.IdCredito == pagoDto.IdCredito && c.Estatus != 0);

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

            var pago = await _baseDatos.Pagos
                .Include(c => c.IdCreditoNavigation)
                .FirstOrDefaultAsync(c => c.IdPago == id && c.Estatus == 1);

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
            
        }


    }
}
