namespace EitechPfe.Entities
{
    public class LicenseActivation
    {
        public int ActivationId { get; set; }
        public int LicenseOrderId { get; set; }
        public string DeviceFingerprint { get; set; } = string.Empty;
        public DateTime ActivationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; } = false;
    }
}