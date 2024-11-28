using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using FinanClickApi.Models;
using System.IO;
using FinanClickApi.Migrations;

[ApiController]
[Route("api/[controller]")]
public class DocumentosEmpresasController : ControllerBase
{
    private readonly FinanclickDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public DocumentosEmpresasController(FinanclickDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("subir")]
    public async Task<IActionResult> SubirDocumento([FromForm] int idEmpresa, [FromForm] int idDocumento, [FromForm] IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
            return BadRequest("Archivo no proporcionado o vacío.");

        var empresa = await _context.Empresas.FindAsync(idEmpresa);
        if (empresa == null)
            return NotFound("Empresa no encontrada.");

        var documento = await _context.Documentos.FindAsync(idDocumento);
        if (documento == null)
            return NotFound("Tipo de documento no encontrado.");

        var documentoEmpresa = await _context.DocumentosEmpresas
            .FirstOrDefaultAsync(de => de.IdEmpresa == idEmpresa && de.IdDocumento == idDocumento);

        if (documentoEmpresa == null)
        {
            documentoEmpresa = new DocumentosEmpresa
            {
                IdEmpresa = idEmpresa,
                IdDocumento = idDocumento,
                EstadoDocumento = "Subido"
            };
            _context.DocumentosEmpresas.Add(documentoEmpresa);
        }
        else
        {
            documentoEmpresa.EstadoDocumento = "Subido";
        }

        string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        string uniqueFileName = Guid.NewGuid().ToString() + "_" + archivo.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await archivo.CopyToAsync(fileStream);
        }

        documentoEmpresa.RutaArchivo = filePath;
        documentoEmpresa.FechaSubida = DateTime.Now;

        await _context.SaveChangesAsync();

        return Ok("Documento subido exitosamente.");
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPut("modificar/{idDocumentoEmpresa}")]
    public async Task<IActionResult> ModificarDocumento(int idDocumentoEmpresa, [FromForm] IFormFile archivo)
    {
        var documentoEmpresa = await _context.DocumentosEmpresas.FindAsync(idDocumentoEmpresa);
        if (documentoEmpresa == null)
            return NotFound("Documento de empresa no encontrado.");

        if (archivo == null || archivo.Length == 0)
            return BadRequest("Archivo no proporcionado o vacío.");

        if (System.IO.File.Exists(documentoEmpresa.RutaArchivo))
            System.IO.File.Delete(documentoEmpresa.RutaArchivo);

        string uniqueFileName = Guid.NewGuid().ToString() + "_" + archivo.FileName;
        string filePath = Path.Combine(_environment.WebRootPath, "uploads", uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await archivo.CopyToAsync(fileStream);
        }

        documentoEmpresa.RutaArchivo = filePath;
        documentoEmpresa.FechaSubida = DateTime.Now;

        await _context.SaveChangesAsync();

        return Ok("Documento modificado exitosamente.");
    }

    [HttpGet("lista/{idEmpresa}")]
    public async Task<IActionResult> ObtenerDocumentos(int idEmpresa)
    {
        var documentos = await _context.DocumentosEmpresas
            .Where(de => de.IdEmpresa == idEmpresa)
            .Select(de => new
            {
                de.IdDocumentoEmpresa,
                de.IdDocumento,
                NombreDocumento = de.IdDocumentoNavigation.NombreDocumento,
                de.EstadoDocumento,
                de.FechaSubida
            })
            .ToListAsync();

        return Ok(documentos);
    }

    [HttpGet("descargar/{idDocumentoEmpresa}")]
    public async Task<IActionResult> DescargarDocumento(int idDocumentoEmpresa)
    {
        var documentoEmpresa = await _context.DocumentosEmpresas.FindAsync(idDocumentoEmpresa);
        if (documentoEmpresa == null)
            return NotFound("Documento de empresa no encontrado.");

        if (!System.IO.File.Exists(documentoEmpresa.RutaArchivo))
            return NotFound("El archivo no existe en el servidor.");

        byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(documentoEmpresa.RutaArchivo);
        string base64String = Convert.ToBase64String(fileBytes);

        return Ok(new { base64 = base64String, fileName = Path.GetFileName(documentoEmpresa.RutaArchivo) });
    }

    [HttpDelete("eliminar/{idDocumentoEmpresa}")]
    public async Task<IActionResult> EliminarDocumento(int idDocumentoEmpresa)
    {
        var documentoEmpresa = await _context.DocumentosEmpresas.FindAsync(idDocumentoEmpresa);
        if (documentoEmpresa == null)
            return NotFound("Documento de empresa no encontrado.");

        if (System.IO.File.Exists(documentoEmpresa.RutaArchivo))
            System.IO.File.Delete(documentoEmpresa.RutaArchivo);

        _context.DocumentosEmpresas.Remove(documentoEmpresa);
        await _context.SaveChangesAsync();

        return Ok("Documento eliminado exitosamente.");
    }
}