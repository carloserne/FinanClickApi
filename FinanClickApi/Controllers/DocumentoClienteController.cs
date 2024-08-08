using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using FinanClickApi.Dtos;
using System.Security.Claims;

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
        public async Task<IActionResult> AsignarDocumentos([FromBody] DocumentoClienteDto request)
        {
            // Verificar si el cliente existe
            var cliente = await _baseDatos.Clientes.FindAsync(request.IdCliente);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            // Asignar nuevos documentos al cliente

            var documento = await _baseDatos.CatalogoDocumentos.FindAsync(request.IdDocumento);
            if (documento == null)
            {
                    return NotFound($"Documento con ID {request.IdDocumento} no encontrado");
            }

            var documentoCliente = new DocumentosCliente
            {
                    DocumentoBase64 = " ",
                    Estatus = 4,
                    IdDocumento = request.IdDocumento,
                    IdCliente = request.IdCliente
            };

                _baseDatos.DocumentosClientes.Add(documentoCliente);

            await _baseDatos.SaveChangesAsync();

            return Ok("Documentos asignados correctamente");
        }

        [HttpPost("desasignar")]
        public async Task<IActionResult> DesAsignarDocumentos([FromBody] DocumentoClienteDto request)
        {

            var cliente = await _baseDatos.Clientes.FindAsync(request.IdCliente);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            var documentosExistentes = _baseDatos.DocumentosClientes
               .Where(dc => dc.IdCliente == request.IdCliente)
               .Where(dc => dc.IdDocumento == request.IdDocumento);


            _baseDatos.DocumentosClientes.RemoveRange(documentosExistentes);
             
            await _baseDatos.SaveChangesAsync();

            return Ok("Documentos eliminados correctamente");

        }


        [HttpGet("cliente/{idCliente}")]
        public async Task<IActionResult> ObtenerDocumentosPorCliente(int idCliente)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            var documentos = await _baseDatos.DocumentosClientes
                .Where(d => d.IdCliente == idCliente && d.IdClienteNavigation.IdEmpresa == user.IdEmpresa)
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
