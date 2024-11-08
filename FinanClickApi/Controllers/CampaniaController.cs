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

        public CampaniaController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
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
        public async Task<IActionResult> SendCampaignEmails(int id, [FromBody] SendEmailRequest request, [FromServices] IOptions<MailgunSettings> mailgunSettings)
        {
            // Buscar la campaña en la base de datos
            var campaign = await _baseDatos.Campanias.FindAsync(id);
            if (campaign == null)
            {
                return NotFound("Campaña no encontrada");
            }

            // Configuración de Mailgun
            var mailgun = mailgunSettings.Value;
            var failedEmails = new List<string>(); // Lista para almacenar correos fallidos

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{mailgun.ApiKey}")));

            foreach (var email in request.Emails)
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("from", $"{mailgun.SenderName} <{mailgun.SenderEmail}>"),
            new KeyValuePair<string, string>("to", email),
            new KeyValuePair<string, string>("subject", campaign.Asunto),
            new KeyValuePair<string, string>("html", campaign.Contenido)
        });

                try
                {
                    var response = await client.PostAsync($"https://api.mailgun.net/v3/{mailgun.Domain}/messages", formContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        failedEmails.Add(email); // Agrega el correo a la lista de fallidos si hubo un error en la respuesta
                    }
                }
                catch
                {
                    failedEmails.Add(email); // Agrega el correo a la lista de fallidos si ocurre una excepción
                }
            }

            // Verifica si hubo correos fallidos y responde en consecuencia
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
