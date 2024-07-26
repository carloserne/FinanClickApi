using FinanClickApi.Dtos;
using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanClickApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioClienteController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;
        private readonly IConfiguration _configuration;


        public UsuarioClienteController(FinanclickDbContext baseDatos, IConfiguration configuration)
        {
            _baseDatos = baseDatos;
            _configuration = configuration;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioCliente>>> GetUsuarioClientes()
        {
            return await _baseDatos.UsuarioClientes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioCliente>> GetUsuarioCliente(int id)
        {
            var usuarioCliente = await _baseDatos.UsuarioClientes.FindAsync(id);

            if (usuarioCliente == null)
            {
                return NotFound();
            }

            return usuarioCliente;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioCliente>> PostUsuarioCliente(UsuarioCliente usuarioCliente)
        {
            _baseDatos.UsuarioClientes.Add(usuarioCliente);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioCliente), new { id = usuarioCliente.IdUsuarioCliente }, usuarioCliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioCliente(int id, UsuarioCliente usuarioCliente)
        {

            usuarioCliente.IdUsuarioCliente = id;

            _baseDatos.Entry(usuarioCliente).State = EntityState.Modified;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioClienteExists(id))
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
        public async Task<IActionResult> DeleteUsuarioCliente(int id)
        {
            var usuarioCliente = await _baseDatos.UsuarioClientes.FindAsync(id);
            if (usuarioCliente == null)
            {
                return NotFound();
            }

            usuarioCliente.Estatus = 0;
            _baseDatos.Entry(usuarioCliente).State = EntityState.Modified;

            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginReq request)
        {
            if (request == null || string.IsNullOrEmpty(request.Usuario) || string.IsNullOrEmpty(request.Contrasenia))
            {
                return BadRequest("Invalid login request.");
            }

            var usuarioCliente = await _baseDatos.UsuarioClientes
                .FirstOrDefaultAsync(u => u.Usuario == request.Usuario && u.Contrasenia == request.Contrasenia);

            if (usuarioCliente == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            var token = GenerateJwtToken(usuarioCliente);
            return Ok(new { token });
        }

        private string GenerateJwtToken(UsuarioCliente usuarioCliente)
        {
            var securityKey = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt").GetSection("Key").Value!);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, usuarioCliente.Usuario),
                new Claim(JwtRegisteredClaimNames.NameId, usuarioCliente.IdUsuarioCliente.ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration.GetSection("Jwt").GetSection("Audience").Value!),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.GetSection("Jwt").GetSection("Issuer").Value!),
                new Claim(ClaimTypes.Role, "Cliente") // Añade el rol de Cliente
            };

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(securityKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }

        private bool UsuarioClienteExists(int id)
        {
            return _baseDatos.UsuarioClientes.Any(e => e.IdUsuarioCliente == id);
        }
    }

}
