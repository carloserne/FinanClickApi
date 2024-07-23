using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanClickApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;

        public EmpresaController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        // GET: api/empresa
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            return await _baseDatos.Empresas.ToListAsync();
        }

        // GET: api/empresa/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _baseDatos.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return empresa;
        }

        // POST: api/empresa
        [HttpPost]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            _baseDatos.Empresas.Add(empresa);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.IdEmpresa }, empresa);
        }

        // PUT: api/empresa/5
        [HttpPut("{id}")]

        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.IdEmpresa)
            {
                return BadRequest();
            }

            _baseDatos.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
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

        // DELETE: api/empresa/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _baseDatos.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            _baseDatos.Empresas.Remove(empresa);
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpresaExists(int id)
        {
            return _baseDatos.Empresas.Any(e => e.IdEmpresa == id);
        }
    }
}
