using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
    {
        if (string.IsNullOrEmpty(emailRequest.To) || string.IsNullOrEmpty(emailRequest.Subject) || string.IsNullOrEmpty(emailRequest.Body))
        {
            return BadRequest("Missing email parameters.");
        }

        try
        {
            await _emailService.SendEmailAsync(emailRequest.To, emailRequest.Subject, emailRequest.Body);
            return Ok("Email sent successfully.");
        }
        catch (System.Exception ex)
        {
            return BadRequest($"Failed to send email. Error: {ex.Message}");
        }
    }
}

public class EmailRequest
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
