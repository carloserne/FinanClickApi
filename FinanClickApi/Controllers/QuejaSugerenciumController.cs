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
    public class QuejaSugerenciumController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;

        public QuejaSugerenciumController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuejaSugerencium>>> GetQuejasSugerencias()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            return await _baseDatos.QuejaSugerencia
                .Where(q => q.Estatus != 0)
                .ToListAsync();
        }

        // GET: api/QuejaSugerencium/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuejaSugerencium>> GetQuejaSugerencium(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            var quejaSugerencium = await _baseDatos.QuejaSugerencia
                .Where(q => q.IdQuejaSugerencia == id && q.Estatus != 0)
                .FirstOrDefaultAsync();

            if (quejaSugerencium == null)
            {
                return NotFound();
            }

            return quejaSugerencium;
        }

        // POST: api/QuejaSugerencium
        [HttpPost]
        public async Task<ActionResult<QuejaSugerencium>> PostQuejaSugerencium(QuejaSugerencium quejaSugerencium)
        {
            _baseDatos.QuejaSugerencia.Add(quejaSugerencium);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuejaSugerencium), new { id = quejaSugerencium.IdQuejaSugerencia }, quejaSugerencium);
        }

        // PUT: api/QuejaSugerencium/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuejaSugerencium(int id, QuejaSugerencium quejaSugerencium)
        {
            /*if (id != quejaSugerencium.IdQuejaSugerencia)
            {
                return BadRequest();
            }*/
            quejaSugerencium.IdQuejaSugerencia = id;

            _baseDatos.Entry(quejaSugerencium).State = EntityState.Modified;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuejaSugerenciumExists(id))
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

        // DELETE: api/QuejaSugerencium/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuejaSugerencium(int id)
        {
            var quejaSugerencium = await _baseDatos.QuejaSugerencia.FindAsync(id);
            if (quejaSugerencium == null)
            {
                return NotFound();
            }
            quejaSugerencium.Estatus = 0;

            _baseDatos.Entry(quejaSugerencium).State = EntityState.Modified;

            //_baseDatos.QuejaSugerencia.Remove(quejaSugerencium);
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        private bool QuejaSugerenciumExists(int id)
        {
            return _baseDatos.QuejaSugerencia.Any(q => q.IdQuejaSugerencia == id);
        }
    }
}
