public class LicenseOption
{
    public int OptionId { get; set; }
    public int LicenseId { get; set; }
    public string OptionName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
    public bool IsArchived { get; set; } = false;
}
