public class SubscriptionTier
{
    public int TierId { get; set; }
    public int ProductId { get; set; }
    public string TierName { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int GracePeriod { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
    public bool IsArchived { get; set; } = false;
}
