using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinanClickApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly FinanclickDbContext _baseDatos;

        public ProductoController(FinanclickDbContext context)
        {
            _baseDatos = context;
        }

        // Obtener todos los productos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            var productos = await _baseDatos.Productos
                .Include(p => p.DetalleProductos)
                .ThenInclude(dp => dp.IdConceptoNavigation)
                .Where(p => p.Estatus != 0 && p.IdEmpresa == user.IdEmpresa)
                .ToListAsync();
            return Ok(productos);
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            var producto = await _baseDatos.Productos
                .Include(p => p.DetalleProductos)
                .ThenInclude(dp => dp.IdConceptoNavigation)
                .FirstOrDefaultAsync(p => p.IdProducto == id && p.Estatus != 0 && p.IdEmpresa == user.IdEmpresa);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // Crear un nuevo producto con detalles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Producto producto)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));

            producto.IdEmpresa = user.IdEmpresa;

            producto.Estatus = 1; // Estatus activo

            _baseDatos.Productos.Add(producto);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = producto.IdProducto }, producto);
        }

        // Actualizar un producto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
        {

            producto.IdProducto = id;

            var existingProducto = await _baseDatos.Productos
                .Include(p => p.DetalleProductos)
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (existingProducto == null || existingProducto.Estatus == 0)
            {
                return NotFound();
            }

            existingProducto.NombreProducto = producto.NombreProducto;
            existingProducto.Reca = producto.Reca;
            existingProducto.MetodoCalculo = producto.MetodoCalculo;
            existingProducto.SubMetodo = producto.SubMetodo;
            existingProducto.Monto = producto.Monto;
            existingProducto.Periodicidad = producto.Periodicidad;
            existingProducto.NumPagos = producto.NumPagos;
            existingProducto.InteresAnual = producto.InteresAnual;
            existingProducto.Iva = producto.Iva;
            existingProducto.InteresMoratorio = producto.InteresMoratorio;
            existingProducto.PagoAnticipado = producto.PagoAnticipado;
            existingProducto.AplicacionDePagos = producto.AplicacionDePagos;
            existingProducto.IdEmpresa = producto.IdEmpresa;

            // Actualizar los detalles del producto
            _baseDatos.DetalleProductos.RemoveRange(existingProducto.DetalleProductos);
            existingProducto.DetalleProductos = producto.DetalleProductos;

            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        // Eliminación lógica de un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _baseDatos.Productos.FindAsync(id);
            if (producto == null || producto.Estatus == 0)
            {
                return NotFound();
            }

            producto.Estatus = 0; // Estatus eliminado lógicamente
            _baseDatos.Entry(producto).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }
    }
}
