using FinanClickApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanClickApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatConceptoController : Controller
    {
        private readonly FinanclickDbContext _baseDatos;

        public CatConceptoController(FinanclickDbContext context)
        {
            _baseDatos = context;
        }

        // Obtener todos los conceptos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conceptos = await _baseDatos.CatConceptos.Where(c => c.Estatus != 0).ToListAsync();
            return Ok(conceptos);
        }

        // Obtener un concepto por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var concepto = await _baseDatos.CatConceptos.FindAsync(id);
            if (concepto == null || concepto.Estatus == 0)
            {
                return NotFound();
            }
            return Ok(concepto);
        }

        // Crear un nuevo concepto
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CatConcepto concepto)
        {
            concepto.Estatus = 1; // Estatus activo
            _baseDatos.CatConceptos.Add(concepto);
            await _baseDatos.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = concepto.IdConcepto }, concepto);
        }

        // Actualizar un concepto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CatConcepto concepto)
        {
            concepto.IdConcepto = id;
            
            var existingConcepto = await _baseDatos.CatConceptos.FindAsync(id);
            if (existingConcepto == null || existingConcepto.Estatus == 0)
            {
                return NotFound();
            }

            existingConcepto.NombreConcepto = concepto.NombreConcepto;
            existingConcepto.Valor = concepto.Valor;
            existingConcepto.TipoValor = concepto.TipoValor;
            existingConcepto.Iva = concepto.Iva;
            existingConcepto.IdEmpresa = concepto.IdEmpresa;

            _baseDatos.Entry(existingConcepto).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        // Eliminación lógica de un concepto
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var concepto = await _baseDatos.CatConceptos.FindAsync(id);
            if (concepto == null || concepto.Estatus == 0)
            {
                return NotFound();
            }

            concepto.Estatus = 0; // Estatus eliminado lógicamente
            _baseDatos.Entry(concepto).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }
    }
}
