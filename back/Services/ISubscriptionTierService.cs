public interface ISubscriptionTierService
{
    Task<int> CreateSubscriptionTier(SubscriptionTier subscriptionTier);
    Task<SubscriptionTier?> GetSubscriptionTierById(int id);
    Task<IEnumerable<SubscriptionTier>> GetAllSubscriptionTiers();
    Task<int> UpdateSubscriptionTier(SubscriptionTier subscriptionTier);
    Task<int> DeleteSubscriptionTier(int id);
}
