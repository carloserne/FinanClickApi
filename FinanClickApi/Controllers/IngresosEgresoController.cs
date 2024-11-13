using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinanClickApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IngresosEgresoController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;

        public IngresosEgresoController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        /*
         Para no hacer dos tablas,el si el TipoTransaccion = 1 es ingreso
         y si TipoTransaccion = 2 es egreso
        */

        //GET INGRESOS
        [HttpGet("ingresos")]
        public async Task<ActionResult<IEnumerable<object>>> GetIngresos()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var ingresos = await _baseDatos.IngresosEgresos
                .Where(c => c.TipoTransaccion == 1 && c.Estatus == 1)
                .Select(c => new
                {
                    c.IdIngresosEgresos,
                    c.Monto,
                    c.TipoTransaccion,
                    c.Fecha,
                    c.Descripcion,
                    c.Categoria
                })
                .ToListAsync();

            return Ok(ingresos);
        }

        //GET EGRESOS
        [HttpGet("egresos")]
        public async Task<ActionResult<IEnumerable<object>>> GetEgresos()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var ingresos = await _baseDatos.IngresosEgresos
                .Where(c => c.TipoTransaccion == 2 && c.Estatus == 1)
                .Select(c => new
                {
                    c.IdIngresosEgresos,
                    c.Monto,
                    c.TipoTransaccion,
                    c.Fecha,
                    c.Descripcion,
                    c.Categoria
                })
                .ToListAsync();

            return Ok(ingresos);
        }

        [HttpGet("IngresosEgresosGetAll")]
        public async Task<ActionResult<IEnumerable<IngresosEgreso>>> GetIngresosEgresos()
        {
            var ingresos = await _baseDatos.IngresosEgresos
                .Where(c => c.Estatus == 1)
                .Select(c => new
                {
                    c.IdIngresosEgresos,
                    c.Monto,
                    c.TipoTransaccion,
                    c.Fecha,
                    c.Descripcion,
                    c.Categoria
                })
                .ToListAsync();

            return Ok(ingresos);
        }

        //POST INGRESOS
        /*
         NOTA: EL MANEJO DEL TIPO DE TRANSACCION SE DEBE DE MANEJAR DEL LADO DEL FRONTEND
         */
        [HttpPost("ingresos")]
        public async Task<ActionResult<IngresosEgreso>> CrearIngreso(IngresosEgreso ingresoEgreso, int idVentaProspecto)
        {
            // Guardar el nuevo ingreso
            _baseDatos.IngresosEgresos.Add(ingresoEgreso);
            await _baseDatos.SaveChangesAsync();

            // Actualizar VentaProspecto con el ID del ingreso creado
            var venta = await _baseDatos.VentaProspectos.FindAsync(idVentaProspecto);
            if (venta != null)
            {
                venta.IdIngresoEgreso = ingresoEgreso.IdIngresosEgresos;
                await _baseDatos.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetIngresosEgresos), new { id = ingresoEgreso.IdIngresosEgresos }, ingresoEgreso);
        }


        //POST EGRESOS
        [HttpPost("egresos")]
        public async Task<ActionResult<IngresosEgreso>> CrearEgreso(IngresosEgreso ingresoEgreso)
        {
            _baseDatos.IngresosEgresos.Add(ingresoEgreso);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIngresosEgresos), new { id = ingresoEgreso.IdIngresosEgresos }, ingresoEgreso);
        }

        //PUT INGRESO
        [HttpPut("ingresos/{id}")]
        public async Task<IActionResult> ModificarIngreso(int id, IngresosEgreso ingresoEgreso)
        {
            ingresoEgreso.IdIngresosEgresos = id;
            _baseDatos.Entry(ingresoEgreso).State = EntityState.Modified;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch
            {
                if (!IngresoEgresosExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //PUT EGRESO
        [HttpPut("egresos/{id}")]
        public async Task<IActionResult> ModificarEgreso(int id, IngresosEgreso ingresoEgreso)
        {
            ingresoEgreso.IdIngresosEgresos = id;
            _baseDatos.Entry(ingresoEgreso).State = EntityState.Modified;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch
            {
                if (!IngresoEgresosExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //DELETE Ingresos
        [HttpDelete("ingresos/{id}")]
        public async Task<IActionResult> EliminarIngreso(int id)
        {
            var ingreso = await _baseDatos.IngresosEgresos.FindAsync(id);
            if (ingreso == null)
            {
                return NotFound();
            }

            ingreso.Estatus = 0;

            _baseDatos.Entry(ingreso).State = EntityState.Modified;

            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        //DELETE Ingresos
        [HttpDelete("egresos/{id}")]
        public async Task<IActionResult> EliminarEgreso(int id)
        {
            var ingreso = await _baseDatos.IngresosEgresos.FindAsync(id);
            if (ingreso == null)
            {
                return NotFound();
            }

            ingreso.Estatus = 0;

            _baseDatos.Entry(ingreso).State = EntityState.Modified;

            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        //Metodos para las vistas del dashboard
        // GET Totales Mensuales
        [HttpGet("totales-mensuales")]
        public async Task<ActionResult<IEnumerable<TotalesMensuales>>> GetTotalesMensuales()
        {
            var result = await _baseDatos.TotalesMensuales.ToListAsync();
            return Ok(result);
        }

        // GET: api/IngresosEgreso/acumulado-anual
        [HttpGet("acumulado-anual")]
        public async Task<ActionResult<IEnumerable<AcumuladoAnual>>> GetAcumuladoAnual()
        {
            var result = await _baseDatos.AcumuladoAnual.ToListAsync();
            return Ok(result);
        }

        private bool IngresoEgresosExist(int id)
        {
            return _baseDatos.IngresosEgresos.Any(i => i.IdIngresosEgresos == id);
        }
    }
}
