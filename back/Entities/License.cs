public class License
{
    public int LicenseId { get; set; }
    public int ProductId { get; set; }
    public int MaxDevices { get; set; }
    public int Duration { get; set; }
    public int GracePeriod { get; set; }
    public string PublicKey { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
    public bool IsArchived { get; set; } = false;
}
