// status functions weirdly here, needs to be updated later
public enum SubscriptionStatus
{
    Active,
    Expired,
    Canceled
}

public class SubscriptionOrder
{
    public int Id { get; set; }
    public int SubscriptionTierId { get; set; }
    public int UserId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubscriptionStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdateAt { get; set; } = DateTime.UtcNow;
    public bool IsArchived { get; set; } = false;
}