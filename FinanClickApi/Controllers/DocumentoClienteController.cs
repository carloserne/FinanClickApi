using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using FinanClickApi.Dtos;

namespace FinanClickApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentoClienteController : Controller
    {
        private readonly FinanclickDbContext _baseDatos;

        public DocumentoClienteController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarDocumento([FromBody] DocumentoClienteDto request)
        {
            var documento = await _baseDatos.CatalogoDocumentos.FindAsync(request.IdDocumento);
            if (documento == null)
            {
                return NotFound("Documento no encontrado");
            }

            var cliente = await _baseDatos.Clientes.FindAsync(request.IdCliente);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            var documentoCliente = new DocumentosCliente
            {
                DocumentoBase64 = " ",
                Estatus = 4, // Pendiente
                IdDocumento = request.IdDocumento,
                IdCliente = request.IdCliente
            };

            _baseDatos.DocumentosClientes.Add(documentoCliente);
            await _baseDatos.SaveChangesAsync();

            return Ok(documentoCliente);
        }

        [HttpGet("cliente/{idCliente}")]
        public async Task<IActionResult> ObtenerDocumentosPorCliente(int idCliente)
        {
            var documentos = await _baseDatos.DocumentosClientes
                .Where(d => d.IdCliente == idCliente)
                .ToListAsync();

            return Ok(documentos);
        }

        [HttpPost("subir")]
        public async Task<IActionResult> SubirDocumento([FromBody] DocumentoClienteReq request)
        {
            var documentoCliente = await _baseDatos.DocumentosClientes.FindAsync(request.IdDocumentoCliente);
            if (documentoCliente == null)
            {
                return NotFound("Documento del cliente no encontrado");
            }

            documentoCliente.DocumentoBase64 = request.DocumentoBase64;
            documentoCliente.Estatus = request.Estatus;

            _baseDatos.DocumentosClientes.Update(documentoCliente);
            await _baseDatos.SaveChangesAsync();

            return Ok(documentoCliente);
        }

        [HttpPut("cambiar-estatus/{idDocumentoCliente}/{estatus}")]
        public async Task<IActionResult> CambiarEstatusDocumento(int idDocumentoCliente, int estatus)
        {
            var documentoCliente = await _baseDatos.DocumentosClientes.FindAsync(idDocumentoCliente);
            if (documentoCliente == null)
            {
                return NotFound("Documento del cliente no encontrado");
            }

            documentoCliente.Estatus = estatus;
            _baseDatos.DocumentosClientes.Update(documentoCliente);
            await _baseDatos.SaveChangesAsync();

            return Ok(documentoCliente);
        }

    }
}
