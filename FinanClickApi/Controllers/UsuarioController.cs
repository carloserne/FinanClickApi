using Azure.Core;
using FinanClickApi.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanClickApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {

        private readonly FinanclickDbContext _baseDatos;
        public UsuarioController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;

        }

        // Metodo GET ListaTareas
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Usuario) || string.IsNullOrEmpty(request.Contrasenia))
            {
                return BadRequest("Invalid login request.");
            }
            var usuario = await _baseDatos.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario1 == request.Usuario && u.Contrasenia == request.Contrasenia);

            if (usuario == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Aquí podrías generar un token JWT u otra lógica de autenticación
            return Ok(new { Message = "Login successful.", Usuario = usuario });
        }



    }
}


public class LoginRequest
{
    public string Usuario { get; set; } = null!;
    public string Contrasenia { get; set; } = null!;
}