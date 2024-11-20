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
    public class VentasProspectoController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;

        public VentasProspectoController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetVentas()
        {
            // Obtener el ID del usuario actual
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            // Verificar si el usuario existe en la base de datos
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Obtener las ventas desde la base de datos
            var ventas = await _baseDatos.VentaProspectos
                .Select(v => new
                {
                    v.IdVenta,
                    v.IdPlan,
                    v.FechaSolicitud,
                    v.NombreCliente,
                    v.NombreEmpresa,
                    v.NumeroContacto,
                    v.Correo,
                    v.Domicilio,
                    v.Ciudad,
                    v.Estado,
                    v.Rfc,
                    v.IdUsuario
                })
                .ToListAsync();

            // Retornar la lista de ventas
            return Ok(ventas);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostVenta([FromBody] VentaProspecto venta)
        {
            if (venta == null)
            {
                return BadRequest("La venta no puede ser vacía.");
            }

            var planExistente = await _baseDatos.PlanEmpresas.FindAsync(venta.IdPlan);
            if (planExistente == null)
            {
                return NotFound("El plan asociado no existe.");
            }

            _baseDatos.VentaProspectos.Add(venta);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(PostVenta), new { id = venta.IdVenta }, venta);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenta(int id, [FromBody] Dictionary<string, int> fields)
        {
            // Verificar si el campo `idUsuario` está presente en los datos
            if (!fields.ContainsKey("idUsuario"))
            {
                return BadRequest("El campo 'idUsuario' es obligatorio.");
            }

            // Obtener la venta existente
            var ventaExistente = await _baseDatos.VentaProspectos.FindAsync(id);
            if (ventaExistente == null)
            {
                return NotFound("La venta no fue encontrada.");
            }

            // Actualizar el campo `IdUsuarioEncargado` en lugar de `IdUsuario`
            if (fields["idUsuario"] is int idUsuario)
            {
                ventaExistente.IdUsuario = idUsuario;
            }

            // Mostrar el valor de `ventaExistente` en la consola antes de guardarlo
            Console.WriteLine($"Venta antes de guardar: IdVenta = {ventaExistente.IdVenta}, IdUsuario = {ventaExistente.IdUsuario}");

            // Guardar los cambios
            await _baseDatos.SaveChangesAsync();

            return NoContent(); // Retornar 204 No Content en caso de éxito
        }

        [HttpGet("pendientes")]
        public async Task<ActionResult<IEnumerable<object>>> GetVentasPendientes()
        {
            // Obtener el ID del usuario actual
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            // Verificar si el usuario existe en la base de datos
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var ventasPendientes = await _baseDatos.VentaProspectos
                .Where(v => v.IdIngresoEgreso == null)
                .Select(v => new
                {
                    v.IdVenta,
                    v.IdPlan,
                    v.FechaSolicitud,
                    v.NombreCliente,
                    v.NombreEmpresa,
                    v.NumeroContacto,
                    v.Correo,
                    v.Domicilio,
                    v.Ciudad,
                    v.Estado,
                    v.Rfc,
                    v.IdUsuario
                })
                .ToListAsync();

            return Ok(ventasPendientes);
        }

    }
}