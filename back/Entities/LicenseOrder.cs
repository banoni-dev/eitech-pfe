namespace EitechPfe.Entities
{
    public enum LicenseOrderStatus
    {
        Active,
        Expired,
        Canceled
    }

    public class LicenseOrder
    {
        public int LicenseOrderId { get; set; }
        public int UserId { get; set; }
        public int LicenseId { get; set; }
        public string PrivateKey { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public LicenseOrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; } = false;
        
        public ICollection<LicenseOrderOption>? Options { get; set; } = new List<LicenseOrderOption>();
    }
}
