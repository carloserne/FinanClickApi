using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using FinanClickApi.Dtos;
using System.Security.Claims;
using System.Net.NetworkInformation;
using Azure.Core;

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

        [HttpPost("asignar-todos-fisica/{idCliente}")]
        public async Task<IActionResult> AsignarTodosDocumentosFisica(int idCliente)
        {
            var cliente = await _baseDatos.Clientes.FindAsync(idCliente);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            var documentosFisica = await _baseDatos.CatalogoDocumentos
                .Where(d => d.Tipo == "FISICA")
                .ToListAsync();

            // Crear asignaciones para cada documento de tipo "FISICA"
            foreach (var documento in documentosFisica)
            {
                var documentoCliente = new DocumentosCliente
                {
                    IdCliente = idCliente,
                    IdDocumento = documento.IdCatalogoDocumento,
                    DocumentoBase64 = " ",
                    Estatus = 4 // Estado "Pendiente"
                };
                _baseDatos.DocumentosClientes.Add(documentoCliente);
            }

            await _baseDatos.SaveChangesAsync();
            return Ok(new { message = "Documentos asignados correctamente" });
        }

        [HttpPost("asignar-todos-moral/{idCliente}")]
        public async Task<IActionResult> AsignarTodosDocumentosMoral(int idCliente)
        {
            var cliente = await _baseDatos.Clientes.FindAsync(idCliente);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            // Obtener todos los documentos de tipo "MORAL"
            var documentosMoral = await _baseDatos.CatalogoDocumentos
                .Where(d => d.Tipo == "MORAL")
                .ToListAsync();

            // Crear asignaciones para cada documento de tipo "MORAL"
            foreach (var documento in documentosMoral)
            {
                var documentoCliente = new DocumentosCliente
                {
                    IdCliente = idCliente,
                    IdDocumento = documento.IdCatalogoDocumento,
                    DocumentoBase64 = " ",
                    Estatus = 4 // Estado "Pendiente"
                };
                _baseDatos.DocumentosClientes.Add(documentoCliente);
            }

            await _baseDatos.SaveChangesAsync();
            return Ok(new { message = "Documentos asignados correctamente" });
        }

        [HttpPost("asignar-a-todos/{tipo}")]
        public async Task<IActionResult> AsignarDocumentoATodosClientes(string tipo, [FromBody] int idDocumento)
        {
            // Obtener el IdEmpresa del usuario actual
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));
            if (user == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            var idEmpresaUsuario = user.IdEmpresa;

            // Filtrar clientes por IdEmpresa y RegimenFiscal
            var clientes = await _baseDatos.Clientes
                .Where(c => c.RegimenFiscal == tipo && c.IdEmpresa == idEmpresaUsuario)
                .ToListAsync();

            // Asignar el documento a cada cliente con estatus "Pendiente" y DocumentoBase64 vacío
            foreach (var cliente in clientes)
            {
                var documentoCliente = new DocumentosCliente
                {
                    IdCliente = cliente.IdCliente,
                    IdDocumento = idDocumento,
                    DocumentoBase64 = "", // Cadena vacía para el documento
                    Estatus = 4 // "Pendiente"
                };
                _baseDatos.DocumentosClientes.Add(documentoCliente);
            }

            await _baseDatos.SaveChangesAsync();
            return Ok("Documento asignado a todos los clientes del tipo especificado y empresa del usuario.");
        }

    }
}
