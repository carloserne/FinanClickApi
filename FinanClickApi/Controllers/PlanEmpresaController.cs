using FinanClickApi.Dtos;
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
    public class PlanEmpresaController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;

        public PlanEmpresaController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        //// GET: api/planempresa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPlanes()
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

            var planes = await _baseDatos.PlanEmpresas
                .Where(c => c.Estatus != 0)
                .Select(c => new
                {
                    c.IdPlan,
                    c.Precio,
                    c.Descripcion,
                    c.Duracion,
                    c.Estatus
                })
                .ToListAsync();

            return Ok(planes);
        }

    }
}
