using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanClickApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentoController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;

        public DocumentoController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogoDocumento>>> GetEmpresas()
        {
            return await _baseDatos.CatalogoDocumentos.ToListAsync();
        }

        // GET: api/catalogodocumentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogoDocumento>> GetCatalogoDocumentos(int id)
        {
            var catalogoDocumento = await _baseDatos.CatalogoDocumentos.FindAsync(id);

            if (catalogoDocumento == null)
            {
                return NotFound();
            }

            return catalogoDocumento;
        }


        // GET: api/catalogodocumentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogoDocumento>> GetCatalogoDocumento(int id)
        {
            var catalogoDocumento = await _baseDatos.CatalogoDocumentos.FindAsync(id);

            if (catalogoDocumento == null)
            {
                return NotFound();
            }

            return catalogoDocumento;
        }

        // POST: api/catalogodocumentos
        [HttpPost]
        public async Task<ActionResult<CatalogoDocumento>> PostCatalogoDocumento(CatalogoDocumento catalogoDocumento)
        {
            _baseDatos.CatalogoDocumentos.Add(catalogoDocumento);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCatalogoDocumento), new { id = catalogoDocumento.IdCatalogoDocumento }, catalogoDocumento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogoDocumento(int id, CatalogoDocumento catalogoDocumento)
        {
            catalogoDocumento.IdCatalogoDocumento = id;

            _baseDatos.Entry(catalogoDocumento).State = EntityState.Modified;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoDocumentoExists(id))
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
        public async Task<IActionResult> DeleteCatalogoDocumento(int id)
        {
            var catalogoDocumento = await _baseDatos.CatalogoDocumentos.FindAsync(id);
            if (catalogoDocumento == null)
            {
                return NotFound();
            }

            catalogoDocumento.Estatus = 0;
            _baseDatos.Entry(catalogoDocumento).State = EntityState.Modified;

            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogoDocumentoExists(int id)
        {
            return _baseDatos.CatalogoDocumentos.Any(e => e.IdCatalogoDocumento == id);
        }


    }
}
