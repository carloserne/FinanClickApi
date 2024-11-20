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
        [AllowAnonymous] // Permite el acceso sin autenticación
        public async Task<ActionResult<IEnumerable<object>>> GetPlanes()
        {
            // Elimina la verificación del usuario actual ya que no se requiere autenticación aquí
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

        [HttpGet("{idPlan}")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> GetPlanById(int idPlan)
        {
            var plan = await _baseDatos.PlanEmpresas
                .Where(c => c.IdPlan == idPlan && c.Estatus != 0)
                .Select(c => new
                {
                    c.IdPlan,
                    c.Precio,
                    c.Descripcion,
                    c.Duracion,
                    c.Estatus
                })
                .FirstOrDefaultAsync();

            if (plan == null)
            {
                return NotFound("Plan no encontrado");
            }

            return Ok(plan);
        }


    }
}
