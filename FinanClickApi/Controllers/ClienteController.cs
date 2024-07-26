using FinanClickApi.Dtos;
using FinanClickApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanClickApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly FinanclickDbContext _baseDatos;

        public ClienteController(FinanclickDbContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        // GET: api/cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetClientes()
        {
            var clientes = await _baseDatos.Clientes.ToListAsync();

            var resultado = new List<object>();

            foreach (var cliente in clientes)
            {
                if (cliente.RegimenFiscal == "MORAL")
                {
                    var datosMoral = await _baseDatos.DatosClienteMorals
                        .Include(dcm => dcm.IdPersonaMoralNavigation)
                        .FirstOrDefaultAsync(dcm => dcm.IdCliente == cliente.IdCliente);

                    if (datosMoral != null)
                    {
                        resultado.Add(new
                        {
                            Cliente = cliente
                        });
                    }
                }
                else if (cliente.RegimenFiscal == "FISICA")
                {
                    var datosFisica = await _baseDatos.DatosClienteFisicas
                        .Include(dcf => dcf.IdPersonaNavigation)
                        .FirstOrDefaultAsync(dcf => dcf.IdCliente == cliente.IdCliente);

                    if (datosFisica != null)
                    {
                        resultado.Add(new
                        {
                            Cliente = cliente
                        });
                    }
                }
                else
                {
                    resultado.Add(new
                    {
                        Cliente = cliente
                    });
                }
            }

            return Ok(resultado);
        }


        // GET: api/cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetCliente(int id)
        {
            var cliente = await _baseDatos.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            if (cliente.RegimenFiscal == "MORAL")
            {
                var datosMoral = await _baseDatos.DatosClienteMorals
                    .Include(dcm => dcm.IdPersonaMoralNavigation)
                    .FirstOrDefaultAsync(dcm => dcm.IdCliente == id);

                if (datosMoral != null)
                {
                    return Ok(new
                    {
                        Cliente = cliente
                    });
                }
            }
            else if (cliente.RegimenFiscal == "FISICA")
            {
                var datosFisica = await _baseDatos.DatosClienteFisicas
                    .Include(dcf => dcf.IdClienteNavigation)
                    .FirstOrDefaultAsync(dcf => dcf.IdCliente == id);

                if (datosFisica != null)
                {
                    return Ok(new
                    {
                        Cliente = cliente
                    });
                }
            }

            return Ok(new
            {
                Cliente = cliente,
                Datos = (object)null
            });
        }


        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("El cliente no puede ser nulo.");
            }

            if (cliente.RegimenFiscal == "FISICA")
            {
                // Verificar y añadir cliente físico
                if (cliente.DatosClienteFisicas.Any())
                {
                    var persona = cliente.DatosClienteFisicas.First().IdPersonaNavigation;
                    _baseDatos.Personas.Add(persona);
                }
            }
            else if (cliente.RegimenFiscal == "MORAL")
            {
                // Verificar y añadir cliente moral
                if (cliente.DatosClienteMorals.Any())
                {
                    var personaMoral = cliente.DatosClienteMorals.First().IdPersonaMoralNavigation;
                    _baseDatos.PersonaMorals.Add(personaMoral);
                }
            }

            _baseDatos.Clientes.Add(cliente);
            await _baseDatos.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCliente), new { id = cliente.IdCliente }, cliente);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("El cliente no puede ser nulo.");
            }

            var clienteExistente = await _baseDatos.Clientes
                .Include(c => c.DatosClienteFisicas)
                .Include(c => c.DatosClienteMorals)
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (clienteExistente == null)
            {
                return NotFound($"Cliente con ID {id} no encontrado.");
            }

            // Actualiza los datos del cliente
            clienteExistente.RegimenFiscal = cliente.RegimenFiscal;
            clienteExistente.IdEmpresa = cliente.IdEmpresa;
            clienteExistente.Estatus = cliente.Estatus;

            // Actualiza los datos del cliente físico
            if (cliente.RegimenFiscal == "FISICA")
            {
                foreach (var clienteFisica in cliente.DatosClienteFisicas)
                {
                    var clienteFisicaExistente = clienteExistente.DatosClienteFisicas
                        .FirstOrDefault(cf => cf.IdClienteFisica == clienteFisica.IdClienteFisica);

                    if (clienteFisicaExistente != null)
                    {
                        clienteFisicaExistente.IdPersona = clienteFisica.IdPersona;
                        clienteFisicaExistente.IdPersonaNavigation = clienteFisica.IdPersonaNavigation;
                        _baseDatos.Entry(clienteFisicaExistente).CurrentValues.SetValues(clienteFisica);
                    }
                    else
                    {
                        clienteExistente.DatosClienteFisicas.Add(clienteFisica);
                        _baseDatos.Personas.Add(clienteFisica.IdPersonaNavigation);
                    }
                }

                // Elimina los datos de clientes físicos que no están en la solicitud
                var idsFisicas = cliente.DatosClienteFisicas.Select(cf => cf.IdClienteFisica).ToHashSet();
                clienteExistente.DatosClienteFisicas
                    .Where(cf => !idsFisicas.Contains(cf.IdClienteFisica))
                    .ToList()
                    .ForEach(cf => clienteExistente.DatosClienteFisicas.Remove(cf));
            }

            // Actualiza los datos del cliente moral
            else if (cliente.RegimenFiscal == "MORAL")
            {
                foreach (var clienteMoral in cliente.DatosClienteMorals)
                {
                    var clienteMoralExistente = clienteExistente.DatosClienteMorals
                        .FirstOrDefault(cm => cm.IdClienteMoral == clienteMoral.IdClienteMoral);

                    if (clienteMoralExistente != null)
                    {
                        clienteMoralExistente.IdPersonaMoral = clienteMoral.IdPersonaMoral;
                        clienteMoralExistente.NombreRepLegal = clienteMoral.NombreRepLegal;
                        clienteMoralExistente.RfcrepLegal = clienteMoral.RfcrepLegal;
                        clienteMoralExistente.IdPersonaMoralNavigation = clienteMoral.IdPersonaMoralNavigation;
                        _baseDatos.Entry(clienteMoralExistente).CurrentValues.SetValues(clienteMoral);
                    }
                    else
                    {
                        clienteExistente.DatosClienteMorals.Add(clienteMoral);
                        _baseDatos.PersonaMorals.Add(clienteMoral.IdPersonaMoralNavigation);
                    }
                }

                // Elimina los datos de clientes morales que no están en la solicitud
                var idsMorales = cliente.DatosClienteMorals.Select(cm => cm.IdClienteMoral).ToHashSet();
                clienteExistente.DatosClienteMorals
                    .Where(cm => !idsMorales.Contains(cm.IdClienteMoral))
                    .ToList()
                    .ForEach(cm => clienteExistente.DatosClienteMorals.Remove(cm));
            }

            // Guarda los cambios
            await _baseDatos.SaveChangesAsync();

            return NoContent();  // O bien, return Ok(clienteExistente);
        }


        // DELETE: api/cliente/5 (eliminación lógica)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _baseDatos.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Estatus = 0;
            _baseDatos.Entry(cliente).State = EntityState.Modified;
            await _baseDatos.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _baseDatos.Clientes.Any(e => e.IdCliente == id);
        }

    }
}
