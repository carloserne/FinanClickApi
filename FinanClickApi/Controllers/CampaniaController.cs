using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Security.Claims;
using FinanClickApi.Models;
using FinanClickApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanClickApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CampaniaController : ControllerBase
    {

        private readonly FinanclickDbContext _baseDatos;
        private readonly EmailService _emailService;


        public CampaniaController(FinanclickDbContext baseDatos, EmailService emailService)
        {
            _baseDatos = baseDatos;
            _emailService = emailService;
        }

        // GET: api/Campania
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campania>>> GetCampanias()
        {
            return await _baseDatos.Campanias.ToListAsync();
        }

        // GET: api/Campania/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Campania>> GetCampania(int id)
        {
            var campania = await _baseDatos.Campanias.FindAsync(id);

            if (campania == null)
            {
                return NotFound();
            }

            return campania;
        }

        // POST: api/Campania
        [HttpPost]
        public async Task<ActionResult<Campania>> CreateCampania(Campania campania)
        {
            campania.CreatedDate = DateTime.Now;
            _baseDatos.Campanias.Add(campania);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCampania), new { id = campania.IdCampania }, campania);
        }

        // PUT: api/Campania/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampania(int id, Campania campania)
        {
            if (id != campania.IdCampania)
            {
                return BadRequest();
            }

            _baseDatos.Entry(campania).State = EntityState.Modified;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaniaExists(id))
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

        // DELETE: api/Campania/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampania(int id)
        {
            var campania = await _baseDatos.Campanias.FindAsync(id);
            if (campania == null)
            {
                return NotFound();
            }

            _baseDatos.Campanias.Remove(campania);
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si existe una campaña por su ID
        private bool CampaniaExists(int id)
        {
            return _baseDatos.Campanias.Any(e => e.IdCampania == id);
        }

        [HttpPost("{id}/sendEmails")]
        public async Task<IActionResult> SendCampaignEmails(int id, [FromBody] SendEmailRequest request)
        {
            // Buscar la campaña en la base de datos
            var campaign = await _baseDatos.Campanias.FindAsync(id);
            if (campaign == null)
            {
                return NotFound("Campaña no encontrada");
            }

            // Configuración de Mailgun
            var failedEmails = new List<string>(); // Lista para almacenar correos fallidos
            var emails = new List<String>(); // Lista para almacenar correos a enviar>


            foreach (var empresa in request.idsEmpresas)
            {
                var Usuarios = await _baseDatos.ContactoPersonas.Where(cp => cp.IdEmpresa == empresa).ToListAsync();
                foreach (var usuario in Usuarios)
                {
                    if(usuario.Email != null)
                    {
                        emails.Add(usuario.Email);

                    }
                }
            }

            foreach (var email in emails)
            {
                try
                {
                    await _emailService.SendEmailAsync(email, campaign.Asunto, campaign.Contenido);

                }
                catch
                {
                    failedEmails.Add(email);
                }
            }


            if (failedEmails.Count > 0)
            {
                campaign.Estatus = 4;
                _baseDatos.Entry(campaign).State = EntityState.Modified;
                await _baseDatos.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Correos enviados, pero algunos no pudieron ser entregados.",
                    FailedEmails = failedEmails
                });
            }

            // Cambiar el estado de la campaña a 2 y guardar en la base de datos
            campaign.Estatus = 2;
            _baseDatos.Entry(campaign).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return Ok("Correos electrónicos enviados exitosamente y estado de la campaña actualizado.");
        }
    }
}
