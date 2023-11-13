using Microsoft.AspNetCore.Http;

namespace Reservation.Services;

public class MailRequest
{
    public string ToEmail { get; set; }= String.Empty;
    public string Subject { get; set; } = "Subject";
    public string Body { get; set; } = "Email text";
    public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
}
