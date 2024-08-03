using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanClickApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CreditoController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;

        public CreditoController(FinanclickDbContext context)
        {
            _baseDatos = context;
        }

        // Obtener todos los créditos con estatus diferente a 0
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
            existingCredito.Estatus = credito.Estatus;
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
    }
}
