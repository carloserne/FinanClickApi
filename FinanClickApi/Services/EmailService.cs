using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string _smtpServer = "smtp.gmail.com";
    private readonly int _smtpPort = 587;
    private readonly string _smtpUsername = "softberry51@gmail.com"; // Cambia esto a tu cuenta de Gmail
    private readonly string _smtpPassword = "xqlq adrr nftp pmet"; // Usa tu contraseña de aplicación si tienes 2FA habilitado

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var fromAddress = new MailAddress(_smtpUsername, "FinanClick");
        var toAddress = new MailAddress(toEmail);
        var smtpClient = new SmtpClient(_smtpServer)
        {
            Port = _smtpPort,
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
            EnableSsl = true
        };

        using (var mailMessage = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
