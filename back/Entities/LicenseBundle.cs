public class LicenseBundle
{
    public int LicenseOrderId { get; set; }
    public int OptionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
}
