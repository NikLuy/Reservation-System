namespace Reservation.Models
{
    public class GraphSettings
    {
        public const string SettingsName = "GraphSettings";
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? TenantId { get; set; }
        public string? SenderEmail { get; set; }
    }
}
