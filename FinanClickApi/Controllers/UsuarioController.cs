using Azure.Core;
using FinanClickApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanClickApi.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace FinanClickApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly FinanclickDbContext _baseDatos;
        private readonly IConfiguration _configuration;


        public UsuarioController(FinanclickDbContext baseDatos, IConfiguration configuration)
        {
            _baseDatos = baseDatos;
            _configuration = configuration;


        }

        // Metodo GET ListaTareas
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginReq request)
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
             var token = GenerateJwtToken(usuario);

            return Ok(new { token });
            // Aquí podrías generar un token JWT u otra lógica de autenticación
        }


        //api/account/detail
        [Authorize]
        [HttpGet("detail")]
        public async Task<ActionResult<Usuario>> GetUserDetail()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var user = await _baseDatos.Usuarios.FindAsync(int.Parse(currentUserId));
            
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        private string GenerateJwtToken(Usuario user)
        {
            var securityKey = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt").GetSection("Key").Value!);
            var tokenHandler = new JwtSecurityTokenHandler();

            List<Claim> claims = [
                   new (JwtRegisteredClaimNames.Name, user.Usuario1??""),
                   new (JwtRegisteredClaimNames.NameId, Convert.ToString(user.IdUsuario)??""),
                   new (JwtRegisteredClaimNames.Aud, _configuration.GetSection("Jwt").GetSection("Issuer").Value!),
                   new (JwtRegisteredClaimNames.Iss, _configuration.GetSection("Jwt").GetSection("Audience").Value!)
            ];

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(securityKey),
                    SecurityAlgorithms.HmacSha256
                )
            };
            var token = tokenHandler.CreateToken(tokenDesc);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //Aqui empieza el método para registrar usuarios
        //GET: api/usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuarios()
        {
            var usuarios = await _baseDatos.Usuarios
                .Include(u => u.IdRolNavigation)
                .ToListAsync();

            if (!usuarios.Any())
            {
                return NotFound();
            }

            
            var resultado = usuarios.Select(u => new
            {
                u.IdUsuario,
                u.IdRol,
                Rol = u.IdRolNavigation?.NombreRol,
                u.ApellidoPaterno,
                u.ApellidoMaterno,
                u.IdEmpresa,
                u.Usuario1,
                u.Nombre,
                u.Imagen
            });

            return Ok(resultado);
        }



        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            // Verificar si el rol existe
            var rol = await _baseDatos.Rols.FindAsync(usuarioDto.IdRol);
            if (rol == null)
            {
                return NotFound(new { message = "Rol no encontrado" });
            }

            // Crear el nuevo usuario
            var usuario = new Usuario
            {
                IdRol = usuarioDto.IdRol,
                Contrasenia = usuarioDto.Contrasenia, // Implementar el método de hashing
                ApellidoPaterno = usuarioDto.ApellidoPaterno,
                ApellidoMaterno = usuarioDto.ApellidoMaterno,
                IdEmpresa = usuarioDto.IdEmpresa,
                Usuario1 = usuarioDto.Usuario1,
                Nombre = usuarioDto.Nombre,
                Imagen = usuarioDto.Imagen
            };

            try
            {
                _baseDatos.Usuarios.Add(usuario);
                await _baseDatos.SaveChangesAsync();

                // Devolver una respuesta exitosa con el usuario creado
                return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.IdUsuario }, usuario);
            }
            catch (Exception ex)
            {
                // Manejar excepciones y devolver una respuesta adecuada
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            // Buscar el usuario existente en la base de datos
            var usuarioExistente = await _baseDatos.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            // Verificar si el rol proporcionado existe
            var rol = await _baseDatos.Rols.FindAsync(usuarioDto.IdRol);
            if (rol == null)
            {
                return NotFound(new { message = "Rol no encontrado" });
            }

            // Actualizar los campos del usuario existente con los valores del DTO
            usuarioExistente.IdRol = usuarioDto.IdRol;
            usuarioExistente.Contrasenia = usuarioDto.Contrasenia;
            usuarioExistente.ApellidoPaterno = usuarioDto.ApellidoPaterno;
            usuarioExistente.ApellidoMaterno = usuarioDto.ApellidoMaterno;
            usuarioExistente.IdEmpresa = usuarioDto.IdEmpresa;
            usuarioExistente.Usuario1 = usuarioDto.Usuario1;
            usuarioExistente.Nombre = usuarioDto.Nombre;
            usuarioExistente.Imagen = usuarioDto.Imagen;

            try
            {
                // Guardar los cambios en la base de datos
                _baseDatos.Usuarios.Update(usuarioExistente);
                await _baseDatos.SaveChangesAsync();

                // Devolver una respuesta exitosa con el usuario actualizado
                return Ok(usuarioExistente);
            }
            catch (Exception ex)
            {
                // Manejar excepciones y devolver una respuesta adecuada
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            // Buscar el usuario existente en la base de datos
            var usuarioExistente = await _baseDatos.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            try
            {
                // Eliminar el usuario
                _baseDatos.Usuarios.Remove(usuarioExistente);
                await _baseDatos.SaveChangesAsync();

                // Devolver una respuesta exitosa
                return Ok(new { message = "Usuario eliminado correctamente" });
            }
            catch (Exception ex)
            {
                // Manejar excepciones y devolver una respuesta adecuada
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }




    }
}


