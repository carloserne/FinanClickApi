using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanClickApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ActividadController : ControllerBase
{
    private readonly FinanclickDbContext _baseDatos;

    public ActividadController(FinanclickDbContext baseDatos)
    {
        _baseDatos = baseDatos;
    }

    // GET: api/actividad
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Actividad>>> GetActividads()
    {
        var Actividads = await _baseDatos.Actividads.ToListAsync();
        return Actividads;
    }

    // GET: api/actividad/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Actividad>> GetActividad(int id)
    {
        var actividad = await _baseDatos.Actividads.FindAsync(id);

        if (actividad == null)
        {
            return NotFound();
        }

        return actividad;
    }

    // GET: api/actividad/usuario/5
    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<IEnumerable<Actividad>>> GetActividadsByUsuario(int idUsuario)
    {
        var Actividads = await _baseDatos.Actividads
            .Where(a => a.IdUsuario == idUsuario)
            .ToListAsync();

        if (Actividads == null || !Actividads.Any())
        {
            return NotFound();
        }

        return Actividads;
    }

    // POST: api/actividad
    [HttpPost]
    public async Task<ActionResult<Actividad>> PostActividad(Actividad actividad)
    {
        actividad.FechaCreacion = DateTime.Now;
        actividad.FechaActualizacion = DateTime.Now;
        _baseDatos.Actividads.Add(actividad);
        await _baseDatos.SaveChangesAsync();

        return CreatedAtAction(nameof(GetActividad), new { id = actividad.IdActividad }, actividad);
    }

    // PUT: api/actividad/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActividad(int id, Actividad actividad)
    {
        if (id != actividad.IdActividad)
        {
            return BadRequest();
        }
        actividad.FechaActualizacion = DateTime.Now;
        _baseDatos.Entry(actividad).State = EntityState.Modified;

        try
        {
            await _baseDatos.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActividadExists(id))
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

    // DELETE: api/actividad/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActividad(int id)
    {
        var actividad = await _baseDatos.Actividads.FindAsync(id);
        if (actividad == null)
        {
            return NotFound();
        }

        _baseDatos.Actividads.Remove(actividad);
        await _baseDatos.SaveChangesAsync();

        return NoContent();
    }

    private bool ActividadExists(int id)
    {
        return _baseDatos.Actividads.Any(a => a.IdActividad == id);
    }
}
