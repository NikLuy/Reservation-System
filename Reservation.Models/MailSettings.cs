namespace Reservation.Models;

public class MailSettings
{
    public string? Mail { get; set; } 
    public string? DisplayName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Host { get; set; }
    public int Port { get; set; } = 587;
    public bool EnableSSL { get; set; } = false;
    public string DebugMail { get; set; }
    public bool DebugMode { get; set; } = false;
}
