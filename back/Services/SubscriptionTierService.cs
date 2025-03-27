public class SubscriptionTierService : ISubscriptionTierService
{
    private readonly ISubscriptionTierRepository _subscriptionTierRepository;

    public SubscriptionTierService(ISubscriptionTierRepository subscriptionTierRepository)
    {
        _subscriptionTierRepository = subscriptionTierRepository;
    }

    public Task<int> CreateSubscriptionTier(SubscriptionTier subscriptionTier) => _subscriptionTierRepository.CreateSubscriptionTier(subscriptionTier);
    public Task<SubscriptionTier?> GetSubscriptionTierById(int id) => _subscriptionTierRepository.GetSubscriptionTierById(id);
    public Task<IEnumerable<SubscriptionTier>> GetAllSubscriptionTiers() => _subscriptionTierRepository.GetAllSubscriptionTiers();
    public Task<int> UpdateSubscriptionTier(SubscriptionTier subscriptionTier) => _subscriptionTierRepository.UpdateSubscriptionTier(subscriptionTier);
    public Task<int> DeleteSubscriptionTier(int id) => _subscriptionTierRepository.DeleteSubscriptionTier(id);
}
