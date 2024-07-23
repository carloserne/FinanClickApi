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

    }
}


