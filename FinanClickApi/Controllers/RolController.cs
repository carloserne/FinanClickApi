using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinanClickApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RolController : Controller
    {
        private readonly FinanclickDbContext _baseDatos;

        public RolController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRols()
        {

            return await _baseDatos.Rols.Where(r => r.Estatus != false).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));
            var rol = await _baseDatos.Rols
                .Where(r => r.Estatus && r.Usuarios.Any(u => u.IdEmpresa == user.IdEmpresa))
                .FirstOrDefaultAsync(r => r.IdRol == id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            rol.Estatus = true;

            _baseDatos.Rols.Add(rol);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRol), new { id = rol.IdRol }, rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Rol rol)
        {
            rol.IdRol = id;

            _baseDatos.Entry(rol).State = EntityState.Modified;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _baseDatos.Rols.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            rol.Estatus = false;
            _baseDatos.Entry(rol).State = EntityState.Modified;

            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(int id)
        {
            return _baseDatos.Rols.Any(e => e.IdRol == id);
        }
    }
}

