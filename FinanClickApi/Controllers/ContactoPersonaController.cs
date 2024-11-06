using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanClickApi.Models;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ContactoPersonaController : ControllerBase
{
    private readonly FinanclickDbContext _baseDatos;

    public ContactoPersonaController(FinanclickDbContext baseDatos)
    {
        _baseDatos = baseDatos;
    }

    // GET: api/contactopersona
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactoPersona>>> GetContactos()
    {
        return await _baseDatos.ContactoPersonas.ToListAsync();
    }

    // GET: api/contactopersona/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactoPersona>> GetContactoPersona(int id)
    {
        var contacto = await _baseDatos.ContactoPersonas.FindAsync(id);

        if (contacto == null)
        {
            return NotFound();
        }

        return contacto;
    }

    // GET: api/contactopersona/empresa/5
    [HttpGet("empresa/{idEmpresa}")]
    public async Task<ActionResult<IEnumerable<ContactoPersona>>> GetContactosByEmpresa(int idEmpresa)
    {
        var contactos = await _baseDatos.ContactoPersonas
            .Where(c => c.IdEmpresa == idEmpresa)
            .ToListAsync();

        if (contactos == null || !contactos.Any())
        {
            return NotFound();
        }

        return contactos;
    }

    // POST: api/contactopersona
    [HttpPost]
    public async Task<ActionResult<ContactoPersona>> PostContactoPersona(ContactoPersona contacto)
    {
        _baseDatos.ContactoPersonas.Add(contacto);
        await _baseDatos.SaveChangesAsync();

        return CreatedAtAction(nameof(GetContactoPersona), new { id = contacto.IdContacto }, contacto);
    }

    // PUT: api/contactopersona/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutContactoPersona(int id, ContactoPersona contacto)
    {
        if (id != contacto.IdContacto)
        {
            return BadRequest();
        }

        _baseDatos.Entry(contacto).State = EntityState.Modified;

        try
        {
            await _baseDatos.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContactoPersonaExists(id))
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

    // DELETE: api/contactopersona/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactoPersona(int id)
    {
        var contacto = await _baseDatos.ContactoPersonas.FindAsync(id);
        if (contacto == null)
        {
            return NotFound();
        }

        _baseDatos.ContactoPersonas.Remove(contacto);
        await _baseDatos.SaveChangesAsync();

        return NoContent();
    }

    private bool ContactoPersonaExists(int id)
    {
        return _baseDatos.ContactoPersonas.Any(c => c.IdContacto == id);
    }
}
